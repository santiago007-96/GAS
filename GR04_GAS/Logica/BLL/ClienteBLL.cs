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
    public class ClienteBLL
    {
        public static ListadoPaginadoVMR<ClienteVMR> LeerTodos(int cantidad, int pagina, string textoBusqueda)
        {
            return ClienteDAL.LeerTodos(cantidad, pagina, textoBusqueda);   
        }

        public static ClienteVMR LeerUno(long codigo)
        {
            return ClienteDAL.LeerUno(codigo);
        }

        public static ClienteVMR clienteRegistrado(string ID)
        {
            return ClienteDAL.clienteRegistrado(ID);
        }

        public static long Crear(CLIENTE item)
        {
            return ClienteDAL.Crear(item);
        }

        public static void Actualizar(ClienteVMR item)
        {
            ClienteDAL.Actualizar(item);
        }

        public static void Eliminar(List<long> codigos)
        {
            ClienteDAL.Eliminar(codigos);
        }
    }
}
