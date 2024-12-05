using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Modelos
{
    [MetadataType(typeof(UsuarioMetadato))]
    public partial class Usuario
    {

    }

    public class UsuarioMetadato
    {
        [Required]
        [StringLength(50)]
        public string USU_NOMBRE { get; set; }
        [Required]
        [StringLength(50)]
        public string USU_APELLIDO { get; set; }
        [Required]
        [StringLength(10)]
        public string USU_CEDULA { get; set; }
        [Required]
        [StringLength(100)]
        public string USU_DIRECCION { get; set; }
        [Required]
        [StringLength(10)]
        public string USU_TELEFONO { get; set; }
        [Required]
        [EmailAddress]
        public string USU_EMAIL { get; set; }
        [Required]
        public string USU_USUARIO { get; set; }
        [Required]
        public string USU_PASSWORD { get; set; }
        [Required]
        public bool USU_ISADMIN { get; set; }
    }
}
