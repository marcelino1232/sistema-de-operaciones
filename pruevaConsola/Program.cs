using Datos;
using Entidad;
using System;
using System.Linq;

namespace pruevaConsola
{
    class Program
    {
        public static Repositorio db = new Repositorio();

        // basedatos/table/metodo

        static void Main(string[] args)
        {

            int b = 0;
            int opera = 0;
            do
            {

                Persona persona = new Persona();

                Console.WriteLine("Ingresa Operacion para Realizar /n 0) Salir del Sistema /n 1) Listado /n 2) Filtro /n  3) Add /n 4)Paginar /n 5)venta ");
                opera = int.Parse(Console.ReadLine());

                if (opera == 0)
                {
                    Console.Clear();
                    b = 1;
                }
                else if (opera == 1)
                {
                    Console.Clear();
                    Listado();
                }
                else if (opera == 2)
                {
                    Console.Clear();
                    Fintral(2);
                }
                else if (opera == 3)
                {
                    Console.Clear();

                    //persona.PersonaId = 8;

                    Console.WriteLine("Ingresa Nombre : ");
                    persona.Nombre = Console.ReadLine();

                    Console.WriteLine("Ingresa Apellido : ");
                    persona.Apellido = Console.ReadLine();

                    Console.WriteLine("Ingresa Edad : ");
                    persona.Edad = int.Parse(Console.ReadLine());

                    Console.WriteLine("Ingresa Telefono : ");
                    persona.Telefono = Console.ReadLine();

                    Console.WriteLine("Ingresa Cedula : ");
                    persona.Cedula = Console.ReadLine();

                    Console.WriteLine("Ingresa Sexo : ");

                    var jquery = db.sexo.Data();
                    db.cliente.Data();

                    foreach (var list in jquery)
                    {
                        Console.WriteLine(list.SexoId + ") " + list.Descripcion);
                    }
                    persona.SexoId = int.Parse(Console.ReadLine());
                    Add(persona);
                }
                else if (opera == 4)
                {
                    int pagina;
                    int column;

                    Console.WriteLine("Ingresa Pagina : ");
                    pagina = int.Parse(Console.ReadLine());

                    Console.WriteLine("Ingresa Colum : ");
                    column = int.Parse(Console.ReadLine());

                    Paginar(pagina, column);
                }
                else if (opera == 5)
                {
                    // encabezado
                    var list = db.venta.Buscar(1);
                    Console.WriteLine("Cliente : " + list.NombreCliente +"");
                    // listado
                    var product = db.producto.Data().Where(x => x.VentaId == list.VentaId).ToList();
                    foreach(var li in product)
                    {
                        Console.WriteLine("Listado de producto");
                        Console.WriteLine("Nombre producto : " + li.Nombre + "");
                    }

                }
                else
                {
                    Console.WriteLine("Operacion No Existe");
                }

                 
            } while (b == 0);

        }

        public static void Fintral(int c)
        {
            int Id;
            do
            {
                Console.WriteLine("Ingresa ID : ");
                Id = int.Parse(Console.ReadLine());
                Buscar(Id);
                Console.WriteLine("Para Continuar Marque 0 de los Contrario Marque otro Numero : ");
                c = int.Parse(Console.ReadLine());

                Console.Clear();
            } while (c == 0);
        }
        public static void Add(Persona persona)
        {
            if (db.persona.Insert(persona) == 1)
            {
                Console.WriteLine("Agregado");
            }
            else
            {
                Console.WriteLine("No Agregado");
            }
        }
        public static void Buscar(int id)
        {

            var list = db.persona.Buscar(id);
            
            //foreach (var list in jquery)
            //{
           if(list == null)
            {
                Console.WriteLine("No Encontrado");
            }
            else
            {
                Console.WriteLine(
            "Nombre : {0}" + " Apellido : {1} " + " Edad : {2} " + " Telefono : {3} " + " Cedula : {4}", list.Nombre, list.Apellido, list.Edad, list.Telefono, list.Cedula);
            }
            //}
        }
        public static void Listado()
        {
            var jquery = (from p in db.persona.Data() join
                        s in db.sexo.Data() on
                        p.SexoId equals s.SexoId
                        select new
                        {
                            p.PersonaId,
                            p.Nombre,
                            p.Apellido,
                            p.Edad,
                            p.Telefono,
                            p.Cedula,
                            s.Descripcion
                        }).ToList();


            foreach (var list in jquery)
            {

                Console.WriteLine(
            "Nombre : {0}" + " Apellido : {1} " + " Edad : {2} " + " Telefono : {3} " + " Cedula : {4}" + " Sexo : {5}", list.Nombre, list.Apellido, list.Edad, list.Telefono, list.Cedula, list.Descripcion);
            }
        }

        public static void Paginar(int pagina, int column)
        {
            var jquery = (from p in db.persona.PaginarData(pagina,column)
                          select new
                          {
                              p.PersonaId,
                              p.Nombre,
                              p.Apellido,
                              p.Edad,
                              p.Telefono,
                              p.Cedula
                          }).ToList();


            for (int i = 0; i < jquery.Count; i++)
            {

                Console.WriteLine("Nombre : {0}" + 
                    " Apellido : {1} " +
                    " Edad : {2} " + 
                    " Telefono : {3} " +
                    " Cedula : {4}", jquery[i].Nombre, jquery[i].Apellido, jquery[i].Edad, jquery[i].Telefono, jquery[i].Cedula);
            }
        }
    }
}
