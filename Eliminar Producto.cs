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
    public partial class Eliminar_Producto : Form
    {
        public Eliminar_Producto()
        {
            InitializeComponent();
        }

        public void Limpiar()
        {
            txtCodigo.Text = "";
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            // Verificar que se haya ingresado un ID válido
            int idProducto;
            if (!int.TryParse(txtCodigo.Text, out idProducto))
            {
                MessageBox.Show("Por favor, ingrese un ID de producto válido.");
                return;
            }

            ConexionBD conexion = new ConexionBD();

            try
            {
                conexion.EliminarProducto(idProducto);
                conexion.listarProductos(dgvProductos);  // Actualizar la lista de productos
                Limpiar();  // Limpiar los campos del formulario
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar el producto: " + ex.Message);
            }
        }

        private void Eliminar_Producto_Load(object sender, EventArgs e)
        {
            ConexionBD conexion = new ConexionBD();
            conexion.listarProductos(dgvProductos);
        }

        private void agregarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Agregar_producto frm = new Agregar_producto();
            frm.ShowDialog();
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Modifica_producto frm = new Modifica_producto();
            frm.ShowDialog();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
