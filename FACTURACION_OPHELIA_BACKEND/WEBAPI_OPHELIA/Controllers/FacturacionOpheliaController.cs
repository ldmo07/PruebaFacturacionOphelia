using CAPA_NEGOCIOS_OPHELIA;
using MODELOS_OPHELIA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace WEBAPI_OPHELIA.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "GET,POST,PUT,DELETE")]
    public class FacturacionOpheliaController : ApiController
    {
        #region INSTANCIAS
        //instancio la capa de negocios
        FacturacionOpheliaNegocio negocios = new FacturacionOpheliaNegocio();
        #endregion

        [HttpGet]
        [Route("Api/Ophelia/listaPreciosProductos")]
        public async Task<HttpResponseMessage> lista_precios_productos(int idProducto=0)
        {
            //Api/Ophelia/listaPreciosProductos
            //Api/Ophelia/listaPreciosProductos?idProducto=1
            try
            {
                var lstProductos = await negocios.lista_precios_productos(idProducto);
                return await Task.FromResult(JsonResponse.Response(HttpStatusCode.OK, lstProductos));
            }
            catch (Exception e)
            {
                MensajeRespuesta mensajeRespuesta = new MensajeRespuesta() { Id = "400",  Mensaje = "Fallo obteniendo la data" };
                return await Task.FromResult(JsonResponse.Response(HttpStatusCode.NotFound, mensajeRespuesta));
            }
        }

        [HttpGet]
        [Route("Api/Ophelia/listaProductosMinimoPermitido")]
        public async Task<HttpResponseMessage> lista_productos_minimo_permitido(int idProducto = 0)
        {
            //Ejemplos de endPoints:
            //Api/Ophelia/listaProductosMinimoPermitido
            //Api/Ophelia/listaProductosMinimoPermitido?idProducto = 2

            try
            {
                var lstProductos = await negocios.lista_productos_minimo_permitido(idProducto);
                return await Task.FromResult(JsonResponse.Response(HttpStatusCode.OK, lstProductos));
            }
            catch (Exception e)
            {
                MensajeRespuesta mensajeRespuesta = new MensajeRespuesta() { Id = "400", Mensaje = "Fallo obteniendo la data" };
                return await Task.FromResult(JsonResponse.Response(HttpStatusCode.NotFound, mensajeRespuesta));
            }
        }

        [HttpGet]
        [Route("Api/Ophelia/listaTotalVentasProducto")]
        public async Task<HttpResponseMessage> lista_total_ventas_producto(int year = 2000)
        {
            //Ejemplos de endPoints:
            //Api/Ophelia/listaTotalVentasProducto
            //Api/Ophelia/listaTotalVentasProducto?year=2002

            try
            {
                var lstVentasProductos = await negocios.lista_total_ventas_producto(year);
                return await Task.FromResult(JsonResponse.Response(HttpStatusCode.OK, lstVentasProductos));
            }
            catch (Exception e)
            {
                MensajeRespuesta mensajeRespuesta = new MensajeRespuesta() { Id = "400", Mensaje = "Fallo obteniendo la data" };
                return await Task.FromResult(JsonResponse.Response(HttpStatusCode.NotFound, mensajeRespuesta));
            }
        }

        [HttpGet]
        [Route("Api/Ophelia/listaClientesMenorEdadMes")]
        public async Task<HttpResponseMessage> lista_clientes_menores_x_edad_mes(DateTime? fecha1=null, DateTime? fecha2=null,short edad=35)
        {
            //Ejemplos de endPoints:
            //Api/Ophelia/listaClientesMenorEdadMes?fecha1=2000-02-01&fecha2=2000-05-25
            //Api/Ophelia/listaClientesMenorEdadMes?fecha1=2000-02-01&fecha2=2000-05-25&edad=35
           
            try
            {
                var lstClientes = await negocios.lista_clientes_menores_x_edad_mes(fecha1,fecha2,edad);
                return await Task.FromResult(JsonResponse.Response(HttpStatusCode.OK, lstClientes));
            }
            catch (Exception e)
            {
                MensajeRespuesta mensajeRespuesta = new MensajeRespuesta() { Id = "400", Mensaje = "Fallo obteniendo la data" };
                return await Task.FromResult(JsonResponse.Response(HttpStatusCode.NotFound, mensajeRespuesta));
            }
        }

        [HttpPost]
        [Route("Api/Ophelia/InsertarCliente")]
        public async Task<HttpResponseMessage> insertar_cliente(Cliente cliente)
        {
            try
            {
                await negocios.insertar_cliente(cliente);
                MensajeRespuesta mensajeRespuesta = new MensajeRespuesta() { Id = "200", Mensaje = "ok", Detalle="Cliente insertado con exito" };
                return await Task.FromResult(JsonResponse.Response(HttpStatusCode.OK, mensajeRespuesta));
            }
            catch (Exception e)
            {

                MensajeRespuesta mensajeRespuesta = new MensajeRespuesta() { Id = "400", Mensaje = "Fallo insertando la data" };
                return await Task.FromResult(JsonResponse.Response(HttpStatusCode.NotFound, mensajeRespuesta));
            }
        }

        [HttpPost]
        [Route("Api/Ophelia/InsertarProducto")]
        public async Task<HttpResponseMessage> insertar_producto(Producto producto)
        {
            try
            {
                await negocios.insertar_producto(producto);
                MensajeRespuesta mensajeRespuesta = new MensajeRespuesta() { Id = "200", Mensaje = "ok", Detalle = "Producto insertado con exito" };
                return await Task.FromResult(JsonResponse.Response(HttpStatusCode.OK, mensajeRespuesta));
            }
            catch (Exception e)
            {

                MensajeRespuesta mensajeRespuesta = new MensajeRespuesta() { Id = "400", Mensaje = "Fallo insertando la data" };
                return await Task.FromResult(JsonResponse.Response(HttpStatusCode.NotFound, mensajeRespuesta));
            }
        }

        [HttpPost]
        [Route("Api/Ophelia/InsertarFactura")]
        public async Task<HttpResponseMessage> insertar_factura(Factura factura)
        {
            try
            {
                await negocios.insertar_factura(factura);
                MensajeRespuesta mensajeRespuesta = new MensajeRespuesta() { Id = "200", Mensaje = "ok", Detalle = "Factura insertada con exito" };
                return await Task.FromResult(JsonResponse.Response(HttpStatusCode.OK, mensajeRespuesta));
            }
            catch (Exception e)
            {

                MensajeRespuesta mensajeRespuesta = new MensajeRespuesta() { Id = "400", Mensaje = "Fallo insertando la data" };
                return await Task.FromResult(JsonResponse.Response(HttpStatusCode.NotFound, mensajeRespuesta));
            }
        }

        [HttpPost]
        [Route("Api/Ophelia/InsertarVenta")]
        public async Task<HttpResponseMessage> insertar_venta(List<DetalleFactura> ventas)
        {
            try
            {
                await negocios.insertar_venta(ventas);
                MensajeRespuesta mensajeRespuesta = new MensajeRespuesta() { Id = "200", Mensaje = "ok", Detalle = "Venta insertada con exito" };
                return await Task.FromResult(JsonResponse.Response(HttpStatusCode.OK, mensajeRespuesta));
            }
            catch (Exception e)
            {

                MensajeRespuesta mensajeRespuesta = new MensajeRespuesta() { Id = "400", Mensaje = "Fallo insertando la data" };
                return await Task.FromResult(JsonResponse.Response(HttpStatusCode.NotFound, mensajeRespuesta));
            }
        }

        [HttpGet]
        [Route("Api/Ophelia/listaClientes")]
        public async Task<HttpResponseMessage> lista_clientes(int idCliente = 0)
        {
            //Api/Ophelia/listaClientes
            //Api/Ophelia/listaClientes?idCliente=1
            try
            {
                var lstClientes = await negocios.lista_clientes(idCliente);
                return await Task.FromResult(JsonResponse.Response(HttpStatusCode.OK, lstClientes));
            }
            catch (Exception e)
            {
                MensajeRespuesta mensajeRespuesta = new MensajeRespuesta() { Id = "400", Mensaje = "Fallo obteniendo la data" };
                return await Task.FromResult(JsonResponse.Response(HttpStatusCode.NotFound, mensajeRespuesta));
            }
        }

        [HttpPut]
        [Route("Api/Ophelia/UpdateCliente")]
        public async Task<HttpResponseMessage> update_cliente(Cliente cliente)
        {
            try
            {
                await negocios.update_cliente(cliente);
                MensajeRespuesta mensajeRespuesta = new MensajeRespuesta() { Id = "200", Mensaje = "ok", Detalle = "Cliente editado exitosamente" };
                return await Task.FromResult(JsonResponse.Response(HttpStatusCode.OK, mensajeRespuesta));
            }
            catch (Exception e)
            {

                MensajeRespuesta mensajeRespuesta = new MensajeRespuesta() { Id = "400", Mensaje = "Fallo editando el cliente" };
                return await Task.FromResult(JsonResponse.Response(HttpStatusCode.NotFound, mensajeRespuesta));
            }
        }

        [HttpPut]
        [Route("Api/Ophelia/UpdateProducto")]
        public async Task<HttpResponseMessage> update_Producto(Producto producto)
        {
            try
            {
                await negocios.update_producto(producto);
                MensajeRespuesta mensajeRespuesta = new MensajeRespuesta() { Id = "200", Mensaje = "ok", Detalle = "Producto editado exitosamente" };
                return await Task.FromResult(JsonResponse.Response(HttpStatusCode.OK, mensajeRespuesta));
            }
            catch (Exception e)
            {

                MensajeRespuesta mensajeRespuesta = new MensajeRespuesta() { Id = "400", Mensaje = "Fallo editando el Producto" };
                return await Task.FromResult(JsonResponse.Response(HttpStatusCode.NotFound, mensajeRespuesta));
            }
        }

    }
}
