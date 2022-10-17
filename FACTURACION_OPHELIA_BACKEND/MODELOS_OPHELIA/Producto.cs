using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODELOS_OPHELIA
{
   public class Producto
    {
        public int idProducto { get; set; }
		public string nombre { get; set; }
        public decimal precio { get; set; }
        public int stock { get; set; }
    }
}
