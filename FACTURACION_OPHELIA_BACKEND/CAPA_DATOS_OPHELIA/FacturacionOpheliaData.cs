using Dapper;
using MODELOS_OPHELIA;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPA_DATOS_OPHELIA
{
    public class FacturacionOpheliaData : IDisposable
    {
        //LEO LA CADENA DE CONEXION DEL WEBCONFIG
        string cadenaConexion = ConfigurationManager.AppSettings["cadenaConexion"];
        public void Dispose()
        {
        
        }

        /// <summary>
        /// RETORNA UNA LISTA DE PRODUCTO O UN PRODUCTO SEGUN EL ID
        /// </summary>
        /// <param name="idProducto">ID DEL PRODUCTO OPCIONAL</param>
        /// <returns>LISTA DE PRODUCTOS</returns>
        public async Task<List<Producto>> lista_precios_productos(int idProducto = 0)
        {
            List<Producto> lstProductos = new List<Producto>();
            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                var param = new { idProducto = idProducto };
                var lstDapper = await connection.QueryAsync<Producto>("SP_LISTA_PRECIOS_PRODUCTOS", param, commandType: CommandType.StoredProcedure);
                lstProductos = lstDapper.ToList();
            }
            return lstProductos;
        }

        /// <summary>
        /// RETORNA UNA LISTA DE PRODUCTO QUE EN EL STOCK TENGA EL VALOR 5 O UN PRODUCTO SEGUN EL ID
        /// </summary>
        /// <param name="idProducto">ID DEL PRODUCTO OPCIONAL</param>
        /// <returns>LISTA DE PRODUCTOS QUE EN EL STOCK TENGA EL VALOR 5</returns>
        public async Task<List<Producto>> lista_productos_minimo_permitido(int idProducto = 0)
        {
            List<Producto> lstProductos = new List<Producto>();
            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                var param = new { idProducto = idProducto };
                var lstDaper = await connection.QueryAsync<Producto>("SP_LISTA_PRODUCTOS_MINIMO_PERMITIDO", param, commandType: CommandType.StoredProcedure);
                lstProductos = lstDaper.ToList();
            }
            return lstProductos;
        }

        /// <summary>
        /// OBTIENE UNA LISTA CON EL TOTAL DE VENTAS DE PRODUCTOS SEGUN EL AÑO
        /// </summary>
        /// <param name="year">AÑO</param>
        /// <returns>LISTA CON EL TOTAL DE VENTAS DE PRODUCTOS SEGUN EL AÑO</returns>
        public async Task<List<VentasProducto>> lista_total_ventas_producto(int year = 2000)
        {
            List<VentasProducto> lstVentasProductos = new List<VentasProducto>();
            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                var param = new { year = year };
                var lstDaper = await connection.QueryAsync<VentasProducto>("SP_LISTA_TOTAL_VENTAS_PRODUCTO", param, commandType: CommandType.StoredProcedure);
                lstVentasProductos = lstDaper.ToList();
            }
            return lstVentasProductos;
        }

        /// <summary>
        /// lista clientes no mayores de 35 años que hayan realizado compras entre el 01/02/2000 y el 25/05/2000
        /// </summary>
        /// <param name="fecha1">Fecha inicial</param>
        /// <param name="fecha2">Fecha Final</param>
        /// <returns></returns>
        public async Task<List<ClienteMenorXEdadMeses>> lista_clientes_menores_x_edad_mes(DateTime? fecha1 , DateTime? fecha2, short edad = 35)
        {
            List<ClienteMenorXEdadMeses> lstClientes = new List<ClienteMenorXEdadMeses>();
            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                var param = new { fecha1 = fecha1,fecha2 = fecha2 ,edad=edad};
                var lstDaper = await connection.QueryAsync<ClienteMenorXEdadMeses>("SP_LISTA_CLIENTES_MENORES_35_FEBR_MAY", param, commandType: CommandType.StoredProcedure);
                lstClientes = lstDaper.ToList();
            }
            return lstClientes;
        }

        /// <summary>
        /// INSERTA UN NUEVO CLIENTE EN LA BD
        /// </summary>
        /// <param name="cliente">OBJETO DE TIPO CLIENTE</param>
        /// <returns></returns>
        public async Task insertar_cliente(Cliente cliente)
        {
            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                var param = new { 
                    nombre = cliente.nombre , 
                    apellido = cliente.apellido,
                    edad = cliente.edad,
                    direccion = cliente.direccion 
                };

                await connection.ExecuteAsync("SP_INSERT_CLIENTE", param, commandType: CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// INSERTA UN NUEVO PRODUCTO EN LA BD
        /// </summary>
        /// <param name="producto">OBJETO DE TIPO PRODUCTO</param>
        /// <returns></returns>
        public async Task insertar_producto(Producto producto)
        {
            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                var param = new
                {
                    nombre = producto.nombre,
                    precio = producto.precio,
                    stock = producto.stock
                };

                await connection.ExecuteAsync("SP_INSERT_PRODUCTO", param, commandType: CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// INSERTA UNA FACTURA
        /// </summary>
        /// <param name="factura">OBJETO DE TIPO FACTURA</param>
        /// <returns></returns>
        public async Task insertar_factura(Factura factura)
        {
            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                //abro la conexion e inicio la transaccion
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    var param = new
                    {
                        fechaCompra = factura.fechaCompra,
                        idCliente = factura.idCliente
                    };

                    await connection.ExecuteAsync("SP_INSERT_FACTURA", param, commandType: CommandType.StoredProcedure, transaction: transaction);
                    //confirmo la transaccion
                    transaction.Commit();
                }
                   
            }
        }

        /// <summary>
        /// INSERTA UNA NUEVA VENTA CON SU DETALLES DE FACTURA EN BD
        /// </summary>
        /// <param name="ventas">LISTA DE DETALLES DE FACTURA</param>
        /// <returns></returns>
        public async Task insertar_venta(List<DetalleFactura> ventas)
        {
            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                //abro la conexion e inicio la transaccion
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    //recorro la lista de ventas de la factura para ir agregandolas 1 a 1
                    foreach (var venta in ventas)
                    {
                        var param = new
                        {
                            idProducto = venta.idProducto,
                            cantidad = venta.cantidad
                        };

                        await connection.ExecuteAsync("SP_INSERT_VENTA", param, commandType: CommandType.StoredProcedure, transaction: transaction);
                    }
                    //confirmo la transaccion
                    transaction.Commit();
                }
                                      
            }
        }

        /// <summary>
        /// RETORNA UNA LISTA DE CLIENTES O UN CLIENTE SEGUN EL ID
        /// </summary>
        /// <param name="idCliente">ID DEL CLIENTE OPCIONAL</param>
        /// <returns>LISTA DE CLIENTES</returns>
        public async Task<List<Cliente>> lista_clientes(int idCliente = 0)
        {
            List<Cliente> lstClientes = new List<Cliente>();
            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                var param = new { idCliente = idCliente };
                var lstDapper = await connection.QueryAsync<Cliente>("SP_LISTA_CLIENTES", param, commandType: CommandType.StoredProcedure);
                lstClientes = lstDapper.ToList();
            }
            return lstClientes;
        }

        /// <summary>
        /// EDITA UN  CLIENTE EN LA BD
        /// </summary>
        /// <param name="cliente">OBJETO DE TIPO CLIENTE</param>
        /// <returns></returns>
        public async Task update_cliente(Cliente cliente)
        {
            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                var param = new
                {
                    idCliente = cliente.idCliente,
                    nombre = cliente.nombre,
                    apellido = cliente.apellido,
                    edad = cliente.edad,
                    direccion = cliente.direccion
                };

                await connection.ExecuteAsync("SP_UPDATE_CLIENTE", param, commandType: CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// EDITA UN PRODUCTO EN LA BD
        /// </summary>
        /// <param name="producto">OBJETO DE TIPO PRODUCTO</param>
        /// <returns></returns>
        public async Task update_producto(Producto producto)
        {
            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                var param = new
                {
                    idProducto = producto.idProducto,
                    nombre = producto.nombre,
                    precio = producto.precio,
                    stock = producto.stock
                };

                await connection.ExecuteAsync("SP_UPDATE_PRODUCTO", param, commandType: CommandType.StoredProcedure);
            }
        }


    }
}
