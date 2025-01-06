using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaDePrestamos.Formularios
{
    public partial class FmrMain : Form
    {
        public FmrMain()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            FmrPrestamos fmrPrestamos = new FmrPrestamos();
            fmrPrestamos.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FmrPagos fmrPagos = new FmrPagos();
            fmrPagos.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FmrHistorialPagos fmrHistorialPagos = new FmrHistorialPagos();
            fmrHistorialPagos.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FmrClientes fmrClientes = new FmrClientes();
            fmrClientes.ShowDialog();
        }
    }
}
