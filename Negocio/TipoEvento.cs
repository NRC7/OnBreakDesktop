using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public partial class TipoEvento
    {
        public TipoEvento()
        {

        }

        public int Id { get; set; }
        public string Nombre_evento { get; set; }
        public int Valor_base { get; set; }
        public int Personal_base { get; set; }
    }
}
