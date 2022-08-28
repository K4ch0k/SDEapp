USE [master]
GO
/****** Object:  Database [SDE]    Script Date: 28.08.2022 22:46:19 ******/
CREATE DATABASE [SDE]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'SDE', FILENAME = N'L:\MyProgramms\SQLServer\MSSQL15.MSSQLSERVER\MSSQL\DATA\SDE.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'SDE_log', FILENAME = N'L:\MyProgramms\SQLServer\MSSQL15.MSSQLSERVER\MSSQL\DATA\SDE_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [SDE] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SDE].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SDE] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SDE] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SDE] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SDE] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SDE] SET ARITHABORT OFF 
GO
ALTER DATABASE [SDE] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [SDE] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SDE] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SDE] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SDE] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SDE] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SDE] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SDE] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SDE] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SDE] SET  DISABLE_BROKER 
GO
ALTER DATABASE [SDE] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SDE] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SDE] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SDE] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SDE] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SDE] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [SDE] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SDE] SET RECOVERY FULL 
GO
ALTER DATABASE [SDE] SET  MULTI_USER 
GO
ALTER DATABASE [SDE] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SDE] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SDE] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SDE] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [SDE] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [SDE] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'SDE', N'ON'
GO
ALTER DATABASE [SDE] SET QUERY_STORE = OFF
GO
USE [SDE]
GO
/****** Object:  Table [dbo].[Departments]    Script Date: 28.08.2022 22:46:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Departments](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Description] [varchar](400) NULL,
 CONSTRAINT [PK_Departments] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employees]    Script Date: 28.08.2022 22:46:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employees](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PositionID] [int] NOT NULL,
	[EnterpriseID] [int] NOT NULL,
	[DepartmentID] [int] NOT NULL,
	[PersonnelNumber] [varchar](10) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Surname] [varchar](50) NOT NULL,
	[LastName] [varchar](50) NULL,
	[DateStartWork] [date] NOT NULL,
	[DateEndWork] [date] NULL,
	[Phone] [varchar](30) NULL,
	[Address] [varchar](100) NULL,
 CONSTRAINT [PK_Employees] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Enterprises]    Script Date: 28.08.2022 22:46:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Enterprises](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Address] [varchar](200) NOT NULL,
	[Description] [varchar](400) NULL,
 CONSTRAINT [PK_Enterprises] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Positions]    Script Date: 28.08.2022 22:46:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Positions](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Description] [varchar](400) NULL,
 CONSTRAINT [PK_Positions] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WorkingTime]    Script Date: 28.08.2022 22:46:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WorkingTime](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeID] [int] NOT NULL,
	[TimeStart] [datetime] NOT NULL,
	[TimeEnd] [datetime] NULL,
 CONSTRAINT [PK_WorkingTime] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Departments] ON 

INSERT [dbo].[Departments] ([ID], [Name], [Description]) VALUES (1, N'Не задано', N'')
INSERT [dbo].[Departments] ([ID], [Name], [Description]) VALUES (2, N'ИТ-отдел', N'Программисты, сисадмины, информационная безопасность')
INSERT [dbo].[Departments] ([ID], [Name], [Description]) VALUES (3, N'Бухгалтерия', NULL)
INSERT [dbo].[Departments] ([ID], [Name], [Description]) VALUES (4, N'Отдел кадров', NULL)
INSERT [dbo].[Departments] ([ID], [Name], [Description]) VALUES (5, N'Финансовый отдел', NULL)
INSERT [dbo].[Departments] ([ID], [Name], [Description]) VALUES (6, N'Склад', NULL)
INSERT [dbo].[Departments] ([ID], [Name], [Description]) VALUES (7, N'Охрана труда и технический контроль', NULL)
INSERT [dbo].[Departments] ([ID], [Name], [Description]) VALUES (8, N'Отдел рекламы и PR', NULL)
INSERT [dbo].[Departments] ([ID], [Name], [Description]) VALUES (9, N'Дорожно-ремонтный отдел', NULL)
INSERT [dbo].[Departments] ([ID], [Name], [Description]) VALUES (12, N'Руководство', N'Директор, помощник директора')
SET IDENTITY_INSERT [dbo].[Departments] OFF
GO
SET IDENTITY_INSERT [dbo].[Employees] ON 

INSERT [dbo].[Employees] ([ID], [PositionID], [EnterpriseID], [DepartmentID], [PersonnelNumber], [Name], [Surname], [LastName], [DateStartWork], [DateEndWork], [Phone], [Address]) VALUES (1, 5, 3, 9, N'Fj386K2JzC', N'Ян', N'Гаврилов ', N'Леонидович', CAST(N'2022-08-01' AS Date), NULL, NULL, NULL)
INSERT [dbo].[Employees] ([ID], [PositionID], [EnterpriseID], [DepartmentID], [PersonnelNumber], [Name], [Surname], [LastName], [DateStartWork], [DateEndWork], [Phone], [Address]) VALUES (3, 4, 3, 3, N'cLJyYDYCaK', N'Дмитрий', N'Герасимов', N'Константинович', CAST(N'2022-06-30' AS Date), NULL, N'+7(958)663-52-30', NULL)
INSERT [dbo].[Employees] ([ID], [PositionID], [EnterpriseID], [DepartmentID], [PersonnelNumber], [Name], [Surname], [LastName], [DateStartWork], [DateEndWork], [Phone], [Address]) VALUES (4, 3, 3, 2, N'5AnnpXmmhO', N'Адель', N'Виноградова', N'Макаровна', CAST(N'2022-08-27' AS Date), NULL, N'+7(958)284-03-42', NULL)
INSERT [dbo].[Employees] ([ID], [PositionID], [EnterpriseID], [DepartmentID], [PersonnelNumber], [Name], [Surname], [LastName], [DateStartWork], [DateEndWork], [Phone], [Address]) VALUES (5, 4, 2, 3, N'UKJKPoZPBU', N'Элизабет', N'Соловьёва', N'Руслановна', CAST(N'2022-08-22' AS Date), NULL, N'+7(983)904-48-42', NULL)
INSERT [dbo].[Employees] ([ID], [PositionID], [EnterpriseID], [DepartmentID], [PersonnelNumber], [Name], [Surname], [LastName], [DateStartWork], [DateEndWork], [Phone], [Address]) VALUES (6, 8, 3, 4, N'q5KNl2qItE', N'Велорий', N'Гришин', N'Арсеньевич', CAST(N'2022-08-01' AS Date), NULL, N'+7(983)118-71-20', NULL)
INSERT [dbo].[Employees] ([ID], [PositionID], [EnterpriseID], [DepartmentID], [PersonnelNumber], [Name], [Surname], [LastName], [DateStartWork], [DateEndWork], [Phone], [Address]) VALUES (7, 3, 2, 2, N'Ef04frx2Lb', N'Жасмин', N'Филиппова', N'Викторовна', CAST(N'2022-08-01' AS Date), NULL, N'+7(983)000-08-98', NULL)
INSERT [dbo].[Employees] ([ID], [PositionID], [EnterpriseID], [DepartmentID], [PersonnelNumber], [Name], [Surname], [LastName], [DateStartWork], [DateEndWork], [Phone], [Address]) VALUES (12, 7, 3, 2, N'al5dHOizYY', N'Гарри', N'Борисов', N'Фролович', CAST(N'2022-08-15' AS Date), NULL, N'+7(923)895-7399', N'Кузбасс, село Топки')
INSERT [dbo].[Employees] ([ID], [PositionID], [EnterpriseID], [DepartmentID], [PersonnelNumber], [Name], [Surname], [LastName], [DateStartWork], [DateEndWork], [Phone], [Address]) VALUES (13, 5, 3, 9, N'hkGqxvIQqb', N'Анатолий', N'Дьячков', N'Всеволодович', CAST(N'2022-08-22' AS Date), NULL, N'+7(923)343-6728', NULL)
INSERT [dbo].[Employees] ([ID], [PositionID], [EnterpriseID], [DepartmentID], [PersonnelNumber], [Name], [Surname], [LastName], [DateStartWork], [DateEndWork], [Phone], [Address]) VALUES (15, 6, 2, 12, N'TLIRT2PPEh', N'del', N'del', NULL, CAST(N'2022-07-28' AS Date), NULL, N'12321312312', NULL)
SET IDENTITY_INSERT [dbo].[Employees] OFF
GO
SET IDENTITY_INSERT [dbo].[Enterprises] ON 

INSERT [dbo].[Enterprises] ([ID], [Name], [Address], [Description]) VALUES (1, N'Не задано', N'Не задано', N'')
INSERT [dbo].[Enterprises] ([ID], [Name], [Address], [Description]) VALUES (2, N'Стройдорэкспорт', N'просп. Кузнецкий, 4, Кемерово, Кемеровская обл., 650040', N'Телефон: 8 (384) 236-95-17')
INSERT [dbo].[Enterprises] ([ID], [Name], [Address], [Description]) VALUES (3, N'Стройдорэкспорт, Дск', N'3-ий участок Топкинского Лога, 1, Кемерово, Кемеровская обл., 650021', NULL)
INSERT [dbo].[Enterprises] ([ID], [Name], [Address], [Description]) VALUES (4, N'Дорожно-эксплуатационный комбинат', N'ул. 2-я Камышинская, 2Б, 3 этаж, Кемерово, Кемеровская обл., 650051', N'83842280734')
INSERT [dbo].[Enterprises] ([ID], [Name], [Address], [Description]) VALUES (5, N'Кузбасстрой Сдэ', N'просп. Кузнецкий, 4, Кемерово, Кемеровская обл., 650000', N'Телефон: 8 (384) 257-22-25')
INSERT [dbo].[Enterprises] ([ID], [Name], [Address], [Description]) VALUES (11, N'123qwe', N'del info', NULL)
SET IDENTITY_INSERT [dbo].[Enterprises] OFF
GO
SET IDENTITY_INSERT [dbo].[Positions] ON 

INSERT [dbo].[Positions] ([ID], [Name], [Description]) VALUES (1, N'Не задано', NULL)
INSERT [dbo].[Positions] ([ID], [Name], [Description]) VALUES (2, N'Главный бухгалтер', N'Руководит бухгалтерией')
INSERT [dbo].[Positions] ([ID], [Name], [Description]) VALUES (3, N'Программист', N'Разрабатывает ПО')
INSERT [dbo].[Positions] ([ID], [Name], [Description]) VALUES (4, N'Бухгалтер', NULL)
INSERT [dbo].[Positions] ([ID], [Name], [Description]) VALUES (5, N'Водитель', NULL)
INSERT [dbo].[Positions] ([ID], [Name], [Description]) VALUES (6, N'Инженер', NULL)
INSERT [dbo].[Positions] ([ID], [Name], [Description]) VALUES (7, N'Системный администратор', NULL)
INSERT [dbo].[Positions] ([ID], [Name], [Description]) VALUES (8, N'Менеджер по персоналу', NULL)
INSERT [dbo].[Positions] ([ID], [Name], [Description]) VALUES (9, N'Директор', NULL)
SET IDENTITY_INSERT [dbo].[Positions] OFF
GO
SET IDENTITY_INSERT [dbo].[WorkingTime] ON 

INSERT [dbo].[WorkingTime] ([ID], [EmployeeID], [TimeStart], [TimeEnd]) VALUES (1, 4, CAST(N'2022-08-28T10:17:53.000' AS DateTime), NULL)
INSERT [dbo].[WorkingTime] ([ID], [EmployeeID], [TimeStart], [TimeEnd]) VALUES (2, 1, CAST(N'2022-08-02T08:32:36.000' AS DateTime), CAST(N'2022-08-02T18:11:15.000' AS DateTime))
INSERT [dbo].[WorkingTime] ([ID], [EmployeeID], [TimeStart], [TimeEnd]) VALUES (3, 1, CAST(N'2022-08-03T07:55:30.000' AS DateTime), CAST(N'2022-08-03T18:01:15.000' AS DateTime))
INSERT [dbo].[WorkingTime] ([ID], [EmployeeID], [TimeStart], [TimeEnd]) VALUES (4, 1, CAST(N'2022-08-04T06:49:26.000' AS DateTime), CAST(N'2022-08-04T16:59:28.000' AS DateTime))
INSERT [dbo].[WorkingTime] ([ID], [EmployeeID], [TimeStart], [TimeEnd]) VALUES (5, 1, CAST(N'2022-08-04T17:40:46.000' AS DateTime), CAST(N'2022-08-04T17:45:12.000' AS DateTime))
INSERT [dbo].[WorkingTime] ([ID], [EmployeeID], [TimeStart], [TimeEnd]) VALUES (6, 1, CAST(N'2022-08-05T08:00:36.000' AS DateTime), CAST(N'2022-08-05T18:26:19.000' AS DateTime))
INSERT [dbo].[WorkingTime] ([ID], [EmployeeID], [TimeStart], [TimeEnd]) VALUES (7, 1, CAST(N'2022-08-08T07:52:46.000' AS DateTime), CAST(N'2022-08-08T10:23:29.000' AS DateTime))
INSERT [dbo].[WorkingTime] ([ID], [EmployeeID], [TimeStart], [TimeEnd]) VALUES (8, 1, CAST(N'2022-08-08T10:30:47.000' AS DateTime), CAST(N'2022-08-08T13:46:00.000' AS DateTime))
INSERT [dbo].[WorkingTime] ([ID], [EmployeeID], [TimeStart], [TimeEnd]) VALUES (9, 1, CAST(N'2022-08-08T13:54:35.000' AS DateTime), CAST(N'2022-08-08T17:58:51.000' AS DateTime))
INSERT [dbo].[WorkingTime] ([ID], [EmployeeID], [TimeStart], [TimeEnd]) VALUES (10, 1, CAST(N'2022-08-09T08:03:32.000' AS DateTime), CAST(N'2022-08-09T18:19:14.000' AS DateTime))
INSERT [dbo].[WorkingTime] ([ID], [EmployeeID], [TimeStart], [TimeEnd]) VALUES (11, 1, CAST(N'2022-08-10T07:12:21.000' AS DateTime), CAST(N'2022-08-10T16:55:13.000' AS DateTime))
INSERT [dbo].[WorkingTime] ([ID], [EmployeeID], [TimeStart], [TimeEnd]) VALUES (12, 1, CAST(N'2022-08-11T10:57:40.000' AS DateTime), CAST(N'2022-08-11T13:11:59.000' AS DateTime))
INSERT [dbo].[WorkingTime] ([ID], [EmployeeID], [TimeStart], [TimeEnd]) VALUES (13, 1, CAST(N'2022-08-11T14:34:04.000' AS DateTime), CAST(N'2022-08-11T18:19:09.000' AS DateTime))
INSERT [dbo].[WorkingTime] ([ID], [EmployeeID], [TimeStart], [TimeEnd]) VALUES (14, 1, CAST(N'2022-08-12T07:59:58.000' AS DateTime), CAST(N'2022-08-12T17:53:41.000' AS DateTime))
INSERT [dbo].[WorkingTime] ([ID], [EmployeeID], [TimeStart], [TimeEnd]) VALUES (15, 1, CAST(N'2022-08-15T08:01:15.000' AS DateTime), CAST(N'2022-08-15T18:00:24.000' AS DateTime))
INSERT [dbo].[WorkingTime] ([ID], [EmployeeID], [TimeStart], [TimeEnd]) VALUES (16, 1, CAST(N'2022-08-16T07:02:26.000' AS DateTime), CAST(N'2022-08-16T17:12:41.000' AS DateTime))
INSERT [dbo].[WorkingTime] ([ID], [EmployeeID], [TimeStart], [TimeEnd]) VALUES (17, 1, CAST(N'2022-08-17T07:38:14.000' AS DateTime), CAST(N'2022-08-17T18:10:04.000' AS DateTime))
INSERT [dbo].[WorkingTime] ([ID], [EmployeeID], [TimeStart], [TimeEnd]) VALUES (18, 1, CAST(N'2022-08-18T08:05:14.000' AS DateTime), CAST(N'2022-08-18T17:41:54.000' AS DateTime))
INSERT [dbo].[WorkingTime] ([ID], [EmployeeID], [TimeStart], [TimeEnd]) VALUES (19, 1, CAST(N'2022-08-19T10:06:07.000' AS DateTime), CAST(N'2022-08-19T17:41:19.000' AS DateTime))
INSERT [dbo].[WorkingTime] ([ID], [EmployeeID], [TimeStart], [TimeEnd]) VALUES (20, 1, CAST(N'2022-08-22T07:15:34.000' AS DateTime), CAST(N'2022-08-22T17:27:46.000' AS DateTime))
INSERT [dbo].[WorkingTime] ([ID], [EmployeeID], [TimeStart], [TimeEnd]) VALUES (21, 1, CAST(N'2022-08-23T07:09:22.000' AS DateTime), CAST(N'2022-08-23T17:42:14.000' AS DateTime))
INSERT [dbo].[WorkingTime] ([ID], [EmployeeID], [TimeStart], [TimeEnd]) VALUES (22, 1, CAST(N'2022-08-24T08:12:16.000' AS DateTime), CAST(N'2022-08-24T17:53:07.000' AS DateTime))
INSERT [dbo].[WorkingTime] ([ID], [EmployeeID], [TimeStart], [TimeEnd]) VALUES (23, 1, CAST(N'2022-08-25T07:44:53.000' AS DateTime), CAST(N'2022-08-25T18:26:41.000' AS DateTime))
INSERT [dbo].[WorkingTime] ([ID], [EmployeeID], [TimeStart], [TimeEnd]) VALUES (24, 15, CAST(N'2022-08-28T11:20:24.000' AS DateTime), CAST(N'2022-08-28T16:14:08.000' AS DateTime))
INSERT [dbo].[WorkingTime] ([ID], [EmployeeID], [TimeStart], [TimeEnd]) VALUES (25, 1, CAST(N'2022-08-26T08:15:24.000' AS DateTime), CAST(N'2022-08-26T18:37:08.000' AS DateTime))
INSERT [dbo].[WorkingTime] ([ID], [EmployeeID], [TimeStart], [TimeEnd]) VALUES (26, 3, CAST(N'2022-07-01T07:45:42.000' AS DateTime), CAST(N'2022-07-01T18:11:18.000' AS DateTime))
INSERT [dbo].[WorkingTime] ([ID], [EmployeeID], [TimeStart], [TimeEnd]) VALUES (27, 3, CAST(N'2022-07-04T07:51:44.000' AS DateTime), CAST(N'2022-07-04T18:12:17.000' AS DateTime))
INSERT [dbo].[WorkingTime] ([ID], [EmployeeID], [TimeStart], [TimeEnd]) VALUES (28, 3, CAST(N'2022-07-05T07:53:01.000' AS DateTime), CAST(N'2022-07-05T18:13:28.000' AS DateTime))
INSERT [dbo].[WorkingTime] ([ID], [EmployeeID], [TimeStart], [TimeEnd]) VALUES (29, 3, CAST(N'2022-07-06T07:39:11.000' AS DateTime), CAST(N'2022-07-06T17:40:44.000' AS DateTime))
INSERT [dbo].[WorkingTime] ([ID], [EmployeeID], [TimeStart], [TimeEnd]) VALUES (30, 3, CAST(N'2022-07-07T07:55:54.000' AS DateTime), CAST(N'2022-07-07T18:13:27.000' AS DateTime))
INSERT [dbo].[WorkingTime] ([ID], [EmployeeID], [TimeStart], [TimeEnd]) VALUES (31, 3, CAST(N'2022-07-08T07:49:42.000' AS DateTime), CAST(N'2022-07-08T18:10:15.000' AS DateTime))
INSERT [dbo].[WorkingTime] ([ID], [EmployeeID], [TimeStart], [TimeEnd]) VALUES (33, 3, CAST(N'2022-07-11T07:58:36.000' AS DateTime), CAST(N'2022-07-11T17:34:09.000' AS DateTime))
INSERT [dbo].[WorkingTime] ([ID], [EmployeeID], [TimeStart], [TimeEnd]) VALUES (34, 3, CAST(N'2022-07-12T07:48:14.000' AS DateTime), CAST(N'2022-08-28T18:00:27.000' AS DateTime))
INSERT [dbo].[WorkingTime] ([ID], [EmployeeID], [TimeStart], [TimeEnd]) VALUES (35, 3, CAST(N'2022-07-13T07:49:21.000' AS DateTime), CAST(N'2022-07-13T18:11:34.000' AS DateTime))
INSERT [dbo].[WorkingTime] ([ID], [EmployeeID], [TimeStart], [TimeEnd]) VALUES (36, 3, CAST(N'2022-07-14T07:49:56.000' AS DateTime), CAST(N'2022-07-14T18:12:09.000' AS DateTime))
INSERT [dbo].[WorkingTime] ([ID], [EmployeeID], [TimeStart], [TimeEnd]) VALUES (37, 3, CAST(N'2022-07-15T07:50:32.000' AS DateTime), CAST(N'2022-07-15T18:05:45.000' AS DateTime))
INSERT [dbo].[WorkingTime] ([ID], [EmployeeID], [TimeStart], [TimeEnd]) VALUES (38, 3, CAST(N'2022-07-18T07:50:59.000' AS DateTime), CAST(N'2022-07-18T18:13:12.000' AS DateTime))
INSERT [dbo].[WorkingTime] ([ID], [EmployeeID], [TimeStart], [TimeEnd]) VALUES (39, 3, CAST(N'2022-07-19T07:51:36.000' AS DateTime), CAST(N'2022-07-19T18:16:49.000' AS DateTime))
INSERT [dbo].[WorkingTime] ([ID], [EmployeeID], [TimeStart], [TimeEnd]) VALUES (40, 3, CAST(N'2022-07-20T07:52:12.000' AS DateTime), CAST(N'2022-07-20T18:15:25.000' AS DateTime))
INSERT [dbo].[WorkingTime] ([ID], [EmployeeID], [TimeStart], [TimeEnd]) VALUES (41, 3, CAST(N'2022-07-21T07:56:53.000' AS DateTime), CAST(N'2022-07-21T18:15:06.000' AS DateTime))
INSERT [dbo].[WorkingTime] ([ID], [EmployeeID], [TimeStart], [TimeEnd]) VALUES (42, 3, CAST(N'2022-07-22T07:55:23.000' AS DateTime), CAST(N'2022-07-22T17:56:36.000' AS DateTime))
INSERT [dbo].[WorkingTime] ([ID], [EmployeeID], [TimeStart], [TimeEnd]) VALUES (43, 3, CAST(N'2022-07-25T07:52:58.000' AS DateTime), CAST(N'2022-07-25T18:16:11.000' AS DateTime))
INSERT [dbo].[WorkingTime] ([ID], [EmployeeID], [TimeStart], [TimeEnd]) VALUES (44, 3, CAST(N'2022-07-26T07:56:32.000' AS DateTime), CAST(N'2022-07-26T18:18:45.000' AS DateTime))
INSERT [dbo].[WorkingTime] ([ID], [EmployeeID], [TimeStart], [TimeEnd]) VALUES (45, 3, CAST(N'2022-07-27T07:56:56.000' AS DateTime), CAST(N'2022-07-27T17:05:09.000' AS DateTime))
INSERT [dbo].[WorkingTime] ([ID], [EmployeeID], [TimeStart], [TimeEnd]) VALUES (47, 3, CAST(N'2022-07-28T07:56:15.000' AS DateTime), CAST(N'2022-07-28T18:10:28.000' AS DateTime))
INSERT [dbo].[WorkingTime] ([ID], [EmployeeID], [TimeStart], [TimeEnd]) VALUES (48, 3, CAST(N'2022-07-29T07:48:22.000' AS DateTime), CAST(N'2022-07-29T18:14:35.000' AS DateTime))
INSERT [dbo].[WorkingTime] ([ID], [EmployeeID], [TimeStart], [TimeEnd]) VALUES (50, 3, CAST(N'2022-08-01T07:55:48.000' AS DateTime), CAST(N'2022-08-01T18:22:01.000' AS DateTime))
INSERT [dbo].[WorkingTime] ([ID], [EmployeeID], [TimeStart], [TimeEnd]) VALUES (51, 3, CAST(N'2022-08-02T07:49:13.000' AS DateTime), CAST(N'2022-08-02T18:05:26.000' AS DateTime))
INSERT [dbo].[WorkingTime] ([ID], [EmployeeID], [TimeStart], [TimeEnd]) VALUES (52, 3, CAST(N'2022-08-03T07:55:09.000' AS DateTime), CAST(N'2022-08-03T18:03:22.000' AS DateTime))
INSERT [dbo].[WorkingTime] ([ID], [EmployeeID], [TimeStart], [TimeEnd]) VALUES (53, 3, CAST(N'2022-08-04T07:59:11.000' AS DateTime), CAST(N'2022-08-04T18:11:24.000' AS DateTime))
INSERT [dbo].[WorkingTime] ([ID], [EmployeeID], [TimeStart], [TimeEnd]) VALUES (54, 3, CAST(N'2022-08-05T07:56:39.000' AS DateTime), CAST(N'2022-08-05T17:58:52.000' AS DateTime))
INSERT [dbo].[WorkingTime] ([ID], [EmployeeID], [TimeStart], [TimeEnd]) VALUES (55, 3, CAST(N'2022-08-08T08:05:35.000' AS DateTime), CAST(N'2022-08-08T18:07:48.000' AS DateTime))
INSERT [dbo].[WorkingTime] ([ID], [EmployeeID], [TimeStart], [TimeEnd]) VALUES (56, 3, CAST(N'2022-08-09T07:57:53.000' AS DateTime), CAST(N'2022-08-09T18:09:06.000' AS DateTime))
INSERT [dbo].[WorkingTime] ([ID], [EmployeeID], [TimeStart], [TimeEnd]) VALUES (57, 3, CAST(N'2022-08-10T07:41:29.000' AS DateTime), CAST(N'2022-08-10T17:49:42.000' AS DateTime))
INSERT [dbo].[WorkingTime] ([ID], [EmployeeID], [TimeStart], [TimeEnd]) VALUES (58, 3, CAST(N'2022-08-11T07:59:02.000' AS DateTime), CAST(N'2022-08-11T18:02:15.000' AS DateTime))
INSERT [dbo].[WorkingTime] ([ID], [EmployeeID], [TimeStart], [TimeEnd]) VALUES (59, 3, CAST(N'2022-08-12T07:52:31.000' AS DateTime), CAST(N'2022-08-12T18:06:44.000' AS DateTime))
INSERT [dbo].[WorkingTime] ([ID], [EmployeeID], [TimeStart], [TimeEnd]) VALUES (60, 3, CAST(N'2022-08-15T07:53:05.000' AS DateTime), CAST(N'2022-08-15T17:53:18.000' AS DateTime))
INSERT [dbo].[WorkingTime] ([ID], [EmployeeID], [TimeStart], [TimeEnd]) VALUES (61, 3, CAST(N'2022-08-16T07:56:00.000' AS DateTime), CAST(N'2022-08-16T18:03:13.000' AS DateTime))
INSERT [dbo].[WorkingTime] ([ID], [EmployeeID], [TimeStart], [TimeEnd]) VALUES (62, 3, CAST(N'2022-08-17T07:46:35.000' AS DateTime), CAST(N'2022-08-17T18:11:48.000' AS DateTime))
INSERT [dbo].[WorkingTime] ([ID], [EmployeeID], [TimeStart], [TimeEnd]) VALUES (63, 3, CAST(N'2022-08-18T07:48:35.000' AS DateTime), CAST(N'2022-08-18T18:10:48.000' AS DateTime))
INSERT [dbo].[WorkingTime] ([ID], [EmployeeID], [TimeStart], [TimeEnd]) VALUES (64, 3, CAST(N'2022-08-19T07:52:00.000' AS DateTime), CAST(N'2022-08-19T17:03:13.000' AS DateTime))
INSERT [dbo].[WorkingTime] ([ID], [EmployeeID], [TimeStart], [TimeEnd]) VALUES (65, 3, CAST(N'2022-08-22T07:54:43.000' AS DateTime), CAST(N'2022-08-22T18:05:56.000' AS DateTime))
INSERT [dbo].[WorkingTime] ([ID], [EmployeeID], [TimeStart], [TimeEnd]) VALUES (66, 3, CAST(N'2022-08-23T07:58:10.000' AS DateTime), CAST(N'2022-08-23T18:02:23.000' AS DateTime))
INSERT [dbo].[WorkingTime] ([ID], [EmployeeID], [TimeStart], [TimeEnd]) VALUES (67, 3, CAST(N'2022-08-24T07:53:28.000' AS DateTime), CAST(N'2022-08-24T18:15:41.000' AS DateTime))
INSERT [dbo].[WorkingTime] ([ID], [EmployeeID], [TimeStart], [TimeEnd]) VALUES (68, 3, CAST(N'2022-08-25T07:55:42.000' AS DateTime), CAST(N'2022-08-25T18:07:55.000' AS DateTime))
INSERT [dbo].[WorkingTime] ([ID], [EmployeeID], [TimeStart], [TimeEnd]) VALUES (69, 3, CAST(N'2022-08-26T07:56:10.000' AS DateTime), CAST(N'2022-08-26T18:18:23.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[WorkingTime] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Departments]    Script Date: 28.08.2022 22:46:19 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Departments] ON [dbo].[Departments]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PersNum]    Script Date: 28.08.2022 22:46:19 ******/
CREATE UNIQUE NONCLUSTERED INDEX [PersNum] ON [dbo].[Employees]
(
	[PersonnelNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PhoneNum]    Script Date: 28.08.2022 22:46:19 ******/
CREATE UNIQUE NONCLUSTERED INDEX [PhoneNum] ON [dbo].[Employees]
(
	[Phone] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Enterprises]    Script Date: 28.08.2022 22:46:19 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Enterprises] ON [dbo].[Enterprises]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Positions]    Script Date: 28.08.2022 22:46:19 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Positions] ON [dbo].[Positions]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Employees]  WITH CHECK ADD  CONSTRAINT [FK_Employees_Departments] FOREIGN KEY([DepartmentID])
REFERENCES [dbo].[Departments] ([ID])
GO
ALTER TABLE [dbo].[Employees] CHECK CONSTRAINT [FK_Employees_Departments]
GO
ALTER TABLE [dbo].[Employees]  WITH CHECK ADD  CONSTRAINT [FK_Employees_Enterprises] FOREIGN KEY([EnterpriseID])
REFERENCES [dbo].[Enterprises] ([ID])
GO
ALTER TABLE [dbo].[Employees] CHECK CONSTRAINT [FK_Employees_Enterprises]
GO
ALTER TABLE [dbo].[Employees]  WITH CHECK ADD  CONSTRAINT [FK_Employees_Positions] FOREIGN KEY([PositionID])
REFERENCES [dbo].[Positions] ([ID])
GO
ALTER TABLE [dbo].[Employees] CHECK CONSTRAINT [FK_Employees_Positions]
GO
ALTER TABLE [dbo].[WorkingTime]  WITH CHECK ADD  CONSTRAINT [FK_WorkingTime_Employees] FOREIGN KEY([EmployeeID])
REFERENCES [dbo].[Employees] ([ID])
GO
ALTER TABLE [dbo].[WorkingTime] CHECK CONSTRAINT [FK_WorkingTime_Employees]
GO
USE [master]
GO
ALTER DATABASE [SDE] SET  READ_WRITE 
GO
