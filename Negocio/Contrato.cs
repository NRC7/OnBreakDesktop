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
        public string IdContrato { get; set; }
        public string NumeroContrato { get; set; }
        public string FechaCreacion { get; set; }
        public string FechaTermino { get; set; }
        public string HoraCreacion { get; set; }
        public string HoraTermino { get; set; }
        public string Direccion { get; set; }
        public string EstadoContrato { get; set; }
        public string TipoDeEvento { get; set; }
        public string Observaciones { get; set; }
        public string DescripcionEvento { get; set; }
        public string ClienteAsociado { get; set; }
        public string FechaInicio { get; set; }
        public string HoraInicio { get; set; }
        public string Alimentacion { get; set; }
        public string Ambientacion { get; set; }
        public string MusicaAmbiental { get; set; }
        public string LocalEvento { get; set; }
        public string ValorContrato { get; set; }
    }
}
