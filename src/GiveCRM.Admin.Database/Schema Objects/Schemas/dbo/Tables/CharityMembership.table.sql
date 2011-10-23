CREATE TABLE [dbo].[CharityMembership] (
    [Id]        INT              NOT NULL,
    [CharityId] INT              NOT NULL,
    [UserId]    UNIQUEIDENTIFIER NOT NULL
);

