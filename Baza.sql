USE [master]
GO
/****** Object:  Database [NTR-Own]    Script Date: 26.01.2017 09:03:19 ******/
CREATE DATABASE [NTR-Own]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'NTR-Own', FILENAME = N'C:\Program Files (x86)\SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\NTR-Own.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'NTR-Own_log', FILENAME = N'C:\Program Files (x86)\SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\NTR-Own_log.ldf' , SIZE = 1536KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [NTR-Own] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [NTR-Own].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [NTR-Own] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [NTR-Own] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [NTR-Own] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [NTR-Own] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [NTR-Own] SET ARITHABORT OFF 
GO
ALTER DATABASE [NTR-Own] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [NTR-Own] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [NTR-Own] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [NTR-Own] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [NTR-Own] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [NTR-Own] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [NTR-Own] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [NTR-Own] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [NTR-Own] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [NTR-Own] SET  DISABLE_BROKER 
GO
ALTER DATABASE [NTR-Own] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [NTR-Own] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [NTR-Own] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [NTR-Own] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [NTR-Own] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [NTR-Own] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [NTR-Own] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [NTR-Own] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [NTR-Own] SET  MULTI_USER 
GO
ALTER DATABASE [NTR-Own] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [NTR-Own] SET DB_CHAINING OFF 
GO
ALTER DATABASE [NTR-Own] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [NTR-Own] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [NTR-Own] SET DELAYED_DURABILITY = DISABLED 
GO
USE [NTR-Own]
GO
/****** Object:  User [MVC-User]    Script Date: 26.01.2017 09:03:19 ******/
CREATE USER [MVC-User] FOR LOGIN [AppUser] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [AppUser]    Script Date: 26.01.2017 09:03:19 ******/
CREATE USER [AppUser] WITHOUT LOGIN WITH DEFAULT_SCHEMA=[[db_datareader]]; [db_datawriter]]]
GO
/****** Object:  Table [dbo].[Głos]    Script Date: 26.01.2017 09:03:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Głos](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Poseł] [int] NOT NULL,
	[Ustawa] [int] NOT NULL,
	[Głosował] [bit] NULL,
	[Stamp] [timestamp] NOT NULL,
 CONSTRAINT [PK_Głos] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Klub]    Script Date: 26.01.2017 09:03:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Klub](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Nazwa] [varchar](256) NOT NULL,
	[Skrót] [varchar](50) NOT NULL,
	[Stamp] [timestamp] NOT NULL,
 CONSTRAINT [PK_Klub] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Poseł]    Script Date: 26.01.2017 09:03:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Poseł](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Imie] [varchar](50) NOT NULL,
	[Nazwisko] [varchar](50) NOT NULL,
	[Foto] [image] NULL,
	[Klub parlamentarny] [int] NULL,
	[Stamp] [timestamp] NOT NULL,
 CONSTRAINT [PK_Poseł] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Ustawa]    Script Date: 26.01.2017 09:03:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Ustawa](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Nazwa] [varchar](256) NOT NULL,
	[Data] [date] NOT NULL,
	[ZgłoszonaPrzez] [int] NOT NULL,
	[Stamp] [timestamp] NOT NULL,
 CONSTRAINT [PK_Ustawa] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Głos] ON 

INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (1, 1, 1, 1)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (2, 2, 1, 1)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (3, 3, 1, 1)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (4, 4, 1, 1)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (5, 5, 1, 1)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (6, 6, 1, 0)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (7, 7, 1, 0)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (8, 8, 1, 0)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (9, 9, 1, 0)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (10, 10, 1, 0)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (11, 11, 1, 0)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (12, 12, 1, 0)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (13, 13, 1, 1)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (14, 14, 1, 0)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (15, 16, 1, 0)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (16, 17, 1, 0)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (17, 18, 1, 0)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (18, 19, 1, 0)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (19, 20, 1, 0)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (20, 21, 1, 0)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (21, 22, 1, 0)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (22, 23, 1, 0)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (23, 24, 1, 0)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (24, 25, 1, NULL)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (25, 26, 1, NULL)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (26, 27, 1, NULL)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (27, 28, 1, NULL)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (28, 1, 3, 1)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (29, 2, 3, 1)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (30, 3, 3, 1)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (31, 4, 3, 1)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (32, 5, 3, 1)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (33, 6, 3, 0)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (34, 7, 3, 0)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (35, 8, 3, 0)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (36, 9, 3, 0)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (37, 10, 3, 0)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (38, 11, 3, 0)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (39, 12, 3, 0)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (40, 13, 3, 0)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (41, 14, 3, 0)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (42, 16, 3, 0)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (43, 17, 3, 0)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (44, 18, 3, 0)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (45, 19, 3, 0)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (46, 20, 3, 0)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (47, 21, 3, 0)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (48, 22, 3, 0)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (49, 23, 3, 0)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (50, 24, 3, 0)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (51, 25, 3, NULL)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (52, 26, 3, NULL)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (53, 27, 3, NULL)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (54, 28, 3, NULL)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (55, 1, 5, 0)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (56, 2, 5, 0)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (57, 3, 5, 0)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (58, 4, 5, 0)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (59, 5, 5, 0)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (60, 6, 5, 0)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (61, 7, 5, 1)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (62, 8, 5, 0)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (63, 9, 5, 1)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (64, 10, 5, 1)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (65, 11, 5, 0)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (66, 12, 5, 0)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (67, 13, 5, 1)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (68, 14, 5, 1)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (69, 16, 5, 1)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (70, 17, 5, 0)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (71, 18, 5, 0)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (72, 19, 5, 0)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (73, 20, 5, 1)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (74, 21, 5, 1)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (75, 22, 5, 1)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (76, 23, 5, 1)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (77, 24, 5, 1)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (78, 25, 5, NULL)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (79, 26, 5, NULL)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (80, 27, 5, NULL)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (81, 28, 5, NULL)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (82, 1, 6, 1)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (83, 2, 6, 1)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (84, 3, 6, 1)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (85, 4, 6, 1)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (86, 5, 6, 1)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (87, 6, 6, 1)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (88, 7, 6, 1)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (89, 8, 6, 1)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (90, 9, 6, 1)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (91, 10, 6, 1)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (92, 11, 6, NULL)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (93, 12, 6, NULL)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (94, 13, 6, NULL)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (95, 14, 6, NULL)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (96, 16, 6, NULL)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (97, 17, 6, 1)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (98, 18, 6, 0)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (99, 19, 6, 0)
GO
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (100, 20, 6, NULL)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (101, 21, 6, NULL)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (102, 22, 6, NULL)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (103, 23, 6, NULL)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (104, 24, 6, NULL)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (105, 25, 6, 0)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (106, 26, 6, 0)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (107, 27, 6, 0)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (108, 28, 6, 0)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (109, 1, 8, 0)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (110, 2, 8, 0)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (111, 3, 8, 0)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (112, 4, 8, 0)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (113, 5, 8, 0)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (114, 6, 8, 1)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (115, 7, 8, 1)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (116, 8, 8, 1)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (117, 9, 8, 1)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (118, 10, 8, 1)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (119, 11, 8, NULL)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (120, 12, 8, NULL)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (121, 13, 8, NULL)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (122, 14, 8, NULL)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (123, 16, 8, NULL)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (124, 17, 8, 1)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (125, 18, 8, 1)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (126, 19, 8, 1)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (127, 20, 8, NULL)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (128, 21, 8, NULL)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (129, 22, 8, NULL)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (130, 23, 8, NULL)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (131, 24, 8, NULL)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (132, 25, 8, 1)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (133, 26, 8, 1)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (134, 27, 8, 1)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (135, 28, 8, 1)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (136, 1, 9, 0)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (137, 2, 9, 0)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (138, 3, 9, 0)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (139, 4, 9, 0)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (140, 5, 9, 1)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (141, 6, 9, 1)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (142, 7, 9, 1)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (143, 8, 9, 1)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (144, 9, 9, 1)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (145, 10, 9, 1)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (146, 11, 9, 1)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (147, 12, 9, 1)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (148, 13, 9, 1)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (149, 14, 9, 1)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (150, 16, 9, 1)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (151, 17, 9, 0)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (152, 18, 9, 0)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (153, 19, 9, 0)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (154, 20, 9, 1)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (155, 21, 9, 0)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (156, 22, 9, 1)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (157, 23, 9, 0)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (158, 24, 9, 1)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (159, 25, 9, NULL)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (160, 26, 9, NULL)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (161, 27, 9, NULL)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (162, 28, 9, NULL)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (163, 1, 10, 1)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (164, 2, 10, 1)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (165, 3, 10, 1)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (166, 4, 10, 1)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (167, 5, 10, 1)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (168, 6, 10, 0)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (169, 7, 10, 1)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (170, 8, 10, 0)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (171, 9, 10, 0)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (172, 10, 10, 0)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (173, 11, 10, 1)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (174, 12, 10, 1)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (175, 13, 10, 0)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (176, 14, 10, 0)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (177, 16, 10, 0)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (178, 17, 10, 1)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (179, 18, 10, 1)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (180, 19, 10, 1)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (181, 20, 10, 0)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (182, 21, 10, 0)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (183, 22, 10, 0)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (184, 23, 10, 0)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (185, 24, 10, 0)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (186, 25, 10, NULL)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (187, 26, 10, NULL)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (188, 27, 10, NULL)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (189, 28, 10, NULL)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (190, 1, 11, 0)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (191, 2, 11, 0)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (192, 3, 11, 0)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (193, 4, 11, 0)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (194, 5, 11, 0)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (195, 6, 11, 0)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (196, 7, 11, 0)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (197, 8, 11, 0)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (198, 9, 11, 0)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (199, 10, 11, 0)
GO
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (200, 11, 11, 0)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (201, 12, 11, 0)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (202, 13, 11, 0)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (203, 14, 11, 1)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (204, 16, 11, 0)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (205, 17, 11, 0)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (206, 18, 11, 0)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (207, 19, 11, 0)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (208, 20, 11, 1)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (209, 21, 11, 1)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (210, 22, 11, 1)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (211, 23, 11, 1)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (212, 24, 11, 1)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (213, 25, 11, 1)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (214, 26, 11, 1)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (215, 27, 11, 1)
INSERT [dbo].[Głos] ([ID], [Poseł], [Ustawa], [Głosował]) VALUES (216, 28, 11, 1)
SET IDENTITY_INSERT [dbo].[Głos] OFF
SET IDENTITY_INSERT [dbo].[Klub] ON 

INSERT [dbo].[Klub] ([ID], [Nazwa], [Skrót]) VALUES (1, N'Prawo i Sprawiedliwość                                                                                                                                                                                                                                          ', N'PiS       ')
INSERT [dbo].[Klub] ([ID], [Nazwa], [Skrót]) VALUES (2, N'Platforma Obywatelska                                                                                                                                                                                                                                           ', N'PO        ')
INSERT [dbo].[Klub] ([ID], [Nazwa], [Skrót]) VALUES (3, N'Kukiz''15                                                                                                                                                                                                                                                        ', N'Kukiz''15  ')
INSERT [dbo].[Klub] ([ID], [Nazwa], [Skrót]) VALUES (4, N'Polskie Stronnictwo Ludowe                                                                                                                                                                                                                                      ', N'PSL       ')
INSERT [dbo].[Klub] ([ID], [Nazwa], [Skrót]) VALUES (5, N'Nowoczesna                                                                                                                                                                                                                                                      ', N'.N        ')
INSERT [dbo].[Klub] ([ID], [Nazwa], [Skrót]) VALUES (6, N'Sojusz Lewicy Demokratycznej                                                                                                                                                                                                                                    ', N'SLD       ')
SET IDENTITY_INSERT [dbo].[Klub] OFF
SET IDENTITY_INSERT [dbo].[Poseł] ON 

INSERT [dbo].[Poseł] ([ID], [Imie], [Nazwisko], [Foto], [Klub parlamentarny]) VALUES (1, N'Kaczyński                       ', N'Jarosław                        ', NULL, 1)
INSERT [dbo].[Poseł] ([ID], [Imie], [Nazwisko], [Foto], [Klub parlamentarny]) VALUES (2, N'Joachim                         ', N'Brudziński                      ', NULL, 1)
INSERT [dbo].[Poseł] ([ID], [Imie], [Nazwisko], [Foto], [Klub parlamentarny]) VALUES (3, N'Jarosław                        ', N'Gowin                           ', NULL, 1)
INSERT [dbo].[Poseł] ([ID], [Imie], [Nazwisko], [Foto], [Klub parlamentarny]) VALUES (4, N'Antoni                          ', N'Macierewicz                     ', NULL, 1)
INSERT [dbo].[Poseł] ([ID], [Imie], [Nazwisko], [Foto], [Klub parlamentarny]) VALUES (5, N'Krystyna                        ', N'Pawłowicz                       ', NULL, 1)
INSERT [dbo].[Poseł] ([ID], [Imie], [Nazwisko], [Foto], [Klub parlamentarny]) VALUES (6, N'Grzegorz                        ', N'Schetyna                        ', NULL, 2)
INSERT [dbo].[Poseł] ([ID], [Imie], [Nazwisko], [Foto], [Klub parlamentarny]) VALUES (7, N'Ewa                             ', N'Kopacz                          ', NULL, 2)
INSERT [dbo].[Poseł] ([ID], [Imie], [Nazwisko], [Foto], [Klub parlamentarny]) VALUES (8, N'Rafał                           ', N'Trzaskowski                     ', NULL, 2)
INSERT [dbo].[Poseł] ([ID], [Imie], [Nazwisko], [Foto], [Klub parlamentarny]) VALUES (9, N'Borys                           ', N'Budka                           ', NULL, 2)
INSERT [dbo].[Poseł] ([ID], [Imie], [Nazwisko], [Foto], [Klub parlamentarny]) VALUES (10, N'Elżbieta                        ', N'Kidawa - Błońska                ', NULL, 2)
INSERT [dbo].[Poseł] ([ID], [Imie], [Nazwisko], [Foto], [Klub parlamentarny]) VALUES (11, N'Paweł                           ', N'Kukiz                           ', NULL, 3)
INSERT [dbo].[Poseł] ([ID], [Imie], [Nazwisko], [Foto], [Klub parlamentarny]) VALUES (12, N'Kornel                          ', N'Morawiecki                      ', NULL, 3)
INSERT [dbo].[Poseł] ([ID], [Imie], [Nazwisko], [Foto], [Klub parlamentarny]) VALUES (13, N'Anna                            ', N'Siarkowska                      ', NULL, 3)
INSERT [dbo].[Poseł] ([ID], [Imie], [Nazwisko], [Foto], [Klub parlamentarny]) VALUES (14, N'Piotr                           ', N'Marzec                          ', NULL, 3)
INSERT [dbo].[Poseł] ([ID], [Imie], [Nazwisko], [Foto], [Klub parlamentarny]) VALUES (16, N'Jacek                           ', N'Wilk                            ', NULL, 3)
INSERT [dbo].[Poseł] ([ID], [Imie], [Nazwisko], [Foto], [Klub parlamentarny]) VALUES (17, N'Janusz                          ', N'Piechociński                    ', NULL, 4)
INSERT [dbo].[Poseł] ([ID], [Imie], [Nazwisko], [Foto], [Klub parlamentarny]) VALUES (18, N'Eugeniusz                       ', N'Kłopotek                        ', NULL, 4)
INSERT [dbo].[Poseł] ([ID], [Imie], [Nazwisko], [Foto], [Klub parlamentarny]) VALUES (19, N'Waldemar                        ', N'Pawlak                          ', NULL, 4)
INSERT [dbo].[Poseł] ([ID], [Imie], [Nazwisko], [Foto], [Klub parlamentarny]) VALUES (20, N'Ryszard                         ', N'Petru                           ', NULL, 5)
INSERT [dbo].[Poseł] ([ID], [Imie], [Nazwisko], [Foto], [Klub parlamentarny]) VALUES (21, N'Paweł                           ', N'Rabiej                          ', NULL, 5)
INSERT [dbo].[Poseł] ([ID], [Imie], [Nazwisko], [Foto], [Klub parlamentarny]) VALUES (22, N'Kamila                          ', N'Gasiuk - Pihowicz               ', NULL, 5)
INSERT [dbo].[Poseł] ([ID], [Imie], [Nazwisko], [Foto], [Klub parlamentarny]) VALUES (23, N'Joanna                          ', N'Schmidt                         ', NULL, 5)
INSERT [dbo].[Poseł] ([ID], [Imie], [Nazwisko], [Foto], [Klub parlamentarny]) VALUES (24, N'Joanna                          ', N'Scheuring - Wielgus             ', NULL, 5)
INSERT [dbo].[Poseł] ([ID], [Imie], [Nazwisko], [Foto], [Klub parlamentarny]) VALUES (25, N'Leszek                          ', N'Miller                          ', NULL, 6)
INSERT [dbo].[Poseł] ([ID], [Imie], [Nazwisko], [Foto], [Klub parlamentarny]) VALUES (26, N'Joanna                          ', N'Senyszyn                        ', NULL, 6)
INSERT [dbo].[Poseł] ([ID], [Imie], [Nazwisko], [Foto], [Klub parlamentarny]) VALUES (27, N'Włodzimierz                     ', N'Czarzasty                       ', NULL, 6)
INSERT [dbo].[Poseł] ([ID], [Imie], [Nazwisko], [Foto], [Klub parlamentarny]) VALUES (28, N'Piotr                           ', N'Gadzinowski                     ', NULL, 6)
SET IDENTITY_INSERT [dbo].[Poseł] OFF
SET IDENTITY_INSERT [dbo].[Ustawa] ON 

INSERT [dbo].[Ustawa] ([ID], [Nazwa], [Data], [ZgłoszonaPrzez]) VALUES (1, N'Ustawa Budżetowa 2017                                                                                                                                                                                                                                           ', CAST(N'2016-12-21' AS Date), 1)
INSERT [dbo].[Ustawa] ([ID], [Nazwa], [Data], [ZgłoszonaPrzez]) VALUES (3, N'Ustawa o reformie edukacji                                                                                                                                                                                                                                      ', CAST(N'2017-01-11' AS Date), 1)
INSERT [dbo].[Ustawa] ([ID], [Nazwa], [Data], [ZgłoszonaPrzez]) VALUES (5, N'Ustawa o związkach zawodowych                                                                                                                                                                                                                                   ', CAST(N'2016-04-03' AS Date), 5)
INSERT [dbo].[Ustawa] ([ID], [Nazwa], [Data], [ZgłoszonaPrzez]) VALUES (6, N'Ustawa o deregulacji                                                                                                                                                                                                                                            ', CAST(N'2013-09-23' AS Date), 2)
INSERT [dbo].[Ustawa] ([ID], [Nazwa], [Data], [ZgłoszonaPrzez]) VALUES (8, N'Ustawa o OFE                                                                                                                                                                                                                                                    ', CAST(N'2013-05-11' AS Date), 2)
INSERT [dbo].[Ustawa] ([ID], [Nazwa], [Data], [ZgłoszonaPrzez]) VALUES (9, N'Ustawa o zmianie ordynacji wyborczej                                                                                                                                                                                                                            ', CAST(N'2016-07-07' AS Date), 3)
INSERT [dbo].[Ustawa] ([ID], [Nazwa], [Data], [ZgłoszonaPrzez]) VALUES (10, N'Ustawa o rentach rolniczych                                                                                                                                                                                                                                     ', CAST(N'2017-01-11' AS Date), 4)
INSERT [dbo].[Ustawa] ([ID], [Nazwa], [Data], [ZgłoszonaPrzez]) VALUES (11, N'Ustawa o związkach partnerskich                                                                                                                                                                                                                                 ', CAST(N'2008-06-06' AS Date), 6)
SET IDENTITY_INSERT [dbo].[Ustawa] OFF
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_Klub]    Script Date: 26.01.2017 09:03:19 ******/
ALTER TABLE [dbo].[Klub] ADD  CONSTRAINT [IX_Klub] UNIQUE NONCLUSTERED 
(
	[Nazwa] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_Klub_1]    Script Date: 26.01.2017 09:03:19 ******/
ALTER TABLE [dbo].[Klub] ADD  CONSTRAINT [IX_Klub_1] UNIQUE NONCLUSTERED 
(
	[Skrót] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_Ustawa]    Script Date: 26.01.2017 09:03:19 ******/
ALTER TABLE [dbo].[Ustawa] ADD  CONSTRAINT [IX_Ustawa] UNIQUE NONCLUSTERED 
(
	[Nazwa] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Głos]  WITH CHECK ADD  CONSTRAINT [FK_Głos_Poseł] FOREIGN KEY([Poseł])
REFERENCES [dbo].[Poseł] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Głos] CHECK CONSTRAINT [FK_Głos_Poseł]
GO
ALTER TABLE [dbo].[Głos]  WITH CHECK ADD  CONSTRAINT [FK_Głos_Ustawa] FOREIGN KEY([Ustawa])
REFERENCES [dbo].[Ustawa] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Głos] CHECK CONSTRAINT [FK_Głos_Ustawa]
GO
ALTER TABLE [dbo].[Poseł]  WITH CHECK ADD  CONSTRAINT [FK_Poseł_Klub] FOREIGN KEY([Klub parlamentarny])
REFERENCES [dbo].[Klub] ([ID])
ON UPDATE CASCADE
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[Poseł] CHECK CONSTRAINT [FK_Poseł_Klub]
GO
ALTER TABLE [dbo].[Ustawa]  WITH CHECK ADD  CONSTRAINT [FK_Ustawa_Klub] FOREIGN KEY([ZgłoszonaPrzez])
REFERENCES [dbo].[Klub] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Ustawa] CHECK CONSTRAINT [FK_Ustawa_Klub]
GO
USE [master]
GO
ALTER DATABASE [NTR-Own] SET  READ_WRITE 
GO
