using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

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
