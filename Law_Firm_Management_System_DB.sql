USE [Law_Firm_Management_System_DB]
GO
/****** Object:  Table [dbo].[User]    Script Date: 7/11/2023 1:29:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserRoleID] [int] NULL,
	[FullName] [nvarchar](max) NULL,
	[Username] [nvarchar](100) NOT NULL,
	[Email] [nvarchar](256) NOT NULL,
	[Password] [nvarchar](100) NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserRole]    Script Date: 7/11/2023 1:29:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRole](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
 CONSTRAINT [PK_UserRole] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Page]    Script Date: 7/11/2023 8:17:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Page](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
 CONSTRAINT [PK_Page] PRIMARY KEY CLUSTERED
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RoleAccessPage]    Script Date: 7/11/2023 8:17:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoleAccessPage](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserRoleID] [int] NOT NULL,
	[PageID] [int] NOT NULL,
 CONSTRAINT [PK_RoleAccessPage] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

SET IDENTITY_INSERT [dbo].[User] ON
INSERT [dbo].[User] ([ID], [UserRoleID], [FullName], [Username], [Email], [Password]) VALUES (1, 1, N'Admin', N'admin', N'admin@gmail.com', N'33354741122871651676713774147412831195')
INSERT [dbo].[User] ([ID], [UserRoleID], [FullName], [Username], [Email], [Password]) VALUES (2, 2, N'User1', N'User1', N'user1@gmail.com', N'107144139120952191609010070521251748216197')
SET IDENTITY_INSERT [dbo].[User] OFF
GO

SET IDENTITY_INSERT [dbo].[UserRole] ON
INSERT [dbo].[UserRole] ([ID], [Name]) VALUES (1, N'Admin')
INSERT [dbo].[UserRole] ([ID], [Name]) VALUES (2, N'Normal')
SET IDENTITY_INSERT [dbo].[UserRole] OFF
GO

SET IDENTITY_INSERT [dbo].[Page] ON
INSERT [dbo].[Page] ([ID], [Name]) VALUES (1, N'Dashboard')
INSERT [dbo].[Page] ([ID], [Name]) VALUES (2, N'Sample Page')
SET IDENTITY_INSERT [dbo].[Page] OFF
GO

SET IDENTITY_INSERT [dbo].[RoleAccessPage] ON
INSERT [dbo].[RoleAccessPage] ([ID], [UserRoleID], [PageID]) VALUES (1, 1, 1)
INSERT [dbo].[RoleAccessPage] ([ID], [UserRoleID], [PageID]) VALUES (2, 1, 2)
INSERT [dbo].[RoleAccessPage] ([ID], [UserRoleID], [PageID]) VALUES (3, 2, 1)
SET IDENTITY_INSERT [dbo].[RoleAccessPage] OFF
GO

ALTER TABLE [dbo].[User] WITH CHECK ADD CONSTRAINT [FK_User_UserRole] FOREIGN KEY([UserRoleID])
REFERENCES [dbo].[UserRole] ([ID])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_UserRole]
GO

ALTER TABLE [dbo].[RoleAccessPage] WITH CHECK ADD CONSTRAINT [FK_RoleAccessPage_Page] FOREIGN KEY([PageID])
REFERENCES [dbo].[Page] ([ID])
GO
ALTER TABLE [dbo].[RoleAccessPage] CHECK CONSTRAINT [FK_RoleAccessPage_Page]
GO
ALTER TABLE [dbo].[RoleAccessPage] WITH CHECK ADD CONSTRAINT [FK_RoleAccessPage_UserRole] FOREIGN KEY([UserRoleID])
REFERENCES [dbo].[UserRole] ([ID])
GO
ALTER TABLE [dbo].[RoleAccessPage] CHECK CONSTRAINT [FK_RoleAccessPage_UserRole]
GO