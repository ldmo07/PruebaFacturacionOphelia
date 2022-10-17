using CAPA_DATOS_OPHELIA;
using MODELOS_OPHELIA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPA_NEGOCIOS_OPHELIA
{
    public class FacturacionOpheliaNegocio
    {
        /// <summary>
        /// RETORNA UNA LISTA DE PRODUCTO O UN PRODUCTO SEGUN EL ID
        /// </summary>
        /// <param name="idProducto">ID DEL PRODUCTO OPCIONAL</param>
        /// <returns>LISTA DE PRODUCTOS</returns>
        public async Task<List<Producto>> lista_precios_productos(int idProducto = 0)
        {
            using (FacturacionOpheliaData data = new FacturacionOpheliaData())
            {
                return await data.lista_precios_productos(idProducto);
            }
        }

        /// <summary>
        /// RETORNA UNA LISTA DE PRODUCTO QUE EN EL STOCK TENGA EL VALOR 5 O UN PRODUCTO SEGUN EL ID
        /// </summary>
        /// <param name="idProducto">ID DEL PRODUCTO OPCIONAL</param>
        /// <returns>LISTA DE PRODUCTOS QUE EN EL STOCK TENGA EL VALOR 5</returns>
        public async Task<List<Producto>> lista_productos_minimo_permitido(int idProducto = 0)
        {
            using (FacturacionOpheliaData data = new FacturacionOpheliaData())
            {
                return await data.lista_productos_minimo_permitido(idProducto);
            }
        }

        /// <summary>
        /// OBTIENE UNA LISTA CON EL TOTAL DE VENTAS DE PRODUCTOS SEGUN EL AÑO
        /// </summary>
        /// <param name="year">AÑO</param>
        /// <returns>LISTA CON EL TOTAL DE VENTAS DE PRODUCTOS SEGUN EL AÑO</returns>
        public async Task<List<VentasProducto>> lista_total_ventas_producto(int year = 2000)
        {
            using (FacturacionOpheliaData data = new FacturacionOpheliaData())
            {
                return await data.lista_total_ventas_producto(year);
            }
        }

        /// <summary>
        /// lista clientes no mayores de 35 años que hayan realizado compras entre el 01/02/2000 y el 25/05/2000
        /// </summary>
        /// <param name="fecha1">Fecha inicial</param>
        /// <param name="fecha2">Fecha Final</param>
        /// <returns></returns>
        public async Task<List<ClienteMenorXEdadMeses>> lista_clientes_menores_x_edad_mes(DateTime? fecha1, DateTime? fecha2, short edad = 35)
        {
            using (FacturacionOpheliaData data = new FacturacionOpheliaData())
            {
                return await data.lista_clientes_menores_x_edad_mes(fecha1, fecha2, edad);
            }
        }

        /// <summary>
        /// INSERTA UN NUEVO CLIENTE EN LA BD
        /// </summary>
        /// <param name="cliente">OBJETO DE TIPO CLIENTE</param>
        /// <returns></returns>
        public async Task insertar_cliente(Cliente cliente)
        {
            using (FacturacionOpheliaData data = new FacturacionOpheliaData())
            {
                await data.insertar_cliente(cliente);
            }
        }

        /// <summary>
        /// INSERTA UN NUEVO PRODUCTO EN LA BD
        /// </summary>
        /// <param name="producto">OBJETO DE TIPO PRODUCTO</param>
        /// <returns></returns>
        public async Task insertar_producto(Producto producto)
        {
            using (FacturacionOpheliaData data = new FacturacionOpheliaData())
            {
                await data.insertar_producto(producto);
            }
        }

        /// <summary>
        /// INSERTA UNA FACTURA
        /// </summary>
        /// <param name="factura">OBJETO DE TIPO FACTURA</param>
        /// <returns></returns>
        public async Task insertar_factura(Factura factura)
        {
            using (FacturacionOpheliaData data = new FacturacionOpheliaData())
            {
                await data.insertar_factura(factura);
            }
        }

        /// <summary>
        /// INSERTA UNA NUEVA VENTA CON SU DETALLES DE FACTURA EN BD
        /// </summary>
        /// <param name="venta">LISTA DE DETALLES DE FACTURA</param>
        /// <returns></returns>
        public async Task insertar_venta(List<DetalleFactura> ventas)
        {
            using (FacturacionOpheliaData data = new FacturacionOpheliaData())
            {
                await data.insertar_venta(ventas);
            }
        }

        /// <summary>
        /// RETORNA UNA LISTA DE CLIENTES O UN CLIENTE SEGUN EL ID
        /// </summary>
        /// <param name="idCliente">ID DEL CLIENTE OPCIONAL</param>
        /// <returns>LISTA DE CLIENTES</returns>
        public async Task<List<Cliente>> lista_clientes(int idCliente = 0)
        {
            using (FacturacionOpheliaData data = new FacturacionOpheliaData())
            {
                return await data.lista_clientes(idCliente);
            }
        }

        /// <summary>
        /// EDITA UN  CLIENTE EN LA BD
        /// </summary>
        /// <param name="cliente">OBJETO DE TIPO CLIENTE</param>
        /// <returns></returns>
        public async Task update_cliente(Cliente cliente)
        {
            using (FacturacionOpheliaData data = new FacturacionOpheliaData())
            {
                await data.update_cliente(cliente);
            }
        }


        /// <summary>
        /// EDITA UN PRODUCTO EN LA BD
        /// </summary>
        /// <param name="producto">OBJETO DE TIPO PRODUCTO</param>
        /// <returns></returns>
        public async Task update_producto(Producto producto)
        {
            using (FacturacionOpheliaData data = new FacturacionOpheliaData())
            {
                await data.update_producto(producto);
            }
        }


        }
}
