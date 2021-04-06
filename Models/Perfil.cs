using System;
using System.Collections.Generic;

namespace EvaCourier.API.Models
{
    public partial class Perfil
    {
        public Perfil()
        {
            Perfilopcion = new HashSet<Perfilopcion>();
            Usuario = new HashSet<Usuario>();
        }

        public int Idperfil { get; set; }
        public string Nombreperfil { get; set; }
        public int Estado { get; set; }
        public int Crea { get; set; }
        public DateTime Fechacrea { get; set; }
        public int? Modifica { get; set; }
        public DateTime? Fechamodifica { get; set; }

        public virtual ICollection<Perfilopcion> Perfilopcion { get; set; }
        public virtual ICollection<Usuario> Usuario { get; set; }
    }
}
