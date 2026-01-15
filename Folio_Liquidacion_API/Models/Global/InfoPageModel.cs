using Folio_Liquidacion_API.Models.Liquidacion;
using Folio_Liquidacion_API.Models.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Folio_Liquidacion_API.Models.Global
{
    public class InfoPageModel
    {
		private int page;
		private UserModel user;
		private string message;
		private bool messageType;

		public int Page { get => page; set => page = value; }
		public UserModel User
		{
			get
			{
				HttpContext context = System.Web.HttpContext.Current;
				return user = (UserModel)context.Session["User"];
			}
			set => user = value;
		}
		public string Message { get => message; set => message = value; }
		public bool MessageType { get => messageType; set => messageType = value; }
		public List<SelectModel> OrgUnity { get; set; }
	}
}