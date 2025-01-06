using SistemaDePrestamos.Data;
using SistemaDePrestamos.DTOS;
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
    public partial class FmrPrestamos : Form
    {
        private int _id { get; set; }
        public FmrPrestamos()
        {
            InitializeComponent();
            this.LlenarComboFrecuencia();
            this.IDPrestamo();
        }
        private void IDPrestamo()
        {
            var prestamoMax = new PrestamosService();
            var maxPrestamo = prestamoMax.Get();
            if (maxPrestamo.Any()) // Verifica si la colección no está vacía
            {
                txt_NPrestamo.Text = maxPrestamo.Max(p=>p.id_prestamo).ToString();
            }
            else
            {
                txt_NPrestamo.Text = "1"; // O el valor predeterminado que desees
            }

        }
        private void LlenarComboFrecuencia()
        {
            var PrestamoFrecuencia = new PrestamosDTO();
            txt_FrecuenciaPago.Items.Clear();

            foreach (var item in Enum.GetValues(typeof(EFrecuencia)))
            {
                txt_FrecuenciaPago.Items.Add(item);
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            var clientes = new FmrClientesBuscar();
            clientes.ClienteSeleccionado += (id,nombre,email,telefono,direccion) =>
            {
                this._id = int.Parse(id);
                this.txt_Nombre.Text = nombre;
                this.txt_Email.Text = email;
                this.txt_Telefono.Text = telefono;
                this.txt_Direccion.Text = direccion;
            };
            clientes.ShowDialog();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                var prestamos = new PrestamosService();
                int cociente = 0;
                int residuo = 0;
                decimal monto_interes = 0;
                decimal montoprestamo = Convert.ToDecimal(txt_MontoPrestamo.Text.ToString());
                int porcentaje_interes = Convert.ToInt32(txt_Interes.Value.ToString());
                int nrocuotas = Convert.ToInt32(txt_NCuotas.Value.ToString());
                monto_interes = (montoprestamo * porcentaje_interes) / 100;
                montoprestamo += monto_interes;
                cociente = Convert.ToInt32((Math.Truncate(montoprestamo / nrocuotas)).ToString());
                residuo = Convert.ToInt32((montoprestamo % nrocuotas).ToString());

                txt_MontoxCuota.Text = cociente.ToString("0.00");
                txt_TotalInteres.Text = monto_interes.ToString("0.00");
                txt_MontoTotal.Text = montoprestamo.ToString("0.00");

                List<CuotasDTO> cuotas = new List<CuotasDTO>();
                for (int i = 0; i < nrocuotas; i++)
                {
                    var cuota = new CuotasDTO()
                    {
                        numero_cuota = i + 1,
                        fecha_pago = dateTimePicker1.Value.AddMonths(Convert.ToInt32(i)),
                        monto_cuota = cociente,
                    };

                    cuotas.Add(cuota);
                }

                this.dataGridView1.DataSource = cuotas.Select(c => new { c.numero_cuota, c.fecha_pago, c.monto_cuota }).ToList();
            }catch(Exception ex)
            {
                MessageBox.Show("Ocurrio un error: " + ex.Message);
            }
        }

        private void clear()
        {
            this.txt_Direccion.Clear();
            this.txt_Email.Clear();
            this.txt_Nombre.Clear();
            this.txt_MontoPrestamo.Clear();
            this.txt_FrecuenciaPago.SelectedIndex = 0;
            this.txt_Interes.ResetText();
            this.txt_MontoTotal.Clear();
            this.txt_Telefono.Clear();
            this.txt_TotalInteres.Clear();
            this.txt_NCuotas.ResetText();
            this.txt_MontoxCuota.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var prestamo = new PrestamosService();
            var cuota = new CuotasService();
            try
            {

                var prestamoDTO = new PrestamosDTO()
                {
                    id_cliente = this._id,
                    monto = float.Parse(txt_MontoPrestamo.Text),
                    interes = Convert.ToInt32(txt_Interes.Value),
                    plazo_meses = Convert.ToInt32(txt_NCuotas.Value),
                    Frecuencia_pagos = (EFrecuencia)txt_FrecuenciaPago.SelectedItem,
                    estado = "Activo",
                    fecha_inicio = DateTime.Now,
                    fecha_fin = DateTime.Now.AddMonths(Convert.ToInt32(txt_NCuotas.Value))
                };
                if (prestamo.Add(prestamoDTO))
                {
                    for (int i = 0; i < txt_NCuotas.Value; i++)
                    {
                        var cuotaDTO = new CuotasDTO()
                        {
                            idprestamo = int.Parse(txt_NPrestamo.Text),
                            numero_cuota = i + 1,
                            fecha_vencimiento = DateTime.Now,
                            fecha_pago = dateTimePicker1.Value.AddMonths(Convert.ToInt32(i)),
                            monto_cuota = decimal.Parse(txt_MontoxCuota.Text),
                            monto_pagado = 0,
                            estado = "Pendiente"
                        };

                        if (cuota.Add(cuotaDTO) == false)
                        {
                            MessageBox.Show("Error al agregar cuota");
                        }
                    }
                    MessageBox.Show("Prestamo agregado correctamente");

                    this.IDPrestamo();
                    this.clear();

                }
            }catch(Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error: " , ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.clear();
        }
    }
}
