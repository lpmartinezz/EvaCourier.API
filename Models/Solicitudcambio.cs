using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace EvaCourier.API.Models
{
    public partial class Solicitudcambio
    {
        public long Idsolicitudcambio { get; set; }
        public string Correo { get; set; }
        public string Codigogenerado { get; set; }
        public int Estadocodigo { get; set; }
        public DateTime Fechasolicitud { get; set; }
        public DateTime Fechavencimiento { get; set; }
        public bool Estado { get; set; }
        public int Crea { get; set; }
        public DateTime Fechacrea { get; set; }
        public int? Modifica { get; set; }
        public DateTime? Fechamodifica { get; set; }
    }
}
