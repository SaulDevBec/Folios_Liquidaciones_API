using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Folio_Liquidacion_API.Models.Liquidacion
{
    public class SelectModel
    {
		private string id;
		private string description;
		private string errorMessage;

		public string Id { get => id; set => id = value; }
		public string Description { get => description; set => description = value; }
		public string ErrorMessage { get => errorMessage; set => errorMessage = value; }
	}
}