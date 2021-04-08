using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace EvaCourier.API.Models
{
    public partial class Usuario
    {
        public int Idusuario { get; set; }
        public string Nombreusuario { get; set; }
        public int Idperfil { get; set; }
        public int Idubigeo { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Direccion01 { get; set; }
        public string Direccion02 { get; set; }
        public string Telefono01 { get; set; }
        public string Telefono02 { get; set; }
        public string Celular01 { get; set; }
        public string Celular02 { get; set; }
        public string Correo { get; set; }
        public string Correo2 { get; set; }
        public string Ubicacion01 { get; set; }
        public string Ubicacion02 { get; set; }
        public string Clave { get; set; }
        public int Intentos { get; set; }
        public bool Bloqueado { get; set; }
        public int Estado { get; set; }
        public int Crea { get; set; }
        public DateTime Fechacrea { get; set; }
        public int? Modifica { get; set; }
        public DateTime? Fechamodifica { get; set; }

        public virtual Perfil IdperfilNavigation { get; set; }
        public virtual Ubigeo IdubigeoNavigation { get; set; }
    }
}
