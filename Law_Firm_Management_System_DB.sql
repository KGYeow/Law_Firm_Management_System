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
/****** Object:  Table [dbo].[Notification]    Script Date: 20/11/2023 9:22:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Notification](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](MAX) NOT NULL,
	[Description] [varchar](MAX) NOT NULL,
 CONSTRAINT [PK_Notification] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserNotification]    Script Date: 20/11/2023 9:25:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserNotification](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[NotificationID] [int] NULL,
	[Title] [varchar](MAX) NULL,
	[Description] [varchar](MAX) NULL,
	[IsRead] [bit] NOT NULL,
 CONSTRAINT [PK_UserNotification] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Announcement]    Script Date: 19/11/2023 4:36:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Announcement](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](MAX) NOT NULL,
	[Description] [varchar](MAX) NOT NULL,
	[CreatedTime] [datetime] NOT NULL,
 CONSTRAINT [PK_Announcement] PRIMARY KEY CLUSTERED
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Appointment]    Script Date: 13/11/2023 8:15:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Appointment](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[Category] [varchar](max) NULL,
	[AppointmentTime] [datetime] NULL,
	[CreatedTime] [datetime] NULL,
	[UpdatedTime] [datetime] NULL,
	[IsAccepted] [bit] NULL,
	[IsPending] [bit] NULL,
 CONSTRAINT [PK_Appointment] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Case]    Script Date: 19/11/2023 3:16:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Case](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](max) NULL,
	[CreatedBy] [nvarchar](100) NULL,
	[UpdatedBy] [nvarchar](100) NULL,
	[CreatedTime] [datetime] NULL,
	[UpdatedTime] [datetime] NULL,
 CONSTRAINT [PK_Case] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Task]    Script Date: 19/11/2023 3:27:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Task](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](max) NULL,
	[AssignedTime] [datetime] NULL,
	[CompletedTime] [datetime] NULL,
 CONSTRAINT [PK_Task] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TaskAssignment]    Script Date: 19/11/2023 3:31:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TaskAssignment](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[TaskID] [int] NOT NULL,
 CONSTRAINT [PK_TaskAssignment] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Event]    Script Date: 19/11/2023 3:37:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Event](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CaseID] [int] NULL,
	[Name] [varchar](max) NULL,
	[CreatedTime] [datetime] NULL,
	[EventTime] [datetime] NULL,
	[IsCompleted] [bit] NULL,
 CONSTRAINT [PK_Event] PRIMARY KEY CLUSTERED
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Document]    Script Date: 19/11/2023 3:53:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Document](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](max) NULL,
	[Version] [varchar](50) NULL,
	[CategoryId] [int] NULL,
	[CreatedBy] [nvarchar](100) NULL,
	[UpdatedBy] [nvarchar](100) NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedDate] [datetime] NULL,
	[Attachment] [varbinary](max) NULL,
	[Type] [varchar](10) NULL,
	[IsArchived] [bit] NULL,
 CONSTRAINT [PK_Document] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DocumentCategory]    Script Date: 19/11/2023 3:53:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DocumentCategory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](max) NULL,
 CONSTRAINT [PK_DocumentCategory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Client]    Script Date: 19/11/2023 4:09:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Client](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NULL,
	[FullName] [nvarchar](max) NOT NULL,
	[PhoneNumber] [nvarchar](256) NULL,
	[Email] [nvarchar](256) NULL,
	[Address] [nvarchar](max) NULL,
 CONSTRAINT [PK_Client] PRIMARY KEY CLUSTERED
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Paralegal]    Script Date: 19/11/2023 4:10:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Paralegal](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[FullName] [nvarchar](max) NOT NULL,
	[PhoneNumber] [nvarchar](256) NULL,
	[Address] [nvarchar](max) NULL,
 CONSTRAINT [PK_Paralegal] PRIMARY KEY CLUSTERED
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

SET IDENTITY_INSERT [dbo].[User] ON
INSERT [dbo].[User] ([ID], [UserRoleID], [FullName], [Username], [Email], [Password]) VALUES (1, 1, N'Admin', N'admin', N'admin@gmail.com', N'33354741122871651676713774147412831195')
INSERT [dbo].[User] ([ID], [UserRoleID], [FullName], [Username], [Email], [Password]) VALUES (2, 2, N'Paralegal', N'paralegal', N'paralegal@gmail.com', N'89137941131780581697199247420039101219')
INSERT [dbo].[User] ([ID], [UserRoleID], [FullName], [Username], [Email], [Password]) VALUES (3, 3, N'Client', N'client', N'client@gmail.com', N'98961428173194154141109188151842308924137')
SET IDENTITY_INSERT [dbo].[User] OFF
GO

SET IDENTITY_INSERT [dbo].[UserRole] ON
INSERT [dbo].[UserRole] ([ID], [Name]) VALUES (1, N'Admin')
INSERT [dbo].[UserRole] ([ID], [Name]) VALUES (2, N'Paralegal')
INSERT [dbo].[UserRole] ([ID], [Name]) VALUES (3, N'Client')
SET IDENTITY_INSERT [dbo].[UserRole] OFF
GO

SET IDENTITY_INSERT [dbo].[Page] ON
INSERT [dbo].[Page] ([ID], [Name]) VALUES (1, N'Dashboard')
INSERT [dbo].[Page] ([ID], [Name]) VALUES (2, N'Appointments')
INSERT [dbo].[Page] ([ID], [Name]) VALUES (3, N'Cases')
INSERT [dbo].[Page] ([ID], [Name]) VALUES (4, N'Tasks')
INSERT [dbo].[Page] ([ID], [Name]) VALUES (5, N'Events')
INSERT [dbo].[Page] ([ID], [Name]) VALUES (6, N'Documents')
INSERT [dbo].[Page] ([ID], [Name]) VALUES (7, N'Clients')
INSERT [dbo].[Page] ([ID], [Name]) VALUES (8, N'Paralegals')
INSERT [dbo].[Page] ([ID], [Name]) VALUES (9, N'User Settings')
INSERT [dbo].[Page] ([ID], [Name]) VALUES (10, N'Legal Information')
SET IDENTITY_INSERT [dbo].[Page] OFF
GO

SET IDENTITY_INSERT [dbo].[RoleAccessPage] ON
/** Admin **/
INSERT [dbo].[RoleAccessPage] ([ID], [UserRoleID], [PageID]) VALUES (1, 1, 1)
INSERT [dbo].[RoleAccessPage] ([ID], [UserRoleID], [PageID]) VALUES (2, 1, 2)
INSERT [dbo].[RoleAccessPage] ([ID], [UserRoleID], [PageID]) VALUES (3, 1, 3)
INSERT [dbo].[RoleAccessPage] ([ID], [UserRoleID], [PageID]) VALUES (4, 1, 4)
INSERT [dbo].[RoleAccessPage] ([ID], [UserRoleID], [PageID]) VALUES (5, 1, 5)
INSERT [dbo].[RoleAccessPage] ([ID], [UserRoleID], [PageID]) VALUES (6, 1, 6)
INSERT [dbo].[RoleAccessPage] ([ID], [UserRoleID], [PageID]) VALUES (7, 1, 7)
INSERT [dbo].[RoleAccessPage] ([ID], [UserRoleID], [PageID]) VALUES (8, 1, 8)
INSERT [dbo].[RoleAccessPage] ([ID], [UserRoleID], [PageID]) VALUES (9, 1, 9)
INSERT [dbo].[RoleAccessPage] ([ID], [UserRoleID], [PageID]) VALUES (10, 1, 10)
/** Paralegal **/
INSERT [dbo].[RoleAccessPage] ([ID], [UserRoleID], [PageID]) VALUES (11, 2, 1)
INSERT [dbo].[RoleAccessPage] ([ID], [UserRoleID], [PageID]) VALUES (12, 2, 2)
INSERT [dbo].[RoleAccessPage] ([ID], [UserRoleID], [PageID]) VALUES (13, 2, 3)
INSERT [dbo].[RoleAccessPage] ([ID], [UserRoleID], [PageID]) VALUES (14, 2, 4)
INSERT [dbo].[RoleAccessPage] ([ID], [UserRoleID], [PageID]) VALUES (15, 2, 5)
INSERT [dbo].[RoleAccessPage] ([ID], [UserRoleID], [PageID]) VALUES (16, 2, 6)
INSERT [dbo].[RoleAccessPage] ([ID], [UserRoleID], [PageID]) VALUES (17, 2, 7)
INSERT [dbo].[RoleAccessPage] ([ID], [UserRoleID], [PageID]) VALUES (18, 2, 10)
/** Client **/
INSERT [dbo].[RoleAccessPage] ([ID], [UserRoleID], [PageID]) VALUES (19, 3, 1)
INSERT [dbo].[RoleAccessPage] ([ID], [UserRoleID], [PageID]) VALUES (20, 3, 2)
INSERT [dbo].[RoleAccessPage] ([ID], [UserRoleID], [PageID]) VALUES (21, 3, 3)
INSERT [dbo].[RoleAccessPage] ([ID], [UserRoleID], [PageID]) VALUES (22, 3, 5)
SET IDENTITY_INSERT [dbo].[RoleAccessPage] OFF
GO

SET IDENTITY_INSERT [dbo].[UserNotification] ON
INSERT [dbo].[UserNotification] ([Id], [UserID], [NotificationID], [Title], [Description], [IsRead]) VALUES (1, 1, NULL, N'Title 1', N'This is notification 1.', 0)
INSERT [dbo].[UserNotification] ([Id], [UserID], [NotificationID], [Title], [Description], [IsRead]) VALUES (2, 1, NULL, N'Title 2', N'This is notification 2.', 0)
INSERT [dbo].[UserNotification] ([Id], [UserID], [NotificationID], [Title], [Description], [IsRead]) VALUES (3, 1, NULL, N'Title 3', N'This is notification 3.', 0)
INSERT [dbo].[UserNotification] ([Id], [UserID], [NotificationID], [Title], [Description], [IsRead]) VALUES (4, 1, NULL, N'Title 4', N'This is notification 4.', 0)
SET IDENTITY_INSERT [dbo].[UserNotification] OFF
GO

SET IDENTITY_INSERT [dbo].[Announcement] ON
INSERT [dbo].[Announcement] ([Id], [Title], [Description], [CreatedTime]) VALUES (1, N'Milestone 1', N'Start preparing the documentation to detail your proposed application. In milestone 1, write about the proposed application, the type of users involved, and the functionalities of the application. You may prepare your own format of document that is professionally written.', CAST(N'2023-11-15T10:54:11.157' AS DateTime))
INSERT [dbo].[Announcement] ([Id], [Title], [Description], [CreatedTime]) VALUES (2, N'Milestone 2', N'Continue your report with: Identify users and draw user diagram (1); Identify the contents of your web application. Draw content diagram (1); Identify the interaction/functionality in your web application; Use use case (1) and sequence diagram (4/5 figures-each functionality) and show the interactions and functionality involved', CAST(N'2023-11-17T10:54:11.157' AS DateTime))
INSERT [dbo].[Announcement] ([Id], [Title], [Description], [CreatedTime]) VALUES (3, N'Test 1 Date', N'Please take note of the Test 1 Date on the 28th November 2023 at 9am. Venue will be announced soon.', CAST(N'2023-11-19T10:54:11.157' AS DateTime))
SET IDENTITY_INSERT [dbo].[Announcement] OFF
GO

SET IDENTITY_INSERT [dbo].[DocumentCategory] ON
INSERT [dbo].[DocumentCategory] ([Id], [Name]) VALUES (1, N'Case Documents')
INSERT [dbo].[DocumentCategory] ([Id], [Name]) VALUES (2, N'Client Documents')
INSERT [dbo].[DocumentCategory] ([Id], [Name]) VALUES (3, N'Legal Research')
INSERT [dbo].[DocumentCategory] ([Id], [Name]) VALUES (4, N'Contracts and Agreements')
INSERT [dbo].[DocumentCategory] ([Id], [Name]) VALUES (5, N'Court Orders and Judgments')
INSERT [dbo].[DocumentCategory] ([Id], [Name]) VALUES (6, N'Financial Documents')
INSERT [dbo].[DocumentCategory] ([Id], [Name]) VALUES (7, N'Legal Forms')
INSERT [dbo].[DocumentCategory] ([Id], [Name]) VALUES (8, N'Regulatory Compliance Documents')
INSERT [dbo].[DocumentCategory] ([Id], [Name]) VALUES (9, N'Intellectual Property Documents')
INSERT [dbo].[DocumentCategory] ([Id], [Name]) VALUES (10, N'Employee and HR Documents')
INSERT [dbo].[DocumentCategory] ([Id], [Name]) VALUES (11, N'Insurance Documents')
INSERT [dbo].[DocumentCategory] ([Id], [Name]) VALUES (12, N'Discovery Documents')
INSERT [dbo].[DocumentCategory] ([Id], [Name]) VALUES (13, N'Notarial Documents')
INSERT [dbo].[DocumentCategory] ([Id], [Name]) VALUES (14, N'Estate Planning Documents')
INSERT [dbo].[DocumentCategory] ([Id], [Name]) VALUES (15, N'Real Estate Documents')
INSERT [dbo].[DocumentCategory] ([Id], [Name]) VALUES (16, N'Correspondence')
INSERT [dbo].[DocumentCategory] ([Id], [Name]) VALUES (17, N'Evidence and Exhibits')
INSERT [dbo].[DocumentCategory] ([Id], [Name]) VALUES (18, N'Administrative Documents')
SET IDENTITY_INSERT [dbo].[DocumentCategory] OFF
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

ALTER TABLE [dbo].[UserNotification] WITH CHECK ADD CONSTRAINT [FK_UserNotification_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[UserNotification] CHECK CONSTRAINT [FK_UserNotification_User]
GO
ALTER TABLE [dbo].[UserNotification] WITH CHECK ADD CONSTRAINT [FK_UserNotification_Notification] FOREIGN KEY([NotificationID])
REFERENCES [dbo].[Notification] ([ID])
GO
ALTER TABLE [dbo].[UserNotification] CHECK CONSTRAINT [FK_UserNotification_Notification]
GO

ALTER TABLE [dbo].[Appointment] WITH CHECK ADD CONSTRAINT [FK_Appointment_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[Appointment] CHECK CONSTRAINT [FK_Appointment_User]
GO

ALTER TABLE [dbo].[TaskAssignment] WITH CHECK ADD CONSTRAINT [FK_TaskAssignment_Task] FOREIGN KEY([TaskID])
REFERENCES [dbo].[Task] ([ID])
GO
ALTER TABLE [dbo].[TaskAssignment] CHECK CONSTRAINT [FK_TaskAssignment_Task]
GO
ALTER TABLE [dbo].[TaskAssignment] WITH CHECK ADD CONSTRAINT [FK_TaskAssignment_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[TaskAssignment] CHECK CONSTRAINT [FK_TaskAssignment_User]
GO

ALTER TABLE [dbo].[Event] WITH CHECK ADD CONSTRAINT [FK_Event_Case] FOREIGN KEY([CaseID])
REFERENCES [dbo].[Case] ([ID])
GO
ALTER TABLE [dbo].[Event] CHECK CONSTRAINT [FK_Event_Case]
GO

ALTER TABLE [dbo].[Document]  WITH CHECK ADD  CONSTRAINT [FK_Document_DocumentCategory] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[DocumentCategory] ([Id])
GO

ALTER TABLE [dbo].[Document] CHECK CONSTRAINT [FK_Document_DocumentCategory]
GO

ALTER TABLE [dbo].[Client] WITH CHECK ADD CONSTRAINT [FK_Client_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[Client] CHECK CONSTRAINT [FK_Client_User]
GO

ALTER TABLE [dbo].[Paralegal] WITH CHECK ADD CONSTRAINT [FK_Paralegal_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[Paralegal] CHECK CONSTRAINT [FK_Paralegal_User]
GO