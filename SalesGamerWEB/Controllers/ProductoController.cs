using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SalesGamerWEB.Models;
using System.Data.SqlClient;
using System.Diagnostics;

namespace SalesGamerWEB.Controllers
{
    public class ProductoController : Controller
    {
        public IActionResult Index()
        {
            var productos = obtenerProductos();
            return View(productos);
        }
        //OBTENER EL PRODUCTO
        public static List<Producto> obtenerProductos()
            {
                List<Producto> list = new List<Producto>();
                string query = "SELECT * FROM dbo.Producto;";

                SqlCommand cmd = new SqlCommand(query, DB_Controller.connection);

                try
                {
                    DB_Controller.connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Producto producto = new Producto(
                            id: reader.GetInt32(reader.GetOrdinal("Id")),
                            nombre: reader.GetString(reader.GetOrdinal("Nombre_producto")),
                            desc: reader.GetString(reader.GetOrdinal("Descripcion")),
                            precio: reader.GetInt32(reader.GetOrdinal("Precio")),
                            cantidad: reader.GetInt32(reader.GetOrdinal("Cantidad")),
                            distribuidor_id: ObtenerDistribuidorId(reader.GetInt32(reader.GetOrdinal("Distribuidor_id"))),
                            oferta_id: ObtenerOfertaId(reader.GetInt32(reader.GetOrdinal("Oferta_id"))),
                            Imagen: (byte[])reader["img"],
                            categoria_id: ObtenerCategoriaId(reader.GetInt32(reader.GetOrdinal("Categoria_id")))
                        );

                        list.Add(producto);
                        Trace.WriteLine("Producto encontrado, nombre: " + producto.Nombre_producto);
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

            //SACAR EL MAXID
            public static int obtenerMaxId()
            {
                int MaxId = 0;
                string query = "select max(id) from dbo.Producto;";

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

            public static Distribuidor ObtenerDistribuidorId(int id)
            {
                Distribuidor distribuidor = new Distribuidor();
                string query = "SELECT * FROM dbo.Distribuidor WHERE id = @id;";
                using (SqlCommand cmd = new SqlCommand(query, DB_Controller.connection))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    try
                    {
                        DB_Controller.connection.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            distribuidor = new Distribuidor
                            {
                                Id = reader.GetInt32(0),
                                Nombre_empresa = reader.GetString(1),
                                stock = reader.GetInt32(2),
                            };
                        }
                        reader.Close();
                    }
                    finally
                    {
                        DB_Controller.connection.Close();
                    }
                }
                return distribuidor;
            }
            public static Oferta ObtenerOfertaId(int id)
            {
                Oferta oferta = new Oferta();
                string query = "SELECT * FROM dbo.Oferta WHERE id = @id;";
                using (SqlCommand cmd = new SqlCommand(query, DB_Controller.connection))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    try
                    {
                        DB_Controller.connection.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            DateTime fechaInicio = reader.GetDateTime(reader.GetOrdinal("Fecha_inicio"));
                            DateTime fechaFinal = reader.GetDateTime(reader.GetOrdinal("Fecha_final"));

                            // Convertir DateTime a DateOnly
                            DateOnly fechaInicioDateOnly = new DateOnly(fechaInicio.Year, fechaInicio.Month, fechaInicio.Day);
                            DateOnly fechaFinalDateOnly = new DateOnly(fechaFinal.Year, fechaFinal.Month, fechaFinal.Day);

                            oferta = new Oferta
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
                        DB_Controller.connection.Close();
                    }
                }
                return oferta;
            }
            public static Categoria ObtenerCategoriaId(int id)
            {
                Categoria categoria = new Categoria();
                string query = "SELECT * FROM dbo.Categoria WHERE id = @id;";
                using (SqlCommand cmd = new SqlCommand(query, DB_Controller.connection))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    try
                    {
                        DB_Controller.connection.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            categoria = new Categoria
                            {
                                Id = reader.GetInt32(0),
                                Nombre_categoria = reader.GetString(1)
                            };
                        }
                        reader.Close();
                    }
                    finally
                    {
                        DB_Controller.connection.Close();
                    }
                }
                return categoria;
            }

        //OBTENER PRODUCTO POR ID
        public static Producto ObtenerProductoID(int id)
            {
                Producto producto = new Producto();
                string query = "SELECT * FROM dbo.Producto WHERE id = @id;";
                using (SqlCommand cmd = new SqlCommand(query, DB_Controller.connection))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    try
                    {
                        DB_Controller.connection.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            int distribuidorId = reader.GetInt32(5);
                            Distribuidor distribuidor = ObtenerDistribuidorId(distribuidorId);
                            int ofertaId = reader.GetInt32(6);
                            Oferta oferta = ObtenerOfertaId(ofertaId);
                            int categoriaId = reader.GetInt32(7);
                            Categoria categoria = ObtenerCategoriaId(categoriaId);

                        producto = new Producto
                            {
                                Id = reader.GetInt32(0),
                                Nombre_producto = reader.GetString(1),
                                Descripcion = reader.GetString(2),
                                Precio = reader.GetInt32(3),
                                Cantidad = reader.GetInt32(4),
                                Distribuidor_id = distribuidor,
                                Oferta_id = oferta,
                                img = (byte[])reader["img"],
                                Categoria_id = categoria
                            };
                        }
                        reader.Close();
                    }
                    finally
                    {
                        DB_Controller.connection.Close();
                    }
                }
                return producto;
            }

            //CREAR PRODUCTO
            public static bool CrearProducto(Producto producto)
            {
                string query = "INSERT INTO dbo.Producto (id,nombre_producto, descripcion, precio, cantidad, Distribuidor_id, Oferta_id, imagen, Categoria_id) " +
                               "VALUES (@id,@nombre, @descripcion, @precio, @cantidad, @distribuidorId, @ofertaId, @imagen, @categoriaId);";
                using (SqlCommand cmd = new SqlCommand(query, DB_Controller.connection))
                {
                    cmd.Parameters.AddWithValue("@id", obtenerMaxId() + 1);
                    cmd.Parameters.AddWithValue("@nombre", producto.Nombre_producto);
                    cmd.Parameters.AddWithValue("@descripcion", producto.Descripcion);
                    cmd.Parameters.AddWithValue("@precio", producto.Precio);
                    cmd.Parameters.AddWithValue("@cantidad", producto.Cantidad);
                    cmd.Parameters.AddWithValue("@distribuidorId", producto.Distribuidor_id);
                    cmd.Parameters.AddWithValue("@ofertaId", producto.Oferta_id);
                    cmd.Parameters.AddWithValue("@imagen", producto.img);
                    cmd.Parameters.AddWithValue("@categoriaId", producto.Categoria_id);
                try
                {
                        DB_Controller.connection.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                    finally
                    {
                        DB_Controller.connection.Close();
                    }
                }
            }

            // EDITAR / CREAR PRODUCTO

            public static bool editarProducto(Producto prod)
            {
                //Darlo de alta en la BBDD

                string query = "update dbo.Producto set nombre_producto = @nombre_producto , " +
                    "descripcion = @descripcion , " +
                    "precio = @precio , " +
                    "cantidad = @cantidad ," +
                    "Distribuidor_id = @Distribuidor_id ," +
                    "Oferta_id = @Oferta_id ," +
                    "imagen = @imagen ," +
                    "Categoria_id = @Categoria_id " +
                    "where id = @id ;";

            SqlCommand cmd = new SqlCommand(query, DB_Controller.connection);
                cmd.Parameters.AddWithValue("@id", prod.Id);
                cmd.Parameters.AddWithValue("@nombre_producto", prod.Nombre_producto);
                cmd.Parameters.AddWithValue("@descripcion", prod.Descripcion);
                cmd.Parameters.AddWithValue("@precio", prod.Precio);
                cmd.Parameters.AddWithValue("@cantidad", prod.Cantidad);
                cmd.Parameters.AddWithValue("@Distribuidor_id", prod.Distribuidor_id);
                cmd.Parameters.AddWithValue("@Oferta_id", prod.Oferta_id);
                cmd.Parameters.AddWithValue("@imagen", prod.img);
                cmd.Parameters.AddWithValue("@Categoria_id", prod.Categoria_id);

            try
            {
                    DB_Controller.connection.Open();
                    cmd.ExecuteNonQuery();
                    DB_Controller.connection.Close();
                    return true;
                }
                catch (Exception ex)
                {
                    throw new Exception("Hay un error en la query: " + ex.Message);
                }

            }

            public static bool eliminarProducto(Producto prodEliminar)
            {
                string query = "DELETE FROM dbo.Producto WHERE id = @id;";

                using (SqlCommand cmd = new SqlCommand(query, DB_Controller.connection))
                {
                    cmd.Parameters.AddWithValue("@id", prodEliminar.Id);

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
