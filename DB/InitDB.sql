CREATE TABLE [User] (
  [Id] int IDENTITY (1,1) NOT NULL  PRIMARY KEY,
  [FirstName] nvarchar(30) NOT NULL,
  [LastName] nvarchar(30) NOT NULL,
  [Phone1] nvarchar(14) NOT NULL,
  [Phone2] nvarchar(14) NULL,
  [Email] nvarchar(50) NULL,
  [Created] datetime NULL,
  [Deleted] datetime NULL
);
GO


CREATE TABLE [Car] (
  [Id] int IDENTITY (1,1) NOT NULL PRIMARY KEY,
  [UserId] int  NOT NULL,
  [Make] nvarchar(30) NOT NULL,
  [Model] nvarchar(30) NOT NULL,
  [RegistrationNumber] nvarchar(8) NOT NULL,
  [Created] datetime NULL,
  [Deleted] datetime NULL
);
GO

ALTER TABLE [Car] ADD CONSTRAINT FK_User_Car FOREIGN KEY (UserId) REFERENCES [User](Id)
GO


CREATE TABLE [Inspection] (
  [Id] int IDENTITY (1,1) NOT NULL PRIMARY KEY,
  [CarId] int  NOT NULL,
  [InspectionDate] datetime NOT NULL,
  [Comments] nvarchar(255) NULL,
  [NextInspectionYears] int  NOT NULL,
  [Notified] datetime NULL,
  [NotificationComments] nvarchar(255) NULL,
  [Created] datetime NULL,
  [Deleted] datetime NULL
);
GO

ALTER TABLE [Inspection] ADD CONSTRAINT FK_Car_Inspection FOREIGN KEY (CarId) REFERENCES [Car](Id)
GO