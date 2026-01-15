using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Folio_Liquidacion_API.Models.Liquidacion
{
    public class EmpleadoModel
    {
        public string Id { get; set; }
        public string NombreEmpleado { get; set; }

        public string Nombre { get; set; }

        public string NombreGerente { get; set; }

        public string Correo { get; set; }

        public string Telefono { get; set; }
        public string TelefonoGerente { get; set; }
        public string Base { get; set; }

        public string IdBase { get; set; }

        public string UnidadesOrganizativas { get; set; }
    }
}