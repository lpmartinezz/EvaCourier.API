using System;
using System.Collections.Generic;

namespace EvaCourier.API.Models
{
    public partial class Ubigeo
    {
        public Ubigeo()
        {
            Usuario = new HashSet<Usuario>();
        }

        public int Idubigeo { get; set; }
        public string Codigoubigeo { get; set; }
        public string Departamento { get; set; }
        public string Provincia { get; set; }
        public string Distrito { get; set; }
        public int Estado { get; set; }
        public int Crea { get; set; }
        public DateTime Fechacrea { get; set; }
        public int? Modifica { get; set; }
        public DateTime? Fechamodifica { get; set; }

        public virtual ICollection<Usuario> Usuario { get; set; }
    }
}
