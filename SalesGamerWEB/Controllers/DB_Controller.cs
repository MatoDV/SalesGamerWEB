using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace SalesGamerWEB.Controllers
{
    public static class DB_Controller
    {
        private static IConfiguration _configuration; // Agrega este campo
        public static string connectionString; // Agrega esta propiedad
        public static SqlConnection connection; // Mantén esta propiedad

        // Método para inicializar la conexión
        public static void Initialize(IConfiguration configuration) // Modifica el método para recibir IConfiguration como parámetro
        {
            _configuration = configuration; // Asigna IConfiguration al campo _configuration

            try
            {
                connectionString = _configuration.GetConnectionString("DefaultConnection");
                connection = new SqlConnection(connectionString);

                connection.Open();
                Debug.WriteLine("Conexión establecida correctamente.");

                // No cerrar la conexión aquí
            }
            catch (SqlException ex)
            {
                Debug.WriteLine("Error al establecer la conexión: " + ex.Message);
            }
        }
        public static void CloseConnection()
        {
            if (connection != null && connection.State == ConnectionState.Open)
            {
                connection.Close();
                Debug.WriteLine("Conexión cerrada correctamente.");
            }
        }
    }
}
