USE [Law_Firm_Management_System_DB]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 3/9/2023 12:27:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](100) NOT NULL,
	[Password] [nvarchar](100) NULL,
	[FullName] [nvarchar](max) NULL,
	[Email] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

SET IDENTITY_INSERT [dbo].[Users] ON
INSERT [dbo].[Users] ([Id], [Username], [Password], [FullName], [Email]) VALUES (1, N'admin', N'33354741122871651676713774147412831195', N'Admin', N'admin@gmail.com')
INSERT [dbo].[Users] ([Id], [Username], [Password], [FullName], [Email]) VALUES (2, N'User1', N'107144139120952191609010070521251748216197', N'User1', N'User1@gmail.com')
SET IDENTITY_INSERT [dbo].[Users] OFF
GO