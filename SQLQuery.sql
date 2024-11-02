Create database AuthApi

USE [AuthApi]

GO

/****** Object:  Table [dbo].[Users]    Script Date: 11/2/2024 9:30:36 PM ******/

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[PasswordHash] [nvarchar](255) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[Role] [nvarchar](50) NOT NULL,
	[CreatedAt] [datetime] NULL,
	[UpdatedAt] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Users] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO

ALTER TABLE [dbo].[Users] ADD  DEFAULT (getdate()) FOR [UpdatedAt]
GO


/*******************add user StoredProcedure*****************/

USE [AuthApi]
GO

/****** Object:  StoredProcedure [dbo].[AddUser]    Script Date: 11/2/2024 9:34:19 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- Create the stored procedure to add a user
CREATE PROCEDURE [dbo].[AddUser] 
    @Username NVARCHAR(50), 
    @PasswordHash NVARCHAR(255), 
    @Email NVARCHAR(100), 
    @Role NVARCHAR(50) 
AS 
BEGIN 
    INSERT INTO Users (Username, PasswordHash, Email, Role) 
    VALUES (@Username, @PasswordHash, @Email, @Role); 
END 
GO


/*******************Select user StoredProcedure*****************/

USE [AuthApi]
GO

/****** Object:  StoredProcedure [dbo].[GetUserByUsername]    Script Date: 11/2/2024 9:36:28 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- Create the stored procedure to get a user by username
CREATE PROCEDURE [dbo].[GetUserByUsername] 
    @Username NVARCHAR(50) 
AS 
BEGIN 
    SELECT * FROM Users WHERE Username = @Username; 
END 
GO





