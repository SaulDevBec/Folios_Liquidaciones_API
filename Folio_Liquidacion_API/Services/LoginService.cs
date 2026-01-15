using Folio_Liquidacion_API.Data.Queries;
using Folio_Liquidacion_API.Data.Security;
using Folio_Liquidacion_API.Models.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Folio_Liquidacion_API.Services
{
    public class LoginService
    {
		#region Variables
		private UserQuery userQuery;
		#endregion

		public LoginService()
		{
			userQuery = new UserQuery();
		}

		public UserModel LogIn(AccountModel account)
		{
			try
			{
				var data = userQuery.GetUserInfo(account);

				if (data.Tables["User"].Rows.Count == 0)
					return new UserModel { Message = "¡El usuario no existe!" };

				if (Encryption.encryptionSHA1(account.Password) != data.Tables["User"].Rows[0]["Password"].ToString())
					return new UserModel { Message = "¡La contraseña es incorrecta!" };

				//if (data.Tables["Permits"].Select("IDPermiso = 'ProductividadOp'").Count<DataRow>() == 0)
				//    return new UserModel { Message = "!El usuario no tiene acceso¡" };

				var userModel = new UserModel
				{
					IDUsuario = data.Tables["User"].Rows[0]["IDUsuario"].ToString().Trim(),
					Patio = string.IsNullOrEmpty(data.Tables["User"].Rows[0]["IDPatio"].ToString()) ? (short?)null : Convert.ToInt16(data.Tables["User"].Rows[0]["IDPatio"].ToString().Trim()),
					Patios = data.Tables["User"].Rows[0]["IDPatios"].ToString().Trim(),
					IDEmpleado = string.IsNullOrEmpty(data.Tables["User"].Rows[0]["IDEmpleado"].ToString()) ? (short?)null : Convert.ToInt16(data.Tables["User"].Rows[0]["IDEmpleado"].ToString().Trim()),
					IDEstatus = string.IsNullOrEmpty(data.Tables["User"].Rows[0]["IDEstatus"].ToString()) ? (short?)null : Convert.ToInt16(data.Tables["User"].Rows[0]["IDEstatus"].ToString().Trim()),
					CaducidadPassword = string.IsNullOrEmpty(data.Tables["User"].Rows[0]["CaducidadPassword"].ToString()) ? (short?)null : Convert.ToInt16(data.Tables["User"].Rows[0]["CaducidadPassword"].ToString().Trim()),
					FechaCaducidadPassword = string.IsNullOrEmpty(data.Tables["User"].Rows[0]["FechaCaducidadPassword"].ToString()) ? (DateTime?)null : Convert.ToDateTime(data.Tables["User"].Rows[0]["FechaCaducidadPassword"].ToString().Trim()),
					Hoy = string.IsNullOrEmpty(data.Tables["User"].Rows[0]["Hoy"].ToString()) ? (DateTime?)null : Convert.ToDateTime(data.Tables["User"].Rows[0]["Hoy"].ToString().Trim()),
					Nombre = System.Globalization.CultureInfo.InvariantCulture.TextInfo.ToTitleCase(data.Tables["User"].Rows[0]["Nombre"].ToString().Trim().ToLower()),
					NombreCorto = System.Globalization.CultureInfo.InvariantCulture.TextInfo.ToTitleCase(data.Tables["User"].Rows[0]["NombreCorto"].ToString().Trim().ToLower()),
					UnidadOrganizativa = string.IsNullOrEmpty(data.Tables["User"].Rows[0]["IDUnidadOrganizativa"].ToString()) ? (short?)null : Convert.ToInt16(data.Tables["User"].Rows[0]["IDUnidadOrganizativa"].ToString().Trim()),
					UnidadesOrganizativas = data.Tables["User"].Rows[0]["UnidadesOrganizativas"].ToString().Trim(),
					Base = System.Globalization.CultureInfo.InvariantCulture.TextInfo.ToTitleCase(data.Tables["User"].Rows[0]["UO"].ToString().Trim().ToLower()),
					IDRol = string.IsNullOrEmpty(data.Tables["User"].Rows[0]["IDRol"].ToString()) ? (short?)null : Convert.ToInt16(data.Tables["User"].Rows[0]["IDRol"].ToString().Trim()),
					Departamento = System.Globalization.CultureInfo.InvariantCulture.TextInfo.ToTitleCase(data.Tables["User"].Rows[0]["Descripcion"].ToString().Trim().ToLower()),
					PasswordGral = data.Tables["User"].Rows[0]["PasswordGral"].ToString().Trim(),
					Permits = data.Tables["Permits"]
				};

				return userModel;
			}
			catch (Exception ex)
			{
				return new UserModel { Message = ex.Message };
			}
		}
	}
}