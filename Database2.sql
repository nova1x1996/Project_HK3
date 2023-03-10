USE [master]
GO
/****** Object:  Database [ProjectHK3]    Script Date: 3/10/2023 3:18:46 PM ******/
CREATE DATABASE [ProjectHK3]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ProjectHK3', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\ProjectHK3.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ProjectHK3_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\ProjectHK3_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [ProjectHK3] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ProjectHK3].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ProjectHK3] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ProjectHK3] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ProjectHK3] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ProjectHK3] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ProjectHK3] SET ARITHABORT OFF 
GO
ALTER DATABASE [ProjectHK3] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ProjectHK3] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ProjectHK3] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ProjectHK3] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ProjectHK3] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ProjectHK3] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ProjectHK3] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ProjectHK3] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ProjectHK3] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ProjectHK3] SET  ENABLE_BROKER 
GO
ALTER DATABASE [ProjectHK3] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ProjectHK3] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ProjectHK3] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ProjectHK3] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ProjectHK3] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ProjectHK3] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [ProjectHK3] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ProjectHK3] SET RECOVERY FULL 
GO
ALTER DATABASE [ProjectHK3] SET  MULTI_USER 
GO
ALTER DATABASE [ProjectHK3] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ProjectHK3] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ProjectHK3] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ProjectHK3] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ProjectHK3] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [ProjectHK3] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'ProjectHK3', N'ON'
GO
ALTER DATABASE [ProjectHK3] SET QUERY_STORE = OFF
GO
USE [ProjectHK3]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 3/10/2023 3:18:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoleClaims]    Script Date: 3/10/2023 3:18:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoleClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 3/10/2023 3:18:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 3/10/2023 3:18:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 3/10/2023 3:18:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](450) NOT NULL,
	[ProviderKey] [nvarchar](450) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 3/10/2023 3:18:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](450) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 3/10/2023 3:18:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](450) NOT NULL,
	[FirstName] [nvarchar](max) NOT NULL,
	[LastName] [nvarchar](max) NOT NULL,
	[ProfilePicture] [nvarchar](max) NULL,
	[UserName] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[Email] [nvarchar](256) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
 CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserTokens]    Script Date: 3/10/2023 3:18:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserTokens](
	[UserId] [nvarchar](450) NOT NULL,
	[LoginProvider] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](450) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ChangePackages]    Script Date: 3/10/2023 3:18:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChangePackages](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[customer_id] [int] NULL,
	[packageOld] [int] NOT NULL,
	[packageNew] [int] NOT NULL,
	[price] [int] NOT NULL,
	[state] [bit] NOT NULL,
	[date] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_ChangePackages] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[contact_us]    Script Date: 3/10/2023 3:18:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[contact_us](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[firstName] [nvarchar](max) NOT NULL,
	[lastName] [nvarchar](max) NOT NULL,
	[email] [nvarchar](max) NOT NULL,
	[phone] [nvarchar](max) NOT NULL,
	[comments] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_contact_us] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[customer]    Script Date: 3/10/2023 3:18:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[customer](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[card_number] [nvarchar](max) NOT NULL,
	[phone] [nvarchar](max) NOT NULL,
	[address] [nvarchar](max) NOT NULL,
	[statePackage] [bit] NULL,
	[user_id] [nvarchar](450) NULL,
	[services_sub_date] [datetime2](7) NULL,
	[date_left] [datetime2](7) NULL,
	[payment_monthly] [decimal](18, 2) NULL,
	[package_id] [int] NULL,
 CONSTRAINT [PK_customer] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[customer_order]    Script Date: 3/10/2023 3:18:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[customer_order](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[pay_type] [nvarchar](max) NOT NULL,
	[total_money] [decimal](18, 2) NOT NULL,
	[state] [bit] NOT NULL,
	[monthPackage] [int] NULL,
	[date] [datetime2](7) NOT NULL,
	[customer_id] [int] NULL,
	[package_id] [int] NULL,
	[movie_id] [int] NULL,
	[setUpBox_id] [int] NULL,
 CONSTRAINT [PK_customer_order] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[customercare]    Script Date: 3/10/2023 3:18:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[customercare](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Phone] [nvarchar](11) NOT NULL,
	[location_id] [int] NULL,
 CONSTRAINT [PK_customercare] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[dealers]    Script Date: 3/10/2023 3:18:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[dealers](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[user_id] [nvarchar](450) NULL,
	[phone] [nvarchar](max) NOT NULL,
	[address] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_dealers] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[dealers_order]    Script Date: 3/10/2023 3:18:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[dealers_order](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[dealers_id] [int] NULL,
	[setup_box_id] [int] NULL,
	[status] [bit] NOT NULL,
	[date] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_dealers_order] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[faq]    Script Date: 3/10/2023 3:18:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[faq](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[question] [nvarchar](max) NOT NULL,
	[answer] [nvarchar](max) NOT NULL,
	[status] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_faq] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[feedback]    Script Date: 3/10/2023 3:18:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[feedback](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[type] [nvarchar](max) NOT NULL,
	[star] [int] NOT NULL,
	[content] [nvarchar](250) NOT NULL,
	[date] [datetime2](7) NULL,
	[customer_id] [int] NOT NULL,
 CONSTRAINT [PK_feedback] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[location]    Script Date: 3/10/2023 3:18:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[location](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](30) NOT NULL,
 CONSTRAINT [PK_location] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[movie]    Script Date: 3/10/2023 3:18:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[movie](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](150) NOT NULL,
	[img] [nvarchar](max) NULL,
	[content] [nvarchar](250) NOT NULL,
	[price] [decimal](18, 2) NOT NULL,
	[movie_cate_id] [int] NOT NULL,
 CONSTRAINT [PK_movie] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[movie_cate]    Script Date: 3/10/2023 3:18:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[movie_cate](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_movie_cate] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[package]    Script Date: 3/10/2023 3:18:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[package](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](150) NOT NULL,
	[duration] [int] NOT NULL,
	[details] [nvarchar](150) NOT NULL,
	[status] [bit] NOT NULL,
	[price] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_package] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[recharge]    Script Date: 3/10/2023 3:18:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[recharge](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[pay_type] [nvarchar](max) NOT NULL,
	[state] [bit] NOT NULL,
	[date] [datetime2](7) NOT NULL,
	[card_number] [nvarchar](max) NOT NULL,
	[month] [int] NOT NULL,
	[customer_id] [int] NULL,
	[package_id] [int] NULL,
 CONSTRAINT [PK_recharge] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[setup_box]    Script Date: 3/10/2023 3:18:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[setup_box](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](max) NOT NULL,
	[details] [nvarchar](max) NOT NULL,
	[img] [nvarchar](max) NULL,
	[price] [int] NOT NULL,
 CONSTRAINT [PK_setup_box] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230310080631_lab1', N'6.0.13')
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'1', N'dealer', N'DEALER', N'9f57bc7e-3866-4b94-bae3-837d43452547')
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'2', N'customer', N'CUSTOMER', N'4c74b8ce-08f7-4d01-9cd2-1d9931862fdb')
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'3', N'admin', N'ADMIN', N'5af97cbc-7557-41c5-970c-50b8de788511')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'05c66664-fa9e-434c-9055-d3553135c025', N'3')
GO
INSERT [dbo].[AspNetUsers] ([Id], [FirstName], [LastName], [ProfilePicture], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'05c66664-fa9e-434c-9055-d3553135c025', N'R-DTH', N'Company', NULL, N'admin', N'ADMIN', N'admin@gmail.com', N'ADMIN@GMAIL.COM', 1, N'AQAAAAEAACcQAAAAECPLzoUBs8MhssPuHS9Ir9Ndmk3IZyX0EzDZmos1SNyfX5buH6N8majqXQIfy+Jdqg==', N'EQSUSKTFDKGDLBAQJGN54FI2NWOO4AJE', N'e929cddc-1696-434e-96f4-fa03a61db9dd', NULL, 1, 0, NULL, 1, 0)
GO
SET IDENTITY_INSERT [dbo].[customercare] ON 

INSERT [dbo].[customercare] ([Id], [Phone], [location_id]) VALUES (1, N'0913546829', 1)
INSERT [dbo].[customercare] ([Id], [Phone], [location_id]) VALUES (2, N'0946824598', 2)
INSERT [dbo].[customercare] ([Id], [Phone], [location_id]) VALUES (3, N'0948657235', 3)
INSERT [dbo].[customercare] ([Id], [Phone], [location_id]) VALUES (4, N'0936659585', 4)
INSERT [dbo].[customercare] ([Id], [Phone], [location_id]) VALUES (5, N'0945825468', 5)
SET IDENTITY_INSERT [dbo].[customercare] OFF
GO
SET IDENTITY_INSERT [dbo].[faq] ON 

INSERT [dbo].[faq] ([id], [question], [answer], [status]) VALUES (1, N'What is satellite television and how does it work?', N'Satellite television is a broadcast delivery system that uses communication satellites orbiting the earth to transmit television signals. A satellite dish installed at the user''s location receives the satellite signals and sends them to a set-top box or integrated television with a built-in receiver. The user can then watch the satellite television channels using their television set.', N'show')
INSERT [dbo].[faq] ([id], [question], [answer], [status]) VALUES (2, N'What are the advantages of satellite television over other broadcast methods?', N'Satellite television offers a wider selection of channels than cable or over-the-air broadcasts, and the signal quality is often superior. It can also be accessed from remote or rural areas where other broadcast methods may be unavailable.', N'show')
INSERT [dbo].[faq] ([id], [question], [answer], [status]) VALUES (3, N'How can I subscribe to your satellite television service?', N'To subscribe to our satellite television service, please visit our website or call our customer service hotline. Our representatives will assist you in choosing a package that best suits your needs and schedule a convenient time for installation.', N'show')
INSERT [dbo].[faq] ([id], [question], [answer], [status]) VALUES (4, N'What types of packages do you offer, and how much do they cost?', N'We offer a variety of packages to fit different viewing needs and budgets, including basic, standard, and premium options. Please visit our website or contact our customer service hotline for detailed information on package offerings and pricing.', N'show')
INSERT [dbo].[faq] ([id], [question], [answer], [status]) VALUES (5, N'Can I watch satellite television on multiple devices?', N'Yes, you can connect multiple televisions or other devices to your satellite service, depending on your package and equipment. Please contact our customer service hotline for more information on multi-room viewing options.', N'show')
INSERT [dbo].[faq] ([id], [question], [answer], [status]) VALUES (6, N'What channels and programs are included in your packages?', N'We offer a wide range of channels and programs, including local and international news, sports, entertainment, movies, and more. Please visit our website or contact our customer service hotline for more information on specific channel lineups and programming.', N'show')
INSERT [dbo].[faq] ([id], [question], [answer], [status]) VALUES (7, N'How can I troubleshoot problems with my satellite service?', N'If you are experiencing issues with your satellite service, please first check your equipment connections and power supply. If the issue persists, please contact our customer service hotline for further troubleshooting assistance.', N'show')
SET IDENTITY_INSERT [dbo].[faq] OFF
GO
SET IDENTITY_INSERT [dbo].[location] ON 

INSERT [dbo].[location] ([Id], [Name]) VALUES (1, N'Ho Chi Minh')
INSERT [dbo].[location] ([Id], [Name]) VALUES (2, N'Da Lat')
INSERT [dbo].[location] ([Id], [Name]) VALUES (3, N'Thu Duc')
INSERT [dbo].[location] ([Id], [Name]) VALUES (4, N'Sai Gon')
INSERT [dbo].[location] ([Id], [Name]) VALUES (5, N'Ha Noi')
SET IDENTITY_INSERT [dbo].[location] OFF
GO
SET IDENTITY_INSERT [dbo].[movie] ON 

INSERT [dbo].[movie] ([id], [name], [img], [content], [price], [movie_cate_id]) VALUES (1, N'The Dark Knight', N'/img/movie/TheDK.jpg', N'When the menace known as the Joker wreaks havoc and chaos on the people of Gotham, Batman must accept one of the greatest psychological and physical tests of his ability to fight injustice.', CAST(10.00 AS Decimal(18, 2)), 2)
INSERT [dbo].[movie] ([id], [name], [img], [content], [price], [movie_cate_id]) VALUES (2, N'The Lord of the Rings: The Return of the King', N'/img/movie/LOTR.jfif', N'Gandalf and Aragorn lead the World of Men against Sauron''s army to draw his gaze from Frodo and Sam as they approach Mount Doom with the One Ring.', CAST(15.00 AS Decimal(18, 2)), 2)
INSERT [dbo].[movie] ([id], [name], [img], [content], [price], [movie_cate_id]) VALUES (3, N'Inception', N'/img/movie/inception.jpg', N'A thief who steals corporate secrets through the use of dream-sharing technology is given the inverse task of planting an idea into the mind of a C.E.O., but his tragic past may doom the project and his team to disaster.', CAST(20.00 AS Decimal(18, 2)), 2)
INSERT [dbo].[movie] ([id], [name], [img], [content], [price], [movie_cate_id]) VALUES (4, N'You People', N'/img/movie/You_People_Film_Poster.jpg', N'Follows a new couple and their families, who find themselves examining modern love and family dynamics amidst clashing cultures, societal expectations and generational differences.', CAST(10.00 AS Decimal(18, 2)), 1)
INSERT [dbo].[movie] ([id], [name], [img], [content], [price], [movie_cate_id]) VALUES (5, N'Your Place or Mine', N'/img/movie/Your_Place_Or_Mine.jpg', N'Two long-distance best friends change each other''s lives when she decides to pursue a lifelong dream and he volunteers to keep an eye on her teenage son.', CAST(15.00 AS Decimal(18, 2)), 1)
INSERT [dbo].[movie] ([id], [name], [img], [content], [price], [movie_cate_id]) VALUES (6, N'Empire of Light', N'/img/movie/large_empire-of-light-movie-poster-2022.jpeg', N'A drama about the power of human connection during turbulent times, set in an English coastal town in the early 1980s.', CAST(12.00 AS Decimal(18, 2)), 1)
INSERT [dbo].[movie] ([id], [name], [img], [content], [price], [movie_cate_id]) VALUES (7, N'Step Brothers', N'/img/movie/Step_Brothers.jfif', N'Two aimless middle-aged losers still living at home are forced against their will to become roommates when their parents marry.', CAST(8.00 AS Decimal(18, 2)), 3)
INSERT [dbo].[movie] ([id], [name], [img], [content], [price], [movie_cate_id]) VALUES (8, N'White Chicks', N'/img/movie/White_Chicks.jpg', N'Two disgraced FBI agents go way undercover in an effort to protect hotel heiresses the Wilson sisters from a kidnapping plot.', CAST(13.00 AS Decimal(18, 2)), 3)
INSERT [dbo].[movie] ([id], [name], [img], [content], [price], [movie_cate_id]) VALUES (9, N'The Hangover', N'/img/movie/The_Hangover.jfif', N'Three buddies wake up from a bachelor party in Las Vegas, with no memory of the previous night and the bachelor missing. They make their way around the city in order to find their friend before his wedding.', CAST(15.00 AS Decimal(18, 2)), 3)
SET IDENTITY_INSERT [dbo].[movie] OFF
GO
SET IDENTITY_INSERT [dbo].[movie_cate] ON 

INSERT [dbo].[movie_cate] ([id], [name]) VALUES (1, N'Romatic')
INSERT [dbo].[movie_cate] ([id], [name]) VALUES (2, N'Action')
INSERT [dbo].[movie_cate] ([id], [name]) VALUES (3, N'Comedy')
SET IDENTITY_INSERT [dbo].[movie_cate] OFF
GO
SET IDENTITY_INSERT [dbo].[package] ON 

INSERT [dbo].[package] ([id], [name], [duration], [details], [status], [price]) VALUES (1, N'Bronze Pack', 1, N'80 channels (including R-DTH Cab) + VOD library', 1, CAST(100.00 AS Decimal(18, 2)))
INSERT [dbo].[package] ([id], [name], [duration], [details], [status], [price]) VALUES (2, N'Sliver Pack', 1, N'100 channels (including R-DTH Cab) + VOD library', 1, CAST(130.00 AS Decimal(18, 2)))
INSERT [dbo].[package] ([id], [name], [duration], [details], [status], [price]) VALUES (3, N'Gold Pack', 1, N'144 channels (including R-DTH Cab) + VOD library', 1, CAST(150.00 AS Decimal(18, 2)))
INSERT [dbo].[package] ([id], [name], [duration], [details], [status], [price]) VALUES (4, N'Diamond Pack', 1, N'160 channels (including R-DTH Cab) + VOD library + Premium Galaxy', 1, CAST(200.00 AS Decimal(18, 2)))
SET IDENTITY_INSERT [dbo].[package] OFF
GO
SET IDENTITY_INSERT [dbo].[setup_box] ON 

INSERT [dbo].[setup_box] ([id], [name], [details], [img], [price]) VALUES (1, N'R-DTH Digital TV SD', N'R-DTH Digital TV SD is a type of digital television service that provides standard definition (SD) channels to viewers. ', N'/img/setupbox/SUBSD.jpg', 500)
INSERT [dbo].[setup_box] ([id], [name], [details], [img], [price]) VALUES (2, N'R-DTH Digital TV HD', N'R - DTH Digital TV HD is a type of digital television service that provides high definition(HD) channels to viewers. ', N'/img/setupbox/SUBHD.jpg', 700)
INSERT [dbo].[setup_box] ([id], [name], [details], [img], [price]) VALUES (3, N'R-DTH Digital TV 4K', N'R-DTH Digital TV 4K is a type of digital television service that provides ultra-high definition (UHD) 4K channels to viewers.', N'/img/setupbox/SUB4K.jpg', 1000)
SET IDENTITY_INSERT [dbo].[setup_box] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetRoleClaims_RoleId]    Script Date: 3/10/2023 3:18:46 PM ******/
CREATE NONCLUSTERED INDEX [IX_AspNetRoleClaims_RoleId] ON [dbo].[AspNetRoleClaims]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [RoleNameIndex]    Script Date: 3/10/2023 3:18:46 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]
(
	[NormalizedName] ASC
)
WHERE ([NormalizedName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserClaims_UserId]    Script Date: 3/10/2023 3:18:46 PM ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserClaims_UserId] ON [dbo].[AspNetUserClaims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserLogins_UserId]    Script Date: 3/10/2023 3:18:46 PM ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserLogins_UserId] ON [dbo].[AspNetUserLogins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserRoles_RoleId]    Script Date: 3/10/2023 3:18:46 PM ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserRoles_RoleId] ON [dbo].[AspNetUserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [EmailIndex]    Script Date: 3/10/2023 3:18:46 PM ******/
CREATE NONCLUSTERED INDEX [EmailIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedEmail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UserNameIndex]    Script Date: 3/10/2023 3:18:46 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedUserName] ASC
)
WHERE ([NormalizedUserName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_ChangePackages_customer_id]    Script Date: 3/10/2023 3:18:46 PM ******/
CREATE NONCLUSTERED INDEX [IX_ChangePackages_customer_id] ON [dbo].[ChangePackages]
(
	[customer_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_customer_package_id]    Script Date: 3/10/2023 3:18:46 PM ******/
CREATE NONCLUSTERED INDEX [IX_customer_package_id] ON [dbo].[customer]
(
	[package_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_customer_user_id]    Script Date: 3/10/2023 3:18:46 PM ******/
CREATE NONCLUSTERED INDEX [IX_customer_user_id] ON [dbo].[customer]
(
	[user_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_customer_order_customer_id]    Script Date: 3/10/2023 3:18:46 PM ******/
CREATE NONCLUSTERED INDEX [IX_customer_order_customer_id] ON [dbo].[customer_order]
(
	[customer_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_customer_order_movie_id]    Script Date: 3/10/2023 3:18:46 PM ******/
CREATE NONCLUSTERED INDEX [IX_customer_order_movie_id] ON [dbo].[customer_order]
(
	[movie_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_customer_order_package_id]    Script Date: 3/10/2023 3:18:46 PM ******/
CREATE NONCLUSTERED INDEX [IX_customer_order_package_id] ON [dbo].[customer_order]
(
	[package_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_customer_order_setUpBox_id]    Script Date: 3/10/2023 3:18:46 PM ******/
CREATE NONCLUSTERED INDEX [IX_customer_order_setUpBox_id] ON [dbo].[customer_order]
(
	[setUpBox_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_customercare_location_id]    Script Date: 3/10/2023 3:18:46 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_customercare_location_id] ON [dbo].[customercare]
(
	[location_id] ASC
)
WHERE ([location_id] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_dealers_user_id]    Script Date: 3/10/2023 3:18:46 PM ******/
CREATE NONCLUSTERED INDEX [IX_dealers_user_id] ON [dbo].[dealers]
(
	[user_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_dealers_order_dealers_id]    Script Date: 3/10/2023 3:18:46 PM ******/
CREATE NONCLUSTERED INDEX [IX_dealers_order_dealers_id] ON [dbo].[dealers_order]
(
	[dealers_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_dealers_order_setup_box_id]    Script Date: 3/10/2023 3:18:46 PM ******/
CREATE NONCLUSTERED INDEX [IX_dealers_order_setup_box_id] ON [dbo].[dealers_order]
(
	[setup_box_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_feedback_customer_id]    Script Date: 3/10/2023 3:18:46 PM ******/
CREATE NONCLUSTERED INDEX [IX_feedback_customer_id] ON [dbo].[feedback]
(
	[customer_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_movie_movie_cate_id]    Script Date: 3/10/2023 3:18:46 PM ******/
CREATE NONCLUSTERED INDEX [IX_movie_movie_cate_id] ON [dbo].[movie]
(
	[movie_cate_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_recharge_customer_id]    Script Date: 3/10/2023 3:18:46 PM ******/
CREATE NONCLUSTERED INDEX [IX_recharge_customer_id] ON [dbo].[recharge]
(
	[customer_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_recharge_package_id]    Script Date: 3/10/2023 3:18:46 PM ******/
CREATE NONCLUSTERED INDEX [IX_recharge_package_id] ON [dbo].[recharge]
(
	[package_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AspNetRoleClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetRoleClaims] CHECK CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserTokens]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserTokens] CHECK CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[ChangePackages]  WITH CHECK ADD  CONSTRAINT [FK_ChangePackages_customer_customer_id] FOREIGN KEY([customer_id])
REFERENCES [dbo].[customer] ([id])
GO
ALTER TABLE [dbo].[ChangePackages] CHECK CONSTRAINT [FK_ChangePackages_customer_customer_id]
GO
ALTER TABLE [dbo].[customer]  WITH CHECK ADD  CONSTRAINT [FK_customer_AspNetUsers_user_id] FOREIGN KEY([user_id])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[customer] CHECK CONSTRAINT [FK_customer_AspNetUsers_user_id]
GO
ALTER TABLE [dbo].[customer]  WITH CHECK ADD  CONSTRAINT [FK_customer_package_package_id] FOREIGN KEY([package_id])
REFERENCES [dbo].[package] ([id])
GO
ALTER TABLE [dbo].[customer] CHECK CONSTRAINT [FK_customer_package_package_id]
GO
ALTER TABLE [dbo].[customer_order]  WITH CHECK ADD  CONSTRAINT [FK_customer_order_customer_customer_id] FOREIGN KEY([customer_id])
REFERENCES [dbo].[customer] ([id])
GO
ALTER TABLE [dbo].[customer_order] CHECK CONSTRAINT [FK_customer_order_customer_customer_id]
GO
ALTER TABLE [dbo].[customer_order]  WITH CHECK ADD  CONSTRAINT [FK_customer_order_movie_movie_id] FOREIGN KEY([movie_id])
REFERENCES [dbo].[movie] ([id])
GO
ALTER TABLE [dbo].[customer_order] CHECK CONSTRAINT [FK_customer_order_movie_movie_id]
GO
ALTER TABLE [dbo].[customer_order]  WITH CHECK ADD  CONSTRAINT [FK_customer_order_package_package_id] FOREIGN KEY([package_id])
REFERENCES [dbo].[package] ([id])
GO
ALTER TABLE [dbo].[customer_order] CHECK CONSTRAINT [FK_customer_order_package_package_id]
GO
ALTER TABLE [dbo].[customer_order]  WITH CHECK ADD  CONSTRAINT [FK_customer_order_setup_box_setUpBox_id] FOREIGN KEY([setUpBox_id])
REFERENCES [dbo].[setup_box] ([id])
GO
ALTER TABLE [dbo].[customer_order] CHECK CONSTRAINT [FK_customer_order_setup_box_setUpBox_id]
GO
ALTER TABLE [dbo].[customercare]  WITH CHECK ADD  CONSTRAINT [FK_customercare_location_location_id] FOREIGN KEY([location_id])
REFERENCES [dbo].[location] ([Id])
GO
ALTER TABLE [dbo].[customercare] CHECK CONSTRAINT [FK_customercare_location_location_id]
GO
ALTER TABLE [dbo].[dealers]  WITH CHECK ADD  CONSTRAINT [FK_dealers_AspNetUsers_user_id] FOREIGN KEY([user_id])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[dealers] CHECK CONSTRAINT [FK_dealers_AspNetUsers_user_id]
GO
ALTER TABLE [dbo].[dealers_order]  WITH CHECK ADD  CONSTRAINT [FK_dealers_order_dealers_dealers_id] FOREIGN KEY([dealers_id])
REFERENCES [dbo].[dealers] ([id])
GO
ALTER TABLE [dbo].[dealers_order] CHECK CONSTRAINT [FK_dealers_order_dealers_dealers_id]
GO
ALTER TABLE [dbo].[dealers_order]  WITH CHECK ADD  CONSTRAINT [FK_dealers_order_setup_box_setup_box_id] FOREIGN KEY([setup_box_id])
REFERENCES [dbo].[setup_box] ([id])
GO
ALTER TABLE [dbo].[dealers_order] CHECK CONSTRAINT [FK_dealers_order_setup_box_setup_box_id]
GO
ALTER TABLE [dbo].[feedback]  WITH CHECK ADD  CONSTRAINT [FK_feedback_customer_customer_id] FOREIGN KEY([customer_id])
REFERENCES [dbo].[customer] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[feedback] CHECK CONSTRAINT [FK_feedback_customer_customer_id]
GO
ALTER TABLE [dbo].[movie]  WITH CHECK ADD  CONSTRAINT [FK_movie_movie_cate_movie_cate_id] FOREIGN KEY([movie_cate_id])
REFERENCES [dbo].[movie_cate] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[movie] CHECK CONSTRAINT [FK_movie_movie_cate_movie_cate_id]
GO
ALTER TABLE [dbo].[recharge]  WITH CHECK ADD  CONSTRAINT [FK_recharge_customer_customer_id] FOREIGN KEY([customer_id])
REFERENCES [dbo].[customer] ([id])
GO
ALTER TABLE [dbo].[recharge] CHECK CONSTRAINT [FK_recharge_customer_customer_id]
GO
ALTER TABLE [dbo].[recharge]  WITH CHECK ADD  CONSTRAINT [FK_recharge_package_package_id] FOREIGN KEY([package_id])
REFERENCES [dbo].[package] ([id])
GO
ALTER TABLE [dbo].[recharge] CHECK CONSTRAINT [FK_recharge_package_package_id]
GO
USE [master]
GO
ALTER DATABASE [ProjectHK3] SET  READ_WRITE 
GO
