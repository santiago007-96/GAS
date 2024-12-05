using Comun.ViewModels;
using Logica.BLL;
using Modelo.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace WebAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*", SupportsCredentials = true)]
    public class ClienteController : ApiController
    {
        [HttpGet]
        public IHttpActionResult LeerTodos(int cantidad = 10, int pagina = 0, string textoBusqueda = null)
        {
            var respuesta = new RespuestaVMR<ListadoPaginadoVMR<ClienteVMR>>();

            try
            {
                respuesta.datos = ClienteBLL.LeerTodos(cantidad, pagina, textoBusqueda);
            } catch(Exception ex)
            {
                respuesta.codigo = HttpStatusCode.InternalServerError;
                respuesta.datos = null;
                respuesta.mensajes.Add(ex.Message);
                respuesta.mensajes.Add(ex.ToString());
            }

            return Content(respuesta.codigo, respuesta);
        }

        
        [HttpGet]
        [Route("api/cliente/{codigo}")]
        public IHttpActionResult LeerUno(long codigo)
        {
            var respuesta = new RespuestaVMR<ClienteVMR>();

            try
            {
                respuesta.datos = ClienteBLL.LeerUno(codigo);
            }
            catch (Exception ex)
            {
                respuesta.codigo = HttpStatusCode.InternalServerError;
                respuesta.datos = null;
                respuesta.mensajes.Add(ex.Message);
                respuesta.mensajes.Add(ex.ToString());
            }

            if(respuesta.datos == null && respuesta.mensajes.Count() == 0)
            {
                respuesta.codigo = HttpStatusCode.NotFound;
                respuesta.mensajes.Add("Elemento no encontrado!");
            }

            return Content(respuesta.codigo, respuesta);
        }

        [HttpGet]
        [Route("api/cliente/registrado/{ID}")]
        public IHttpActionResult clienteRegistrado(string ID)
        {
            var respuesta = new RespuestaVMR<ClienteVMR>();

            try
            {
                respuesta.datos = ClienteBLL.clienteRegistrado(ID);
                if(respuesta.datos != null)
                {
                    respuesta.mensajes.Add("Cliente registrado!!");
                }
                
            }
            catch (Exception ex)
            {
                respuesta.codigo = HttpStatusCode.InternalServerError;
                respuesta.datos = null;
                respuesta.mensajes.Add(ex.Message);
                respuesta.mensajes.Add(ex.ToString());
            }

            return Content(respuesta.codigo, respuesta);
        }

        [HttpPost]
        public IHttpActionResult Crear(CLIENTE item)
        {
            var respuesta = new RespuestaVMR<long?>();

            try
            {
                respuesta.datos = ClienteBLL.Crear(item);
            }
            catch (Exception ex)
            {
                respuesta.codigo = HttpStatusCode.InternalServerError;
                respuesta.datos = null;
                respuesta.mensajes.Add(ex.Message);
                respuesta.mensajes.Add(ex.ToString());
            }

            return Content(respuesta.codigo, respuesta);
        }

        //[Route("api/cliente/{codigo}")]
        [HttpPut]
        public IHttpActionResult Actualizar(long codigo, ClienteVMR item)
        {
            var respuesta = new RespuestaVMR<bool>();

            try
            {
                item.CLI_CODIGO = codigo;
                ClienteBLL.Actualizar(item);
                respuesta.datos = true;
            }
            catch (Exception ex)
            {
                respuesta.codigo = HttpStatusCode.InternalServerError;
                respuesta.datos = false;
                respuesta.mensajes.Add(ex.Message);
                respuesta.mensajes.Add(ex.ToString());
            }

            return Content(respuesta.codigo, respuesta);
        }

        [HttpDelete]
        public IHttpActionResult Eliminar(List<long> codigos)
        {
            var respuesta = new RespuestaVMR<bool>();

            try
            {
                ClienteBLL.Eliminar(codigos);
                respuesta.datos = true;
            }
            catch (Exception ex)
            {
                respuesta.codigo = HttpStatusCode.InternalServerError;
                respuesta.datos = false;
                respuesta.mensajes.Add(ex.Message);
                respuesta.mensajes.Add(ex.ToString());
            }

            return Content(respuesta.codigo, respuesta);
        }
    }
}
