using Comun.ViewModels;
using Modelo.Modelos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.DAL
{
    public class FacturaDAL
    {
        public static ListadoPaginadoVMR<FacturaVMR> LeerTodos(int cantidad, int pagina, string textoBusqueda)
        {
            ListadoPaginadoVMR<FacturaVMR> resultado = new ListadoPaginadoVMR<FacturaVMR>();
            

            using (var db = DbConexion.Create())
            {
                var query = db.FACTURA.Where(f => !f.FACT_BORRADO).Select(f => new FacturaVMR
                {
                    FACT_CODIGO = f.FACT_CODIGO,
                    CLI_CODIGO = f.CLI_CODIGO,
                    FACT_NUMERO = f.FACT_NUMERO,
                    FACT_FECHA = f.FACT_FECHA,
                    FACT_CANTIDAD = f.FACT_CANTIDAD,
                    FACT_MONTOTOTAL = f.FACT_MONTOTOTAL,
                    CLI_NOMBRE = db.CLIENTE.Where(c => !c.CLI_BORRADO && c.CLI_CODIGO == f.CLI_CODIGO).Select(c => c.CLI_NOMBRE).FirstOrDefault(),
                    CLI_APELLIDO = db.CLIENTE.Where(c => !c.CLI_BORRADO && c.CLI_CODIGO == f.CLI_CODIGO).Select(c => c.CLI_APELLIDO).FirstOrDefault(),
                    CLI_ID = db.CLIENTE.Where(c => !c.CLI_BORRADO && c.CLI_CODIGO == f.CLI_CODIGO).Select(c => c.CLI_ID).FirstOrDefault()
                });

                if (!string.IsNullOrEmpty(textoBusqueda))
                {
                    query = query.Where(f => f.FACT_NUMERO.Contains(textoBusqueda));
                }

                resultado.cantidadTotal = query.Count();

                resultado.elementos = query
                    .OrderBy(f => f.FACT_NUMERO)
                    .Skip(pagina * cantidad)
                    .Take(cantidad)
                    .ToList();

            }

            return resultado;
        }

        public static FacturaVMR LeerUno(long codigo)
        {
            FacturaVMR item = null;

            using (var db = DbConexion.Create())
            {
                item = db.FACTURA.Where(f => !f.FACT_BORRADO && f.FACT_CODIGO == codigo).Select(f => new FacturaVMR
                {
                   FACT_CODIGO = f.CLI_CODIGO,
                   CLI_CODIGO = f.CLI_CODIGO,
                   FACT_NUMERO = f.FACT_NUMERO,
                   FACT_FECHA = f.FACT_FECHA,
                   FACT_CANTIDAD = f.FACT_CANTIDAD,
                   FACT_MONTOTOTAL = f.FACT_MONTOTOTAL
                   
                }).FirstOrDefault();
            }

            return item;
        }

        public static FacturaVMR facturaRegistrada(string numeroFactura)
        {
            FacturaVMR item = null;

            using (var db = DbConexion.Create())
            {
                item = db.FACTURA.Where(f => !f.FACT_BORRADO && f.FACT_NUMERO.Equals(numeroFactura)).Select(f => new FacturaVMR
                {
                    FACT_CODIGO = f.CLI_CODIGO,
                    CLI_CODIGO = f.CLI_CODIGO,
                    FACT_NUMERO = f.FACT_NUMERO,
                    FACT_FECHA = f.FACT_FECHA,
                    FACT_CANTIDAD = f.FACT_CANTIDAD,
                    FACT_MONTOTOTAL = f.FACT_MONTOTOTAL


                }).FirstOrDefault();
            }

            return item;
        }

        //Devuelve el PK el elemento creado
        public static long Crear(FACTURA item)
        {

            using (var db = DbConexion.Create())
            {
                item.FACT_BORRADO = false;
                db.FACTURA.Add(item);
                db.SaveChanges();
            }

            return item.FACT_CODIGO;
        }

        public static void Actualizar(FacturaVMR item)
        {
            using (var db = DbConexion.Create())
            {
                var itemUpdate = db.FACTURA.Find(item.FACT_CODIGO);

                itemUpdate.FACT_NUMERO = item.FACT_NUMERO;
                itemUpdate.FACT_FECHA = item.FACT_FECHA;
                itemUpdate.FACT_CANTIDAD = item.FACT_CANTIDAD;
                itemUpdate.FACT_MONTOTOTAL = item.FACT_MONTOTOTAL;

                db.Entry(itemUpdate).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }

        public static void Eliminar(List<long> codigos)
        {
            using (var db = DbConexion.Create())
            {
                var items = db.FACTURA.Where(f => codigos.Contains(f.FACT_CODIGO));

                foreach (var item in items)
                {
                    item.FACT_BORRADO = true;
                    db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                }
                db.SaveChanges();
            }
        }
    }
}
