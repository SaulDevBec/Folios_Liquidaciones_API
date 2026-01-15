using Folio_Liquidacion_API.Data.Security;
using Folio_Liquidacion_API.Models.Global;
using Folio_Liquidacion_API.Models.Liquidacion;
using Folio_Liquidacion_API.Models.Security;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Folio_Liquidacion_API.Data.Queries
{
    public class PrincipalQuery : ExecQuery
    {
		public DataTable GetTiemposLiquidacion(LiquidacionModel model)
		{
			try
			{
				string query = "SELECT * FROM [dbo].[BI.LE.TiempoLiquidacion]";
				query += (!string.IsNullOrEmpty(model.FechaDepositoConductor) && !string.IsNullOrEmpty(model.FechaDepositoConductorFin))
						 ? $" WHERE FechaDepositoConductor BETWEEN '{model.FechaDepositoConductor} 00:00:00' AND '{model.FechaDepositoConductorFin} 23:59:59'"
						 : "";
				query += (!string.IsNullOrEmpty(model.PrevioLiq) && !string.IsNullOrEmpty(model.PrevioLiqFin))
						 ? $"{(query.Contains("WHERE") ? " AND " : " WHERE ")} PrevioLiq BETWEEN '{model.PrevioLiq} 00:00:00' AND '{model.PrevioLiqFin} 23:59:59'"
						 : "";
				query += (!string.IsNullOrEmpty(model.EntregaDocumentos) && !string.IsNullOrEmpty(model.EntregaDocumentosFin))
						 ? $"{(query.Contains("WHERE") ? " AND " : " WHERE ")} EntregaDocumentos BETWEEN '{model.EntregaDocumentos} 00:00:00' AND '{model.EntregaDocumentosFin} 23:59:59'"
						 : " ";
				query += $"{(query.Contains("WHERE") ? " AND " : " WHERE ")} IdUnidadOrganizativa IN ( {model.UnidadOrganizativa})";
				query += " ORDER BY EntregaDocumentos DESC";

				return ExecQueryDT(query);

			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public DataTable GetUnidadOrganizativa(int? id)
        {
            try
            {
				var sqlQuery = string.Empty;
				sqlQuery = "SELECT * FROM [dbo].[General.UnidadOrganizativa] WHERE IdUnidadOrganizativa="+id;
				return ExecQueryDT(sqlQuery);
			}
			catch(Exception e)
            {
				throw new Exception(e.Message);
            }
        }
		public DataTable GetCatalogs(UserModel user)
		{
			var sqlQuery = string.Empty;

			sqlQuery = "SELECT IDUnidadOrganizativa AS Id, Descripcion AS Description" +
						   " FROM [dbo].[General.UnidadOrganizativa] WHERE IDUnidadOrganizativa IN(" + user.UnidadesOrganizativas + ")";


			return ExecQueryDT(sqlQuery);
		}
		public DataTable getEmpleados(InfoPageModel model)
		{
			try
			{
				var sqlQuery = string.Empty;

				sqlQuery = @" SELECT RH.IDEmpleado,  Convert(varchar(6), Rh.IDEmpleado)  + ' - '+ DP.ApellidoPaterno + ' ' + dp.ApellidoMaterno + ' ' + Dp.Nombre AS Nombre,
                             DP.ApellidoPaterno + ' ' + dp.ApellidoMaterno + ' ' + Dp.Nombre AS NombreC
                             FROM [Tracusa].[dbo].[RH.Empleado] RH
                             INNER JOIN [dbo].[RH.Empleado.DatosPersonales] DP ON RH.IDEmpleado = DP.IDEmpleado
                             WHERE  RH.IDUnidadOrganizativa in (" + model.User.UnidadesOrganizativas + ")  ANd RH.IDEmpleado not in(679,1749,5838,5840) ORDER BY dp.ApellidoPaterno, dp.ApellidoMaterno, dp.Nombre ";
				return ExecQueryDT(sqlQuery);

			}
			catch (Exception)
			{
				throw new Exception("Error al consultar la lista de Empleados.");
			}
		}
		public DataTable getEstatus()
        {
            try
            {
				var sqlQuery = string.Empty;
				sqlQuery = "SELECT Estatus FROM [dbo].[BI.LE.TiempoLiquidacion] GROUP BY Estatus having Estatus is not null";
				return ExecQueryDT(sqlQuery);


			}
			catch(Exception e)
            {
				throw new Exception(e.Message);
            }
        }
		public DataTable Getconductor(EmpleadoModel model)
        {
            try
            {
				var sqlQuery = string.Empty;

				sqlQuery = "SELECT RH.IDEmpleado, " +
								"DP.ApellidoPaterno + ' ' + DP.ApellidoMaterno + ' ' + DP.Nombre AS Nombre, " +
								"DP.ApellidoPaterno + ' ' + DP.ApellidoMaterno + ' ' + DP.Nombre AS NombreC, " +
								"RH.IDUnidadOrganizativa " +
								"FROM [dbo].[RH.Empleado] RH " +
								"INNER JOIN [dbo].[RH.Empleado.DatosPersonales] DP ON RH.IDEmpleado = DP.IDEmpleado " +
								$"WHERE RH.IDEmpleado = {model.Id} " +
								"AND RH.IDEstatus = 1 " +
								"AND RH.IDTipoEmpleado IN (2,4) " +
								$"AND RH.IDUnidadOrganizativa IN ({model.UnidadesOrganizativas})";
				return ExecQueryDT(sqlQuery);
			}
			catch(Exception e)
            {
				throw new Exception(e.Message);
            }
        }

		public bool IniciarLiquidacion(LiquidacionModel model)
        {
            try
            {
				var sqlQuery = string.Empty;

				sqlQuery = $"UPDATE [dbo].[BI.LE.TiempoLiquidacion] set IdEstatus = 20, Estatus = 'Liq. iniciada', FechaInicioProceso= GETDATE(), InicioLiq= GETDATE(), ResultadoLiq='Liq. iniciada', IdEjecutivoLiq={model.IdEjecutivoLiq}, " +
					$"EjecutivoLiq='{model.EjecutivoLiq}', UsuarioSAPLiq='{model.UsuarioLiquidacion}'" +
					$"WHERE FolioLiquidacion='{model.FolioLiquidacion}' ";
				return ExecQueryBool(sqlQuery);

			}
			catch(Exception e)
            {
				throw new Exception(e.Message);
            }
        }

		public bool UpdateEstatus(string Folio)
		{
			try
			{
				var sqlQuery = string.Empty;

				sqlQuery = $"UPDATE [dbo].[BI.LE.TiempoLiquidacion] set IdEstatus = 25, Estatus = 'Rechazo' " +
					$"WHERE FolioLiquidacion='{Folio}' ";  
				return ExecQueryBool(sqlQuery);

			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}


	}
}