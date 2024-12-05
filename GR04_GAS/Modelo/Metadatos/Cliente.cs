using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Modelos
{
    [MetadataType(typeof(ClienteMetadato))]
    public partial class Cliente
    {

    }

    public class ClienteMetadato
    {
        [Required]
        [StringLength(50)]
        public string CLI_NOMBRE { get; set; }
        [Required]
        [StringLength(50)]
        public string CLI_APELLIDO { get; set; }
        [Required]
        [StringLength(10)]
        public string CLI_ID { get; set; }
        [Required]
        [EmailAddress]
        public string CLI_EMAIL { get; set; }
        [Required]
        [StringLength(10)]
        public string CLI_TEL { get; set; }
        [Required]
        [StringLength(100)]
        public string CLI_DIRECCION { get; set; }
    }
}
