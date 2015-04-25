SET NOCOUNT ON;
GO

UPDATE BaseLib.EntityMetadata SET Discriminator = N'EntityMetadata' WHERE Discriminator = N''