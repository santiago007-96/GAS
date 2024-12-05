using Comun.ViewModels;
using Datos.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica.BLL
{
    public class UsuarioBLL
    {
        public static UsuarioVMR Login(string user, string password)
        {
            return UsuarioDAL.Login(user, password);
        }
    }
}
