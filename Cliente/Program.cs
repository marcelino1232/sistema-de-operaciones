using Datos;
using Entidad;
using System;
using System.Linq;

namespace Cliente
{
 class Program
    {
        static void Main(string[] args)
        {

            int b = 0;
            int opera = 0;

            do
            {
                Persona persona = new Persona();

                Console.WriteLine("Ingresa Operacion para Realizar /n 0) Salir del Sistema /n 1) Listado /n 2) Filtro /n  3) Add ");
                opera = int.Parse(Console.ReadLine());

                if (opera == 0) {
                    Console.Clear();
                    b = 1;
                }
                else if(opera == 1)
                {
                    Console.Clear();
                    Listado();
                }
                else if(opera == 2)
                {
                    Console.Clear();
                    Fintral(2);
                }
                else if (opera == 3)
                {
                    Console.Clear();

                    persona.PersonaId = 2;

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
                    Repositorio<Sexo> obj = new Repositorio<Sexo>();
                    var jquery = obj.Table();

                    foreach (var list in jquery)
                    {

                        Console.WriteLine(list.SexoId + ") " + list.Descripcion);
                    }
                    persona.SexoId = int.Parse(Console.ReadLine());
                    Add(persona);
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
            Repositorio<Persona> procesoPersona = new Repositorio<Persona>();

            if (procesoPersona.Update(persona) == 1)
            {
                Console.WriteLine("Actualizado");
            }
            else
            {
                Console.WriteLine("No Agregado");
            }
        }
        public static void Buscar(int id)
        {
            Repositorio<Persona> procesoPersona = new Repositorio<Persona>();

            Repositorio<Sexo> procesoSexo = new Repositorio<Sexo>();
            

            var list = (from p in procesoPersona.Table() join
                        s in procesoSexo.Table() on
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
                        }).FirstOrDefault(x => x.PersonaId == id);


            //foreach (var list in jquery)
            //{
           if(list == null)
            {
                Console.WriteLine("No Encontrado");
            }
            else
            {
                Console.WriteLine(
            "Nombre : {0}" + " Apellido : {1} " + " Edad : {2} " + " Telefono : {3} " + " Cedula : {4}" + " Sexo : {5}", list.Nombre, list.Apellido, list.Edad, list.Telefono, list.Cedula, list.Descripcion);
            }
            //}
        }
        public static void Listado()
        {
            Repositorio<Persona> procesoPersona = new Repositorio<Persona>();

            Repositorio<Sexo> procesoSexo = new Repositorio<Sexo>();

            var jquery = (from p in procesoPersona.Table() join
                        s in procesoSexo.Table() on
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
    }
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
