using Folio_Liquidacion_API.Data.Queries;
using Folio_Liquidacion_API.Data.Security;
using Folio_Liquidacion_API.Models.Security;
using SAP.Middleware.Connector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Folio_Liquidacion_API.Services
{
    public class AccountService
    {
        #region Local variables
        private ExecQuery execQuery;
        private UserQuery userQuery;
        private PrincipalQuery pquery;
        #endregion  

        public AccountService()
        {
            execQuery = new ExecQuery();
            userQuery = new UserQuery();
            pquery = new PrincipalQuery();
        }




        public AccountModel VerificarFolio(AccountModel account)
        {
            var accountModel = new AccountModel();
            var userModel = new UserModel();
            var data = new DataSet();
            var dataT = new DataTable();
            //var access = false;

            try
            {
                //data = execQuery.ExecQueryDS(userQuery.GetUserInfo(account.User));
                dataT = execQuery.ExecQueryDT(userQuery.GetUserIniInfo(account.UserIni));
                var mensajeFolio = VerificarFolio(account.User);


                if (!mensajeFolio.Equals("Folio disponible."))
                {
                    accountModel.Message = "!El número de folio no existe¡";
                    return accountModel;
                }



                /*foreach (DataRow oRow in data.Tables[1].Rows)
                {
                    switch (oRow[0].ToString())
                    {
                        case "TransporteMovil":
                            access = true;
                            break;
                    }
                }*/

                /*if (!access)
                {
                    accountModel.Message = "!EL usuario no tiene acceso¡";
                    return accountModel;
                }*/


                //userModel.OrgUnits = data.Tables[0].Rows[0]["unidadesorganizativas"].ToString();
                //userModel.Permissions = data.Tables[1];
                //userModel.IdUser = (int)data.Tables[0].Rows[0]["IDEmpleado"];
                //userModel.UserName = string.IsNullOrEmpty(data.Tables[0].Rows[0]["Nombre"].ToString()) ? "" : CultureInfo.CurrentCulture.TextInfo.ToTitleCase(data.Tables[0].Rows[0]["Nombre"].ToString().ToLower());
                //userModel.ShortName = string.IsNullOrEmpty(data.Tables[0].Rows[0]["NombreAlias"].ToString()) ? "" : CultureInfo.CurrentCulture.TextInfo.ToTitleCase(data.Tables[0].Rows[0]["NombreAlias"].ToString().ToLower());
                //userModel.Phone = string.IsNullOrEmpty(data.Tables[0].Rows[0]["TelMovil1"].ToString()) ? "Número de teléfono celular no registrado" : data.Tables[0].Rows[0]["TelMovil1"].ToString();
                //userModel.Phone2 = string.IsNullOrEmpty(data.Tables[0].Rows[0]["TelMovil2"].ToString()) ? "Número de teléfono celular no registrado" : data.Tables[0].Rows[0]["TelMovil2"].ToString();
                //userModel.Uo = data.Tables[0].Rows[0]["Base"].ToString();
                //userModel.Circuit = data.Tables[0].Rows[0]["Circuito"].ToString();
                //userModel.IdOrgUnit = Convert.ToInt16(data.Tables[0].Rows[0]["IDUnidadOrganizativa"].ToString());
                //userModel.OrgUnitDescription = string.IsNullOrEmpty(data.Tables[0].Rows[0]["UnidadOrganizativa"].ToString()) ? "" : CultureInfo.CurrentCulture.TextInfo.ToTitleCase(data.Tables[0].Rows[0]["UnidadOrganizativa"].ToString().ToLower());
                //userModel.IdRole = Convert.ToInt16(data.Tables[0].Rows[0]["IDRol"].ToString());
                //userModel.RoleDescription = data.Tables[0].Rows[0]["Rol"].ToString();
                //userModel.IdYard = string.IsNullOrEmpty(data.Tables[0].Rows[0]["IDPatio"].ToString()) ? null : (int?)data.Tables[0].Rows[0]["IDPatio"];

                accountModel.MUser = userModel;
            }
            catch (Exception ex)
            {
                accountModel.Message = "Error: " + ex.ToString();
            }

            return accountModel;
        }

        public AccountModel LoginUser(AccountModel account)
        {
            var accountModel = new AccountModel();
            var userModel = new UserModel();
            var data = new DataSet();
            //var access = false;

            try
            {
                data = execQuery.ExecQueryDS(userQuery.GetUserInfo(account.User));

                if (data.Tables[0].Rows.Count == 0)
                {
                    accountModel.Message = "!El usuario no existe o es de tipo administrativo¡";
                    return accountModel;
                }

                /*if (Encryption.encryptionSHA1(account.Password) != data.Tables[0].Rows[0]["Password"].ToString())
                {
                    accountModel.Message = "!La contraseña es incorrecta¡";
                    return accountModel;
                }*/

                /*foreach (DataRow oRow in data.Tables[1].Rows)
                {
                    switch (oRow[0].ToString())
                    {
                        case "TransporteMovil":
                            access = true;
                            break;
                    }
                }*/

                /*if (!access)
                {
                    accountModel.Message = "!EL usuario no tiene acceso¡";
                    return accountModel;
                }*/

                //userModel.User = "";
                //userModel.OrgUnits = data.Tables[0].Rows[0]["unidadesorganizativas"].ToString();
                //userModel.Permissions = data.Tables[1];
                userModel.IDEmpleado = (int)data.Tables[0].Rows[0]["IDEmpleado"];
                userModel.Nombre = string.IsNullOrEmpty(data.Tables[0].Rows[0]["Nombre"].ToString()) ? "" : CultureInfo.CurrentCulture.TextInfo.ToTitleCase(data.Tables[0].Rows[0]["Nombre"].ToString().ToLower());
                userModel.NombreCorto = string.IsNullOrEmpty(data.Tables[0].Rows[0]["NombreAlias"].ToString()) ? "" : CultureInfo.CurrentCulture.TextInfo.ToTitleCase(data.Tables[0].Rows[0]["NombreAlias"].ToString().ToLower());
                userModel.Phone = string.IsNullOrEmpty(data.Tables[0].Rows[0]["TelMovil1"].ToString()) ? "Número de teléfono celular no registrado" : data.Tables[0].Rows[0]["TelMovil1"].ToString();
                userModel.Phone2 = string.IsNullOrEmpty(data.Tables[0].Rows[0]["TelMovil2"].ToString()) ? "Número de teléfono celular no registrado" : data.Tables[0].Rows[0]["TelMovil2"].ToString();
                userModel.Base = data.Tables[0].Rows[0]["Base"].ToString();
                userModel.Circuit = data.Tables[0].Rows[0]["Circuito"].ToString();
                //userModel.IdOrgUnit = Convert.ToInt16(data.Tables[0].Rows[0]["IDUnidadOrganizativa"].ToString());
                //userModel.OrgUnitDescription = string.IsNullOrEmpty(data.Tables[0].Rows[0]["UnidadOrganizativa"].ToString()) ? "" : CultureInfo.CurrentCulture.TextInfo.ToTitleCase(data.Tables[0].Rows[0]["UnidadOrganizativa"].ToString().ToLower());
                //userModel.IdRole = Convert.ToInt16(data.Tables[0].Rows[0]["IDRol"].ToString());
                //userModel.RoleDescription = data.Tables[0].Rows[0]["Rol"].ToString();
                //userModel.IdYard = string.IsNullOrEmpty(data.Tables[0].Rows[0]["IDPatio"].ToString()) ? null : (int?)data.Tables[0].Rows[0]["IDPatio"];

                accountModel.MUser = userModel;
            }

            catch (Exception ex)
            {
                accountModel.Message = "Error: " + ex.ToString();
            }

            return accountModel;
        }

        public string VerificarFolio(string folio)
        {
            try
            {
                SAPSystemConnect SAPCon = new SAPSystemConnect(SAPSystemConnect.Servers.PRD);
                RfcRepository repo = SAPCon.Repository;
                //IRfcTable _Entrada;
                IRfcFunction funcion = repo.CreateFunction("ZFI_LIQ_RECHAZO");

                #region Declaracion de Elementos de la Funcion SAP
                //Atributos
                //Import
                //Export
                //Tablas
                #endregion

                //Paso de parametros a la funcion
                funcion.SetValue("VL_FOLIO_TICKET", folio);
                funcion.SetValue("VL_ASIST", "");
                funcion.SetValue("VL_KM", "");
                funcion.SetValue("VL_DIESEL", "");
                funcion.SetValue("VL_TRANSP", "");
                funcion.SetValue("VL_VALES", "");
                funcion.SetValue("VL_DRIVE", "");
                funcion.SetValue("VL_CARGOS", "");
                funcion.SetValue("VL_DESCUENTOS", "");
                funcion.SetValue("VL_FLAG", "C");

                //Ejecutamos la funcion
                funcion.Invoke(SAPCon.Destination);


                //Procesamos el resultado que nos devuelve la funcion (tabla Export)
                var mensaje = string.Empty;
                mensaje = funcion.GetString("VL_MSG");


                return mensaje;
            }
            catch (RfcCommunicationException Exception)
            {
                //error de conexion
                throw Exception;
            }
            catch (RfcLogonException Exception)
            {
                // Error en login...
                throw Exception;
            }
            catch (RfcAbapRuntimeException Exception)
            {
                // Error ABAP en tiempo de ejecucion de parte de la funcion...
                throw Exception;
            }
            catch (RfcAbapBaseException Exception)
            {
                // The function module returned an ABAP exception, an ABAP message
                // or an ABAP class-based exception...
                throw Exception;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public string RechazarFolio(string folio, string[] motivos)
        {
            try
            {
                SAPSystemConnect SAPCon = new SAPSystemConnect(SAPSystemConnect.Servers.QAS);
                RfcRepository repo = SAPCon.Repository;
                //IRfcTable _Entrada;
                IRfcFunction funcion = repo.CreateFunction("ZFI_LIQ_RECHAZO");

                #region Declaracion de Elementos de la Funcion SAP
                //Atributos
                //Import
                //Export
                //Tablas
                #endregion

                //Paso de parametros a la funcion
                funcion.SetValue("VL_FOLIO_TICKET", folio);
                funcion.SetValue("VL_ASIST", motivos.Contains("Asistencia") ? "X" : "");
                funcion.SetValue("VL_KM", motivos.Contains("KM") ? "X" : "");
                funcion.SetValue("VL_DIESEL", motivos.Contains("Diesel") ? "X" : "");
                funcion.SetValue("VL_TRANSP", motivos.Contains("Transp (A,C)") ? "X" : "");
                funcion.SetValue("VL_VALES", motivos.Contains("Vales") ? "X" : "");
                funcion.SetValue("VL_DRIVE", motivos.Contains("Drive") ? "X" : "");
                funcion.SetValue("VL_CARGOS", motivos.Contains("Cargos no reconocidos") ? "X" : "");
                funcion.SetValue("VL_DESCUENTOS", motivos.Contains("Descuentos por INFONAVIT \\ FONACOT") ? "X" : "");
                funcion.SetValue("VL_FLAG", "A");

                //Ejecutamos la funcion
                funcion.Invoke(SAPCon.Destination);


                //Procesamos el resultado que nos devuelve la funcion (tabla Export)
                var mensaje = string.Empty;
                mensaje = funcion.GetString("VL_MSG");

                if (mensaje.Equals("Folio rechazado."))
                    pquery.UpdateEstatus(folio);


                return mensaje;
            }
            catch (RfcCommunicationException Exception)
            {
                //error de conexion
                throw Exception;
            }
            catch (RfcLogonException Exception)
            {
                // Error en login...
                throw Exception;
            }
            catch (RfcAbapRuntimeException Exception)
            {
                // Error ABAP en tiempo de ejecucion de parte de la funcion...
                throw Exception;
            }
            catch (RfcAbapBaseException Exception)
            {
                // The function module returned an ABAP exception, an ABAP message
                // or an ABAP class-based exception...
                throw Exception;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public List<string> GetTicket(int? idTrucker, string comments, int? unidadOrg, string userReg)
        {
            try
            {
                SAPSystemConnect SAPCon = new SAPSystemConnect(SAPSystemConnect.Servers.QAS);
                RfcRepository repo = SAPCon.Repository;
                //IRfcTable _Entrada;
                IRfcFunction funcion = repo.CreateFunction("ZFI_LIQ_TICKET");

                #region Declaracion de Elementos de la Funcion SAP
                //Atributos
                //Import
                //Export
                //Tablas
                #endregion

                string date = DateTime.Now.ToString("ddMMyyyy");
                string hour = DateTime.Now.ToString("HH:mm:ss");
                string organizacion = pquery.GetUnidadOrganizativa(unidadOrg).Rows[0]["descripcion"].ToString();

                //Paso de parametros a la funcion
                funcion.SetValue("P_NUMOPERADOR", idTrucker);
                funcion.SetValue("P_FECHA", DateTime.Now);
                funcion.SetValue("P_HORA", hour);
                funcion.SetValue("P_OBSERVACIONES", comments);
                funcion.SetValue("P_USER", userReg);
                funcion.SetValue("P_BASE", GetUnidadOrgId(organizacion.ToUpper()));

                //Ejecutamos la funcion
                funcion.Invoke(SAPCon.Destination);


                //Procesamos el resultado que nos devuelve la funcion (tabla Export)
                List<string> Datos;
                Datos = new List<string>();
                Datos.Add(funcion.GetString("FOLIO_TICKET"));
                Datos.Add(funcion.GetString("RESULTADO"));

                return Datos;
            }
            catch (RfcCommunicationException Exception)
            {
                //error de conexion
                throw Exception;
            }
            catch (RfcLogonException Exception)
            {
                // Error en login...
                throw Exception;
            }
            catch (RfcAbapRuntimeException Exception)
            {
                // Error ABAP en tiempo de ejecucion de parte de la funcion...
                throw Exception;
            }
            catch (RfcAbapBaseException Exception)
            {
                // The function module returned an ABAP exception, an ABAP message
                // or an ABAP class-based exception...
                throw Exception;
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        string GetUnidadOrgId(string unidadOrg)
        {
            switch (unidadOrg.Trim().ToUpper())
            {
                case "BASE BAJIO":
                    return "BJO";
                case "BASE MEXICO":
                    return "BMX";
                case "BASE EDOMEX":
                    return "EDX";
                case "CORPORATIVO":
                    return "COR";
                case "BASE MONTERREY":
                    return "MTY";
                case "BASE SANTA BARBARA":
                    return "SBA";
                case "P. LA MARINA":
                    return "PLM";
                case "MTTO MEXICO":
                    return "MTM";
                case "BASE JJMS":
                    return "JJM";
                case "BASE JAMS":
                    return "JAM";
                case "BASE FMS":
                    return "FMS";
                case "BASE MSV":
                    return "MSV";
                case "CECAP":
                    return "CCP";
                case "BASE GUADALAJARA":
                    return "GDL";
                case "CDS MEXICO":
                    return "CDM";
                case "BASE CENTRO":
                    return "CTR";
                case "CDS LEON":
                    return "CDL";
                case "CDS GUADALAJARA":
                    return "CDG";
                case "CDS MONTERREY":
                    return "CDT";
                case "BASE WM CUAUTITLAN LA LUZ":
                    return "WML";
                case "BASE PUEBLA":
                    return "PUE";
                case "BASE RMS":
                    return "RMS";
                case "BASE NVO LAREDO":
                    return "NLD";
                case "BASE NORTE":
                    return "NRT";
                case "BASE LAREDO TEXAS":
                    return "LTX";
                case "BASE REFRIGERADOS":
                    return "REF";
                case "BASE PORTACONTENEDORES":
                    return "PTC";
                case "BASE TRS FARMA":
                    return "TRF";
                case "BASE 3PL MTY":
                    return "3PM";

                default:
                    return null; // o string.Empty, o lanzar excepción si lo prefieres
            }
        }
        public List<string> GetTicket(int? idTrucker, string comments)
        {
            try
            {
                SAPSystemConnect SAPCon = new SAPSystemConnect(SAPSystemConnect.Servers.PRD);
                RfcRepository repo = SAPCon.Repository;
                //IRfcTable _Entrada;
                IRfcFunction funcion = repo.CreateFunction("ZFI_LIQ_TICKET");

                #region Declaracion de Elementos de la Funcion SAP
                //Atributos
                //Import
                //Export
                //Tablas
                #endregion

                string date = DateTime.Now.ToString("ddMMyyyy");
                string hour = DateTime.Now.ToString("HH:mm:ss");

                //Paso de parametros a la funcion
                funcion.SetValue("P_NUMOPERADOR", idTrucker);
                funcion.SetValue("P_FECHA", DateTime.Now);
                funcion.SetValue("P_HORA", hour);
                funcion.SetValue("P_OBSERVACIONES", comments);

                //Ejecutamos la funcion
                funcion.Invoke(SAPCon.Destination);


                //Procesamos el resultado que nos devuelve la funcion (tabla Export)
                List<string> Datos;
                Datos = new List<string>();
                Datos.Add(funcion.GetString("FOLIO_TICKET"));
                Datos.Add(funcion.GetString("RESULTADO"));

                return Datos;
            }
            catch (RfcCommunicationException Exception)
            {
                //error de conexion
                throw Exception;
            }
            catch (RfcLogonException Exception)
            {
                // Error en login...
                throw Exception;
            }
            catch (RfcAbapRuntimeException Exception)
            {
                // Error ABAP en tiempo de ejecucion de parte de la funcion...
                throw Exception;
            }
            catch (RfcAbapBaseException Exception)
            {
                // The function module returned an ABAP exception, an ABAP message
                // or an ABAP class-based exception...
                throw Exception;
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }
    }
}