import os
import pickle
import numpy as np
import tensorflow as tf
import pandas as pd
from flask import Flask, request, jsonify
from tensorflow.keras.models import load_model
from tensorflow.keras.preprocessing.sequence import pad_sequences
from sklearn.preprocessing import LabelEncoder
from sklearn.feature_extraction.text import TfidfVectorizer
from sklearn.metrics.pairwise import cosine_similarity

app = Flask(__name__)

# Load artifacts
MODEL_DIR = os.path.dirname(os.path.abspath(__file__))
MODEL_PATH = os.path.join(MODEL_DIR, "intent_model.keras")
TOKENIZER_PATH = os.path.join(MODEL_DIR, "tokenizer.pkl")
ENCODER_PATH = os.path.join(MODEL_DIR, "intent_encoder.pkl")
CSV_PATH = os.path.join(MODEL_DIR, "verixsoft_chat_dataset.csv")

# Global variables for Fallback
vectorizer = None
tfidf_matrix = None
df_train = None

print(f"Loading model from {MODEL_PATH}...")
model = load_model(MODEL_PATH)

print(f"Loading tokenizer from {TOKENIZER_PATH}...")
with open(TOKENIZER_PATH, 'rb') as handle:
    try:
        tokenizer = pickle.load(handle)
    except Exception as e:
        print(f"Error loading tokenizer: {e}")
        # Retrain tokenizer if needed? For now just fail or handle.
        raise e

print(f"Loading encoder from {ENCODER_PATH}...")
label_encoder = None

# ---------------------------------------------------------
# Robust Loading & TF-IDF Setup
# ---------------------------------------------------------
if os.path.exists(CSV_PATH):
    print("Loading CSV for TF-IDF Fallback & Encoder Rebuild...")
    try:
        df = pd.read_csv(CSV_PATH)
        # 1. Clean Data
        df = df.dropna(subset=["User_Query", "Intent", "Ideal_Response"])
        df["User_Query"] = df["User_Query"].str.lower().str.strip()
        df["Intent"] = df["Intent"].str.strip()
        
        # Keep for TF-IDF looking up
        df_train = df.copy()
        
        # 2. Setup TF-IDF
        vectorizer = TfidfVectorizer(min_df=1, stop_words='english')
        tfidf_matrix = vectorizer.fit_transform(df_train["User_Query"])
        print(f"TF-IDF Matrix created: {tfidf_matrix.shape}")

        # 3. Encoder Rebuild (Always good to have a fresh one aligned with CSV)
        # Match training notebook logic for filtering?
        # Actually, for the fallback to work best, we should support ALL intents in the CSV.
        # But if we want to respect the Keras model's limitation, we might have a mismatch.
        # Decision: Use the CSV's full intent list for the fallback. 
        # The Keras model might predict an index. 
        # We need to ensure Keras predictions map to something valid.
        
        counts = df["Intent"].value_counts()
        valid_intents = counts[counts >= 2].index.sort_values()
        
        # Try to load pickle first to match Keras model exactly
        try:
            with open(ENCODER_PATH, 'rb') as handle:
                label_encoder = pickle.load(handle)
                print("Encoder loaded from pickle.")
        except:
            print("Pickle failed/missing. Rebuilding encoder from valid intents...")
            label_encoder = LabelEncoder()
            label_encoder.fit(valid_intents)

    except Exception as ex:
        print(f"Error initializing fallback: {ex}")
else:
    print("Warning: CSV not found. TF-IDF fallback disabled.")
    # Try just pickle
    try:
        with open(ENCODER_PATH, 'rb') as handle:
            label_encoder = pickle.load(handle)
    except:
        print("Critical: No CSV and No Encoder Pickle.")

# Parameters (Must match training)
MAX_LEN = 20 

@app.route('/predict', methods=['POST'])
def predict():
    data = request.json
    text = data.get('text', '')
    
    if not text:
        return jsonify({'error': 'No text provided'}), 400

    processed_text = text.lower().strip()
    
    # ---------------------------------------------------------
    # 1. Try TF-IDF / Similarity Matching (The "Senior" Fallback)
    # ---------------------------------------------------------
    fallback_intent = None
    fallback_conf = 0.0
    
    if vectorizer is not None and tfidf_matrix is not None:
        try:
            query_vec = vectorizer.transform([processed_text])
            similarities = cosine_similarity(query_vec, tfidf_matrix).flatten()
            best_idx = np.argmax(similarities)
            best_score = float(similarities[best_idx])
            
            if best_score > 0.3: # Threshold for similarity
                fallback_intent = df_train.iloc[best_idx]["Intent"]
                fallback_conf = best_score
                print(f"[TF-IDF] Match: '{df_train.iloc[best_idx]['User_Query']}' -> {fallback_intent} ({best_score:.4f})")
        except Exception as e:
            print(f"[TF-IDF] Error: {e}")

    # ---------------------------------------------------------
    # 2. Try Keras Model
    # ---------------------------------------------------------
    keras_intent = "Unknown"
    keras_conf = 0.0
    
    try:
        seq = tokenizer.texts_to_sequences([processed_text])
        padded = pad_sequences(seq, maxlen=MAX_LEN, padding='post')
        prediction = model.predict(padded)
        intent_index = np.argmax(prediction)
        keras_conf = float(np.max(prediction))
        
        if label_encoder and intent_index < len(label_encoder.classes_):
            keras_intent = label_encoder.inverse_transform([intent_index])[0]
        
        print(f"[Keras] Prediction: {keras_intent} ({keras_conf:.4f})")
    except Exception as e:
        print(f"[Keras] Error: {e}")

    # ---------------------------------------------------------
    # 3. Decision Logic
    # ---------------------------------------------------------
    # If TF-IDF is high confidence (exact or near exact match), trust it over the likely-weak DL model.
    # If TF-IDF is weak, check Keras.
    
    final_intent = keras_intent
    final_conf = keras_conf
    
    if fallback_conf > 0.6: # Strong text match
        final_intent = fallback_intent
        final_conf = fallback_conf
        print("-> Selecting TF-IDF result (Strong Match)")
    elif fallback_conf > keras_conf: # TF-IDF is better than Keras
        final_intent = fallback_intent
        final_conf = fallback_conf
        print("-> Selecting TF-IDF result (Better than Keras)")
    
    return jsonify({
        'intent': final_intent,
        'confidence': final_conf
    })

if __name__ == '__main__':
    print("Starting Chat Server on port 5000...")
    app.run(port=5000)
