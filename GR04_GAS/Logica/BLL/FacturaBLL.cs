using Comun.ViewModels;
using Datos.DAL;
using Modelo.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica.BLL
{
    public class FacturaBLL
    {
        public static ListadoPaginadoVMR<FacturaVMR> LeerTodos(int cantidad, int pagina, string textoBusqueda)
        {
            return FacturaDAL.LeerTodos(cantidad, pagina, textoBusqueda);    
        }

        public static FacturaVMR LeerUno(long codigo)
        {
            return FacturaDAL.LeerUno(codigo);
        }

        public static FacturaVMR facturaRegistrada(string numeroFactura)
        {
            return FacturaDAL.facturaRegistrada(numeroFactura);
        }

        public static long Crear(FACTURA item)
        {
            return FacturaDAL.Crear(item);   
        }

        public static void Actualizar(FacturaVMR item)
        {
            FacturaDAL.Actualizar(item);
        }

        public static void Eliminar(List<long> codigos)
        {
            FacturaDAL.Eliminar(codigos);
        }
    }
}
