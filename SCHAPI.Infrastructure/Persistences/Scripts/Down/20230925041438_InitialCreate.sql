BEGIN TRANSACTION;
GO

DROP TABLE [LessonStudents];
GO

DROP TABLE [Lessons];
GO

DROP TABLE [Students];
GO

DROP TABLE [Classrooms];
GO

DROP TABLE [Schedules];
GO

DROP TABLE [Subjects];
GO

DROP TABLE [Teachers];
GO

DELETE FROM [dbo].[__EFMigrationsHistory]
WHERE [MigrationId] = N'20230925041438_InitialCreate';
GO

COMMIT;
GO

