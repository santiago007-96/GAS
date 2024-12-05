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
    public class ClienteDAL
    {
        public static ListadoPaginadoVMR<ClienteVMR> LeerTodos(int cantidad, int pagina, string textoBusqueda)
        {
            ListadoPaginadoVMR<ClienteVMR> resultado = new ListadoPaginadoVMR<ClienteVMR>();



            using (var db = DbConexion.Create())
            {
                var query = db.CLIENTE.Where(c => !c.CLI_BORRADO).Select(c => new ClienteVMR
                {
                    CLI_CODIGO = c.CLI_CODIGO,
                    CLI_ID = c.CLI_ID,
                    CLI_NOMBRE = c.CLI_NOMBRE + " " + (c.CLI_APELLIDO != null ? (" " + c.CLI_APELLIDO) : ""),
                    CLI_EMAIL = c.CLI_EMAIL,
                    CLI_FACT_TOTAL = db.FACTURA.Where(f => !f.FACT_BORRADO && f.CLI_CODIGO == c.CLI_CODIGO).Select(f => f.FACT_MONTOTOTAL).DefaultIfEmpty(0).Sum()
            });

                if (!string.IsNullOrEmpty(textoBusqueda))
                {
                    query = query.Where(c => 
                                        c.CLI_ID.Contains(textoBusqueda)
                                        || c.CLI_NOMBRE.Contains(textoBusqueda)
                                        );
                }

                resultado.cantidadTotal = query.Count();

                resultado.elementos = query
                    .OrderBy(c => c.CLI_CODIGO)
                    .Skip(pagina * cantidad)
                    .Take(cantidad)
                    .ToList();
                                   
            }

            return resultado;
        }
        public static ClienteVMR LeerUno(long codigo)
        {
            ClienteVMR item = null;

            using (var db = DbConexion.Create())
            {
                item = db.CLIENTE.Where(c => !c.CLI_BORRADO && c.CLI_CODIGO == codigo).Select(c => new ClienteVMR
                {
                    CLI_CODIGO = c.CLI_CODIGO,
                    CLI_ID = c.CLI_ID,
                    CLI_NOMBRE = c.CLI_NOMBRE,
                    CLI_APELLIDO = c.CLI_APELLIDO,
                    CLI_EMAIL = c.CLI_EMAIL,
                    CLI_TEL = c.CLI_TEL,
                    CLI_DIRECCION = c.CLI_DIRECCION
        //CLI_FACT_TOTAL = db.FACTURA.Where(f => !f.FACT_BORRADO && f.CLI_CODIGO == c.CLI_CODIGO).Select(f => f.FACT_MONTOTOTAL).ToList().Sum()


                }).FirstOrDefault();
            }

            return item;
        }

        public static ClienteVMR clienteRegistrado(string ID)
        {
            ClienteVMR item = null;

            using (var db = DbConexion.Create())
            {
                item = db.CLIENTE.Where(c => !c.CLI_BORRADO && c.CLI_ID.Equals(ID)).Select(c => new ClienteVMR
                {
                    CLI_CODIGO = c.CLI_CODIGO,
                    CLI_ID = c.CLI_ID,
                    CLI_NOMBRE = c.CLI_NOMBRE,
                    CLI_APELLIDO = c.CLI_APELLIDO,
                    CLI_EMAIL = c.CLI_EMAIL,
                    CLI_TEL = c.CLI_TEL,
                    CLI_DIRECCION = c.CLI_DIRECCION
                    //CLI_FACT_TOTAL = db.FACTURA.Where(f => !f.FACT_BORRADO && f.CLI_CODIGO == c.CLI_CODIGO).Select(f => f.FACT_MONTOTOTAL).ToList().Sum()


                }).FirstOrDefault();
            }

            return item;
        }
        //Devuelve el PK el elemento creado
        public static long Crear(CLIENTE item)
        {
            

            using (var db = DbConexion.Create())
            {
                item.CLI_BORRADO = false;
                db.CLIENTE.Add(item);
                db.SaveChanges();
            }

            return item.CLI_CODIGO;
        }
        public static void Actualizar(ClienteVMR item)
        {
            using (var db = DbConexion.Create())
            {
                var itemUpdate = db.CLIENTE.Find(item.CLI_CODIGO);
                Console.WriteLine(itemUpdate.CLI_CODIGO);

                itemUpdate.CLI_NOMBRE = item.CLI_NOMBRE;
                itemUpdate.CLI_APELLIDO = item.CLI_APELLIDO;
                itemUpdate.CLI_ID = item.CLI_ID;
                itemUpdate.CLI_EMAIL = item.CLI_EMAIL;
                itemUpdate.CLI_TEL = item.CLI_TEL;
                itemUpdate.CLI_DIRECCION = item.CLI_DIRECCION; 
                
                db.Entry(itemUpdate).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }

        //Elimina las facturas correspondientes al cliente
        public static void Eliminar(List<long> codigos)
        {
            using (var db = DbConexion.Create())
            {
                var items = db.CLIENTE.Where(c => codigos.Contains(c.CLI_CODIGO));
                var items2 = db.FACTURA.Where(f => codigos.Contains(f.CLI_CODIGO));

                foreach (var item in items)
                {
                    item.CLI_BORRADO = true;
                    db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                }

                foreach (var item in items2)
                {
                    item.FACT_BORRADO = true;
                    db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                }
                db.SaveChanges();
            }
        }
    }
}
