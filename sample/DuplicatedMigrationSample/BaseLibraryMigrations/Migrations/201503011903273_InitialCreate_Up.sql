SET NOCOUNT ON;
GO

SET IDENTITY_INSERT [dbo].[EntityMetadatas] ON;

INSERT INTO [dbo].[EntityMetadatas]([Id], [EntityName])
SELECT 1, N'Lookup'
GO

SET IDENTITY_INSERT [dbo].[EntityMetadatas] OFF;

SET IDENTITY_INSERT [dbo].[EntityPropertyMetadatas] ON;

INSERT INTO [dbo].[EntityPropertyMetadatas]([Id], [PropertyName], [EntityId])
SELECT 1, N'Name', 1
GO

SET IDENTITY_INSERT [dbo].[EntityPropertyMetadatas] OFF;