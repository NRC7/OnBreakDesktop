using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public partial class Cliente
    {
        public Cliente()
        {

        }

        public string Rut { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string TipoEmpresa { get; set; }
        public string ActividadEmpresa { get; set; }
        public string Servicio { get; set; }
        public string Contrato { get; set; }
    }
}
