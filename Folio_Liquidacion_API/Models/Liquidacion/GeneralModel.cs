using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Folio_Liquidacion_API.Models.Liquidacion
{
    public class GeneralModel
    {
        public List<LiquidacionModel> reporte { get; set; }
        public TotalesModel totales { get; set; }
    }
}