/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

USE [GiveCRM_Admin]
GO

IF NOT EXISTS (SELECT 1 FROM [dbo].[aspnet_SchemaVersions] WHERE [Feature] = 'common')
INSERT INTO [dbo].[aspnet_SchemaVersions]
 ([Feature]
 ,[CompatibleSchemaVersion]
 ,[IsCurrentVersion])
 VALUES
 ('common'
 ,1
 ,1)
GO


IF NOT EXISTS (SELECT 1 FROM [dbo].[aspnet_SchemaVersions] WHERE [Feature] = 'membership')
INSERT INTO [dbo].[aspnet_SchemaVersions]
 ([Feature]
 ,[CompatibleSchemaVersion]
 ,[IsCurrentVersion])
 VALUES
 ('membership'
 ,1
 ,1)
GO