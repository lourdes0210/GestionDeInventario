using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data;
using System.Windows.Forms;
using System.Collections;
using System.Data.SqlClient;

namespace GestionDeInventario
{
    public class ConexionBD
    {
        OleDbConnection conexion;
        OleDbCommand comando;
        OleDbDataAdapter adaptador;

        string cadena;

        public ConexionBD()
        {
            cadena = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source= ProductosL.accdb ";
        }

        public void listarProductos(DataGridView dgvProductos)
        {
            try
            {
                conexion = new OleDbConnection(cadena);
                comando = new OleDbCommand();

                comando.Connection = conexion;
                comando.CommandType = CommandType.Text;
                comando.CommandText = "SELECT * FROM Productos";// Selecciona todos los productos.  

                DataTable tablaProductos = new DataTable(); // Crea un DataTable para almacenar los datos.

                adaptador = new OleDbDataAdapter(comando);// Adaptador para llenar el DataTable
                adaptador.Fill(tablaProductos); // Llenar el DataTable con datos de la base de datos

                dgvProductos.DataSource = tablaProductos; // Asignar el DataTable al DataGridView para mostrar los productos.
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void listarProductosPorCodigo(DataGridView dgvProductos, ProductosL idProducto)
        {
            using (OleDbConnection conexion = new OleDbConnection(cadena))
            {
                using (OleDbCommand comando = new OleDbCommand())
                {
                    try
                    {
                        comando.Connection = conexion;
                        comando.CommandType = CommandType.Text;

                        comando.CommandText = "SELECT * FROM Productos WHERE Codigo = @idProducto";
                        comando.Parameters.AddWithValue("@idProducto", idProducto.codigo);

                        DataTable tablaProductos = new DataTable();
                        adaptador = new OleDbDataAdapter(comando);
                        adaptador.Fill(tablaProductos);

                        // Limpiar el DataGridView antes de asignar los nuevos datos
                        //dgvProductos.Rows.Clear();
                        dgvProductos.DataSource = tablaProductos;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        conexion.Close();
                    }
                }
            }
        }
        public DataTable ObtenerDatosInventario()
        {
            DataTable dt = new DataTable();
            string consulta = "SELECT Nombre, Stock FROM Productos";

            using (OleDbConnection conexion = new OleDbConnection(cadena))
            {
                using (OleDbDataAdapter adaptador = new OleDbDataAdapter(consulta, conexion))
                {
                    adaptador.Fill(dt);
                }
            }

            return dt;
        }

        public void CargarProductosBajoStock(ListBox listBox)
        {
            using (OleDbConnection conexion = new OleDbConnection(cadena))
            {
                using (OleDbCommand comando = new OleDbCommand())
                {
                    comando.Connection = conexion;
                    comando.CommandText = "SELECT Nombre, Stock FROM Productos WHERE Stock < @umbral";
                    comando.Parameters.AddWithValue("@umbral", 51); // Ajusta el umbral

                    conexion.Open();
                    using (OleDbDataReader reader = comando.ExecuteReader())
                    {
                        listBox.Items.Clear();
                        while (reader.Read())
                        {
                            string nombre = reader.GetString(0);
                            int stock = reader.GetInt32(1);
                            listBox.Items.Add($"{nombre} - Stock: {stock} (Bajo)");
                        }
                    }
                }
            }
        }

        public void llenarListbox(ListBox listBox, string nombre, string descripcion, double precio, int stock)
        {

            try
            {
                // Limpiar los elementos actuales del ListBox.
                listBox.Items.Clear();

                // Añadir el nuevo producto al ListBox.
                listBox.Items.Add($"{nombre} - {descripcion} - {precio:C} - Stock: {stock}");
            }
            catch (Exception ex)
            {
                // Mostrar mensaje en caso de error.
                MessageBox.Show(ex.Message);
            }
        }

        public void LlenarcmbCategorias(ComboBox cmbCategorias)
        {
            try
            {
                conexion = new OleDbConnection(cadena);
                comando = new OleDbCommand();

                comando.Connection = conexion;
                comando.CommandType = CommandType.Text;
                comando.CommandText = "SELECT DISTINCT Categoria FROM Productos";

                conexion.Open();

                cmbCategorias.Items.Clear();
                using (OleDbDataReader reader = comando.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cmbCategorias.Items.Add(reader.GetString(0));
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //conexion.Close();
            }
        }


        public void AgregarProducto(ProductosL producto)
        {
            using (OleDbConnection conexion = new OleDbConnection(cadena))
            {
                using (OleDbCommand comando = new OleDbCommand())
                {
                    comando.Connection = conexion;
                    comando.CommandType = CommandType.Text;
                    comando.CommandText = @"INSERT INTO Productos (Codigo, Nombre, Categoria, Descripcion, Precio, Stock)
                                    VALUES (@codigo,@nombre, @categoria, @descripcion, @precio, @stock)";

                    // Agregar los parámetros
                    comando.Parameters.AddWithValue("@codigo", producto.codigo);
                    comando.Parameters.AddWithValue("@nombre", producto.nombre);
                    comando.Parameters.AddWithValue("@categoria", producto.categoria);
                    comando.Parameters.AddWithValue("@descripcion", producto.descripcion);
                    comando.Parameters.AddWithValue("@precio", producto.precio);
                    comando.Parameters.AddWithValue("@stock", producto.stock);

                    try
                    {
                        conexion.Open();
                        comando.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error al agregar el producto: " + ex.Message);
                    }
                }
            }
        }

        public void ModificarProductos(ProductosL producto)
        {
            using (OleDbConnection conexion = new OleDbConnection(cadena))
            {
                using (OleDbCommand comando = new OleDbCommand())
                {
                    comando.Connection = conexion;
                    comando.CommandType = CommandType.Text;
                    comando.CommandText
                         = @"UPDATE Productos
                                 SET Codigo = @nuevoCodigo,
                                     Nombre = @nuevoNombre,
                                     Descripcion = @nuevaDescripcion,
                                     Precio = @nuevoPrecio,
                                     Stock = @nuevoStock
                                 WHERE Codigo = @codigo"; // Modificamos la condición WHERE

                    // Agregar los parámetros
                    comando.Parameters.AddWithValue("@nuevoCodigo", producto.codigo);
                    comando.Parameters.AddWithValue("@nombre", producto.nombre);
                    comando.Parameters.AddWithValue("@descripcion", producto.descripcion);

                    comando.Parameters.AddWithValue("@precio", producto.precio);
                    comando.Parameters.AddWithValue("@stock", producto.stock);
                    comando.Parameters.AddWithValue("@codigo", producto.codigo);


                    try
                    {
                        conexion.Open();
                        int filasAfectadas = comando.ExecuteNonQuery();

                        if (filasAfectadas > 0)
                        {
                            MessageBox.Show("Producto modificado correctamente.");
                        }
                        else
                        {
                            MessageBox.Show("No se encontró ningún producto con el ID especificado.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al modificar el producto: " + ex.Message);
                    }
                }
            }
        }


        public void EliminarProducto(int codigo)
        {
            using (OleDbConnection conexion = new OleDbConnection(cadena))
            {
                using (OleDbCommand comando = new OleDbCommand())
                {
                    comando.Connection = conexion;
                    comando.CommandType = CommandType.Text;
                    comando.CommandText = @"DELETE FROM Productos WHERE Codigo = @codigo";

                    // Agregar el parámetro
                    comando.Parameters.AddWithValue("@codigo", codigo);

                    try
                    {
                        conexion.Open();
                        int filasAfectadas = comando.ExecuteNonQuery();

                        if (filasAfectadas > 0)
                        {
                            MessageBox.Show("Producto eliminado correctamente.");
                        }
                        else
                        {
                            MessageBox.Show("No se encontró ningún producto con el ID especificado.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al eliminar el producto: " + ex.Message);
                    }
                }
            }

        }




    }
}
