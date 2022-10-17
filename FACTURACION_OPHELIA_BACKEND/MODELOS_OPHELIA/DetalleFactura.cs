using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODELOS_OPHELIA
{
    public class DetalleFactura
    {
        public int? idDetalleFactura { get; set; }
        public int? idFactura { get; set; }
        public int idProducto { get; set; }
        public int cantidad { get; set; }

    }
}
