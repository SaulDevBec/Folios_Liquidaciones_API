using Folio_Liquidacion_API.Data.Queries;
using Folio_Liquidacion_API.Data.Security;
using Folio_Liquidacion_API.Models.Global;
using Folio_Liquidacion_API.Models.Liquidacion;
using Folio_Liquidacion_API.Models.Security;
using SAP.Middleware.Connector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Folio_Liquidacion_API
{
    public class LiquidacionService
    {
        PrincipalQuery _query;

        public LiquidacionService()
        {
            _query = new PrincipalQuery();
        }


        public List<SelectModel> GetCatalogs(UserModel model)
        {
            try
            {
                List<SelectModel> dataModel = _query.GetCatalogs(model).AsEnumerable().Select(row => new SelectModel
                {
                    Id = row["Id"].ToString().Trim(),
                    Description = System.Globalization.CultureInfo.InvariantCulture.TextInfo.ToTitleCase(row["Description"].ToString().Trim())
                }).ToList();

                dataModel.Insert(0, new SelectModel
                {
                    Id = "0",
                    Description = "Todas las bases"
                });

                return dataModel;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                GC.Collect();
            }
        }

        public List<SelectModel> GetEstatus()
        {
            try
            {
                List<SelectModel> dataModel = _query.getEstatus().AsEnumerable().Select(row => new SelectModel
                {
                   
                    Description = System.Globalization.CultureInfo.InvariantCulture.TextInfo.ToTitleCase(row["Estatus"].ToString().Trim())
                }).ToList();

                return dataModel;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                GC.Collect();
            }
        }


        public List<EmpleadoModel> getConductor(EmpleadoModel model)
        {
            try
            {
                List<EmpleadoModel> dataModel = _query.Getconductor(model).AsEnumerable().Select(row => new EmpleadoModel
                {
                    
                    NombreEmpleado = row["Nombre"].ToString(),
                    Nombre = row["NombreC"].ToString(),
                    UnidadesOrganizativas = row["IDUnidadOrganizativa"].ToString()


                }).ToList();

                return dataModel;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public List<EmpleadoModel> getEmpleado()
        {
            try
            {
                var model = new InfoPageModel();

                List<EmpleadoModel> dataModel = _query.getEmpleados(model).AsEnumerable().Select(row => new EmpleadoModel
                {
                    Id = row["IDEmpleado"].ToString(),
                    NombreEmpleado = row["Nombre"].ToString(),
                    Nombre = row["NombreC"].ToString()


                }).ToList();

                return dataModel;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public GeneralModel Reporte(LiquidacionModel model)
        {
            try
            {
                var data = _query.GetTiemposLiquidacion(model).AsEnumerable().Select(row => new LiquidacionModel
                {
                    FolioLiquidacion = row.Field<string>("FolioLiquidacion"),
                    IdEjecutivoLiq = row.Field<int?>("IdEjecutivoLiq"),
                    EjecutivoLiq = row.Field<string>("EjecutivoLiq"),
                    IdConductor = row.Field<int?>("IdConductor"),
                    Conductor = row.Field<string>("Conductor"),
                    IdUnidadOrganizativa = row.Field<int?>("IdUnidadOrganizativa"),
                    UnidadOrganizativa = row.Field<string>("UnidadOrganizativa"),
                    IdCircuito = row.Field<int?>("IdCircuito"),
                    Circuito = row.Field<string>("Circuito"),
                    IdEjecutivoOper = row.Field<int?>("IdEjecutivoOper"),
                    EjecutivoOper = row.Field<string>("EjecutivoOper"),
                    IdEstatus = row.Field<int?>("IdEstatus"),
                    Estatus = row.Field<string>("Estatus"),
                    Asistencia = row.Field<bool?>("Asistencia"),
                    Km = row.Field<bool?>("Km"),
                    Diesel = row.Field<bool?>("Diesel"),
                    Transp = row.Field<bool?>("Transp"),
                    Vales = row.Field<bool?>("Vales"),
                    Drive = row.Field<bool?>("Drive"),
                    CargosNoReconocidos = row.Field<bool?>("CargosNoReconocidos"),
                    DescuentosInfoFanc = row.Field<bool?>("DescuentosInfoFanct"),
                    NoLiq = row.Field<int?>("NoLiq"),
                    EntregaDocumentos = row["EntregaDocumentos"].ToString(),
                    Inicio = row["InicioLiq"].ToString(),
                    PrevioLiq = row["PrevioLiq"].ToString(),
                    TransferenciaFl = row["TransferenciaFI"].ToString(),
                    ResultadoLiq = row.Field<string>("ResultadoLiq"),
                    FechaDepositoConductor = row["FechaDepositoConductor"].ToString(),
                    TiempoTranscurrio = row.Field<decimal?>("TiempoTranscurrio")
                }).ToList();

                //if(!string.IsNullOrEmpty(model.IdUnidadOrganizativa.ToString()))
                //    data = data.Where(x => x.IdUnidadOrganizativa.ToString().Equals(model.IdUnidadOrganizativa.ToString())).ToList();


                

                if (!string.IsNullOrEmpty(model.Circuito))
                {
                    if(model.Circuito.Equals("LIQUIDADO"))
                        data = data.Where(x => !string.IsNullOrEmpty(x.NoLiq?.ToString())).ToList();

                    if (model.Circuito.Equals("NO_LIQUIDADO"))
                        data = data.Where(x => string.IsNullOrEmpty(x.NoLiq?.ToString())).ToList();
                }

                if (!string.IsNullOrEmpty(model.Rechazos))
                {
                    if (model.Rechazos.Equals("RECHAZO"))
                        data = data = data.Where(x => new bool?[]
                        {
                            x.Asistencia,
                            x.Km,
                            x.Diesel,
                            x.Transp,
                            x.Vales,
                            x.Drive,
                            x.CargosNoReconocidos,
                            x.DescuentosInfoFanc
                        }.Any(b => b == true))
                        .ToList();

                    if (model.Rechazos.Equals("NO_RECHAZO"))
                        data = data = data.Where(x => new bool?[]
                        {
                            x.Asistencia,
                            x.Km,
                            x.Diesel,
                            x.Transp,
                            x.Vales,
                            x.Drive,
                            x.CargosNoReconocidos,
                            x.DescuentosInfoFanc
                        }.All(b => b != true))
                        .ToList();
                }


                
                TotalesModel total = new TotalesModel();
                total.Total = data.Count;
                total.TotalT = data.Count(x => string.Equals(x.Estatus, "Terminada"));
                total.TotalP = data.Count(x => string.Equals(x.ResultadoLiq, "Saldo a favor"));
                total.TotalD = data.Count(x => string.Equals(x.ResultadoLiq, "Saldo en contra"));
                total.TotalCL = total.TotalP + total.TotalD;
                if (model.UnidadOrganizativa.Equals("0") || model.IdUnidadOrganizativa == 0)
                    total.Base = "Todas las bases";
                else
                {
                    total.Base = string.IsNullOrEmpty(model.UnidadOrganizativa) ?
                   _query.GetUnidadOrganizativa(model.IdUnidadOrganizativa).Rows[0]["descripcion"].ToString() : _query.GetUnidadOrganizativa(int.Parse(model.UnidadOrganizativa)).Rows[0]["descripcion"].ToString();

                }


                GeneralModel datos = new GeneralModel();
                datos.reporte = data;
                datos.totales = total;


                return datos;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public List<LiquidacionModel> Seguimiento(LiquidacionModel model)
        {
            try
            {



                SAPSystemConnect SAPCon = new SAPSystemConnect(SAPSystemConnect.Servers.PRD);
                RfcRepository repo = SAPCon.Repository;
                //IRfcTable _Entrada;
                IRfcFunction funcion = repo.CreateFunction("ZFI_GET_LIQ_TICKET");
                IRfcTable _SALIDA;
                #region Declaracion de Elementos de la Funcion SAP
                //Atributos
                //Import
                //Export
                //Tablas
                #endregion

                //Paso de parametros a la funcion
                funcion.SetValue("IV_FECHA_INI", Convert.ToDateTime(model.FechaInicio).ToString("yyyyMMdd"));
                funcion.SetValue("IV_FECHA_FIN", Convert.ToDateTime(model.FechaFin).ToString("yyyyMMdd"));

                _SALIDA = funcion.GetTable("ET_RESULTADO");

                //Ejecutamos la funcion
                funcion.Invoke(SAPCon.Destination);

                List<LiquidacionModel> data = new List<LiquidacionModel>();
                //Procesamos el resultado que nos devuelve la funcion (tabla Export)
                for (int i = 0; i < _SALIDA.RowCount; i++)
                {
                    _SALIDA.CurrentIndex = i;
                    data.Add(new LiquidacionModel
                    {
                        FolioLiquidacion = _SALIDA.GetString("FOLIO_TICKET"),
                        IdConductor = _SALIDA.GetInt("PERNR"),
                        Observaciones = _SALIDA.GetString("OBSERVACIONES"),
                        FechaOperacion = _SALIDA.GetString("FECHA_OPER") + " " + _SALIDA.GetString("HORA_OPER"),
                        FechaLiquidacion = _SALIDA.GetString("FECHA_LIQ") + " " + _SALIDA.GetString("HORA_LIQ"),
                        UsuarioOperacion = _SALIDA.GetString("USUARIO_OPER"),
                        UsuarioLiquidacion = _SALIDA.GetString("USUARIO_LIQ"),
                        UsuarioRegistro = _SALIDA.GetString("USUARIO_REG"),
                        Km = _SALIDA.GetChar("KM").Equals('X'),
                        Diesel = _SALIDA.GetChar("DIESEL").Equals('X'),
                        Asistencia = _SALIDA.GetChar("ASIST").Equals('X'),
                        Transp = _SALIDA.GetChar("TRANSP").Equals('X'),
                        Vales = _SALIDA.GetChar("VALES").Equals('X'),
                        CargosNoReconocidos = _SALIDA.GetChar("CARGOS").Equals('X'),
                        Drive = _SALIDA.GetChar("DRIVE").Equals('X'),
                        DescuentosInfoFanc = _SALIDA.GetChar("DESCUENTOS").Equals('X'),
                        NoLiq = !string.IsNullOrEmpty(_SALIDA.GetString("NOLIQ")) ? int.Parse(_SALIDA.GetString("NOLIQ")) : 0,

                    });
                }



                return data;

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

        public bool IniciarLiquidacion(LiquidacionModel model)
        {
            try
            {
                return _query.IniciarLiquidacion(model);

            }catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}