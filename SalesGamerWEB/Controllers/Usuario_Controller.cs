using SalesGamerWEB.Controllers;
using SalesGamerWEB.Models;
using System.Data.SqlClient;

public static class Usuario_Controller
{
	public static bool Autenticar(string email, string pass)
	{
		Usuario user = null;
		string query = "SELECT * FROM dbo.Usuario WHERE mail = @mail AND contrasena_usuario = @pass;";

		using (SqlCommand cmd = new SqlCommand(query, DB_Controller.connection))
		{
			cmd.Parameters.AddWithValue("@mail", email);
			cmd.Parameters.AddWithValue("@pass", pass);

			try
			{
				DB_Controller.connection.Open();
				SqlDataReader reader = cmd.ExecuteReader();

				if (reader.Read())
				{
					user = new Usuario
					{
						Id = reader.GetInt32(0),
						usuario = reader.GetString(1),
						Mail = reader.GetString(2),
						Contraseña = reader.GetString(3),
						Nombre = reader.IsDBNull(4) ? null : reader.GetString(4),
						Apellido = reader.IsDBNull(5) ? null : reader.GetString(5),
						Telefono = reader.IsDBNull(6) ? (int?)null : reader.GetInt32(6),
						Direccion = reader.IsDBNull(7) ? null : reader.GetString(7),
						Rol = reader.IsDBNull(8) ? (int?)null : reader.GetInt32(8)
					};
				}

				reader.Close();
			}
			catch (Exception ex)
			{
				// Manejar el error adecuadamente
				throw new Exception("Error al autenticar el usuario: " + ex.Message);
			}
			finally
			{
				DB_Controller.connection.Close();
			}
		}

		return user != null;
	}

	public static bool CrearUsuario(Usuario usuario)
	{
		string query = "INSERT INTO dbo.Usuario (id,nombre_usuario, mail, contrasena_usuario) VALUES (@id,@nombre_usuario, @mail, @pass);";

		using (SqlCommand cmd = new SqlCommand(query, DB_Controller.connection))
		{
            cmd.Parameters.AddWithValue("@id", ObtenerMaxId() + 1);
            cmd.Parameters.AddWithValue("@nombre_usuario", usuario.usuario);
			cmd.Parameters.AddWithValue("@mail", usuario.Mail);
			cmd.Parameters.AddWithValue("@pass", usuario.Contraseña);

			try
			{
				DB_Controller.connection.Open();
				int result = cmd.ExecuteNonQuery();
				return result > 0;
			}
			catch (Exception ex)
			{
				throw new Exception("Error al crear el usuario: " + ex.Message);
			}
			finally
			{
				DB_Controller.connection.Close();
			}
		}
	}
    private static int ObtenerMaxId()
    {
        int maxId = 0;
        string query = "SELECT MAX(id) FROM dbo.Usuario;";

        using (SqlCommand cmd = new SqlCommand(query, DB_Controller.connection))
        {
            try
            {
                DB_Controller.connection.Open();
                maxId = (int)cmd.ExecuteScalar();
                DB_Controller.connection.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Error en la consulta: " + ex.Message);
            }
        }

        return maxId;
    }
}
