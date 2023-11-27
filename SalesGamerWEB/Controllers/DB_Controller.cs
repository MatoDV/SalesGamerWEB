using System;
using System.Data.SqlClient;

namespace SalesGamerWEB.Controllers
{
    public static class DB_Controller
    {
        private static string connectionString = "";

        public static SqlConnection connection;

        // Método para inicializar la conexión
        public static void Initialize()
        {
            try
            {
                // Cadena de conexión
                string serverName = "(localdb)\\Local"; // Nombre del servidor
                string databaseName = "SalesGamer"; // Nombre de la base de datos
                string integratedSecurity = "True"; // Autenticación de Windows

                // Cadena de conexión completa
                connectionString = $"Server={serverName};Database={databaseName};Integrated Security={integratedSecurity};";

                // Inicializar la conexión
                connection = new SqlConnection(connectionString);
                connection.Open(); // Abrir la conexión

                Console.WriteLine("Conexión establecida correctamente.");
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error al establecer la conexión: " + ex.Message);
            }
            finally
            {
                if (connection != null && connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close(); // Cerrar la conexión si está abierta
                }
            }
        }
    }
}
