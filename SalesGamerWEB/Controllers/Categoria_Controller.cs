using SalesGamerWEB.Models;
using System.Data.SqlClient;
using System.Diagnostics;

namespace SalesGamerWEB.Controllers
{
    public class Categoria_Controller
    {
        public static List<Categoria> obtenerCategoria()
        {
            List<Categoria> list = new List<Categoria>();
            string query = "select * from dbo.Categoria;";

            SqlCommand cmd = new SqlCommand(query, DB_Controller.Connection);

            try
            {
                DB_Controller.Connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new Categoria(reader.GetInt32(0), reader.GetString(1)));
                    Trace.WriteLine("Categoria encontrado, nombre: " + reader.GetString(1));
                }

                reader.Close();
                DB_Controller.Connection.Close();

            }
            catch (Exception ex)
            {
                throw new Exception("Hay un error en la query: " + ex.Message);
            }

            return list;
        }

        //SACAR EL MAXID
        public static int obtenerMaxId()
        {
            int MaxId = 0;
            string query = "select max(id) from dbo.Categoria;";

            SqlCommand cmd = new SqlCommand(query, DB_Controller.Connection);

            try
            {
                DB_Controller.Connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    MaxId = reader.GetInt32(0);
                }

                reader.Close();
                DB_Controller.Connection.Close();
                return MaxId;
            }
            catch (Exception ex)
            {
                throw new Exception("Hay un error en la query: " + ex.Message);
            }
        }

        //OBTENER CATEGORIA POR ID
        public static Categoria ObtenerCategoriaID(int id)
        {
            Categoria categoria = new Categoria();
            string query = "SELECT * FROM dbo.Categoria WHERE id = @id;";
            using (SqlCommand cmd = new SqlCommand(query, DB_Controller.Connection))
            {
                cmd.Parameters.AddWithValue("@id", id);
                try
                {
                    DB_Controller.Connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        categoria = new Categoria
                        {
                            Id = reader.GetInt32(0),
                            Nombre_categoria = reader.GetString(1),

                        };
                    }
                    reader.Close();
                }
                finally
                {
                    DB_Controller.Connection.Close();
                }
            }
            return categoria;
        }

        //CREAR CATEGORIA
        public static bool CrearCategoria(Categoria categoria)
        {
            string query = "INSERT INTO dbo.Categoria (id,nombre_categoria) " +
                           "VALUES (@id,@nombre);";
            using (SqlCommand cmd = new SqlCommand(query, DB_Controller.Connection))
            {
                cmd.Parameters.AddWithValue("@id", obtenerMaxId() + 1);
                cmd.Parameters.AddWithValue("@nombre", categoria.Nombre_categoria);

                try
                {
                    DB_Controller.Connection.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
                finally
                {
                    DB_Controller.Connection.Close();
                }
            }
        }

        // EDITAR / CREAR CATEGORIA

        public static bool editarCategoria(Categoria cat)
        {
            //Darlo de alta en la BBDD

            string query = "update dbo.Categoria set nombre_categoria = @nombre_categoria , " +
                "where id = @id ;";

            SqlCommand cmd = new SqlCommand(query, DB_Controller.Connection);
            cmd.Parameters.AddWithValue("@id", cat.Id);
            cmd.Parameters.AddWithValue("@nombre_categoria", cat.Nombre_categoria);


            try
            {
                DB_Controller.Connection.Open();
                cmd.ExecuteNonQuery();
                DB_Controller.Connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Hay un error en la query: " + ex.Message);
            }

        }

        //Eliminar Categoria
        public static bool eliminarCategoria(Categoria catEliminar)
        {
            string query = "DELTE FROM dbo.Categoria where id=@id";

            using (SqlCommand cmd = new SqlCommand(query, DB_Controller.Connection))
            {
                cmd.Parameters.AddWithValue("@id", catEliminar.Id);

                try
                {
                    DB_Controller.Connection.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    DB_Controller.Connection.Close();

                    return rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al intentar eliminar la categoria: " + ex.Message);
                }
            }
        }
    }
}
