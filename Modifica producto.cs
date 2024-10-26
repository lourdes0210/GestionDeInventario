using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace GestionDeInventario
{
    public partial class Modifica_producto : Form
    {
        public Modifica_producto()
        {
            InitializeComponent();
        }

        public void Limpiar()
        {
            txtCodigo.Text = "";
            txtNombre.Text = "";
            txtDescripcion.Text = "";
            txtPrecio.Text = "";
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            ProductosL productoModificado = new ProductosL();
            ConexionBD conexion = new ConexionBD();

            // Obtener el ID del producto a modificar (suponiendo que está en txtCodigo)
            int codigo;
            if (!int.TryParse(txtCodigo.Text, out codigo))
            {
                MessageBox.Show("Por favor, ingrese un ID de producto válido.");
                return;
            }

            productoModificado.codigo = codigo;
            productoModificado.nombre = txtNombre.Text;
            productoModificado.descripcion = txtDescripcion.Text;
            productoModificado.precio = double.Parse(txtPrecio.Text);
            productoModificado.stock = Convert.ToInt32(nupStock.Value);

            try
            {
                conexion.ModificarProductos(productoModificado);
                conexion.listarProductos(dgvProductos);
                conexion.llenarListbox(lstDescripcion, productoModificado.nombre, productoModificado.descripcion, (double)productoModificado.precio, productoModificado.stock);
                Limpiar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar el producto: " + ex.Message);
            }
        }

        private void Modifica_producto_Load(object sender, EventArgs e)
        {
            ConexionBD conexion = new ConexionBD();
            conexion.listarProductos(dgvProductos);
            conexion.LlenarcmbCategorias(cmbCategorias);
        }

        private void agregarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Agregar_producto frm = new Agregar_producto();
            frm.ShowDialog();
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Eliminar_Producto frm = new Eliminar_Producto();
            frm.ShowDialog();
        }

        private void reporteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reporte frm = new Reporte();
            frm.ShowDialog();
        }
    }
}
