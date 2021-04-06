using System;
using System.Collections.Generic;

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
