CREATE TABLE [dbo].[CharityMembership] (
    [Id]        INT           IDENTITY (1, 1) NOT NULL,
    [CharityId] INT           NOT NULL,
    [UserName]  NVARCHAR (50) NOT NULL
);



