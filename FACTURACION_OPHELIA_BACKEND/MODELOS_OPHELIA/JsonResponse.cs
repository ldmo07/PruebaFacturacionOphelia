using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace MODELOS_OPHELIA
{
    public class JsonResponse
    {
        /// <summary>
        /// MANEJARA LAS RESPUESTAS DE LA PETICION
        /// </summary>
        /// <param name="estadoHttp">ESATUS CODE HTTP</param>
        /// <param name="datos">DATOS </param>
        /// <returns></returns>
        public static HttpResponseMessage Response(HttpStatusCode estadoHttp, Object datos)
        {
            string respuesta = JsonConvert.SerializeObject(datos);
            var content = new StringContent(respuesta);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = new HttpResponseMessage(estadoHttp) { Content = content };
            return response;
        }
    }
}
