using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace EvaCourier.API.Models
{
    public partial class Perfilopcion
    {
        public int Idperfilopcion { get; set; }
        public int Idperfil { get; set; }
        public int Idopcion { get; set; }
        public int Estado { get; set; }
        public int Crea { get; set; }
        public DateTime Fechacrea { get; set; }
        public int? Modifica { get; set; }
        public DateTime? Fechamodifica { get; set; }

        public virtual Opcion IdopcionNavigation { get; set; }
        public virtual Perfil IdperfilNavigation { get; set; }
    }
}
