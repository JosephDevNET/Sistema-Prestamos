using Negocio.DTOS;
using Negocio.Validation;
using SistemaDePrestamos.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Services
{
    public class ClienteService
    {
        public IEnumerable<dynamic> Get()
        {
            using (DataPrestamos data = new DataPrestamos())
            {
                var Clientes = data.Clientes.Select(c => new
                {
                    c.id_cliente,
                    c.nombre,
                    c.telefono,
                    c.email,
                    c.direccion,
                    c.fecha_registro
                }).ToList();

                return Clientes;
            }   
        }
        public Clientes Get(int id)
        {
            using (DataPrestamos data = new DataPrestamos())
            {
                return data.Clientes.FirstOrDefault(x=>x.id_cliente == id);
            }
        }
        public bool Add(ClienteDTO cliente)
        {
            if (ClienteValidation.ValidarCliente(cliente))
            {
                using (DataPrestamos data = new DataPrestamos())
                {
                    data.Clientes.Add(new Clientes
                    {
                        nombre = cliente.Nombre,
                        email = cliente.Email,
                        telefono = cliente.Telefono,
                        direccion = cliente.Direccion,
                        fecha_registro = DateTime.Now
                    });
                    data.SaveChanges();
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
        public bool Update(ClienteDTO cliente,int id)
        {
            using (DataPrestamos data = new DataPrestamos())
            {
                var clienteUpdate = data.Clientes.FirstOrDefault(x => x.id_cliente == id);
                if (clienteUpdate == null) return false;
                else
                {
                    clienteUpdate.nombre = cliente.Nombre;
                    clienteUpdate.email = cliente.Email;
                    clienteUpdate.telefono = cliente.Telefono;
                    clienteUpdate.direccion = cliente.Direccion;
                    clienteUpdate.fecha_registro = clienteUpdate.fecha_registro;
                    data.Entry(clienteUpdate).State = System.Data.EntityState.Modified;
                    data.SaveChanges();
                    return true;
                }
            }
        }
        public bool Delete(int id)
        {
            using (DataPrestamos data = new DataPrestamos())
            {
                var clienteDelete = data.Clientes.FirstOrDefault(x => x.id_cliente == id);
                if (clienteDelete == null) return false;
                else
                {
                    data.Clientes.Remove(clienteDelete);
                    data.SaveChanges();
                    return true;
                }
            }
        }
    }
}
