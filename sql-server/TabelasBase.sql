



CREATE TABLE [MessageInBroker]
(
	[MessageId] INT PRIMARY KEY IDENTITY(1,1)
	,[Name] VARCHAR(1000) NOT NULL
	,[CurrentContext] VARCHAR(100) NOT NULL
	,[Body] NVARCHAR(MAX) NOT NULL
	,[Stored] DATETIMEOFFSET NOT NULL
	,[Processed] DATETIMEOFFSET NULL
	,[Num] INT NOT NULL
	,[IsEvent] BIT NOT NULL
	,[OriginalContext] VARCHAR(100) NULL
	,[MessageIdReference] INT NULL
);