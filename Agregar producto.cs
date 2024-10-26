using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestionDeInventario
{
    public partial class Agregar_producto : Form
    {
        public Agregar_producto()
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

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            ProductosL nuevoProducto = new ProductosL();
            ConexionBD conexion = new ConexionBD();

            // No asignar idProducto aquí, porque es autoincremental
            nuevoProducto.codigo = int.Parse(txtCodigo.Text);
            nuevoProducto.nombre = txtNombre.Text;
            nuevoProducto.categoria = cmbCategorias.Text;
            nuevoProducto.descripcion = txtDescripcion.Text;
            nuevoProducto.precio = double.Parse(txtPrecio.Text);
            nuevoProducto.stock = Convert.ToInt32(nupStock.Value);

            try
            {
                conexion.AgregarProducto(nuevoProducto);
                conexion.listarProductos(dgvProductos);
                conexion.llenarListbox(lstDescripcion, nuevoProducto.nombre, nuevoProducto.descripcion, (double)nuevoProducto.precio, nuevoProducto.stock);

                Limpiar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar el producto: " + ex.Message);
            }
        }

        private void Agregar_producto_Load(object sender, EventArgs e)
        {
            ConexionBD conexion = new ConexionBD();
            conexion.listarProductos(dgvProductos);
            conexion.LlenarcmbCategorias(cmbCategorias);
        }

        private void agregarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Modifica_producto frm = new Modifica_producto();
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

        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
