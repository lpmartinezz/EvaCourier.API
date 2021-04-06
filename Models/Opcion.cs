using System;
using System.Collections.Generic;

namespace EvaCourier.API.Models
{
    public partial class Opcion
    {
        public Opcion()
        {
            Perfilopcion = new HashSet<Perfilopcion>();
        }

        public int Idopcion { get; set; }
        public string Nombreopcion { get; set; }
        public string Rutaopcion { get; set; }
        public int Estado { get; set; }
        public int Crea { get; set; }
        public DateTime Fechacrea { get; set; }
        public int? Modifica { get; set; }
        public DateTime? Fechamodifica { get; set; }

        public virtual ICollection<Perfilopcion> Perfilopcion { get; set; }
    }
}
