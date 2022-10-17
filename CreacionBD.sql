
--Creo la bd si no existe
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'FacturacionOphelia')
BEGIN
  CREATE DATABASE FacturacionOphelia;
END;
GO

IF  EXISTS (SELECT * FROM sys.databases WHERE name = 'FacturacionOphelia')
BEGIN
  Use FacturacionOphelia
END;
GO


--creo la tabla cliente
CREATE TABLE Cliente (
    idCliente INT IDENTITY(1,1),
    nombre NVARCHAR (100),
	apellido NVARCHAR (100),
    edad TINYINT,
	direccion NVARCHAR(200),
	CONSTRAINT PK_Cliente PRIMARY KEY (idCliente)
);

--creo la tabla Producto
CREATE TABLE Producto (
    idProducto INT IDENTITY(1,1),
    nombre NVARCHAR(100),
	precio MONEY,
	stock INT,
	CONSTRAINT PK_Producto PRIMARY KEY (idProducto)
);

--creo la tabla Factura
CREATE TABLE Factura (
    idFactura INT IDENTITY(1,1),
    fechaCompra DATETIME,
	idCliente INT,
	CONSTRAINT PK_Factura PRIMARY KEY (idFactura),
	CONSTRAINT FK_Factura FOREIGN KEY (idCliente) REFERENCES Cliente(idCliente)
);

--creo la tabla Producto
CREATE TABLE DetalleFactura (
    idDetalleFactura INT IDENTITY(1,1),
    idFactura INT,
	idProducto INT,
	cantidad INT,
	CONSTRAINT PK_DetalleFactura PRIMARY KEY (idDetalleFactura),
	CONSTRAINT FK_DetalleFactura_Producto FOREIGN KEY (idProducto) REFERENCES Producto(idProducto),
	CONSTRAINT FK_DetalleFactura_Factura FOREIGN KEY (idFactura) REFERENCES Factura(idFactura)
);


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Author:		LUIS DAVID MERCADO ORTEGA
-- Create date: 13/10/2022
-- Description:	Obtiene la lista de precios de todos los productos
-- =============================================
CREATE PROCEDURE SP_LISTA_PRECIOS_PRODUCTOS
	-- Add the parameters for the stored procedure here
	@idProducto int = 0
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	IF (@idProducto=0)
	BEGIN
		SELECT [idProducto]
		  ,[nombre]
		  ,[precio]
		  ,[stock]
		FROM [dbo].[Producto]
	END
	ELSE
	BEGIN
		SELECT [idProducto]
		  ,[nombre]
		  ,[precio]
		  ,[stock]
		FROM [dbo].[Producto] WHERE idProducto = @idProducto
	END
END
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Author:		LUIS DAVID MERCADO ORTEGA
-- Create date: 13/10/2022
-- Description:	lista productos cuya existencia en el inventario haya llegado al mínimo permitido (5 unidades)
-- =============================================
CREATE PROCEDURE SP_LISTA_PRODUCTOS_MINIMO_PERMITIDO
	-- Add the parameters for the stored procedure here
	@idProducto INT = 0
AS
BEGIN
	SET NOCOUNT ON;

    IF (@idProducto = 0)
	BEGIN
		SELECT [idProducto]
		  ,[nombre]
		  ,[precio]
		  ,[stock]
		FROM [dbo].[Producto] WHERE stock =5
	END
	ELSE
	BEGIN
		SELECT [idProducto]
		  ,[nombre]
		  ,[precio]
		  ,[stock]
		FROM [dbo].[Producto] WHERE idProducto = @idProducto AND stock =5
	END

END
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Author:		LUIS DAVID MERCADO ORTEGA
-- Create date: 13/10/2022
-- Description:	lista clientes no mayores de 35 años que hayan realizado compras entre el 01/02/2000 y el 25/05/2000
-- =============================================
CREATE PROCEDURE SP_LISTA_CLIENTES_MENORES_35_FEBR_MAY
	@fecha1 DATE = '2000-02-01',
	@fecha2 DATE = '2000-05-25',
	@edad TINYINT = 35
AS
BEGIN
	SET NOCOUNT ON;

    SELECT c.idCliente, CONCAT(c.nombre,' ',c.apellido)AS cliente,f.fechaCompra,c.edad
	FROM Cliente AS c 
	INNER JOIN FACTURA AS f 
	ON  f.idCliente = c.idCliente
	WHERE c.edad<@edad
	AND f.fechaCompra BETWEEN @fecha1 AND @fecha2
END
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Author:		LUIS DAVID MERCADO ORTEGA
-- Create date: 13/10/2022
-- Description:	Obtiene el valor total vendido por cada producto en el año 2000
-- =============================================
CREATE PROCEDURE SP_LISTA_TOTAL_VENTAS_PRODUCTO
	@year int = '2000'
AS
BEGIN
	SET NOCOUNT ON;

    SELECT p.nombre,MAX(p.precio) AS precio ,SUM(df.cantidad) As cantidad
	,MAX(f.fechaCompra) As fechaCompra, SUM(df.cantidad*p.precio)AS total
	FROM Producto AS p 
	INNER JOIN DetalleFactura AS df 
	ON  df.idProducto = p.idProducto
	INNER JOIN Factura AS f
	ON df.idFactura = f.idFactura
	WHERE DATEPART(YEAR, f.fechaCompra) = @year
	GROUP BY P.nombre
END
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Author:		LUIS DAVID MERCADO ORTEGA
-- Create date: 14/10/2022
-- Description:	INSERTA UN CLIENTE
-- =============================================
CREATE PROCEDURE SP_INSERT_CLIENTE 
	-- Add the parameters for the stored procedure here
	@nombre NVARCHAR(150)=NULL, 
	@apellido NVARCHAR(150)=NULL,
	@edad TINYINT = 0,
	@direccion NVARCHAR(150)=NULL
AS
BEGIN

	SET NOCOUNT ON;

INSERT INTO [dbo].[Cliente]
           ([nombre]
           ,[apellido]
           ,[edad]
           ,[direccion])
     VALUES
           (@nombre, 
            @apellido, 
            @edad, 
            @direccion )

END
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Author:		LUIS DAVID MERCADO ORTEGA
-- Create date: 14/10/2022
-- Description:	INSERTA UN PRODUCTO
-- =============================================
CREATE PROCEDURE SP_INSERT_PRODUCTO
	-- Add the parameters for the stored procedure here
	@nombre NVARCHAR(100),
    @precio MONEY,
    @stock INT
AS
BEGIN

	SET NOCOUNT ON;

INSERT INTO [dbo].[Producto]
           ([nombre]
           ,[precio]
           ,[stock])
     VALUES
           (@nombre
           ,@precio
           ,@stock)
END
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Author:		LUIS DAVID MERCADO ORTEGA
-- Create date: 14/10/2022
-- Description:	INSERTA UNA FACTURA
-- =============================================
CREATE PROCEDURE SP_INSERT_FACTURA
	-- Add the parameters for the stored procedure here
	@fechaCompra DATETIME,
    @idCliente INT
AS
BEGIN

	SET NOCOUNT ON;

INSERT INTO [dbo].[Factura]
           ([fechaCompra]
           ,[idCliente])
     VALUES
           (@fechaCompra,
            @idCliente)
END
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Author:		LUIS DAVID MERCADO ORTEGA
-- Create date: 14/10/2022
-- Description:	INSERTA UNA VENTA O UN DETALLE DE FACTURA
-- =============================================
CREATE PROCEDURE SP_INSERT_VENTA
	-- Add the parameters for the stored procedure here
	@idProducto INT, 
    @cantidad INT
AS
BEGIN

	SET NOCOUNT ON;

	--OBTENGO EL ULTIMO ID INSERTADO EN UNA FACTURA
	DECLARE @idFactura INT = ( SELECT IDENT_CURRENT('Factura') )

	INSERT INTO [dbo].[DetalleFactura]
           ([idFactura]
           ,[idProducto]
           ,[cantidad])
     VALUES
           (@idFactura
           ,@idProducto
           ,@cantidad)

END
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Author:		LUIS DAVID MERCADO ORTEGA
-- Create date: 14/10/2022
-- Description:	LISTA LOS CLIENTES
-- =============================================
CREATE PROCEDURE SP_LISTA_CLIENTES

	@idCliente INT = 0 
AS
BEGIN
	
	SET NOCOUNT ON;

	IF(@idCliente = 0)
	BEGIN

		SELECT [idCliente]
			  ,[nombre]
			  ,[apellido]
			  ,[edad]
			  ,[direccion]
		  FROM [dbo].[Cliente]

	END
	ELSE
	BEGIN
	SELECT [idCliente]
			  ,[nombre]
			  ,[apellido]
			  ,[edad]
			  ,[direccion]
		  FROM [dbo].[Cliente] WHERE idCliente = @idCliente
	END
END
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Author:		LUIS DAVID MERCADO ORTEGA
-- Create date: 17/10/2022
-- Description:	EDITA UN PRODUCTO
-- =============================================
CREATE PROCEDURE SP_UPDATE_PRODUCTO
	-- Add the parameters for the stored procedure here
	@idProducto int,
	@nombre NVARCHAR(100),
    @precio MONEY,
    @stock INT
AS
BEGIN

	SET NOCOUNT ON;

UPDATE [dbo].[Producto] SET
           [nombre] = @nombre
           ,[precio] = @precio
           ,[stock] = @stock 
		   WHERE idProducto = @idProducto
END
GO



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Author:		LUIS DAVID MERCADO ORTEGA
-- Create date: 17/10/2022
-- Description:	EDITA UN CLIENTE
-- =============================================
CREATE PROCEDURE SP_UPDATE_CLIENTE 
	-- Add the parameters for the stored procedure here
	@idCliente int,
	@nombre NVARCHAR(150)=NULL, 
	@apellido NVARCHAR(150)=NULL,
	@edad TINYINT = 0,
	@direccion NVARCHAR(150)=NULL
AS
BEGIN

	SET NOCOUNT ON;

UPDATE [dbo].[Cliente] SET
           [nombre] = @nombre
           ,[apellido] = @apellido
           ,[edad] = @edad
           ,[direccion] = @direccion
     WHERE idCliente = @idCliente

END
GO