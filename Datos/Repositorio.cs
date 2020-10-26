using System;
using System.Collections.Generic;
using System.Text;

namespace Datos
{
    public class Repositorio<T> : OperacionesDB<T> where T : class
    {
        public  Repositorio() : base("Data Source=DESKTOP-EAAM30S;Initial Catalog=Consola;Integrated Security=True")
        {

        }
    }
}
