using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comun.ViewModels
{
    public class FacturaVMR
    {
        /*
        public long codigo { get; set; }
        public long cli_codigo { get; set; }
        public string numero { get; set; }
        public System.DateTime fecha { get; set; }
        public decimal montoTotal { get; set; }
        */
        public long FACT_CODIGO { get; set; }
        public long CLI_CODIGO { get; set; }
        public string CLI_NOMBRE { get; set; }
        public string CLI_APELLIDO { get; set; }
        public string CLI_ID { get; set; }
        public string CLI_EMAIL { get; set; }
        public string FACT_NUMERO { get; set; }
        public long FACT_CANTIDAD { get; set; }
        public System.DateTime FACT_FECHA { get; set; }
        public decimal FACT_MONTOTOTAL { get; set; }

    }
}
