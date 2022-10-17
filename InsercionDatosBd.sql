USE [FacturacionOphelia]
GO

--LIMPIO LA TABLAS Y REINICIO LOS IDENTITIS SI TIENEN DATOS 
IF( (Select COUNT (idCliente) FROM Cliente ) > 0)
BEGIN

	--Limpio las Tablas
	delete from DetalleFactura
	delete from Factura
	delete from Producto
	delete from Cliente

	--reinicio los identitys
	DBCC CHECKIDENT (DetalleFactura, RESEED, 0)
	DBCC CHECKIDENT (Factura, RESEED, 0)
	DBCC CHECKIDENT (Producto, RESEED, 0)
	DBCC CHECKIDENT (Cliente, RESEED, 0)

END

--inserto datos
INSERT INTO [dbo].[Cliente] 
           ([nombre],[apellido] ,[edad] ,[direccion]) 
     VALUES ('Luis David' ,'Mercado Ortega' ,26,'Monteria Cll#64-3'),
	  ('Maria Jose' ,'Vega Mercado' ,24,'Monteria Cll#4-3'),
	  ('Pedro Luis' ,'Bruno Garcia' ,26,'Cerete Cll#34-32'),
	  ('Andres David' ,'Durango Perez' ,40,'Bogota Cll#41-31')

INSERT INTO [dbo].[Producto]
           ([nombre] ,[precio], [stock])
     VALUES
           ('Coca-cola' ,2000 ,10),
		   ('Chocorramo' ,1500 ,5),
		   ('Gol' ,1000 ,5),
		   ('Jumbo' ,2000 ,20)


INSERT INTO [dbo].[Factura]
           ([fechaCompra]
           ,[idCliente])
     VALUES
           ('2000-02-05',1),
		   ('2000-04-05',2),
		   ('2000-06-01',3),
		   ('2000-03-03',4)


INSERT INTO [dbo].[DetalleFactura]
           ([idFactura]
           ,[idProducto]
           ,[cantidad])
     VALUES
           (1,1,10),
		   (1,2,2),
		   (1,3,4),
		   (1,4,5),
		   (2,1,10),
		   (2,2,2),
		   (2,3,4),
		   (3,3,4),
		   (3,4,5),
		   (4,3,4),
		   (4,3,4),
		   (4,4,5)

GO







		   select * from Producto 
		   select * from Cliente
		   select * from Factura
		   select * from DetalleFactura




