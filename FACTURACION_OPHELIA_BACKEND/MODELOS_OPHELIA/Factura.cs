using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODELOS_OPHELIA
{
    public class Factura
    {
        public int idFactura { get; set; }
        public DateTime fechaCompra { get; set; }
        public int idCliente { get; set; }
    }
}
