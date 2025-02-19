USE [master]
GO
/****** Object:  Database [Sis_Prestamos]    Script Date: 5/1/2025 23:34:28 ******/
CREATE DATABASE [Sis_Prestamos]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Sis_Prestamos', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\Sis_Prestamos.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Sis_Prestamos_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\Sis_Prestamos_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [Sis_Prestamos] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Sis_Prestamos].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Sis_Prestamos] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Sis_Prestamos] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Sis_Prestamos] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Sis_Prestamos] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Sis_Prestamos] SET ARITHABORT OFF 
GO
ALTER DATABASE [Sis_Prestamos] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [Sis_Prestamos] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Sis_Prestamos] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Sis_Prestamos] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Sis_Prestamos] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Sis_Prestamos] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Sis_Prestamos] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Sis_Prestamos] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Sis_Prestamos] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Sis_Prestamos] SET  ENABLE_BROKER 
GO
ALTER DATABASE [Sis_Prestamos] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Sis_Prestamos] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Sis_Prestamos] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Sis_Prestamos] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Sis_Prestamos] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Sis_Prestamos] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Sis_Prestamos] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Sis_Prestamos] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Sis_Prestamos] SET  MULTI_USER 
GO
ALTER DATABASE [Sis_Prestamos] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Sis_Prestamos] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Sis_Prestamos] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Sis_Prestamos] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Sis_Prestamos] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Sis_Prestamos] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [Sis_Prestamos] SET QUERY_STORE = OFF
GO
USE [Sis_Prestamos]
GO
/****** Object:  Table [dbo].[Clientes]    Script Date: 5/1/2025 23:34:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Clientes](
	[id_cliente] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](255) NOT NULL,
	[email] [varchar](255) NOT NULL,
	[telefono] [varchar](20) NULL,
	[direccion] [varchar](255) NULL,
	[fecha_registro] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[id_cliente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[cuotas]    Script Date: 5/1/2025 23:34:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cuotas](
	[idcuota] [int] IDENTITY(1,1) NOT NULL,
	[idprestamo] [int] NOT NULL,
	[numero_cuota] [int] NOT NULL,
	[fecha_vencimiento] [date] NOT NULL,
	[fecha_pago] [date] NULL,
	[monto_cuota] [decimal](10, 2) NOT NULL,
	[monto_pagado] [decimal](10, 2) NULL,
	[estado] [varchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[idcuota] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pagos]    Script Date: 5/1/2025 23:34:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pagos](
	[id_pago] [int] IDENTITY(1,1) NOT NULL,
	[id_prestamo] [int] NOT NULL,
	[monto_pago] [real] NOT NULL,
	[fecha_pago] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id_pago] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Prestamos]    Script Date: 5/1/2025 23:34:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Prestamos](
	[id_prestamo] [int] IDENTITY(1,1) NOT NULL,
	[id_cliente] [int] NOT NULL,
	[monto] [real] NOT NULL,
	[interes] [real] NOT NULL,
	[plazo_meses] [int] NOT NULL,
	[Frecuencia_pagos] [varchar](50) NOT NULL,
	[estado] [varchar](50) NULL,
	[fecha_inicio] [datetime] NOT NULL,
	[fecha_fin] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id_prestamo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[cuotas] ADD  DEFAULT ((0)) FOR [monto_pagado]
GO
ALTER TABLE [dbo].[cuotas] ADD  DEFAULT ('pendiente') FOR [estado]
GO
ALTER TABLE [dbo].[cuotas]  WITH CHECK ADD  CONSTRAINT [FK_cuotas_Prestamos] FOREIGN KEY([idprestamo])
REFERENCES [dbo].[Prestamos] ([id_prestamo])
GO
ALTER TABLE [dbo].[cuotas] CHECK CONSTRAINT [FK_cuotas_Prestamos]
GO
ALTER TABLE [dbo].[Pagos]  WITH CHECK ADD  CONSTRAINT [FK_Pagos_Prestamos] FOREIGN KEY([id_prestamo])
REFERENCES [dbo].[Prestamos] ([id_prestamo])
GO
ALTER TABLE [dbo].[Pagos] CHECK CONSTRAINT [FK_Pagos_Prestamos]
GO
ALTER TABLE [dbo].[Prestamos]  WITH CHECK ADD  CONSTRAINT [FK_Prestamos_Clientes] FOREIGN KEY([id_cliente])
REFERENCES [dbo].[Clientes] ([id_cliente])
GO
ALTER TABLE [dbo].[Prestamos] CHECK CONSTRAINT [FK_Prestamos_Clientes]
GO
USE [master]
GO
ALTER DATABASE [Sis_Prestamos] SET  READ_WRITE 
GO
