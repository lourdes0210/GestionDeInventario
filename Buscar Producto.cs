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
    public partial class Buscar_Producto : Form
    {
        public Buscar_Producto()
        {
            InitializeComponent();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            ProductosL productos = new ProductosL();
            int idProducto;

            if (!int.TryParse(txtCodigo.Text, out idProducto))
            {
                MessageBox.Show("Por favor, ingrese un ID de producto válido (debe ser un número entero).");
                return;
            }

            productos.codigo = idProducto;

            ConexionBD conexion = new ConexionBD();

            try
            {
                conexion.listarProductosPorCodigo(dgvProductos, productos);


                // Mostrar mensaje si no se encuentra el producto
                if (dgvProductos.Rows.Count == 0)
                {
                    MessageBox.Show("No se encontró ningún producto con el código ingresado.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar el producto: " + ex.Message);
            }
        }

        private void agregarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Agregar_producto frm = new Agregar_producto();
            frm.ShowDialog();
        }

        private void modiToolStripMenuItem_Click(object sender, EventArgs e)
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
    }
}
