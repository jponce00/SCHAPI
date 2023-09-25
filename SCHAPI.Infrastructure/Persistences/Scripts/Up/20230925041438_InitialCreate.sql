IF OBJECT_ID(N'[dbo].[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [dbo].[__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Classrooms] (
    [Id] int NOT NULL IDENTITY,
    [ClassroomCode] AS CONCAT('AU', CAST(Id AS VARCHAR)),
    [Name] nvarchar(100) NOT NULL,
    [AuditCreateUser] int NOT NULL,
    [AuditCreateDate] datetime2 NOT NULL,
    [AuditUpdateUser] int NULL,
    [AuditUpdateDate] datetime2 NULL,
    [AuditDeleteUser] int NULL,
    [AuditDeleteDate] datetime2 NULL,
    [State] int NOT NULL,
    CONSTRAINT [PK_Classrooms] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Schedules] (
    [Id] int NOT NULL IDENTITY,
    [StartHour] time NOT NULL,
    [EndHour] time NOT NULL,
    [AuditCreateUser] int NOT NULL,
    [AuditCreateDate] datetime2 NOT NULL,
    [AuditUpdateUser] int NULL,
    [AuditUpdateDate] datetime2 NULL,
    [AuditDeleteUser] int NULL,
    [AuditDeleteDate] datetime2 NULL,
    [State] int NOT NULL,
    CONSTRAINT [PK_Schedules] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Students] (
    [Id] int NOT NULL IDENTITY,
    [StudentCode] AS CONCAT('ALU', RIGHT('000' + CAST(Id AS VARCHAR), 3)),
    [Name] nvarchar(200) NOT NULL,
    [Phone] nvarchar(50) NOT NULL,
    [Email] nvarchar(100) NOT NULL,
    [AuditCreateUser] int NOT NULL,
    [AuditCreateDate] datetime2 NOT NULL,
    [AuditUpdateUser] int NULL,
    [AuditUpdateDate] datetime2 NULL,
    [AuditDeleteUser] int NULL,
    [AuditDeleteDate] datetime2 NULL,
    [State] int NOT NULL,
    CONSTRAINT [PK_Students] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Subjects] (
    [Id] int NOT NULL IDENTITY,
    [SubjectCode] AS CONCAT('MAT', RIGHT('000' + CAST(Id AS VARCHAR), 3)),
    [Name] nvarchar(100) NOT NULL,
    [AuditCreateUser] int NOT NULL,
    [AuditCreateDate] datetime2 NOT NULL,
    [AuditUpdateUser] int NULL,
    [AuditUpdateDate] datetime2 NULL,
    [AuditDeleteUser] int NULL,
    [AuditDeleteDate] datetime2 NULL,
    [State] int NOT NULL,
    CONSTRAINT [PK_Subjects] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Teachers] (
    [Id] int NOT NULL IDENTITY,
    [TeacherCode] AS CONCAT('MAE', RIGHT('000' + CAST(Id AS VARCHAR), 3)),
    [Name] nvarchar(200) NOT NULL,
    [Phone] nvarchar(50) NOT NULL,
    [Email] nvarchar(100) NOT NULL,
    [AuditCreateUser] int NOT NULL,
    [AuditCreateDate] datetime2 NOT NULL,
    [AuditUpdateUser] int NULL,
    [AuditUpdateDate] datetime2 NULL,
    [AuditDeleteUser] int NULL,
    [AuditDeleteDate] datetime2 NULL,
    [State] int NOT NULL,
    CONSTRAINT [PK_Teachers] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Lessons] (
    [Id] int NOT NULL IDENTITY,
    [LessonCode] AS CONCAT('CLA', RIGHT('000' + CAST(Id AS VARCHAR), 3)),
    [ScheduleId] int NOT NULL,
    [TeacherId] int NOT NULL,
    [SubjectId] int NOT NULL,
    [ClassroomId] int NOT NULL,
    [AuditCreateUser] int NOT NULL,
    [AuditCreateDate] datetime2 NOT NULL,
    [AuditUpdateUser] int NULL,
    [AuditUpdateDate] datetime2 NULL,
    [AuditDeleteUser] int NULL,
    [AuditDeleteDate] datetime2 NULL,
    [State] int NOT NULL,
    CONSTRAINT [PK_Lessons] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Lessons_Classrooms_ClassroomId] FOREIGN KEY ([ClassroomId]) REFERENCES [Classrooms] ([Id]),
    CONSTRAINT [FK_Lessons_Schedules_ScheduleId] FOREIGN KEY ([ScheduleId]) REFERENCES [Schedules] ([Id]),
    CONSTRAINT [FK_Lessons_Subjects_SubjectId] FOREIGN KEY ([SubjectId]) REFERENCES [Subjects] ([Id]),
    CONSTRAINT [FK_Lessons_Teachers_TeacherId] FOREIGN KEY ([TeacherId]) REFERENCES [Teachers] ([Id])
);
GO

CREATE TABLE [LessonStudents] (
    [Id] int NOT NULL IDENTITY,
    [LessonId] int NOT NULL,
    [StudentId] int NOT NULL,
    [AuditCreateUser] int NOT NULL,
    [AuditCreateDate] datetime2 NOT NULL,
    [AuditUpdateUser] int NULL,
    [AuditUpdateDate] datetime2 NULL,
    [AuditDeleteUser] int NULL,
    [AuditDeleteDate] datetime2 NULL,
    [State] int NOT NULL,
    CONSTRAINT [PK_LessonStudents] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_LessonStudents_Lessons_LessonId] FOREIGN KEY ([LessonId]) REFERENCES [Lessons] ([Id]),
    CONSTRAINT [FK_LessonStudents_Students_StudentId] FOREIGN KEY ([StudentId]) REFERENCES [Students] ([Id])
);
GO

CREATE UNIQUE INDEX [IX_Classrooms_ClassroomCode] ON [Classrooms] ([ClassroomCode]);
GO

CREATE INDEX [IX_Lessons_ClassroomId] ON [Lessons] ([ClassroomId]);
GO

CREATE UNIQUE INDEX [IX_Lessons_LessonCode] ON [Lessons] ([LessonCode]);
GO

CREATE UNIQUE INDEX [IX_Lessons_ScheduleId_ClassroomId] ON [Lessons] ([ScheduleId], [ClassroomId]);
GO

CREATE UNIQUE INDEX [IX_Lessons_ScheduleId_TeacherId] ON [Lessons] ([ScheduleId], [TeacherId]);
GO

CREATE INDEX [IX_Lessons_SubjectId] ON [Lessons] ([SubjectId]);
GO

CREATE INDEX [IX_Lessons_TeacherId] ON [Lessons] ([TeacherId]);
GO

CREATE UNIQUE INDEX [IX_LessonStudents_LessonId_StudentId] ON [LessonStudents] ([LessonId], [StudentId]);
GO

CREATE INDEX [IX_LessonStudents_StudentId] ON [LessonStudents] ([StudentId]);
GO

CREATE UNIQUE INDEX [IX_Students_StudentCode] ON [Students] ([StudentCode]);
GO

CREATE UNIQUE INDEX [IX_Subjects_SubjectCode] ON [Subjects] ([SubjectCode]);
GO

CREATE UNIQUE INDEX [IX_Teachers_TeacherCode] ON [Teachers] ([TeacherCode]);
GO

INSERT INTO [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230925041438_InitialCreate', N'6.0.22');
GO

COMMIT;
GO

