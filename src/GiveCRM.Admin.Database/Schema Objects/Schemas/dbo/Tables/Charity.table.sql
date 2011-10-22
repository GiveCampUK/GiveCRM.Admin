CREATE TABLE [dbo].[Charity]
(
	Id int IDENTITY(1,1) NOT NULL PRIMARY KEY, 
	Name nvarchar(100) NOT NULL,
	UserName nvarchar(100) NULL,
	RegisteredCharityNumber nvarchar(20) NULL,
	SubDomain nvarchar(20) NULL,
	UserId nvarchar(50) NOT NULL,
	EncryptedPassword binary(64) NOT NULL,
	Salt binary(16) NOT NULL
)
