using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace GestionDeInventario
{
    public partial class Reporte : Form
    {
        public Reporte()
        {
            InitializeComponent();
            CargarDatosGrafico();
            ConexionBD conexion = new ConexionBD();
            conexion.CargarProductosBajoStock(lstControl);
        }

        private void CargarDatosGrafico()
        {
            ConexionBD conexion = new ConexionBD();
            // Obtener los datos de la base de datos
            DataTable dt = conexion.ObtenerDatosInventario();

            // Crear el gráfico y configurarlo
            chartInventario.Series.Clear();
            Series serie = new Series();
            serie.ChartType = SeriesChartType.Column; // Puedes cambiar el tipo de gráfico
            serie.Name = "Inventario";
            chartInventario.Series.Add(serie);

            foreach (DataRow row in dt.Rows)
            {
                serie.Points.AddXY(row["Nombre"], row["Stock"]);
            }

            // Personalizar el gráfico
            chartInventario.Titles.Add("Reporte de Inventario");
            chartInventario.ChartAreas[0].AxisX.Title = "Producto";
            chartInventario.ChartAreas[0].AxisY.Title = "Cantidad";
        }

        private void buscarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Buscar_Producto frm = new Buscar_Producto();
            frm.ShowDialog();
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
            Eliminar_Producto frm = new Eliminar_Producto();
            frm.ShowDialog();
        }
    }
}
