USE [master]
GO
/****** Object:  Database [Products]    Script Date: 05/05/2019 16:10:13 ******/
CREATE DATABASE [Products] ON  PRIMARY 
( NAME = N'Products', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10.SQLEXPRESS\MSSQL\DATA\Products.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Products_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10.SQLEXPRESS\MSSQL\DATA\Products_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [Products] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Products].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Products] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [Products] SET ANSI_NULLS OFF
GO
ALTER DATABASE [Products] SET ANSI_PADDING OFF
GO
ALTER DATABASE [Products] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [Products] SET ARITHABORT OFF
GO
ALTER DATABASE [Products] SET AUTO_CLOSE OFF
GO
ALTER DATABASE [Products] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [Products] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [Products] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [Products] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [Products] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [Products] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [Products] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [Products] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [Products] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [Products] SET  DISABLE_BROKER
GO
ALTER DATABASE [Products] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [Products] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [Products] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [Products] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [Products] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [Products] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [Products] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [Products] SET  READ_WRITE
GO
ALTER DATABASE [Products] SET RECOVERY FULL
GO
ALTER DATABASE [Products] SET  MULTI_USER
GO
ALTER DATABASE [Products] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [Products] SET DB_CHAINING OFF
GO
USE [Products]
GO
/****** Object:  Table [dbo].[Unit]    Script Date: 05/05/2019 16:10:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Unit](
	[ID] [int] NOT NULL,
	[UnitName] [nvarchar](10) NOT NULL,
 CONSTRAINT [PK_Unit] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Unit] ([ID], [UnitName]) VALUES (1, N'Bottle')
INSERT [dbo].[Unit] ([ID], [UnitName]) VALUES (2, N'Box')
INSERT [dbo].[Unit] ([ID], [UnitName]) VALUES (3, N'Can')
INSERT [dbo].[Unit] ([ID], [UnitName]) VALUES (4, N'Kilo')
INSERT [dbo].[Unit] ([ID], [UnitName]) VALUES (5, N'Liter')
/****** Object:  Table [dbo].[Supplier]    Script Date: 05/05/2019 16:10:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Supplier](
	[SupplierID] [int] IDENTITY(1,1) NOT NULL,
	[SupplierName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Supplier] PRIMARY KEY CLUSTERED 
(
	[SupplierID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Supplier] ON
INSERT [dbo].[Supplier] ([SupplierID], [SupplierName]) VALUES (4, N'Test')
SET IDENTITY_INSERT [dbo].[Supplier] OFF
/****** Object:  Table [dbo].[Product]    Script Date: 05/05/2019 16:10:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[ProductID] [int] IDENTITY(1,1) NOT NULL,
	[ProductName] [nvarchar](50) NOT NULL,
	[ReorderLevel] [int] NOT NULL,
	[UnitPrice] [decimal](5, 2) NOT NULL,
	[UnitInStock] [int] NOT NULL,
	[UnitOnOrder] [int] NOT NULL,
	[QuantityPerUnit] [int] NOT NULL,
	[SupplierID] [int] NOT NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[ProductID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Product] ON
INSERT [dbo].[Product] ([ProductID], [ProductName], [ReorderLevel], [UnitPrice], [UnitInStock], [UnitOnOrder], [QuantityPerUnit], [SupplierID]) VALUES (5, N'Mostafa', 7, CAST(30.50 AS Decimal(5, 2)), 10, 3, 4, 4)
SET IDENTITY_INSERT [dbo].[Product] OFF
/****** Object:  StoredProcedure [dbo].[Products_search]    Script Date: 05/05/2019 16:10:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Products_search] 
	-- Add the parameters for the stored procedure here
	@ProductName nvarchar(50),
	@QuantityPerUnit int,
	@ReorderLevel int,
	@UnitPrice decimal(5, 2),
	@UnitInStock int,
	@UnitOnOrder int,
	@SupplierID int
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM [Products].[dbo].[Product] as p
	where (p.ProductName = @ProductName OR @ProductName IS NULL) 
	AND (p.ReorderLevel = @ReorderLevel OR @ReorderLevel IS NULL)
	AND (p.UnitPrice = @UnitPrice OR @UnitPrice IS NULL)
	AND (p.UnitInStock = @UnitInStock OR @UnitInStock IS NULL)
	AND (p.UnitOnOrder = @UnitOnOrder OR @UnitOnOrder IS NULL)
	AND (p.QuantityPerUnit = @QuantityPerUnit OR @QuantityPerUnit IS NULL)
	AND (p.SupplierID = @SupplierID OR @SupplierID IS NULL)	
END
GO
/****** Object:  ForeignKey [FK_Product_Supplier]    Script Date: 05/05/2019 16:10:13 ******/
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Supplier] FOREIGN KEY([SupplierID])
REFERENCES [dbo].[Supplier] ([SupplierID])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_Supplier]
GO
/****** Object:  ForeignKey [FK_Product_Unit]    Script Date: 05/05/2019 16:10:13 ******/
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Unit] FOREIGN KEY([QuantityPerUnit])
REFERENCES [dbo].[Unit] ([ID])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_Unit]
GO
