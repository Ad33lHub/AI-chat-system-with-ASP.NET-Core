IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
CREATE TABLE [Benefits] (
    [Id] int NOT NULL IDENTITY,
    [Text] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Benefits] PRIMARY KEY ([Id])
);

CREATE TABLE [Services] (
    [Id] int NOT NULL IDENTITY,
    [Title] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [IconName] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Services] PRIMARY KEY ([Id])
);

CREATE TABLE [Stats] (
    [Id] int NOT NULL IDENTITY,
    [Number] nvarchar(max) NOT NULL,
    [Label] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Stats] PRIMARY KEY ([Id])
);

CREATE TABLE [TechStacks] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [LogoUrl] nvarchar(max) NOT NULL,
    [Category] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_TechStacks] PRIMARY KEY ([Id])
);

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Text') AND [object_id] = OBJECT_ID(N'[Benefits]'))
    SET IDENTITY_INSERT [Benefits] ON;
INSERT INTO [Benefits] ([Id], [Text])
VALUES (1, N'Agile Development Methodology'),
(2, N'24/7 Technical Support'),
(3, N'Transparent Communication'),
(4, N'On-time Delivery Guarantee'),
(5, N'Quality Assurance'),
(6, N'Scalable Solutions');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Text') AND [object_id] = OBJECT_ID(N'[Benefits]'))
    SET IDENTITY_INSERT [Benefits] OFF;

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Description', N'IconName', N'Title') AND [object_id] = OBJECT_ID(N'[Services]'))
    SET IDENTITY_INSERT [Services] ON;
INSERT INTO [Services] ([Id], [Description], [IconName], [Title])
VALUES (1, N'Custom web applications built with cutting-edge technologies for optimal performance and scalability.', N'Code2', N'Web Development'),
(2, N'Native and cross-platform mobile solutions that deliver exceptional user experiences.', N'Smartphone', N'Mobile Apps'),
(3, N'Scalable cloud infrastructure and migration services for modern businesses.', N'Cloud', N'Cloud Solutions'),
(4, N'Intelligent automation and machine learning solutions to drive innovation.', N'Brain', N'AI/ML Integration');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Description', N'IconName', N'Title') AND [object_id] = OBJECT_ID(N'[Services]'))
    SET IDENTITY_INSERT [Services] OFF;

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Label', N'Number') AND [object_id] = OBJECT_ID(N'[Stats]'))
    SET IDENTITY_INSERT [Stats] ON;
INSERT INTO [Stats] ([Id], [Label], [Number])
VALUES (1, N'Projects Delivered', N'100+'),
(2, N'Happy Clients', N'80+'),
(3, N'Team Members', N'10+'),
(4, N'Years Experience', N'2+');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Label', N'Number') AND [object_id] = OBJECT_ID(N'[Stats]'))
    SET IDENTITY_INSERT [Stats] OFF;

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Category', N'LogoUrl', N'Name') AND [object_id] = OBJECT_ID(N'[TechStacks]'))
    SET IDENTITY_INSERT [TechStacks] ON;
INSERT INTO [TechStacks] ([Id], [Category], [LogoUrl], [Name])
VALUES (1, N'Frontend', N'https://cdn.jsdelivr.net/gh/devicons/devicon/icons/react/react-original.svg', N'React'),
(2, N'Frontend', N'https://cdn.jsdelivr.net/gh/devicons/devicon/icons/nextjs/nextjs-original.svg', N'Next.js'),
(3, N'Frontend', N'https://cdn.jsdelivr.net/gh/devicons/devicon/icons/typescript/typescript-original.svg', N'TypeScript'),
(4, N'Backend', N'https://cdn.jsdelivr.net/gh/devicons/devicon/icons/nodejs/nodejs-original.svg', N'Node.js'),
(5, N'Backend', N'https://cdn.jsdelivr.net/gh/devicons/devicon/icons/dotnetcore/dotnetcore-original.svg', N'ASP.NET Core'),
(6, N'Database', N'https://cdn.jsdelivr.net/gh/devicons/devicon/icons/microsoftsqlserver/microsoftsqlserver-plain.svg', N'SQL Server');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Category', N'LogoUrl', N'Name') AND [object_id] = OBJECT_ID(N'[TechStacks]'))
    SET IDENTITY_INSERT [TechStacks] OFF;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251214103511_InitialCreate', N'10.0.1');

COMMIT;
GO

