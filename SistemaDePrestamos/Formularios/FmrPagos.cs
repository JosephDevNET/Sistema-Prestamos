using Negocio.Services;
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
    public partial class FmrPagos : Form
    {
        public FmrPagos()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                var id = int.Parse(txt_NPrestamo.Text);
                PrestamosService prestamosService = new PrestamosService();
                var prestamo = prestamosService.Get(id);
                if(prestamo != null)
                {
                    this.button1.Enabled = true;

                }

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

                //Conseguir la primera cuota pendiente
                var primercuota = cuotasPrestamo.FirstOrDefault(x => x.estado == "Pendiente");
                txt_NCuota.Text = primercuota.numero_cuota.ToString();
                txt_FechaLimite.Text = primercuota.fecha_vencimiento.ToString();
                txtMontoxCuota.Text = primercuota.monto_cuota.ToString();
            }catch(Exception ex)
            {
                MessageBox.Show("No se pudo obtener el número del prestamo. Rectifique el N°", ex.Message);
            }
        }

        //Pagar cuota
        private void button1_Click(object sender, EventArgs e)
        {
            var id = int.Parse(txt_NPrestamo.Text);
            var cuotas = new CuotasService();
            var cuotasPrestamo = cuotas.Get().Where(x => x.idprestamo == id).ToList();
            var primercuota = cuotasPrestamo.FirstOrDefault(x => x.estado == "Pendiente");

            var CuotaDTO = new CuotasDTO()
            {
                numero_cuota = primercuota.numero_cuota,
                fecha_vencimiento = primercuota.fecha_vencimiento,
                monto_cuota = primercuota.monto_cuota,
                monto_pagado = primercuota.monto_cuota,
                estado = "Cancelado"
            };
            
            int IdCuota = primercuota.idcuota;

            if (cuotas.Update(CuotaDTO, IdCuota))
            {
                MessageBox.Show("Cuota PAGADO!");
                this.button1.Enabled = false;
                this.clear();
            }
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
            this.txt_NCuota.Clear();
            this.txt_FechaLimite.Clear();
            this.txtMontoxCuota.Clear();
            dateTimePicker1.ResetText();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.clear();
        }
    }
}
