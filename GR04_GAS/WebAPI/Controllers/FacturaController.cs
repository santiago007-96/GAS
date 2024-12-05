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
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class FacturaController : ApiController
    {
        [HttpGet]
        public IHttpActionResult LeerTodos(int cantidad = 10, int pagina = 0, string textoBusqueda = null)
        {
            var respuesta = new RespuestaVMR<ListadoPaginadoVMR<FacturaVMR>>();

            try
            {
                respuesta.datos = FacturaBLL.LeerTodos(cantidad, pagina, textoBusqueda);
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


        [HttpGet]
        [Route("api/factura/{codigo}")]
        public IHttpActionResult LeerUno(long codigo)
        {
            var respuesta = new RespuestaVMR<FacturaVMR>();

            try
            {
                respuesta.datos = FacturaBLL.LeerUno(codigo);
                if (respuesta.datos != null)
                {
                    respuesta.mensajes.Add("Factura registrada!!");
                }
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
                respuesta.mensajes.Add("Elemento no encontrado!");
            }

            return Content(respuesta.codigo, respuesta);
        }

        [HttpGet]
        [Route("api/factura/registrada/{numero}")]
        public IHttpActionResult facturaRegistrada(string numero)
        {
            var respuesta = new RespuestaVMR<FacturaVMR>();

            try
            {
                respuesta.datos = FacturaBLL.facturaRegistrada(numero);
                if (respuesta.datos != null)
                {
                    respuesta.mensajes.Add("Factura registrada!!");
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
        public IHttpActionResult Crear(FACTURA item)
        {
            var respuesta = new RespuestaVMR<long?>();

            try
            {
                respuesta.datos = FacturaBLL.Crear(item);
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
        public IHttpActionResult Actualizar(long codigo, FacturaVMR item)
        {
            var respuesta = new RespuestaVMR<bool>();

            try
            {
                item.CLI_CODIGO = codigo;
                FacturaBLL.Actualizar(item);
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
                FacturaBLL.Eliminar(codigos);
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
