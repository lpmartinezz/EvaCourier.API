using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

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
