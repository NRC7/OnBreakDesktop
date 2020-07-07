using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public partial class Contrato
    {
        public Contrato()
        {

        }

        public string NumeroContrato { get; set; }
        public string FechaCreacion { get; set; }
        public string FechaTermino { get; set; }
        public string HoraInicio { get; set; }
        public string HoraTermino { get; set; }
        public string Direccion { get; set; }
        public string EstadoContrato { get; set; }
        public string TipoEvento { get; set; }
        public string Observaciones { get; set; }
        public string ClienteAsociado { get; set; }
        public int ValorContrato { get; set; }

    }
}
