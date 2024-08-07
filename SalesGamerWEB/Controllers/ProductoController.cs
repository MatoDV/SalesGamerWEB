using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SalesGamerWEB.Models;
using System.Data.SqlClient;
using System.Diagnostics;

namespace SalesGamerWEB.Controllers
{
    public class ProductoController : Controller
    {
        private readonly ILogger<ProductoController> _logger;

        public ProductoController(ILogger<ProductoController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(string categoria, int pagina = 1)
        {
            List<Producto> productos = ObtenerProductos(categoria, pagina, out int totalProductos);
            ViewBag.Categorias = ObtenerCategorias();
            ViewBag.PaginaActual = pagina;
            ViewBag.TotalPaginas = (int)Math.Ceiling((double)totalProductos / 9);
            ViewBag.Categoria = categoria;
            return View(productos);
        }

        //OBTENER EL PRODUCTO
        public static List<Producto> ObtenerProductos(string categoria, int pagina, out int totalProductos)
        {
            List<Producto> list = new List<Producto>();
            totalProductos = 0;

            string query = @"SELECT * FROM dbo.Producto";
            string countQuery = @"SELECT COUNT(*) FROM dbo.Producto";

            List<SqlParameter> parameters = new List<SqlParameter>();

            if (!string.IsNullOrEmpty(categoria))
            {
                query += " WHERE Categoria_id = (SELECT Id FROM dbo.Categoria WHERE Nombre_categoria = @categoria)";
                countQuery += " WHERE Categoria_id = (SELECT Id FROM dbo.Categoria WHERE Nombre_categoria = @categoria)";

                parameters.Add(new SqlParameter("@categoria", categoria));
            }

            query += " ORDER BY Id OFFSET @offset ROWS FETCH NEXT @rows ROWS ONLY;";

            // Define offset and rows parameters
            SqlParameter offsetParam = new SqlParameter("@offset", (pagina - 1) * 9);
            SqlParameter rowsParam = new SqlParameter("@rows", 9);

            // Add parameters for pagination
            parameters.Add(offsetParam);
            parameters.Add(rowsParam);

            try
            {
                DB_Controller.connection.Open();

                // Execute count query
                using (SqlCommand countCmd = new SqlCommand(countQuery, DB_Controller.connection))
                {
                    if (parameters.Count > 0 && !string.IsNullOrEmpty(categoria))
                    {
                        countCmd.Parameters.Add(parameters.First(p => p.ParameterName == "@categoria"));
                    }

                    totalProductos = (int)countCmd.ExecuteScalar();
                }

                // Execute product query
                using (SqlCommand cmd = new SqlCommand(query, DB_Controller.connection))
                {
                    foreach (var param in parameters)
                    {
                        if (param.ParameterName != "@offset" && param.ParameterName != "@rows")
                        {
                            cmd.Parameters.Add(param);
                        }
                    }

                    cmd.Parameters.Add(offsetParam);
                    cmd.Parameters.Add(rowsParam);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Producto producto = new Producto
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Nombre_producto = reader.GetString(reader.GetOrdinal("Nombre_producto")),
                                Precio = reader.GetInt32(reader.GetOrdinal("Precio"))
                            };

                            list.Add(producto);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Hay un error en la query: " + ex.Message);
            }
            finally
            {
                DB_Controller.connection.Close();
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

        public static List<Categoria> ObtenerCategorias()
        {
            List<Categoria> list = new List<Categoria>();
            string query = "SELECT * FROM dbo.Categoria;";

            try
            {
                DB_Controller.connection.Open();
                SqlCommand cmd = new SqlCommand(query, DB_Controller.connection);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Categoria categoria = new Categoria
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("Id")),
                        Nombre_categoria = reader.GetString(reader.GetOrdinal("Nombre_categoria"))
                    };

                    list.Add(categoria);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Hay un error en la query: " + ex.Message);
            }
            finally
            {
                DB_Controller.connection.Close();
            }

            return list;
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
            Producto producto = null;
            string query = "SELECT * FROM dbo.Producto WHERE id = @id;";

            using (SqlCommand cmd = new SqlCommand(query, DB_Controller.connection))
            {
                cmd.Parameters.AddWithValue("@id", id);

                try
                {
                    DB_Controller.connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        producto = new Producto
                        {
                            Id = reader.GetInt32(0),
                            Nombre_producto = reader.GetString(1),
                            Descripcion = reader.GetString(2),
                            Precio = reader.GetInt32(3),
                            Cantidad = reader.GetInt32(4),
                            Distribuidor_id = reader.GetInt32(5),
                            Oferta_id = reader.GetInt32(6),
                            Categoria_id = reader.GetInt32(7)
                        };

                        // Puedes cargar las entidades relacionadas aquí si es necesario
                        producto.Distribuidor = ObtenerDistribuidorId(producto.Distribuidor_id);
                        producto.Oferta = ObtenerOfertaId(producto.Oferta_id);
                        producto.Categoria = ObtenerCategoriaId(producto.Categoria_id);
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
            string query = "INSERT INTO dbo.Producto (id, nombre_producto, descripcion, precio, cantidad, Distribuidor_id, Oferta_id, Categoria_id) " +
                           "VALUES (@id, @nombre, @descripcion, @precio, @cantidad, @distribuidorId, @ofertaId, @categoriaId);";

            using (SqlCommand cmd = new SqlCommand(query, DB_Controller.connection))
            {
                cmd.Parameters.AddWithValue("@id", obtenerMaxId() + 1);
                cmd.Parameters.AddWithValue("@nombre", producto.Nombre_producto);
                cmd.Parameters.AddWithValue("@descripcion", producto.Descripcion);
                cmd.Parameters.AddWithValue("@precio", producto.Precio);
                cmd.Parameters.AddWithValue("@cantidad", producto.Cantidad);
                cmd.Parameters.AddWithValue("@distribuidorId", producto.Distribuidor_id);
                cmd.Parameters.AddWithValue("@ofertaId", producto.Oferta_id);
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

        public static bool EditarProducto(Producto prod)
        {
            string query = "UPDATE dbo.Producto SET nombre_producto = @nombre_producto, " +
                           "descripcion = @descripcion, " +
                           "precio = @precio, " +
                           "cantidad = @cantidad, " +
                           "Distribuidor_id = @Distribuidor_id, " +
                           "Oferta_id = @Oferta_id, " +
                           "Categoria_id = @Categoria_id " +
                           "WHERE id = @id;";

            using (SqlCommand cmd = new SqlCommand(query, DB_Controller.connection))
            {
                cmd.Parameters.AddWithValue("@id", prod.Id);
                cmd.Parameters.AddWithValue("@nombre_producto", prod.Nombre_producto);
                cmd.Parameters.AddWithValue("@descripcion", prod.Descripcion);
                cmd.Parameters.AddWithValue("@precio", prod.Precio);
                cmd.Parameters.AddWithValue("@cantidad", prod.Cantidad);
                cmd.Parameters.AddWithValue("@Distribuidor_id", prod.Distribuidor_id);
                cmd.Parameters.AddWithValue("@Oferta_id", prod.Oferta_id);
                cmd.Parameters.AddWithValue("@Categoria_id", prod.Categoria_id);

                try
                {
                    DB_Controller.connection.Open();
                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception ex)
                {
                    throw new Exception("Hay un error en la query: " + ex.Message);
                }
                finally
                {
                    DB_Controller.connection.Close();
                }
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
