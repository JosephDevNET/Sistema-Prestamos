using Negocio.DTOS;
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
    public partial class FmrClientesOPTS : Form
    {
        bool nuevo = false;
        bool editar = false;
        public FmrClientesOPTS()
        {
            InitializeComponent();
            this.Activar(false);
        }
        private void Activar(bool act)
        {
            if (act)
            {
                txt_Nombre.Enabled = true;
                txt_Email.Enabled = true;
                txt_Direccion.Enabled = true;
                txt_Telefono.Enabled = true;
                button2.Enabled = true;
            }
            else
            {
                txt_Nombre.Enabled = false;
                txt_Email.Enabled = false;
                txt_Direccion.Enabled = false;
                txt_Telefono.Enabled = false;
                button2.Enabled = false;
            }
        }
        public FmrClientesOPTS(int? id)
        {
            InitializeComponent();
            this.validar(id);
        }
        private void validar(int? id)
        {
            if(id != null)
            {
                var cliente = new ClienteService();
                var clienteDTO = cliente.Get((int)id);
                txt_id.Text = id.ToString();
                txt_Nombre.Text = clienteDTO.nombre;
                txt_Email.Text = clienteDTO.email;
                txt_Direccion.Text = clienteDTO.direccion;
                txt_Telefono.Text = clienteDTO.telefono;
                this.nuevo = false;
                this.editar = true;
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if(nuevo && editar == false)
            {
                var cliente = new ClienteDTO();

                cliente.Nombre = txt_Nombre.Text;
                cliente.Email = txt_Email.Text;
                cliente.Direccion = txt_Direccion.Text;
                cliente.Telefono = txt_Telefono.Text;

                var clienteInsertar = new ClienteService();
                if (clienteInsertar.Add(cliente))
                {
                    MessageBox.Show("Cliente insertado exitosamente");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Error al insertar el cliente");
                }
            }
            else if(editar && nuevo == false)
            {
                var cliente = new ClienteDTO();
                cliente.Nombre = txt_Nombre.Text;
                cliente.Email = txt_Email.Text;
                cliente.Direccion = txt_Direccion.Text;
                cliente.Telefono = txt_Telefono.Text;
                var clienteInsertar = new ClienteService();
                if (clienteInsertar.Update(cliente, int.Parse(txt_id.Text)))
                {
                    MessageBox.Show("Cliente actualizado exitosamente");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Error al actualizar el cliente");
                }
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            this.nuevo = true;
            this.editar = false;
            this.Activar(true);
        }
    }
}
