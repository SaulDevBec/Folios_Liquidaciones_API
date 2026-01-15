using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Folio_Liquidacion_API.Models.Liquidacion
{
    public class LiquidacionModel
    {
        public string FolioLiquidacion { get; set; }

        public string[] Motivos { get; set; }
        public int? IdEjecutivoLiq { get; set; }
        public string EjecutivoLiq { get; set; }

        public int? IdConductor { get; set; }
        public string Conductor { get; set; }

        public int? IdUnidadOrganizativa { get; set; }
        public string UnidadOrganizativa { get; set; }

        public int? IdCircuito { get; set; }
        public string Circuito { get; set; }

        public int? IdEjecutivoOper { get; set; }
        public string EjecutivoOper { get; set; }

        public int? IdEstatus { get; set; }
        public string Estatus { get; set; }

        public bool? Asistencia { get; set; }

        public bool? Km { get; set; }
        public bool? Diesel { get; set; }
        public bool? Transp { get; set; }
        public bool? Vales { get; set; }
        public bool? Drive { get; set; }
        public bool? CargosNoReconocidos { get; set; }
        public bool? DescuentosInfoFanc { get; set; }
        public int? NoLiq { get; set; }
        public string EntregaDocumentos { get; set; }
        public string EntregaDocumentosFin { get; set; }
        public string Inicio { get; set; }
        public string PrevioLiq { get; set; }
        public string PrevioLiqFin { get; set; }
        public string TransferenciaFl { get; set; }
        public string ResultadoLiq { get; set; }
        public string FechaDepositoConductor { get; set; }
        public string FechaDepositoConductorFin { get; set; }
        public decimal? TiempoTranscurrio { get; set; }

        public string Rechazos { get; set; }
        public string Observaciones { get; set; }

        public string FechaOperacion { get; set; }

        public string FechaLiquidacion { get; set; }
        public string UsuarioOperacion { get; set; }
        public string UsuarioRegistro { get; set; }
        public string UsuarioLiquidacion { get; set; }
        public string FechaInicio { get; set; }
        public string FechaFin { get; set; }
    }
}