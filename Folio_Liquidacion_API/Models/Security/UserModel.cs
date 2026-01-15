using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Folio_Liquidacion_API.Models.Security
{
    public class UserModel
    {
		private string iDUsuario;
		private short? patio;
		private string patios;
		private int? iDEmpleado;
		private short? iDEstatus;
		private short? caducidadPassword;
		private DateTime? fechaCaducidadPassword;
		private DateTime? hoy;
		private string nombre;
		private short? unidadOrganizativa;
		private string unidadesOrganizativas;
		private string @base;
		private short? iDRol;
		private string departamento;
		private string passwordGral;
		private DataTable permits;
		private string message;
		private string nombreCorto;
		private string usuarioSAP;

		public string IDUsuario { get => iDUsuario; set => iDUsuario = value; }
		public Nullable<short> Patio { get => patio; set => patio = value; }
		public string Patios { get => patios; set => patios = value; }
		public Nullable<int> IDEmpleado { get => iDEmpleado; set => iDEmpleado = value; }
		public Nullable<short> IDEstatus { get => iDEstatus; set => iDEstatus = value; }
		public Nullable<short> CaducidadPassword { get => caducidadPassword; set => caducidadPassword = value; }
		public Nullable<System.DateTime> FechaCaducidadPassword { get => fechaCaducidadPassword; set => fechaCaducidadPassword = value; }
		public Nullable<System.DateTime> Hoy { get => hoy; set => hoy = value; }
		public string Nombre { get => nombre; set => nombre = value; }
		public Nullable<short> UnidadOrganizativa { get => unidadOrganizativa; set => unidadOrganizativa = value; }
		public string UnidadesOrganizativas { get => unidadesOrganizativas; set => unidadesOrganizativas = value; }
		public string Base { get => @base; set => @base = value; }
		public Nullable<short> IDRol { get => iDRol; set => iDRol = value; }
		public string Departamento { get => departamento; set => departamento = value; }
		public string PasswordGral { get => passwordGral; set => passwordGral = value; }
		public DataTable Permits { get => permits; set => permits = value; }
		public string Message { get => message; set => message = value; }
		public string NombreCorto { get => nombreCorto; set => nombreCorto = value; }
		public string UsuarioSAP { get => usuarioSAP; set => usuarioSAP = value; }

		public string Phone { get; set; }
		public string Phone2 { get; set; }
		public string Circuit { get; set; }
	}
}