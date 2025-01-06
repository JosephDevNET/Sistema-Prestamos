using Negocio.Services;
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
    public partial class FmrClientes : Form
    {
        public FmrClientes()
        {
            InitializeComponent();
            this.MostrarClientes();
        }

        private void MostrarClientes()
        {
            var clientes = new ClienteService();
            this.dataGridView1.DataSource = clientes.Get();
        }
        private int? GetId()
        {
            try
            {
                return int.Parse(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString());
            }
            catch
            {
                return null;
            }

        }

        //Buscar En el DataGrid
        private void button4_Click(object sender, EventArgs e)
        {
            var clientes = new ClienteService();

            if (comboBox1.SelectedIndex == 0)
            {
                this.dataGridView1.DataSource = clientes.Get().Where(x => x.nombre.ToLower() == txt_buscador.Text.ToLower()).ToList();
            }
            else if (comboBox1.SelectedIndex == 2)
            {
                this.dataGridView1.DataSource = clientes.Get().Where(x => x.telefono.ToLower() == txt_buscador.Text.ToLower()).ToList();
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                this.dataGridView1.DataSource = clientes.Get().Where(x => x.email.ToLower() == txt_buscador.Text.ToLower()).ToList();
            }
        }

        //Refrescar el DataGrid
        private void button5_Click(object sender, EventArgs e)
        {
            this.txt_buscador.Clear();
            this.MostrarClientes();
        }

        //Ingresar nuevo cliente
        private void button1_Click(object sender, EventArgs e)
        {
            var fmr = new FmrClientesOPTS();
            fmr.ShowDialog();
            this.MostrarClientes();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var fmr = new FmrClientesOPTS(GetId());
            fmr.ShowDialog();
            this.MostrarClientes();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var mensaje = MessageBox.Show("Desea eliminar el registro?","Eliminar",MessageBoxButtons.OKCancel,MessageBoxIcon.Warning);
            if(mensaje == DialogResult.OK)
            {
                var cliente = new ClienteService();
                if (cliente.Delete((int)GetId()))
                {
                    MessageBox.Show("Cliente eliminado exitosamente");
                    this.MostrarClientes();
                }
                else
                {
                    MessageBox.Show("Error al eliminar el cliente");
                }
            }
        }

        private void FmrClientes_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Show();
        }
    }
}
