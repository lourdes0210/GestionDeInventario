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
    public partial class Inicio : Form
    {
        public Inicio()
        {
            InitializeComponent();
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

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Eliminar_Producto fr = new Eliminar_Producto();
            fr.ShowDialog();
        }

        private void buscarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Buscar_Producto frm = new Buscar_Producto();
            frm.ShowDialog();
        }

        private void reporteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reporte frm = new Reporte();
            frm.ShowDialog();
        }

        private void Inicio_Load(object sender, EventArgs e)
        {

        }
    }
}
