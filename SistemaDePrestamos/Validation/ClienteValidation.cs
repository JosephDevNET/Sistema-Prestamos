using Negocio.DTOS;
using SistemaDePrestamos.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Validation
{
    public class ClienteValidation
    {
        public static bool ValidarCliente(ClienteDTO cliente)
        {
            if (cliente == null) return false;
            if(string.IsNullOrEmpty(cliente.Nombre) 
            || string.IsNullOrEmpty(cliente.Telefono) || string.IsNullOrEmpty(cliente.Direccion) 
            || ValidarEmail(cliente.Email) == false)
            {
                return false;
            }

            return true;
        }

        private static bool ValidarEmail(string email)
        {
            if (email.Contains("@"))
            {
                using (DataPrestamos context = new DataPrestamos())
                {
                    bool emailExist = context.Clientes.Any(x => x.email.ToLower() == email.ToLower());
                    if (emailExist) return false;
                    else return true;
                }
            }
            else return false;
        }
    }
}
