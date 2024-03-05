using SalesGamerWEB.Models;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace SalesGamerWEB.Controllers
{
    public class Usuario_Controller
    {
        // GET ONE
        public static bool autenticar(string usr, string pass, bool hasheado)
        {
            Usuario user = null; // Inicializamos como null para indicar que no se encontró ningún usuario
            string query = "SELECT * FROM dbo.Usuario WHERE nombre_usuario = @usr AND contrasena_usuario = @pass;";

            SqlCommand cmd = new SqlCommand(query, DB_Controller.connection);
            cmd.Parameters.AddWithValue("@usr", usr);
            cmd.Parameters.AddWithValue("@pass", pass);

            try
            {
                DB_Controller.connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    // Se encontró un usuario con las credenciales proporcionadas
                    user = new Usuario(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), "", reader.GetString(4), reader.GetString(5), reader.GetInt32(6), reader.GetString(7), reader.GetInt32(8));
                    break; // Salir del bucle ya que se encontró el usuario
                }

                reader.Close();
                DB_Controller.connection.Close();

                // Devolver verdadero si se encontró un usuario correspondiente, falso de lo contrario
                return user != null;
            }
            catch (Exception ex)
            {
                throw new Exception("Hay un error en la query: " + ex.Message);
            }
        }



        // OBTENER EL MAX ID

        public static int obtenerMaxId()
        {
            int MaxId = 0;
            string query = "select max(id) from dbo.Usuario;";

            SqlCommand cmd = new SqlCommand(query, DB_Controller.connection);

            try
            {
                DB_Controller.connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    MaxId = reader.GetInt32(0);
                }

                reader.Close();
                DB_Controller.connection.Close();
                return MaxId;
            }
            catch (Exception ex)
            {
                throw new Exception("Hay un error en la query: " + ex.Message);
            }
        }


        // GET ALL

        public static List<Usuario> obtenerTodos()
        {
            List<Usuario> list = new List<Usuario>();
            string query = "select * from dbo.Usuario;";

            SqlCommand cmd = new SqlCommand(query, DB_Controller.connection);

            try
            {
                DB_Controller.connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new Usuario(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), "", reader.GetString(4), reader.GetString(5), reader.GetInt32(6), reader.GetString(7), reader.GetInt32(8)));
                    Trace.WriteLine("Usr encontrado, nombre: " + reader.GetString(1));
                }

                reader.Close();
                DB_Controller.connection.Close();

            }
            catch (Exception ex)
            {
                throw new Exception("Hay un error en la query: " + ex.Message);
            }

            return list;
        }



        // GET ONE BY ID

        public static Usuario obtenerPorId(int id)
        {
            Usuario usr = new Usuario();
            string query = "select * from dbo.Usuario where id = @id;";

            SqlCommand cmd = new SqlCommand(query, DB_Controller.connection);
            cmd.Parameters.AddWithValue("@id", id);

            try
            {
                DB_Controller.connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    usr = new Usuario(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), "", reader.GetString(4), reader.GetString(5), reader.GetInt32(6), reader.GetString(7), reader.GetInt32(8));
                    Trace.WriteLine("Usr encontrado, nombre: " + reader.GetString(1));
                }

                reader.Close();
                DB_Controller.connection.Close();

            }
            catch (Exception ex)
            {
                throw new Exception("Hay un error en la query: " + ex.Message);
            }

            return usr;
        }



        // EDIT / PUT

        public static bool editarUsuario(Usuario usr)
        {
            //Darlo de alta en la BBDD

            string query = "update dbo.Usuario set nombre_usuario = @nombre_usuario , " +
                "nombre = @nombre , " +
                "apellido = @apellido , " +
                "rol = @rol " +
                "where id = @id ;";

            SqlCommand cmd = new SqlCommand(query, DB_Controller.connection);
            cmd.Parameters.AddWithValue("@id", usr.Id);
            cmd.Parameters.AddWithValue("@nombre_usuario", usr.usuario);
            cmd.Parameters.AddWithValue("@mail", usr.Mail);
            cmd.Parameters.AddWithValue("@contrasena_usuario", usr.Contraseña);
            cmd.Parameters.AddWithValue("@nombre", usr.Nombre);
            cmd.Parameters.AddWithValue("@apellido", usr.Apellido);
            cmd.Parameters.AddWithValue("@telefono", usr.Telefono);
            cmd.Parameters.AddWithValue("@direccion", usr.Direccion);
            cmd.Parameters.AddWithValue("@rol", usr.ID_rol);

            try
            {
                cmd.ExecuteNonQuery();
                DB_Controller.connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Hay un error en la query: " + ex.Message);
            }

        }
        public static bool crearUsuario(Usuario usr)
        {
            // Dar de alta en la BBDD

            string query = "INSERT INTO dbo.Usuario (id,nombre_usuario, mail, contrasena_usuario, nombre, apellido, telefono, direccion) " +
                           "VALUES (@id, @nombre_usuario, @mail, @contrasena_usuario, @nombre, @apellido, @telefono, @direccion);";

            SqlCommand cmd = new SqlCommand(query, DB_Controller.connection);
            cmd.Parameters.AddWithValue("@id", obtenerMaxId() + 1);
            cmd.Parameters.AddWithValue("@nombre_usuario", usr.usuario);
            cmd.Parameters.AddWithValue("@mail", usr.Mail);
            cmd.Parameters.AddWithValue("@contrasena_usuario", usr.Contraseña);
            cmd.Parameters.AddWithValue("@nombre", usr.Nombre);
            cmd.Parameters.AddWithValue("@apellido", usr.Apellido);
            cmd.Parameters.AddWithValue("@telefono", usr.Telefono);
            cmd.Parameters.AddWithValue("@direccion", usr.Direccion);

            try
            {
                cmd.ExecuteNonQuery();
                return true;
                DB_Controller.connection.Close();

            }
            catch (Exception ex)
            {
                throw new Exception("Hay un error en la query: " + ex.Message);
            }
            finally
            {
                if (DB_Controller.connection.State == ConnectionState.Open)
                {
                    DB_Controller.connection.Close();
                }
            }
        }

        

        public static bool eliminarUsuario(Usuario usuarioEliminar)
        {
            string query = "DELETE FROM dbo.Usuario WHERE id = @id;";

            using (SqlCommand cmd = new SqlCommand(query, DB_Controller.connection))
            {
                cmd.Parameters.AddWithValue("@id", usuarioEliminar.Id);

                try
                {
                    DB_Controller.connection.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    DB_Controller.connection.Close();

                    return rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al intentar eliminar el producto: " + ex.Message);
                }
            }
        }
    }
}
