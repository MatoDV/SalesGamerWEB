using SalesGamerWEB.Models;
using System.Data.SqlClient;
using System.Diagnostics;

namespace SalesGamerWEB.Controllers
{
    public class Oferta_Controller
    {
        //OBTENER LA OFERTA

        public static List<Oferta> obtenerOfertas()
        {
            List<Oferta> list = new List<Oferta>();
            string query = "SELECT * FROM dbo.Oferta;";

            SqlCommand cmd = new SqlCommand(query, DB_Controller.Connection);

            try
            {
                DB_Controller.Connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    DateTime fechaInicio = reader.GetDateTime(reader.GetOrdinal("Fecha_inicio"));
                    DateTime fechaFinal = reader.GetDateTime(reader.GetOrdinal("Fecha_final"));

                    // Convertir DateTime a DateOnly
                    DateOnly fechaInicioDateOnly = new DateOnly(fechaInicio.Year, fechaInicio.Month, fechaInicio.Day);
                    DateOnly fechaFinalDateOnly = new DateOnly(fechaFinal.Year, fechaFinal.Month, fechaFinal.Day);

                    Oferta oferta = new Oferta(
                        id: reader.GetInt32(reader.GetOrdinal("Id")),
                        nombre: reader.GetString(reader.GetOrdinal("Nombre_producto")),
                        tipo_oferta: reader.GetString(reader.GetOrdinal("Tipo_oferta")),
                        fecha_inicio: fechaInicioDateOnly,
                        fecha_Final: fechaFinalDateOnly,
                        condiciones: reader.GetString(reader.GetOrdinal("condiciones"))
                    );

                    list.Add(oferta);
                    Trace.WriteLine("Producto encontrado, nombre: " + oferta.Nombre);
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
            string query = "select max(id) from dbo.Oferta;";

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

        //OBTENER OFERTA POR ID
        public static Oferta ObtenerOfertaID(int id)
        {
            Oferta ofer = new Oferta();
            string query = "SELECT * FROM dbo.Oferta WHERE id = @id;";
            using (SqlCommand cmd = new SqlCommand(query, DB_Controller.Connection))
            {
                cmd.Parameters.AddWithValue("@id", id);
                try
                {
                    DB_Controller.Connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        DateTime fechaInicio = reader.GetDateTime(reader.GetOrdinal("Fecha_inicio"));
                        DateTime fechaFinal = reader.GetDateTime(reader.GetOrdinal("Fecha_final"));

                        // Convertir DateTime a DateOnly
                        DateOnly fechaInicioDateOnly = new DateOnly(fechaInicio.Year, fechaInicio.Month, fechaInicio.Day);
                        DateOnly fechaFinalDateOnly = new DateOnly(fechaFinal.Year, fechaFinal.Month, fechaFinal.Day);

                        ofer = new Oferta
                        {
                            Id = reader.GetInt32(0),
                            Nombre = reader.GetString(1),
                            Tipo_oferta = reader.GetString(2),
                            Fecha_inicio = fechaInicioDateOnly,
                            Fecha_final = fechaFinalDateOnly,
                            Condiciones = reader.GetString(5),
                        };
                    }
                    reader.Close();
                }
                finally
                {
                    DB_Controller.Connection.Close();
                }
            }
            return ofer;
        }

        //CREAR OFERTA
        public static bool CrearOferta(Oferta ofert)
        {
            string query = "INSERT INTO dbo.Oferta (id,nombre,tipo_oferta,fecha_inicio,fecha_final,condiciones) " +
                           "VALUES (@id,@nombre,@tipo_oferta,@fecha_inicio,@fecha_final,@condiciones);";
            using (SqlCommand cmd = new SqlCommand(query, DB_Controller.Connection))
            {
                cmd.Parameters.AddWithValue("@id", obtenerMaxId() + 1);
                cmd.Parameters.AddWithValue("@nombre", ofert.Nombre);
                cmd.Parameters.AddWithValue("@tipo_oferta", ofert.Tipo_oferta);
                cmd.Parameters.AddWithValue("@fecha_inicio", ofert.Fecha_inicio);
                cmd.Parameters.AddWithValue("@fecha_final", ofert.Fecha_final);
                cmd.Parameters.AddWithValue("@condiciones", ofert.Condiciones);

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

        // EDITAR / CREAR OFERTA

        public static bool editarOferta(Oferta ofer)
        {
            //Darlo de alta en la BBDD

            string query = "update dbo.Oferta set nombre = @nombre , " +
                "tipo_oferta = @tipo_oferta, " +
                "fecha_inicio = @fecha_inicio, " +
                "fecha_final = @fecha_final, " +
                "condiciones = @condiciones, " +
                "where id = @id ;";

            SqlCommand cmd = new SqlCommand(query, DB_Controller.Connection);
            cmd.Parameters.AddWithValue("@id", ofer.Id);
            cmd.Parameters.AddWithValue("@nombre", ofer.Nombre);
            cmd.Parameters.AddWithValue("@tipo_oferta", ofer.Tipo_oferta);
            cmd.Parameters.AddWithValue("@fecha_inicio", ofer.Fecha_inicio);
            cmd.Parameters.AddWithValue("@fecha_final", ofer.Fecha_final);
            cmd.Parameters.AddWithValue("@condiciones", ofer.Condiciones);


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

        //Eliminar Oferta
        public static bool eliminarOferta(Oferta oferEliminar)
        {
            string query = "DELETE FROM dbo.Oferta WHERE id = @id;";

            using (SqlCommand cmd = new SqlCommand(query, DB_Controller.Connection))
            {
                cmd.Parameters.AddWithValue("@id", oferEliminar.Id);

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
