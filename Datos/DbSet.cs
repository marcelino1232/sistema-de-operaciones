using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace Datos
{
    public class DbSet<T> where T : class
    {
        private PropertyInfo[] properties = null;

        private string jquery = null;
        private string[] parameters = null;

        private SqlConnection con = null;
        private SqlCommand cmd = null;
        private SqlDataReader dr = null;


        public DbSet()
        {
            con = new SqlConnection(ConexionSqlServer());
        }

        private static string ConexionSqlServer()
        {
            return string.Format(@"Data Source=DESKTOP-EAAM30S;Initial Catalog=Consola;Integrated Security=True");
        }

        public List<T> Data() 
        {
            // Lista de Objecto a de Volver

            List<T> listado = new List<T>();

            try
            {
                // Abrir Conexion a BD

                con.Open();
                
                // Consulta Sql Server

                jquery = string.Format(@"select * from {0}", typeof(T).Name);

                cmd = new SqlCommand(jquery, con);

                cmd.CommandType = CommandType.Text;

                // respuesta de Sql Server

                dr = cmd.ExecuteReader();

                // Detecto Objecto Por Defecto  T

                T obj = default(T);

                while (dr.Read())
                {
                    // Referencia al Objecto Por defecto T

                    obj = Activator.CreateInstance<T>();

                    // llenar Objecto por defecto de T

                    foreach (PropertyInfo prop in obj.GetType().GetProperties())
                    {
                        if (!object.Equals(dr[prop.Name], DBNull.Value))
                        {
                            prop.SetValue(obj, dr[prop.Name], null);
                        }
                    }

                    // Agregar a la Lista de Objecto por Defecto de T

                    listado.Add(obj);
                }
            }
            catch (Exception ex)
            {
                // Algun Error por x Razon

                throw ex;
            }
            finally
            {
                // Liberar Recursos 

                jquery = null;
                cmd = null;

                dr.Close();
                con.Close();
            }

            // Listado de T a de Volver

            return listado;
        }

        public List<T> PaginarData(int Pagina,int CantidaRegistro)
        {
            // Lista de Objecto a de Volver

            List<T> listado = new List<T>();

            try
            {
                // Abrir Conexion a BD

                con.Open();

                // Array de los atributos de la clase por defecto 

                properties = typeof(T).GetProperties();

                // Consulta Sql Server

                int skip = Pagina * CantidaRegistro;

                jquery = string.Format(@"select * from {0} order by {1} OFFSET {2} ROWS FETCH NEXT {3} ROWS ONLY", typeof(T).Name,properties[0].Name,skip,CantidaRegistro);

                cmd = new SqlCommand(jquery, con);

                cmd.CommandType = CommandType.Text;

                // respuesta de Sql Server

                dr = cmd.ExecuteReader();

                // Detecto Objecto Por Defecto  T

                T obj = default(T);

                while (dr.Read())
                {
                    // Referencia al Objecto Por defecto T

                    obj = Activator.CreateInstance<T>();

                    // llenar Objecto por defecto de T

                    foreach (PropertyInfo prop in obj.GetType().GetProperties())
                    {
                        if (!object.Equals(dr[prop.Name], DBNull.Value))
                        {
                            prop.SetValue(obj, dr[prop.Name], null);
                        }
                    }

                    // Agregar a la Lista de Objecto por Defecto de T

                    listado.Add(obj);
                }
            }
            catch (Exception ex)
            {
                // Algun Error por x Razon

                throw ex;
            }
            finally
            {
                // Liberar Recursos 

                jquery = null;
                cmd = null;

                dr.Close();
                con.Close();
            }

            // Listado de T a de Volver

            return listado;
        }

        public T Buscar(int id)
        {
            // estado de la respuesta

            T obj = default(T);

            try
            {
                // Abrir Conexion a BD

                con.Open();

                // Array de los atributos de la clase por defecto 

                properties = typeof(T).GetProperties();

                // Consulta Sql Server

                jquery = string.Format(@"select * from {0} where {1} = @Id", typeof(T).Name, properties[0].Name);

                cmd = new SqlCommand(jquery, con);

                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@Id", id);

                // respuesta de Sql Server

                dr = cmd.ExecuteReader();

                obj = Activator.CreateInstance<T>();

                if (dr.Read())
                {
                    foreach (PropertyInfo prop in obj.GetType().GetProperties())
                    {
                        if (!object.Equals(dr[prop.Name], DBNull.Value))
                        {
                            prop.SetValue(obj, dr[prop.Name], null);
                        }
                    }
                }
                else
                {
                    obj = null;
                }

            }
            catch (Exception ex)
            {
                // Algun Error por x Razon

                throw ex;
            }
            finally
            {
                // Liberar Recursos 

                jquery = null;
                cmd = null;

                dr.Close();
                con.Close();
            }

            // Valor a de Volver

            return obj;
        }

        public int Insert(T obj)
        {
            int res = 0;

            try
            {
                // Abrir Conexion a BD

                con.Open();

                // Array de los atributos de la clase por defecto 

                properties = typeof(T).GetProperties();

                // Array para longitud de los atributos

                parameters = new string[properties.Length];

                for (int i = 1; i < properties.Length; i++)
                {
                    // parametros para consulta Sql Server
                    parameters[i] = string.Format("{0}", properties[i].Name);
                }

                // Consulta Sql Server

                string jquery = string.Format(@"insert into {0} ({1}) values ({2})"
                               , typeof(T).Name, string.Join(",", parameters).Substring(1), string.Join(",@", parameters).Substring(1));

                cmd = new SqlCommand(jquery, con);

                cmd.CommandType = CommandType.Text;

                // Valores de los parametros 

                for (int i = 1; i < properties.Length; i++)
                {
                    cmd.Parameters.AddWithValue("@" + properties[i].Name, properties[i].GetValue(obj));
                }

                // respuesta de Sql Server

                res = cmd.ExecuteNonQuery();

            }catch(Exception ex)
            {
                // Algun Error por x Razon

                throw ex;
            }
            finally
            {
                // Liberar Recursos 

                properties = null;
                parameters = null;
                jquery = null;
                cmd = null;

                con.Close();
            }

            // Valor a de Volver

            return res;
        }

        public int InsertList(List<T> obj)
        {
            int res;

            if (obj != null && obj.Count > 0)
            {
                foreach (T item in obj.ToList())
                {
                    Insert(item);
                }
                res = 1;
            }
            else
            {
                res = 0;
            }
            return res;
        }

        public int Update(T obj)
        {
            int res = 0;

            try
            {
                // Abrir Conexion a BD

                con.Open();

                // Array de los atributos de la clase por defecto 

                properties = typeof(T).GetProperties();

                // parametros para consulta Sql Server

                string param = "";

                for (int i = 1; i < properties.Length; i++)
                {
                    param += "," + properties[i].Name + "=@" + properties[i].Name;
                }

                // Consulta Sql Server

                jquery = string.Format(@"update {0} set {1} where {2}={3}"
                               , typeof(T).Name, param.Substring(1), properties[0].Name, properties[0].GetValue(obj));

                cmd = new SqlCommand(jquery, con);

                cmd.CommandType = CommandType.Text;

                // Valores de los parametros 

                for (int i = 1; i < properties.Length; i++)
                {
                    cmd.Parameters.AddWithValue("@" + properties[i].Name, properties[i].GetValue(obj));
                }

                // respuesta de Sql Server

                res = cmd.ExecuteNonQuery();

            }catch(Exception ex)
            {
                // Algun Error por x Razon

                throw ex;
            }
            finally
            {
                // Liberar Recursos 

                properties = null;
                jquery = null;
                cmd = null;

                con.Close();
            }

            // Valor a de Volver

            return res;
        }

        public int Delete(int id)
        {
            int res = 0;

            try
            {
                // Abrir Conexion a BD

                con.Open();

                // parametros para consulta Sql Server

                properties = typeof(T).GetProperties();

                // Consulta Sql Server

                jquery = string.Format(@"delete from {0} where {1} = @Id", typeof(T).Name, properties[0].Name);

                cmd = new SqlCommand(jquery, con);

                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@Id",id);

                // respuesta de Sql Server

                res = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // Algun Error por x Razon

                throw ex;
            }
            finally
            {
                // Liberar Recursos 

                properties = null;
                jquery = null;
                cmd = null;

                con.Close();
            }

            // Valor a de Volver

            return res;
        }

        public bool Existe(int id)
        {
            // estado de la respuesta

            bool status;

            try
            {
                // Abrir Conexion a BD

                con.Open();

                // parametros para consulta Sql Server

                properties = typeof(T).GetProperties();

                // Consulta Sql Server

                jquery = string.Format(@"select * from {0} where {1} = @Id", typeof(T).Name, properties[0].Name);

                cmd = new SqlCommand(jquery, con);

                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@Id", id);

                // respuesta de Sql Server

                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    status = true;
                }
                else
                {
                    status = false;
                }
               
            }
            catch (Exception ex)
            {
                // Algun Error por x Razon

                throw ex;
            }
            finally
            {
                // Liberar Recursos 

                jquery = null;
                cmd = null;

                dr.Close();
                con.Close();
            }

            // Valor a de Volver

            return status;
        }

    }
}
