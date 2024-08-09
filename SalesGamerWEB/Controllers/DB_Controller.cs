using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace SalesGamerWEB.Controllers
{
    public static class DB_Controller
    {
        private static IConfiguration _configuration;
        public static string _connectionString;
        private static SqlConnection _connection;

        public static SqlConnection Connection
        {
            get
            {
                if (_connection == null)
                {
                    throw new InvalidOperationException("La conexión a la base de datos no ha sido inicializada. Llame a DB_Controller.Initialize antes de usarla.");
                }
                return _connection;
            }
        }

        public static void Initialize(IConfiguration configuration)
        {
            _configuration = configuration;

            try
            {
                _connectionString = _configuration.GetConnectionString("DefaultConnection");
                if (string.IsNullOrEmpty(_connectionString))
                {
                    throw new InvalidOperationException("La cadena de conexión no está configurada.");
                }

                _connection = new SqlConnection(_connectionString);
                _connection.Open();
                Debug.WriteLine("Conexión establecida correctamente.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error al establecer la conexión: " + ex.Message);
                throw; // Re-throw the exception to ensure it's handled properly
            }
        }

        public static void CloseConnection()
        {
            if (_connection != null && _connection.State == ConnectionState.Open)
            {
                _connection.Close();
                Debug.WriteLine("Conexión cerrada correctamente.");
            }
        }
    }
}
