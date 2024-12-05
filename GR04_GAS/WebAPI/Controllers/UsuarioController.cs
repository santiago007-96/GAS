using Comun.ViewModels;
using Logica.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace WebAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UsuarioController : ApiController
    {
        [HttpPost]
        public IHttpActionResult Login(UsuarioVMR userData)
        {
            var respuesta = new RespuestaVMR<UsuarioVMR>();
            var user = userData.USU_USUARIO;
            var password = userData.USU_PASSWORD;
            try
            {
                respuesta.datos = UsuarioBLL.Login(user, password);
            }
            catch (Exception ex)
            {
                respuesta.codigo = HttpStatusCode.InternalServerError;
                respuesta.datos = null;
                respuesta.mensajes.Add(ex.Message);
                respuesta.mensajes.Add(ex.ToString());
            }

            if (respuesta.datos == null && respuesta.mensajes.Count() == 0)
            {
                respuesta.codigo = HttpStatusCode.NotFound;
                respuesta.mensajes.Add("Usuario no encontrado!");
            }

            return Content(respuesta.codigo, respuesta);
        }
    }
}
