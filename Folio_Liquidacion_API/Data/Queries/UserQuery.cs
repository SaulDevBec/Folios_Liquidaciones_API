using Folio_Liquidacion_API.Data.Security;
using Folio_Liquidacion_API.Models.Security;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Folio_Liquidacion_API.Data.Queries
{
    public class UserQuery:ExecQuery
    {

        public string GetUserInfo(string user)
        {
            var sqlQuery = string.Empty;


            /*sqlQuery =  " SELECT RE.IDEmpleado, (DP.Nombre + ' ' + DP.ApellidoPaterno + ' ' + DP.ApellidoMaterno) AS Nombre, RE.NombreAlias";
            sqlQuery += "\n FROM[RH.Empleado] RE";
            sqlQuery += "\n LEFT OUTER JOIN[RH.Empleado.DatosPersonales] DP ON DP.IDEmpleado = RE.IDEmpleado";
            sqlQuery += "\n WHERE RE.IDEmpleado = " + user + " AND IDTipoEmpleado = 2 AND IDEstatus = 1";*/

            sqlQuery += "SELECT RE.IDEmpleado, (DP.Nombre + ' ' + DP.ApellidoPaterno + ' ' + DP.ApellidoMaterno) AS Nombre, RE.NombreAlias, DP.TelMovil1, DP.TelMovil2, UO.Descripcion AS 'Base', CR.Descripcion AS 'Circuito'";
            sqlQuery += "\n FROM[RH.Empleado] RE";
            sqlQuery += "\n LEFT OUTER JOIN[RH.Empleado.DatosPersonales] DP ON DP.IDEmpleado = RE.IDEmpleado";
            sqlQuery += "\n LEFT OUTER JOIN[dbo].[General.UnidadOrganizativa] UO ON RE.IDUnidadOrganizativa = UO.IDUnidadOrganizativa";
            sqlQuery += "\n LEFT OUTER JOIN[dbo].[General.Circuito] CR ON RE.IDCircuito = CR.IDCircuito";
            sqlQuery += "\n WHERE RE.IDEmpleado = " + user + " AND IDTipoEmpleado = 2 AND IDEstatus = 1";

            return sqlQuery;
        }

        public string GetUserIniInfo(string user)
        {
            var sqlQuery = string.Empty;

            sqlQuery = "SELECT SU.IDUsuario,SU.Password, SU.IDPatio, SU.IDPatios, SU.IDEmpleado, SU.IDEstatus, SU.CaducidadPassword, SU.FechaCaducidadPassword, getdate() AS Hoy,(DP.Nombre + ' ' + DP.ApellidoPaterno + ' ' + DP.ApellidoMaterno) AS Nombre,SU.IDUnidadOrganizativa,SU.UnidadesOrganizativas,UO.Descripcion AS UO,SU.IDRol,SR.Descripcion, \n" +
                       "(SELECT PasswordGeneral FROM [Seguridad]) AS PasswordGral, CASE WHEN (FechaCaducidadPassword IS NULL OR FechaCaducidadPassword>GETDATE()) THEN 0 ELSE 1 END AS StatusCaducidad \n" +
                       "From [Seguridad.Usuario] SU \n" +
                       "INNER JOIN [General.UnidadOrganizativa] UO ON SU.IDUnidadOrganizativa=UO.IDUnidadOrganizativa \n" +
                       "INNER JOIN [Seguridad.Rol] SR ON SU.IDRol = SR.IDRol \n" +
                       "LEFT OUTER JOIN [RH.Empleado.DatosPersonales] DP ON DP.IDEmpleado=SU.IDEmpleado \n" +
                       "WHERE SU.IDUsuario = '" + user + "'";

            return sqlQuery;
        }

        public DataSet GetUserInfo(AccountModel user)
        {
            var sqlQuery = string.Empty;

            sqlQuery = "SELECT SU.IDUsuario,SU.Password, SU.IDPatio, SU.IDPatios, SU.IDEmpleado, SU.IDEstatus, SU.CaducidadPassword, SU.FechaCaducidadPassword, getdate() AS Hoy,(DP.Nombre + ' ' + DP.ApellidoPaterno + ' ' + DP.ApellidoMaterno) AS Nombre, \n" +
                       "(DP.Nombre + ' ' + DP.ApellidoPaterno) AS NombreCorto, SU.IDUnidadOrganizativa,SU.UnidadesOrganizativas,UO.Descripcion AS UO,SU.IDRol,SR.Descripcion, \n" +
                       "(SELECT PasswordGeneral FROM [Seguridad]) AS PasswordGral, CASE WHEN (FechaCaducidadPassword IS NULL OR FechaCaducidadPassword>GETDATE()) THEN 0 ELSE 1 END AS StatusCaducidad \n" +
                       "From [Seguridad.Usuario] SU \n" +
                       "INNER JOIN [General.UnidadOrganizativa] UO ON SU.IDUnidadOrganizativa=UO.IDUnidadOrganizativa \n" +
                       "INNER JOIN [Seguridad.Rol] SR ON SU.IDRol = SR.IDRol \n" +
                       "LEFT OUTER JOIN [RH.Empleado.DatosPersonales] DP ON DP.IDEmpleado=SU.IDEmpleado \n" +
                       "WHERE SU.IDUsuario = '" + user.User + "' \n";

            sqlQuery += "\n SELECT SA.Objeto AS IDPermiso" +
                       "\n FROM[Seguridad.Acceso] SA " +
                       "\n INNER JOIN[Seguridad.Usuario_Acceso] SUA ON SUA.IDAcceso = SA.IDAcceso" +
                       "\n WHERE SUA.IDUsuario = '" + user.User + "' AND SUA.IDEstatus = 1 ";

            var data = ExecQueryDS(sqlQuery);
            data.Tables[0].TableName = "User";
            data.Tables[1].TableName = "Permits";

            return data;
        }
    }
}