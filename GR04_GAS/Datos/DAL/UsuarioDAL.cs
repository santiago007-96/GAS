using Comun.ViewModels;
using Modelo.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.DAL
{
    public class UsuarioDAL
    {
        public static UsuarioVMR Login(string user, string password)
        {
            UsuarioVMR item = null;

            using (var db = DbConexion.Create())
            {
                /*
                var query = (from u in db.USUARIO
                             where
                                u.USU_USUARIO == user
                                && u.USU_PASSWORD == password

                             select new UsuarioVMR
                             {
                                 USU_NOMBRE = u.USU_NOMBRE,
                                 USU_APELLIDO = u.USU_APELLIDO,
                                 USU_EMAIL = u.USU_EMAIL,
                                 USU_USUARIO = u.USU_USUARIO,
                                 USU_PASSWORD = u.USU_PASSWORD,
                                 USU_ISADMIN = u.USU_ISADMIN
                             }
                            );
                */
                
                item = db.USUARIO.Where(u => u.USU_USUARIO == user && u.USU_PASSWORD == password).Select(u => new UsuarioVMR
                {
                    USU_NOMBRE = u.USU_NOMBRE,
                    USU_APELLIDO = u.USU_APELLIDO,
                    USU_EMAIL = u.USU_EMAIL,
                    USU_USUARIO = u.USU_USUARIO,
                    USU_PASSWORD = u.USU_PASSWORD,
                    USU_ISADMIN = u.USU_ISADMIN
                }).FirstOrDefault();
                
                //item = query.FirstOrDefault();
            }
            
            return item;
        }
    }
}
