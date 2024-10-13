USE [master]
GO
/****** Object:  Database [BibliotecaDBX]    Script Date: 13/10/2024 15:38:39 ******/
CREATE DATABASE [BibliotecaDBX]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'BibliotecaDBX_Data', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\BibliotecaDBX.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'BibliotecaDBX_Log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\BibliotecaDBX.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [BibliotecaDBX] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [BibliotecaDBX].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [BibliotecaDBX] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [BibliotecaDBX] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [BibliotecaDBX] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [BibliotecaDBX] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [BibliotecaDBX] SET ARITHABORT OFF 
GO
ALTER DATABASE [BibliotecaDBX] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [BibliotecaDBX] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [BibliotecaDBX] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [BibliotecaDBX] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [BibliotecaDBX] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [BibliotecaDBX] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [BibliotecaDBX] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [BibliotecaDBX] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [BibliotecaDBX] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [BibliotecaDBX] SET  ENABLE_BROKER 
GO
ALTER DATABASE [BibliotecaDBX] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [BibliotecaDBX] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [BibliotecaDBX] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [BibliotecaDBX] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [BibliotecaDBX] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [BibliotecaDBX] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [BibliotecaDBX] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [BibliotecaDBX] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [BibliotecaDBX] SET  MULTI_USER 
GO
ALTER DATABASE [BibliotecaDBX] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [BibliotecaDBX] SET DB_CHAINING OFF 
GO
ALTER DATABASE [BibliotecaDBX] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [BibliotecaDBX] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [BibliotecaDBX] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [BibliotecaDBX] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [BibliotecaDBX] SET QUERY_STORE = ON
GO
ALTER DATABASE [BibliotecaDBX] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [BibliotecaDBX]
GO
/****** Object:  Table [dbo].[Autores]    Script Date: 13/10/2024 15:38:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Autores](
	[AutorID] [int] IDENTITY(1,1) NOT NULL,
	[NombreAutor] [nvarchar](100) NOT NULL,
	[Nacionalidad] [nvarchar](100) NOT NULL,
	[FechaNacimiento] [date] NOT NULL,
	[Bibliografia] [nvarchar](max) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[AutorID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Carrera]    Script Date: 13/10/2024 15:38:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Carrera](
	[CarreraID] [int] IDENTITY(1,1) NOT NULL,
	[NombreCarrera] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CarreraID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Categorias]    Script Date: 13/10/2024 15:38:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categorias](
	[CategoriaID] [int] IDENTITY(1,1) NOT NULL,
	[NombreCategoria] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CategoriaID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Compras]    Script Date: 13/10/2024 15:38:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Compras](
	[CompraID] [int] IDENTITY(1,1) NOT NULL,
	[Cantidad] [int] NOT NULL,
	[CostoUnidad] [decimal](10, 2) NOT NULL,
	[FechaCompra] [date] NOT NULL,
	[CostoTotal] [decimal](10, 2) NOT NULL,
	[EdicionID] [int] NULL,
	[ProveedorID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[CompraID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Credenciales]    Script Date: 13/10/2024 15:38:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Credenciales](
	[CredencialID] [int] IDENTITY(1,1) NOT NULL,
	[Clave] [varbinary](1000) NOT NULL,
	[UsuarioID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[CredencialID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ediciones]    Script Date: 13/10/2024 15:38:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ediciones](
	[EdicionID] [int] IDENTITY(1,1) NOT NULL,
	[ISBN] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[EdicionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Editoriales]    Script Date: 13/10/2024 15:38:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Editoriales](
	[EditorialID] [int] IDENTITY(1,1) NOT NULL,
	[NombreEditorial] [nvarchar](100) NOT NULL,
	[DireccionEditorial] [nvarchar](255) NOT NULL,
	[TelefonoEditorial] [nvarchar](15) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[EditorialID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Estado]    Script Date: 13/10/2024 15:38:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Estado](
	[EstadoID] [int] IDENTITY(1,1) NOT NULL,
	[Estado] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[EstadoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Libros]    Script Date: 13/10/2024 15:38:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Libros](
	[LibroID] [int] IDENTITY(1,1) NOT NULL,
	[Titulo] [nvarchar](255) NOT NULL,
	[Imagen] [varbinary](max) NULL,
	[Descripcion] [nvarchar](max) NOT NULL,
	[EstadoLibro] [nvarchar](50) NOT NULL,
	[AutorID] [int] NULL,
	[EditorialID] [int] NULL,
	[CategoriaID] [int] NULL,
	[EdicionID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[LibroID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PagosPrestamos]    Script Date: 13/10/2024 15:38:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PagosPrestamos](
	[PagoID] [int] IDENTITY(1,1) NOT NULL,
	[PrecioPrestamo] [decimal](10, 2) NOT NULL,
	[ValorPagar] [decimal](10, 2) NOT NULL,
	[Estado] [nvarchar](50) NOT NULL,
	[FechaPago] [date] NOT NULL,
	[PrestamoID] [int] NULL,
	[PenalizacionID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[PagoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Penalizacion]    Script Date: 13/10/2024 15:38:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Penalizacion](
	[PenalizacionID] [int] IDENTITY(1,1) NOT NULL,
	[ValorMulta] [decimal](10, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[PenalizacionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Prestamos]    Script Date: 13/10/2024 15:38:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Prestamos](
	[PrestamoID] [int] IDENTITY(1,1) NOT NULL,
	[FechaPrestamo] [date] NOT NULL,
	[FechaDevolucion] [date] NOT NULL,
	[EstadoPrestamo] [nvarchar](50) NOT NULL,
	[TipoPrestamo] [nvarchar](50) NOT NULL,
	[TiempoEntrega] [int] NULL,
	[Renovaciones] [int] NULL,
	[FechaRenovacion] [date] NULL,
	[SolicitudID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[PrestamoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Proveedores]    Script Date: 13/10/2024 15:38:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Proveedores](
	[ProveedorID] [int] IDENTITY(1,1) NOT NULL,
	[NombreProveedor] [nvarchar](100) NOT NULL,
	[DUIProveedor] [nvarchar](10) NOT NULL,
	[TelefonoProveedor] [nvarchar](15) NOT NULL,
	[DireccionProveedor] [nvarchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ProveedorID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Solicitudes]    Script Date: 13/10/2024 15:38:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Solicitudes](
	[SolicitudID] [int] IDENTITY(1,1) NOT NULL,
	[FechaSolicitud] [date] NOT NULL,
	[EstadoSolicitud] [nvarchar](50) NOT NULL,
	[TiempoEspera] [int] NULL,
	[UsuarioID] [int] NULL,
	[LibroID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[SolicitudID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Stock]    Script Date: 13/10/2024 15:38:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Stock](
	[StockID] [int] IDENTITY(1,1) NOT NULL,
	[StockMinimo] [int] NOT NULL,
	[StockMaximo] [int] NOT NULL,
	[StockActual] [int] NOT NULL,
	[EdicionID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[StockID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TipoUsuario]    Script Date: 13/10/2024 15:38:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TipoUsuario](
	[TipoUsuarioID] [int] IDENTITY(1,1) NOT NULL,
	[Tipo] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[TipoUsuarioID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuarios]    Script Date: 13/10/2024 15:38:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuarios](
	[UsuarioID] [int] IDENTITY(1,1) NOT NULL,
	[Nombres] [nvarchar](100) NOT NULL,
	[Apellidos] [nvarchar](100) NOT NULL,
	[Direccion] [nvarchar](255) NOT NULL,
	[Correo] [nvarchar](100) NOT NULL,
	[Telefono1] [nvarchar](15) NOT NULL,
	[Telefono2] [nvarchar](15) NULL,
	[TelefonoFijo] [nvarchar](15) NULL,
	[FechaRegistro] [date] NOT NULL,
	[Carnet] [nvarchar](50) NOT NULL,
	[EstadoID] [int] NULL,
	[TipoUsuarioID] [int] NULL,
	[CarreraID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[UsuarioID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Compras]  WITH CHECK ADD FOREIGN KEY([EdicionID])
REFERENCES [dbo].[Ediciones] ([EdicionID])
GO
ALTER TABLE [dbo].[Compras]  WITH CHECK ADD FOREIGN KEY([ProveedorID])
REFERENCES [dbo].[Proveedores] ([ProveedorID])
GO
ALTER TABLE [dbo].[Credenciales]  WITH CHECK ADD FOREIGN KEY([UsuarioID])
REFERENCES [dbo].[Usuarios] ([UsuarioID])
GO
ALTER TABLE [dbo].[Libros]  WITH CHECK ADD FOREIGN KEY([AutorID])
REFERENCES [dbo].[Autores] ([AutorID])
GO
ALTER TABLE [dbo].[Libros]  WITH CHECK ADD FOREIGN KEY([CategoriaID])
REFERENCES [dbo].[Categorias] ([CategoriaID])
GO
ALTER TABLE [dbo].[Libros]  WITH CHECK ADD FOREIGN KEY([EdicionID])
REFERENCES [dbo].[Ediciones] ([EdicionID])
GO
ALTER TABLE [dbo].[Libros]  WITH CHECK ADD FOREIGN KEY([EditorialID])
REFERENCES [dbo].[Editoriales] ([EditorialID])
GO
ALTER TABLE [dbo].[PagosPrestamos]  WITH CHECK ADD FOREIGN KEY([PenalizacionID])
REFERENCES [dbo].[Penalizacion] ([PenalizacionID])
GO
ALTER TABLE [dbo].[PagosPrestamos]  WITH CHECK ADD FOREIGN KEY([PrestamoID])
REFERENCES [dbo].[Prestamos] ([PrestamoID])
GO
ALTER TABLE [dbo].[Prestamos]  WITH CHECK ADD FOREIGN KEY([SolicitudID])
REFERENCES [dbo].[Solicitudes] ([SolicitudID])
GO
ALTER TABLE [dbo].[Solicitudes]  WITH CHECK ADD FOREIGN KEY([LibroID])
REFERENCES [dbo].[Libros] ([LibroID])
GO
ALTER TABLE [dbo].[Solicitudes]  WITH CHECK ADD FOREIGN KEY([UsuarioID])
REFERENCES [dbo].[Usuarios] ([UsuarioID])
GO
ALTER TABLE [dbo].[Stock]  WITH CHECK ADD FOREIGN KEY([EdicionID])
REFERENCES [dbo].[Ediciones] ([EdicionID])
GO
ALTER TABLE [dbo].[Usuarios]  WITH CHECK ADD FOREIGN KEY([CarreraID])
REFERENCES [dbo].[Carrera] ([CarreraID])
GO
ALTER TABLE [dbo].[Usuarios]  WITH CHECK ADD FOREIGN KEY([EstadoID])
REFERENCES [dbo].[Estado] ([EstadoID])
GO
ALTER TABLE [dbo].[Usuarios]  WITH CHECK ADD FOREIGN KEY([TipoUsuarioID])
REFERENCES [dbo].[TipoUsuario] ([TipoUsuarioID])
GO
/****** Object:  StoredProcedure [dbo].[SP_ENCONTRARUSUARIO]    Script Date: 13/10/2024 15:38:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_ENCONTRARUSUARIO](
    @CARNET VARCHAR(50),
    @CLAVE VARCHAR(50)
) 
AS
BEGIN
    -- DECLARAR VARIABLE LOCAL PARA EL RESULTADO
    DECLARE @USUARIOID INT = 0;

    -- CONSULTA PARA OBTENER EL USUARIO ID VERIFICANDO CARNET EN 'USUARIOS'
    SELECT 
        @USUARIOID = ISNULL(U.UsuarioID, 0)
    FROM 
        Usuarios U
    INNER JOIN 
        Credenciales C ON U.UsuarioID = C.UsuarioID -- Relacionando UsuarioID entre Usuarios y Credenciales
    WHERE 
        U.Carnet = @CARNET -- El carnet ahora se verifica en la tabla Usuarios
        AND CONVERT(VARCHAR, DECRYPTBYPASSPHRASE('POE2024!', C.Clave)) = @CLAVE; -- La clave se sigue verificando en la tabla Credenciales

    -- RETORNAR EL USUARIOID COMO RESULTADO
    SELECT @USUARIOID AS UsuarioID;
END;
GO
/****** Object:  StoredProcedure [dbo].[SP_ObtenerDetallesLibros]    Script Date: 13/10/2024 15:38:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_ObtenerDetallesLibros]
AS
BEGIN
    -- Seleccionar los detalles de los libros junto con su stock
    SELECT 
        L.Titulo,               -- Título del libro
        A.NombreAutor AS Autor, -- Nombre del autor
        E.NombreEditorial AS Editorial, -- Nombre de la editorial
        C.NombreCategoria AS Categoria,   -- Nombre de la categoría
        Ed.ISBN AS Edicion,           -- Mostrar el ISBN en lugar de EdicionID
        S.StockActual AS StockActual       -- Stock actual del libro
    FROM 
        Libros L
    JOIN 
        Autores A ON L.AutorID = A.AutorID        
    JOIN 
        Editoriales E ON L.EditorialID = E.EditorialID 
    JOIN 
        Categorias C ON L.CategoriaID = C.CategoriaID
    JOIN 
        Stock S ON L.EdicionID = S.EdicionID  -- Obtener stock usando EdicionID
    JOIN 
        Ediciones Ed ON L.EdicionID = Ed.EdicionID;  -- Unir con la tabla de Ediciones para obtener el ISBN
END;

GO
USE [master]
GO
ALTER DATABASE [BibliotecaDBX] SET  READ_WRITE 
GO
