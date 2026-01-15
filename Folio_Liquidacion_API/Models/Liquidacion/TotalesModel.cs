using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Folio_Liquidacion_API.Models.Liquidacion
{
    public class TotalesModel
    {
        public int TotalD { get; set; }
        public int TotalP { get; set; }
        public int TotalT { get; set; }
        public int Total { get; set; }
        public int TotalCL { get; set; }
        public string Base { get; set; }
        public string Periodo { get; set; }
    }
}