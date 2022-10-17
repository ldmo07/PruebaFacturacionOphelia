using System;
using System.Runtime.Serialization;

namespace MODELOS_OPHELIA
{
    public class MensajeRespuesta
    {
        [DataMember]
        public string Id { get; set; }
        [DataMember]
        public string Mensaje { get; set; }
        [DataMember]
        public string Detalle { get; set; }
    }
}
