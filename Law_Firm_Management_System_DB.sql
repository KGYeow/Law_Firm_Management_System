USE [Law_Firm_Management_System_DB]
GO
/****** Object:  Table [dbo].[User]    Script Date: 7/11/2023 1:29:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserRoleID] [int] NOT NULL,
	[FullName] [nvarchar](max) NOT NULL,
	[Username] [nvarchar](100) NOT NULL,
	[Email] [nvarchar](256) NOT NULL,
	[Password] [nvarchar](100) NOT NULL,
	[ProfilePhoto] [varbinary](max) NULL,
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
	[AccessName] [varchar](MAX) NULL,
 CONSTRAINT [PK_Page] PRIMARY KEY CLUSTERED
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
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
/****** Object:  Table [dbo].[Notification]    Script Date: 20/11/2023 9:25:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Notification](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[Title] [varchar](MAX) NOT NULL,
	[Description] [varchar](MAX) NOT NULL,
	[IsRead] [bit] NOT NULL,
 CONSTRAINT [PK_Notification] PRIMARY KEY CLUSTERED 
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
	[PartnerUserID] [int] NOT NULL,
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
	[ClientID] [int] NOT NULL,
	[PartnerUserID] [int] NOT NULL,
	[CategoryID] [int] NOT NULL,
	[AppointmentTime] [datetime] NOT NULL,
	[Status] [varchar](100) NOT NULL,
 CONSTRAINT [PK_Appointment] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AppointmentCategory]    Script Date: 21/12/2023 8:53:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AppointmentCategory](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NOT NULL,
 CONSTRAINT [PK_AppointmentCategory] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Case]    Script Date: 19/11/2023 3:16:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Case](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PartnerUserID] [int] NULL,
	[ClientID] [int] NULL,
	[Name] [varchar](max) NULL,
	[CreatedTime] [datetime] NOT NULL,
	[UpdatedTime] [datetime] NULL,
	[ClosedTime] [datetime] NULL,
	[StatusID] [int] NULL,
 CONSTRAINT [PK_Case] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CaseStatus]    Script Date: 1/2/2024 12:07:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CaseStatus](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StatusName] [varchar](100) NULL,
	[StatusDescription] [nvarchar](max) NULL,
 CONSTRAINT [PK_Case_Status] PRIMARY KEY CLUSTERED 
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
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ParalegalUserID] [int] NULL,
	[PartnerUserID] [int] NOT NULL,
	[CaseID]  [int] NULL,
	[EventID]  [int] NULL,
	[DocumentID]  [int] NULL,
	[Title] [varchar](max) NOT NULL,
	[Description] [varchar](max) NOT NULL,
	[AssignedTime] [datetime] NOT NULL,
	[CompletedTime] [datetime] NULL,
	[DueTime] [datetime] NOT NULL,
	[InProgress] [bit] NOT NULL,
 CONSTRAINT [PK_Task] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Event]    Script Date: 19/11/2023 3:37:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Event](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CaseID] [int] NULL,
	[PartnerUserID] [int] NULL,
	[ClientID] [int] NULL,
	[Name] [varchar](max) NOT NULL,
	[CreatedTime] [datetime] NOT NULL,
	[EventTime] [datetime] NOT NULL,
	[Description] [varchar](max) NOT NULL,
	[IsCompleted] [bit] NOT NULL,
 CONSTRAINT [PK_Event] PRIMARY KEY CLUSTERED
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Document]    Script Date: 19/11/2023 3:53:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Document](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](max) NOT NULL,
	[CategoryID] [int] NOT NULL,
	[CaseID] [int] NULL,
	[PartnerUserID] [int] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[Type] [varchar](10) NOT NULL,
	[IsArchived] [bit] NOT NULL,
	[Attachment] [varbinary](max) NOT NULL,
 CONSTRAINT [PK_Document] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DocumentCategory]    Script Date: 19/11/2023 3:53:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DocumentCategory](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](max) NOT NULL,
 CONSTRAINT [PK_DocumentCategory] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
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
	[UserID] [int] NOT NULL,
	[PhoneNumber] [nvarchar](256) NULL,
	[Address] [nvarchar](max) NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Paralegal] PRIMARY KEY CLUSTERED
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Partner]    Script Date: 29/11/2023 6:18:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Partner](
	[UserID] [int] NOT NULL,
	[ParalegalUserID] [int] NULL,
	[PhoneNumber] [nvarchar](256) NULL,
	[Address] [nvarchar](max) NULL,
 CONSTRAINT [PK_Partner] PRIMARY KEY CLUSTERED
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

SET IDENTITY_INSERT [dbo].[User] ON
INSERT [dbo].[User] ([ID], [UserRoleID], [FullName], [Username], [Email], [Password]) VALUES (1, 1, N'Partner', N'partner', N'partner@gmail.com', N'1168411515814412785149174972167513371245116')
INSERT [dbo].[User] ([ID], [UserRoleID], [FullName], [Username], [Email], [Password]) VALUES (2, 2, N'Paralegal', N'paralegal', N'paralegal@gmail.com', N'89137941131780581697199247420039101219')
INSERT [dbo].[User] ([ID], [UserRoleID], [FullName], [Username], [Email], [Password]) VALUES (3, 3, N'Client', N'client', N'client@gmail.com', N'98961428173194154141109188151842308924137')
INSERT [dbo].[User] ([ID], [UserRoleID], [FullName], [Username], [Email], [Password]) VALUES (4, 3, N'Kaedehara Kazuha', N'kaedeharakazuha', N'kaedeharakazuha@gmail.com', N'1698229722215936261825011619413220617112')
INSERT [dbo].[User] ([ID], [UserRoleID], [FullName], [Username], [Email], [Password]) VALUES (5, 3, N'Zhongli', N'zhongli', N'zhongli@gmail.com', N'160230839085551011691861531893924893237115')
INSERT [dbo].[User] ([ID], [UserRoleID], [FullName], [Username], [Email], [Password]) VALUES (6, 3, N'Eula', N'eula', N'eula@gmail.com', N'1401131841402441261731401620461320023622254')
INSERT [dbo].[User] ([ID], [UserRoleID], [FullName], [Username], [Email], [Password]) VALUES (7, 3, N'Nahida', N'nahida', N'nahida@gmail.com', N'1602187344681455415123073156876889172')
INSERT [dbo].[User] ([ID], [UserRoleID], [FullName], [Username], [Email], [Password]) VALUES (8, 3, N'Venti', N'venti', N'venti@gmail.com', N'17699147227128127110140841261138315010621071')
INSERT [dbo].[User] ([ID], [UserRoleID], [FullName], [Username], [Email], [Password]) VALUES (9, 3, N'Raiden Ei', N'raidenei', N'raidenei@gmail.com', N'107135552716210199193165434580186180116203')
INSERT [dbo].[User] ([ID], [UserRoleID], [FullName], [Username], [Email], [Password]) VALUES (10, 3, N'Furina', N'furina', N'furina@gmail.com', N'881562001651901052256918214722090894819320')
INSERT [dbo].[User] ([ID], [UserRoleID], [FullName], [Username], [Email], [Password]) VALUES (11, 3, N'Yelan', N'yelan', N'yelan@gmail.com', N'622011094815217810815163472101785479182184')
INSERT [dbo].[User] ([ID], [UserRoleID], [FullName], [Username], [Email], [Password]) VALUES (12, 3, N'Cyno', N'cyno', N'cyno@gmail.com', N'3125514245153154221163672511941482102111671')
INSERT [dbo].[User] ([ID], [UserRoleID], [FullName], [Username], [Email], [Password]) VALUES (13, 3, N'Kamisato Ayaka', N'kamisatoayaka', N'kamisatoayaka@gmail.com', N'10302252491371331381971833523518207176203124')
INSERT [dbo].[User] ([ID], [UserRoleID], [FullName], [Username], [Email], [Password]) VALUES (14, 3, N'Kamisato Ayato', N'kamisatoayato', N'kamisatoayato@gmail.com', N'971751295718011822825314925166148162210223228')
INSERT [dbo].[User] ([ID], [UserRoleID], [FullName], [Username], [Email], [Password]) VALUES (15, 3, N'Yae Miko', N'yaemiko', N'yaemiko@gmail.com', N'14124282371541311961724013872009619218100')
INSERT [dbo].[User] ([ID], [UserRoleID], [FullName], [Username], [Email], [Password]) VALUES (16, 2, N'Yanfei', N'yanfei', N'yanfei@gmail.com', N'10812554593111297225504418545781096242')
INSERT [dbo].[User] ([ID], [UserRoleID], [FullName], [Username], [Email], [Password]) VALUES (17, 1, N'Sangonomiya Kokomi', N'sangonomiyakokomi', N'sangonomiyakokomi@gmail.com', N'231367268801502362213214126112115194240144')
INSERT [dbo].[User] ([ID], [UserRoleID], [FullName], [Username], [Email], [Password]) VALUES (18, 1, N'Neuvillette', N'neuvillette', N'neuvillette@gmail.com', N'10169119360185239137213194719522160242186')
INSERT [dbo].[User] ([ID], [UserRoleID], [FullName], [Username], [Email], [Password]) VALUES (19, 2, N'Navia', N'navia', N'navia@gmail.com', N'199210190154140229241542024021514513556177193')
INSERT [dbo].[User] ([ID], [UserRoleID], [FullName], [Username], [Email], [Password]) VALUES (20, 2, N'Clorinde', N'clorinde', N'clorinde@gmail.com', N'179170115257585241115441481492411931746055')
INSERT [dbo].[User] ([ID], [UserRoleID], [FullName], [Username], [Email], [Password]) VALUES (21, 2, N'Gorou', N'gorou', N'gorou@gmail.com', N'145228208160331994022912622921219011880213')
INSERT [dbo].[User] ([ID], [UserRoleID], [FullName], [Username], [Email], [Password]) VALUES (22, 3, N'Dehya', N'dehya', N'dehya@gmail.com', N'056210218223621831531095511965612247028')
SET IDENTITY_INSERT [dbo].[User] OFF
GO

SET IDENTITY_INSERT [dbo].[UserRole] ON
INSERT [dbo].[UserRole] ([ID], [Name]) VALUES (1, N'Partner')
INSERT [dbo].[UserRole] ([ID], [Name]) VALUES (2, N'Paralegal')
INSERT [dbo].[UserRole] ([ID], [Name]) VALUES (3, N'Client')
SET IDENTITY_INSERT [dbo].[UserRole] OFF
GO

SET IDENTITY_INSERT [dbo].[Page] ON
INSERT [dbo].[Page] ([ID], [Name], [AccessName]) VALUES (1, N'Dashboard', N'Dashboard_Client')
INSERT [dbo].[Page] ([ID], [Name], [AccessName]) VALUES (2, N'Dashboard', N'Dashboard_Employee')
INSERT [dbo].[Page] ([ID], [Name], [AccessName]) VALUES (3, N'Appointments', N'Appointments_Client')
INSERT [dbo].[Page] ([ID], [Name], [AccessName]) VALUES (4, N'Appointments', N'Appointments_Employee')
INSERT [dbo].[Page] ([ID], [Name], [AccessName]) VALUES (5, N'Cases', N'Cases')
INSERT [dbo].[Page] ([ID], [Name], [AccessName]) VALUES (6, N'Tasks', N'Tasks')
INSERT [dbo].[Page] ([ID], [Name], [AccessName]) VALUES (7, N'Events', N'Events_Client')
INSERT [dbo].[Page] ([ID], [Name], [AccessName]) VALUES (8, N'Events', N'Events_Employee')
INSERT [dbo].[Page] ([ID], [Name], [AccessName]) VALUES (9, N'Repositories', N'Repositories')
INSERT [dbo].[Page] ([ID], [Name], [AccessName]) VALUES (10, N'Archives', N'Archives')
INSERT [dbo].[Page] ([ID], [Name], [AccessName]) VALUES (11, N'Clients', N'Clients')
INSERT [dbo].[Page] ([ID], [Name], [AccessName]) VALUES (12, N'Paralegals', N'Paralegals')
INSERT [dbo].[Page] ([ID], [Name], [AccessName]) VALUES (13, N'Partners', N'Partners_Client')
INSERT [dbo].[Page] ([ID], [Name], [AccessName]) VALUES (14, N'Partners', N'Partners_Employee')
INSERT [dbo].[Page] ([ID], [Name], [AccessName]) VALUES (15, N'User Settings', N'UserSettings')
INSERT [dbo].[Page] ([ID], [Name], [AccessName]) VALUES (16, N'Legal Information', N'LegalInformation')
INSERT [dbo].[Page] ([ID], [Name], [AccessName]) VALUES (17, N'Cases', N'Cases_Client')
INSERT [dbo].[Page] ([ID], [Name], [AccessName]) VALUES (18, N'Cases', N'Cases_Paralegal')
SET IDENTITY_INSERT [dbo].[Page] OFF
GO

SET IDENTITY_INSERT [dbo].[RoleAccessPage] ON
/** Admin **/
INSERT [dbo].[RoleAccessPage] ([ID], [UserRoleID], [PageID]) VALUES (1, 1, 2)
INSERT [dbo].[RoleAccessPage] ([ID], [UserRoleID], [PageID]) VALUES (2, 1, 4)
INSERT [dbo].[RoleAccessPage] ([ID], [UserRoleID], [PageID]) VALUES (3, 1, 5)
INSERT [dbo].[RoleAccessPage] ([ID], [UserRoleID], [PageID]) VALUES (4, 1, 6)
INSERT [dbo].[RoleAccessPage] ([ID], [UserRoleID], [PageID]) VALUES (5, 1, 8)
INSERT [dbo].[RoleAccessPage] ([ID], [UserRoleID], [PageID]) VALUES (6, 1, 9)
INSERT [dbo].[RoleAccessPage] ([ID], [UserRoleID], [PageID]) VALUES (7, 1, 10)
INSERT [dbo].[RoleAccessPage] ([ID], [UserRoleID], [PageID]) VALUES (8, 1, 11)
INSERT [dbo].[RoleAccessPage] ([ID], [UserRoleID], [PageID]) VALUES (9, 1, 12)
INSERT [dbo].[RoleAccessPage] ([ID], [UserRoleID], [PageID]) VALUES (10, 1, 14)
INSERT [dbo].[RoleAccessPage] ([ID], [UserRoleID], [PageID]) VALUES (11, 1, 15)
INSERT [dbo].[RoleAccessPage] ([ID], [UserRoleID], [PageID]) VALUES (12, 1, 16)
/** Paralegal **/
INSERT [dbo].[RoleAccessPage] ([ID], [UserRoleID], [PageID]) VALUES (13, 2, 2)
INSERT [dbo].[RoleAccessPage] ([ID], [UserRoleID], [PageID]) VALUES (14, 2, 4)
INSERT [dbo].[RoleAccessPage] ([ID], [UserRoleID], [PageID]) VALUES (15, 2, 18)
INSERT [dbo].[RoleAccessPage] ([ID], [UserRoleID], [PageID]) VALUES (16, 2, 6)
INSERT [dbo].[RoleAccessPage] ([ID], [UserRoleID], [PageID]) VALUES (17, 2, 8)
INSERT [dbo].[RoleAccessPage] ([ID], [UserRoleID], [PageID]) VALUES (18, 2, 9)
INSERT [dbo].[RoleAccessPage] ([ID], [UserRoleID], [PageID]) VALUES (19, 2, 11)
INSERT [dbo].[RoleAccessPage] ([ID], [UserRoleID], [PageID]) VALUES (20, 2, 14)
INSERT [dbo].[RoleAccessPage] ([ID], [UserRoleID], [PageID]) VALUES (21, 2, 16)
/** Client **/
INSERT [dbo].[RoleAccessPage] ([ID], [UserRoleID], [PageID]) VALUES (22, 3, 1)
INSERT [dbo].[RoleAccessPage] ([ID], [UserRoleID], [PageID]) VALUES (23, 3, 3)
INSERT [dbo].[RoleAccessPage] ([ID], [UserRoleID], [PageID]) VALUES (24, 3, 17)
INSERT [dbo].[RoleAccessPage] ([ID], [UserRoleID], [PageID]) VALUES (25, 3, 7)
INSERT [dbo].[RoleAccessPage] ([ID], [UserRoleID], [PageID]) VALUES (26, 3, 13)
SET IDENTITY_INSERT [dbo].[RoleAccessPage] OFF
GO

SET IDENTITY_INSERT [dbo].[Notification] ON
INSERT [dbo].[Notification] ([ID], [UserID], [Title], [Description], [IsRead]) VALUES (1, 1, N'Title 1', N'This is notification 1.', 0)
INSERT [dbo].[Notification] ([ID], [UserID], [Title], [Description], [IsRead]) VALUES (2, 1, N'Title 2', N'This is notification 2.', 0)
INSERT [dbo].[Notification] ([ID], [UserID], [Title], [Description], [IsRead]) VALUES (3, 1, N'Title 3', N'This is notification 3.', 0)
INSERT [dbo].[Notification] ([ID], [UserID], [Title], [Description], [IsRead]) VALUES (4, 1, N'Title 4', N'This is notification 4.', 0)
SET IDENTITY_INSERT [dbo].[Notification] OFF
GO

SET IDENTITY_INSERT [dbo].[Announcement] ON
INSERT [dbo].[Announcement] ([ID], [PartnerUserID], [Title], [Description], [CreatedTime]) VALUES (1, 1, N'Milestone 1', N'Start preparing the documentation to detail your proposed application. In milestone 1, write about the proposed application, the type of users involved, and the functionalities of the application. You may prepare your own format of document that is professionally written.', CAST(N'2023-11-15T10:54:11.157' AS DateTime))
INSERT [dbo].[Announcement] ([ID], [PartnerUserID], [Title], [Description], [CreatedTime]) VALUES (2, 17, N'Milestone 2', N'Continue your report with: Identify users and draw user diagram (1); Identify the contents of your web application. Draw content diagram (1); Identify the interaction/functionality in your web application; Use use case (1) and sequence diagram (4/5 figures-each functionality) and show the interactions and functionality involved', CAST(N'2023-11-17T10:54:11.157' AS DateTime))
INSERT [dbo].[Announcement] ([ID], [PartnerUserID], [Title], [Description], [CreatedTime]) VALUES (3, 18, N'Test 1 Date', N'Please take note of the Test 1 Date on the 28th November 2023 at 9am. Venue will be announced soon.', CAST(N'2023-11-19T10:54:11.157' AS DateTime))
SET IDENTITY_INSERT [dbo].[Announcement] OFF
GO

SET IDENTITY_INSERT [dbo].[Appointment] ON
INSERT [dbo].[Appointment] ([ID], [ClientID], [PartnerUserID], [CategoryID], [AppointmentTime], [Status]) VALUES (1, 1, 18, N'2', CAST(N'2023-11-15T10:54:11.157' AS DateTime), N'Pending')
INSERT [dbo].[Appointment] ([ID], [ClientID], [PartnerUserID], [CategoryID], [AppointmentTime], [Status]) VALUES (2, 2, 17, N'5', CAST(N'2023-11-23T10:54:11.157' AS DateTime), N'Pending')
INSERT [dbo].[Appointment] ([ID], [ClientID], [PartnerUserID], [CategoryID], [AppointmentTime], [Status]) VALUES (3, 3, 18, N'3', CAST(N'2023-11-08T10:54:11.157' AS DateTime), N'Pending')
INSERT [dbo].[Appointment] ([ID], [ClientID], [PartnerUserID], [CategoryID], [AppointmentTime], [Status]) VALUES (4, 4, 17, N'2', CAST(N'2023-11-09T10:54:11.157' AS DateTime), N'Pending')
INSERT [dbo].[Appointment] ([ID], [ClientID], [PartnerUserID], [CategoryID], [AppointmentTime], [Status]) VALUES (5, 5, 17, N'3', CAST(N'2023-12-01T10:54:11.157' AS DateTime), N'Pending')
INSERT [dbo].[Appointment] ([ID], [ClientID], [PartnerUserID], [CategoryID], [AppointmentTime], [Status]) VALUES (6, 6, 18, N'6', CAST(N'2023-11-20T10:54:11.157' AS DateTime), N'Pending')
INSERT [dbo].[Appointment] ([ID], [ClientID], [PartnerUserID], [CategoryID], [AppointmentTime], [Status]) VALUES (7, 7, 17, N'2', CAST(N'2023-11-30T10:54:11.157' AS DateTime), N'Pending')
INSERT [dbo].[Appointment] ([ID], [ClientID], [PartnerUserID], [CategoryID], [AppointmentTime], [Status]) VALUES (8, 8, 17, N'3', CAST(N'2023-11-23T10:54:11.157' AS DateTime), N'Pending')
INSERT [dbo].[Appointment] ([ID], [ClientID], [PartnerUserID], [CategoryID], [AppointmentTime], [Status]) VALUES (9, 9, 18, N'2', CAST(N'2023-11-25T10:54:11.157' AS DateTime), N'Pending')
INSERT [dbo].[Appointment] ([ID], [ClientID], [PartnerUserID], [CategoryID], [AppointmentTime], [Status]) VALUES (10, 1, 1, N'4', CAST(N'2023-11-15T10:54:11.157' AS DateTime), N'Rejected')
INSERT [dbo].[Appointment] ([ID], [ClientID], [PartnerUserID], [CategoryID], [AppointmentTime], [Status]) VALUES (11, 1, 17, N'2', CAST(N'2023-11-18T10:54:11.157' AS DateTime), N'Pending')
INSERT [dbo].[Appointment] ([ID], [ClientID], [PartnerUserID], [CategoryID], [AppointmentTime], [Status]) VALUES (12, 1, 1, N'3', CAST(N'2023-11-22T10:54:11.157' AS DateTime), N'Pending')
INSERT [dbo].[Appointment] ([ID], [ClientID], [PartnerUserID], [CategoryID], [AppointmentTime], [Status]) VALUES (13, 1, 17, N'4', CAST(N'2023-12-10T10:54:11.157' AS DateTime), N'Approved')
SET IDENTITY_INSERT [dbo].[Appointment] OFF
GO

SET IDENTITY_INSERT [dbo].[AppointmentCategory] ON
INSERT [dbo].[AppointmentCategory] ([ID], [Name]) VALUES (1, N'Meeting')
INSERT [dbo].[AppointmentCategory] ([ID], [Name]) VALUES (2, N'Consultation')
INSERT [dbo].[AppointmentCategory] ([ID], [Name]) VALUES (3, N'Case Review')
INSERT [dbo].[AppointmentCategory] ([ID], [Name]) VALUES (4, N'Document Review')
INSERT [dbo].[AppointmentCategory] ([ID], [Name]) VALUES (5, N'Client Interview')
INSERT [dbo].[AppointmentCategory] ([ID], [Name]) VALUES (6, N'Mediation')
SET IDENTITY_INSERT [dbo].[AppointmentCategory] OFF
GO

SET IDENTITY_INSERT [dbo].[DocumentCategory] ON
INSERT [dbo].[DocumentCategory] ([ID], [Name]) VALUES (1, N'Other Documents')
INSERT [dbo].[DocumentCategory] ([ID], [Name]) VALUES (2, N'Case Documents')
INSERT [dbo].[DocumentCategory] ([ID], [Name]) VALUES (3, N'Client Documents')
INSERT [dbo].[DocumentCategory] ([ID], [Name]) VALUES (4, N'Legal Research')
INSERT [dbo].[DocumentCategory] ([ID], [Name]) VALUES (5, N'Contracts and Agreements')
INSERT [dbo].[DocumentCategory] ([ID], [Name]) VALUES (6, N'Court Orders and Judgments')
INSERT [dbo].[DocumentCategory] ([ID], [Name]) VALUES (7, N'Financial Documents')
INSERT [dbo].[DocumentCategory] ([ID], [Name]) VALUES (8, N'Legal Forms')
INSERT [dbo].[DocumentCategory] ([ID], [Name]) VALUES (9, N'Regulatory Compliance Documents')
INSERT [dbo].[DocumentCategory] ([ID], [Name]) VALUES (10, N'Intellectual Property Documents')
INSERT [dbo].[DocumentCategory] ([ID], [Name]) VALUES (11, N'Employee and HR Documents')
INSERT [dbo].[DocumentCategory] ([ID], [Name]) VALUES (12, N'Insurance Documents')
INSERT [dbo].[DocumentCategory] ([ID], [Name]) VALUES (13, N'Discovery Documents')
INSERT [dbo].[DocumentCategory] ([ID], [Name]) VALUES (14, N'Notarial Documents')
INSERT [dbo].[DocumentCategory] ([ID], [Name]) VALUES (15, N'Estate Planning Documents')
INSERT [dbo].[DocumentCategory] ([ID], [Name]) VALUES (16, N'Real Estate Documents')
INSERT [dbo].[DocumentCategory] ([ID], [Name]) VALUES (17, N'Correspondence')
INSERT [dbo].[DocumentCategory] ([ID], [Name]) VALUES (18, N'Evidence and Exhibits')
INSERT [dbo].[DocumentCategory] ([ID], [Name]) VALUES (19, N'Administrative Documents')
SET IDENTITY_INSERT [dbo].[DocumentCategory] OFF
GO

SET IDENTITY_INSERT [dbo].[Client] ON
INSERT [dbo].[Client] ([ID], [UserID], [FullName], [PhoneNumber], [Email], [Address]) VALUES (1, 3, N'Client', NULL, N'client@gmail.com', N'Client Address')
INSERT [dbo].[Client] ([ID], [UserID], [FullName], [PhoneNumber], [Email], [Address]) VALUES (2, 4, N'Kaedehara Kazuha', NULL, N'kaedeharakazuha@gmail.com', N'Inazuma')
INSERT [dbo].[Client] ([ID], [UserID], [FullName], [PhoneNumber], [Email], [Address]) VALUES (3, 5, N'Zhongli', NULL, N'zhongli@gmail.com', N'Liyue')
INSERT [dbo].[Client] ([ID], [UserID], [FullName], [PhoneNumber], [Email], [Address]) VALUES (4, 6, N'Eula', NULL, N'eula@gmail.com', N'Mondstadt')
INSERT [dbo].[Client] ([ID], [UserID], [FullName], [PhoneNumber], [Email], [Address]) VALUES (5, 7, N'Nahida', NULL, N'nahida@gmail.com', N'Sumeru')
INSERT [dbo].[Client] ([ID], [UserID], [FullName], [PhoneNumber], [Email], [Address]) VALUES (6, 8, N'Venti', NULL, N'venti@gmail.com', N'Mondstadt')
INSERT [dbo].[Client] ([ID], [UserID], [FullName], [PhoneNumber], [Email], [Address]) VALUES (7, 9, N'Raiden Ei', NULL, N'raidenei@gmail.com', N'Inazuma')
INSERT [dbo].[Client] ([ID], [UserID], [FullName], [PhoneNumber], [Email], [Address]) VALUES (8, 10, N'Furina', NULL, N'furina@gmail.com', N'Fontaine')
INSERT [dbo].[Client] ([ID], [UserID], [FullName], [PhoneNumber], [Email], [Address]) VALUES (9, 11, N'Yelan', NULL, N'yelan@gmail.com', N'Liyue')
INSERT [dbo].[Client] ([ID], [UserID], [FullName], [PhoneNumber], [Email], [Address]) VALUES (10, 12, N'Cyno', NULL, N'cyno@gmail.com', N'Sumeru')
INSERT [dbo].[Client] ([ID], [UserID], [FullName], [PhoneNumber], [Email], [Address]) VALUES (11, 13, N'Kamisato Ayaka', NULL, N'kamisatoayaka@gmail.com', N'Inazuma')
INSERT [dbo].[Client] ([ID], [UserID], [FullName], [PhoneNumber], [Email], [Address]) VALUES (12, 14, N'Kamisato Ayato', NULL, N'kamisatoayato@gmail.com', N'Inazuma')
INSERT [dbo].[Client] ([ID], [UserID], [FullName], [PhoneNumber], [Email], [Address]) VALUES (13, 15, N'Yae Miko', NULL, N'yaemiko@gmail.com', N'Inazuma')
INSERT [dbo].[Client] ([ID], [UserID], [FullName], [PhoneNumber], [Email], [Address]) VALUES (14, NULL, N'Arataki Itto', NULL, N'aratakiitto@gmail.com', N'Inazuma')
INSERT [dbo].[Client] ([ID], [UserID], [FullName], [PhoneNumber], [Email], [Address]) VALUES (15, NULL, N'Diluc', NULL, N'diluc@gmail.com', N'Mondstadt')
INSERT [dbo].[Client] ([ID], [UserID], [FullName], [PhoneNumber], [Email], [Address]) VALUES (16, 22, N'Dehya', NULL, N'dehya@gmail.com', N'Sumeru')
SET IDENTITY_INSERT [dbo].[Client] OFF
GO

SET IDENTITY_INSERT [dbo].[Case] ON
INSERT [dbo].[Case] ([ID], [PartnerUserID], [ClientID], [Name], [CreatedTime], [UpdatedTime], [ClosedTime], [StatusID]) VALUES (1, 1, 1, N'Civil Case A', CAST(N'2023-11-15T10:54:11.157'AS DateTime), CAST(N'2023-12-10T10:54:11.157' AS DateTime), NULL, 1)
INSERT [dbo].[Case] ([ID], [PartnerUserID], [ClientID], [Name], [CreatedTime], [UpdatedTime], [ClosedTime], [StatusID]) VALUES (2, 18, 2, N'Civil Case B', CAST(N'2023-11-15T10:54:11.157'AS DateTime), CAST(N'2023-11-30T10:54:11.157' AS DateTime), CAST(N'2023-12-10T10:54:11.157' AS DateTime), 6)
INSERT [dbo].[Case] ([ID], [PartnerUserID], [ClientID], [Name], [CreatedTime], [UpdatedTime], [ClosedTime], [StatusID]) VALUES (3, 1, 5, N'Criminal Case', CAST(N'2023-10-24T10:54:11.157'AS DateTime), CAST(N'2023-10-10T10:54:11.157' AS DateTime), NULL, 3)
SET IDENTITY_INSERT [dbo].[Case] OFF
GO

SET IDENTITY_INSERT [dbo].[CaseStatus] ON
INSERT [dbo].[CaseStatus] ([ID], [StatusName], [StatusDescription]) VALUES (1, N'Active', N'Lawyer accept the case and start working on it.')
INSERT [dbo].[CaseStatus] ([ID], [StatusName], [StatusDescription]) VALUES (2, N'Under Review', N'Lawyer assess the case details, identify legal issues, determine the appropriate strategy, and prepare for potential court filings.')
INSERT [dbo].[CaseStatus] ([ID], [StatusName], [StatusDescription]) VALUES (3, N'Negotiation', N'Lawyer engage in negotiations with clients, draft settlement agreements, prepare relevant court documents, and work towards a resolution.')
INSERT [dbo].[CaseStatus] ([ID], [StatusName], [StatusDescription]) VALUES (4, N'Court Proceedings', N'Lawyer: Conduct investigations, prepare legal documents, engage in discovery, file necessary court documents, and actively participate in court proceedings.')
INSERT [dbo].[CaseStatus] ([ID], [StatusName], [StatusDescription]) VALUES (5, N'On Hold', N'Lawyer inform the court about the temporary pause, state the reasons for the hold, monitor any external factors causing the delay, and plan for resumption.')
INSERT [dbo].[CaseStatus] ([ID], [StatusName], [StatusDescription]) VALUES (6, N'Settled', N'Lawyer finalize all necessary documentation, inform the client of case resolution, and ensure any post-settlement tasks are completed. ')
SET IDENTITY_INSERT [dbo].[CaseStatus] OFF
GO

SET IDENTITY_INSERT [dbo].[Task] ON
INSERT [dbo].[Task] ([ID], [PartnerUserID], [ParalegalUserID], [CaseID], [EventID], [DocumentID], [Title], [Description], [AssignedTime], [CompletedTime], [DueTime], [InProgress]) VALUES (1, 1, 2, 1, NULL, NULL, N'First Task', N'This is the 1st task.', CAST(N'2023-12-10T10:54:11.157' AS DateTime), NULL, CAST(N'2023-12-25T10:54:11.157' AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[Task] OFF
GO

SET IDENTITY_INSERT [dbo].[Event] ON
INSERT [dbo].[Event] ([ID], [CaseID], [PartnerUserID], [ClientID],[Name], [CreatedTime], [EventTime], [Description], [IsCompleted]) VALUES (1, 1, 1, 1, N'Court Hearings', CAST(N'2023-11-15T10:54:11.157'AS DateTime), CAST(N'2023-12-10T10:54:11.157' AS DateTime), N'Please attend court hearing', 0)
INSERT [dbo].[Event] ([ID], [CaseID], [PartnerUserID], [ClientID], [Name], [CreatedTime], [EventTime], [Description], [IsCompleted]) VALUES (2, 2, 18, 2, N'Discussions', CAST(N'2023-12-10T10:54:11.157' AS DateTime), CAST(N'2023-12-10T10:54:11.157' AS DateTime), N'Please attend the discussion session ', 1)
SET IDENTITY_INSERT [dbo].[Event] OFF
GO

INSERT [dbo].[Paralegal] ([UserID], [PhoneNumber], [Address], [IsActive]) VALUES (2, NULL, N'Paralegal Address', 1)
INSERT [dbo].[Paralegal] ([UserID], [PhoneNumber], [Address], [IsActive]) VALUES (16, NULL, N'Liyue', 1)
INSERT [dbo].[Paralegal] ([UserID], [PhoneNumber], [Address], [IsActive]) VALUES (19, NULL, N'Fontaine', 1)
INSERT [dbo].[Paralegal] ([UserID], [PhoneNumber], [Address], [IsActive]) VALUES (20, NULL, N'Fontaine', 0)

INSERT [dbo].[Partner] ([UserID], [ParalegalUserID], [PhoneNumber], [Address]) VALUES (1, 2, NULL, N'Partner Address')
INSERT [dbo].[Partner] ([UserID], [ParalegalUserID], [PhoneNumber], [Address]) VALUES (17, 19, NULL, N'Inazuma')
INSERT [dbo].[Partner] ([UserID], [ParalegalUserID], [PhoneNumber], [Address]) VALUES (18, 16, NULL, N'Fontaine')

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

ALTER TABLE [dbo].[Notification] WITH CHECK ADD CONSTRAINT [FK_Notification_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[Notification] CHECK CONSTRAINT [FK_Notification_User]
GO

ALTER TABLE [dbo].[Announcement] WITH CHECK ADD CONSTRAINT [FK_Announcement_Partner] FOREIGN KEY([PartnerUserID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[Announcement] CHECK CONSTRAINT [FK_Announcement_Partner]
GO

ALTER TABLE [dbo].[Appointment] WITH CHECK ADD CONSTRAINT [FK_Appointment_Client] FOREIGN KEY([ClientID])
REFERENCES [dbo].[Client] ([ID])
GO
ALTER TABLE [dbo].[Appointment] CHECK CONSTRAINT [FK_Appointment_Client]
GO
ALTER TABLE [dbo].[Appointment] WITH CHECK ADD CONSTRAINT [FK_Appointment_Partner] FOREIGN KEY([PartnerUserID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[Appointment] CHECK CONSTRAINT [FK_Appointment_Partner]
GO

ALTER TABLE [dbo].[Appointment]  WITH CHECK ADD  CONSTRAINT [FK_Appointment_AppointmentCategory] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[AppointmentCategory] ([Id])
GO
ALTER TABLE [dbo].[Appointment] CHECK CONSTRAINT [FK_Appointment_AppointmentCategory]
GO

ALTER TABLE [dbo].[Task] WITH CHECK ADD CONSTRAINT [FK_Task_Partner] FOREIGN KEY([PartnerUserID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[Task] CHECK CONSTRAINT [FK_Task_Partner]
GO
ALTER TABLE [dbo].[Task] WITH CHECK ADD CONSTRAINT [FK_Task_Paralegal] FOREIGN KEY([ParalegalUserID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[Task] CHECK CONSTRAINT [FK_Task_Paralegal]
GO
ALTER TABLE [dbo].[Task] WITH CHECK ADD CONSTRAINT [FK_Task_Case] FOREIGN KEY([CaseID])
REFERENCES [dbo].[Case] ([ID])
GO
ALTER TABLE [dbo].[Task] CHECK CONSTRAINT [FK_Task_Case]
GO
ALTER TABLE [dbo].[Task] WITH CHECK ADD CONSTRAINT [FK_Task_Event] FOREIGN KEY([EventID])
REFERENCES [dbo].[Event] ([ID])
GO
ALTER TABLE [dbo].[Task] CHECK CONSTRAINT [FK_Task_Event]
GO
ALTER TABLE [dbo].[Task] WITH CHECK ADD CONSTRAINT [FK_Task_Document] FOREIGN KEY([DocumentID])
REFERENCES [dbo].[Document] ([ID])
GO
ALTER TABLE [dbo].[Task] CHECK CONSTRAINT [FK_Task_Document]
GO

ALTER TABLE [dbo].[Case] WITH CHECK ADD CONSTRAINT [FK_Partner_Case] FOREIGN KEY([PartnerUserID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[Case] CHECK CONSTRAINT [FK_Partner_Case]
GO

ALTER TABLE [dbo].[Case] WITH CHECK ADD CONSTRAINT [FK_Client_Case] FOREIGN KEY([ClientID])
REFERENCES [dbo].[Client] ([ID])
GO
ALTER TABLE [dbo].[Case] CHECK CONSTRAINT [FK_Client_Case]
GO

ALTER TABLE [dbo].[Case] WITH CHECK ADD CONSTRAINT [FK_Case_Status] FOREIGN KEY([StatusID])
REFERENCES [dbo].[CaseStatus] ([ID])
GO
ALTER TABLE [dbo].[Case] CHECK CONSTRAINT [FK_Case_Status]
GO

ALTER TABLE [dbo].[Event] WITH CHECK ADD CONSTRAINT [FK_Event_Case] FOREIGN KEY([CaseID])
REFERENCES [dbo].[Case] ([ID])
GO
ALTER TABLE [dbo].[Event] CHECK CONSTRAINT [FK_Event_Case]
GO

ALTER TABLE [dbo].[Event] WITH CHECK ADD CONSTRAINT [FK_Event_Partner] FOREIGN KEY([PartnerUserID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[Event] CHECK CONSTRAINT [FK_Event_Partner]
GO

ALTER TABLE [dbo].[Event] WITH CHECK ADD CONSTRAINT [FK_Event_Client] FOREIGN KEY([ClientID])
REFERENCES [dbo].[Client] ([ID])
GO
ALTER TABLE [dbo].[Event] CHECK CONSTRAINT [FK_Event_Client]
GO

ALTER TABLE [dbo].[Document]  WITH CHECK ADD  CONSTRAINT [FK_Document_DocumentCategory] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[DocumentCategory] ([Id])
GO
ALTER TABLE [dbo].[Document] CHECK CONSTRAINT [FK_Document_DocumentCategory]
GO

ALTER TABLE [dbo].[Document]  WITH CHECK ADD  CONSTRAINT [FK_Document_Case] FOREIGN KEY([CaseId])
REFERENCES [dbo].[Case] ([Id])
GO
ALTER TABLE [dbo].[Document] CHECK CONSTRAINT [FK_Document_Case]
GO

ALTER TABLE [dbo].[Document] WITH CHECK ADD CONSTRAINT [FK_Document_Partner] FOREIGN KEY([PartnerUserID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[Document] CHECK CONSTRAINT [FK_Document_Partner]
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

ALTER TABLE [dbo].[Partner] WITH CHECK ADD CONSTRAINT [FK_Partner_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[Partner] CHECK CONSTRAINT [FK_Partner_User]
GO

ALTER TABLE [dbo].[Partner] WITH CHECK ADD CONSTRAINT [FK_Partner_Paralegal] FOREIGN KEY([ParalegalUserID])
REFERENCES [dbo].[Paralegal] ([UserID])
GO
ALTER TABLE [dbo].[Partner] CHECK CONSTRAINT [FK_Partner_Paralegal]
GO