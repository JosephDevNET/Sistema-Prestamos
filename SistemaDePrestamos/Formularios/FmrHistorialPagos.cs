using Negocio.Services;
using SistemaDePrestamos.Services;
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
    public partial class FmrHistorialPagos : Form
    {
        public FmrHistorialPagos()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.clear();
        }
        private void clear()
        {
            this.txt_NPrestamo.Clear();
            this.txt_Direccion.Clear();
            this.txt_Email.Clear();
            this.txt_Nombre.Clear();
            this.txt_MontoPrestamo.Clear();
            this.txt_FrecuenciaPago.ResetText();
            this.txt_Interes.ResetText();
            this.txt_MontoTotal.Clear();
            this.txt_Telefono.Clear();
            this.txt_TotalInteres.Clear();
            this.txt_NCuotas.ResetText();
            this.txt_MontoxCuota.Clear();
            dateTimePicker1.ResetText();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {

                var id = int.Parse(txt_NPrestamo.Text);
                PrestamosService prestamosService = new PrestamosService();
                var prestamo = prestamosService.Get(id);
                if (prestamo == null) MessageBox.Show("Rectifique el N° Prestamo");
                else
                {
                    var ClienteId = prestamo.id_cliente;
                    var Cliente = new ClienteService().Get(ClienteId);

                    txt_Nombre.Text = Cliente.nombre;
                    txt_Telefono.Text = Cliente.telefono;
                    txt_Email.Text = Cliente.email;
                    txt_Direccion.Text = Cliente.direccion;

                    txt_MontoPrestamo.Text = prestamo.monto.ToString();
                    txt_Interes.Text = prestamo.interes.ToString();
                    txt_FrecuenciaPago.Text = prestamo.Frecuencia_pagos;
                    dateTimePicker1.Value = prestamo.fecha_inicio;

                    var cuotas = new CuotasService();
                    var cuotasPrestamo = cuotas.Get().Where(x => x.idprestamo == id).ToList();

                    txt_NCuotas.Text = cuotasPrestamo.Count().ToString();
                    txt_MontoxCuota.Text = cuotasPrestamo[0].monto_cuota.ToString();
                    txt_MontoTotal.Text = (int.Parse(txt_NCuotas.Text) * decimal.Parse(txt_MontoxCuota.Text)).ToString();

                    this.dataGridView1.DataSource = cuotasPrestamo.Select(c => new
                    {
                        c.numero_cuota,
                        c.fecha_pago,
                        c.monto_cuota,
                        c.estado
                    }).ToList();
                }
            }catch(Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error: " + ex.Message);
            }
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "estado")
            {
                if (e.Value != null)
                {
                    switch (e.Value)
                    {
                        case "Pendiente":
                            e.CellStyle.BackColor = Color.Red;
                            e.CellStyle.ForeColor = Color.White;
                            break;
                        case "Cancelado":
                            e.CellStyle.BackColor = Color.Green;
                            e.CellStyle.ForeColor = Color.White;
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}
