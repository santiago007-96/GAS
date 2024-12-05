using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Modelos
{
    [MetadataType(typeof(FacturaMetadato))]
    public partial class Factura
    {

    }

    public class FacturaMetadato
    {
        [Required]
        public string FACT_NUMERO { get; set; }
        [Required]
        public long CLI_CODIGO { get; set; }
        [Required]
        public System.DateTime FACT_FECHA { get; set; }
        [Required]
        [Range(0, 99999999999.99)]
        public decimal FACT_MONTOTOTAL { get; set; }
        [Required]
        public bool FACT_BORRADO { get; set; }
        [Required]
        public long FACT_CANTIDAD { get; set; }
    }
}
