/*
PM> Update-Database
Build started...
Build succeeded.
Microsoft.EntityFrameworkCore.Infrastructure[10403]
      Entity Framework Core 6.0.8 initialized 'BankTransactions' using provider 'Microsoft.EntityFrameworkCore.SqlServer:6.0.8' with options: None
Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (18ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      SELECT 1
Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (15ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      SELECT OBJECT_ID(N'[__EFMigrationsHistory]');
Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (1ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      SELECT 1
Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (13ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      CREATE TABLE [__EFMigrationsHistory] (
          [MigrationId] nvarchar(150) NOT NULL,
          [ProductVersion] nvarchar(32) NOT NULL,
          CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
      );
Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (0ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      SELECT 1
Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (1ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      SELECT OBJECT_ID(N'[__EFMigrationsHistory]');
Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (1ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      SELECT [MigrationId], [ProductVersion]
      FROM [__EFMigrationsHistory]
      ORDER BY [MigrationId];
Microsoft.EntityFrameworkCore.Migrations[20402]
      Applying migration '20220821221753_createDB'.
Applying migration '20220821221753_createDB'.
*/
-- Microsoft.EntityFrameworkCore.Database.Command[20101]
--       Executed DbCommand (2ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      CREATE TABLE [Person] (
          [Id] int NOT NULL IDENTITY,
          [IdentityDocument] nvarchar(25) NOT NULL,
          [Name] nvarchar(250) NOT NULL,
          [Gender] nvarchar(25) NULL,
          [BirthDate] datetime2 NULL,
          [Address] nvarchar(250) NULL,
          [Phone] nvarchar(75) NULL,
          CONSTRAINT [PK_Person] PRIMARY KEY ([Id])
      );
-- Microsoft.EntityFrameworkCore.Database.Command[20101]
--       Executed DbCommand (2ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      CREATE TABLE [Customer] (
          [Id] int NOT NULL,
          [Password] nvarchar(250) NULL,
          [Status] bit NOT NULL,
          CONSTRAINT [PK_Customer] PRIMARY KEY ([Id]),
          CONSTRAINT [FK_Customer_Person_Id] FOREIGN KEY ([Id]) REFERENCES [Person] ([Id])
      );
-- Microsoft.EntityFrameworkCore.Database.Command[20101]
--       Executed DbCommand (11ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      CREATE TABLE [Account] (
          [AccountNumber] nvarchar(25) NOT NULL,
          [AccountType] nvarchar(250) NOT NULL,
          [StartAmount] decimal(18,4) NOT NULL,
          [CustomerId] int NOT NULL,
          [Status] bit NOT NULL,
          CONSTRAINT [PK_Account] PRIMARY KEY ([AccountNumber]),
          CONSTRAINT [FK_Account_Customer_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [Customer] ([Id]) ON DELETE CASCADE
      );
-- Microsoft.EntityFrameworkCore.Database.Command[20101]
--       Executed DbCommand (2ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      CREATE TABLE [Transaction] (
          [Id] int NOT NULL IDENTITY,
          [CreateDate] datetimeoffset NOT NULL,
          [AccountNumber] nvarchar(25) NOT NULL,
          [Amount] decimal(18,4) NOT NULL,
          [Balance] decimal(18,4) NOT NULL,
          CONSTRAINT [PK_Transaction] PRIMARY KEY ([Id]),
          CONSTRAINT [FK_Transaction_Account_AccountNumber] FOREIGN KEY ([AccountNumber]) REFERENCES [Account] ([AccountNumber]) ON DELETE CASCADE
      );
-- Microsoft.EntityFrameworkCore.Database.Command[20101]
--       Executed DbCommand (1ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      CREATE INDEX [IX_Account_CustomerId] ON [Account] ([CustomerId]);
-- Microsoft.EntityFrameworkCore.Database.Command[20101]
--       Executed DbCommand (1ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      CREATE UNIQUE INDEX [IX_Person_IdentityDocument] ON [Person] ([IdentityDocument]);
-- Microsoft.EntityFrameworkCore.Database.Command[20101]
--       Executed DbCommand (1ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      CREATE INDEX [IX_Transaction_AccountNumber] ON [Transaction] ([AccountNumber]);
-- Microsoft.EntityFrameworkCore.Database.Command[20101]
--       Executed DbCommand (1ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
      VALUES (N'20220821221753_createDB', N'6.0.8');
-- Done.
-- PM> 