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
    public partial class FmrClientesBuscar : Form
    {
        public event Action<string,string,string,string,string> ClienteSeleccionado;
        public FmrClientesBuscar()
        {
            InitializeComponent();
            this.MostrarClientes();
        }
        private void MostrarClientes()
        {
            var clientes = new ClienteService();
            this.dataGridView1.DataSource = clientes.Get();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Asegúrate de que se ha hecho clic en una fila válida
            {
                var id = dataGridView1.Rows[e.RowIndex].Cells["id_cliente"].Value.ToString(); // Ajusta el nombre de la columna según sea necesario
                var clienteSeleccionado = dataGridView1.Rows[e.RowIndex].Cells["nombre"].Value.ToString(); // Ajusta el nombre de la columna según sea necesario
                var email = dataGridView1.Rows[e.RowIndex].Cells["email"].Value.ToString(); // Ajusta el nombre de la columna según sea necesario
                var telefono = dataGridView1.Rows[e.RowIndex].Cells["telefono"].Value.ToString(); // Ajusta el nombre de la columna según sea necesario
                var direccion = dataGridView1.Rows[e.RowIndex].Cells["direccion"].Value.ToString(); // Ajusta el nombre de la columna según sea necesario

                ClienteSeleccionado?.Invoke(id,clienteSeleccionado,email,telefono,direccion); // Llama al evento
                this.Close(); // Cierra el formulario después de la selección (opcional)
            }
        }
    }
}
