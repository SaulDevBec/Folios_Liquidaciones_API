using Folio_Liquidacion_API.Models.Global;
using Folio_Liquidacion_API.Models.Liquidacion;
using Folio_Liquidacion_API.Models.Security;
using Folio_Liquidacion_API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Folio_Liquidacion_API.Controllers
{
    public class LiquidacionController :ApiController
    {
        [HttpPost]
        [Route("api/Liquidacion/GetTicket")]
        public IHttpActionResult GetTicket(LiquidacionModel model)
        {
            var accountService = new AccountService();
            int? idConductor;
            try
            {
                idConductor = 500000 + model.IdConductor;
                var getTicket = accountService.GetTicket(idConductor, model.Observaciones, model.IdUnidadOrganizativa, model.UsuarioRegistro);

                if (!string.IsNullOrEmpty(getTicket[0].ToString()))
                {
                    if (string.IsNullOrEmpty(getTicket[1]?.ToString()))
                    {
                        return Ok(new
                        {
                            data = new
                            {
                                FolioLiquidacion = getTicket[0].ToString()
                            },
                            error = "0",
                            exists = "1"
                        });
                    }
                    else
                    {
                        // if (!string.IsNullOrEmpty(comments))
                        //     SendMail(mUser, getTicket[0].ToString(), comments);

                        return Ok(new
                        {
                            data = new
                            {
                                FolioLiquidacion = getTicket[0].ToString()
                            },
                            error = "0",
                            exists = "0"
                        });
                    }
                }

                return Ok(new
                {
                    data = "Error de proceso favor de contactar con sistemas",
                    error = "1"
                });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [Route("api/Liquidacion/VerificarFolio")]
        public IHttpActionResult VerificarFolio(string folio)
        {
            var accountService = new AccountService();
            var info = new InfoPageModel();

            try
            {
                string mensaje = accountService.VerificarFolio(folio);

                if (mensaje == "Folio disponible.")
                {
                    return Ok(new
                    {
                        data = info.User.IDUsuario,
                        error = "0"
                    });
                }

                return Ok(new
                {
                    data = mensaje,
                    error = "1"
                });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [Route("api/Liquidacion/ReporteGeneral")]
        public IHttpActionResult ReporteTiemposLiquidacion(LiquidacionModel model)
        {
            try
            {
                var service = new LiquidacionService();

                return Ok(new
                {
                    data = new
                    {
                        liquidacion = service.Reporte(model).reporte,
                        totales = service.Reporte(model).totales,
                    },
                    error = "0",
                    exists = "0"
                });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


        [HttpGet]
        [Route("api/Liquidacion/GetEmpleados")]
        public IHttpActionResult GetEmpleados()
        {
            var service = new LiquidacionService();

            try
            {
                var data = service.getEmpleado();

                return Ok(new
                {
                    data = data,
                    message = ""
                });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


        [HttpPost]
        [Route("api/Liquidacion/GetConductor")]
        public IHttpActionResult GetEmpleados(EmpleadoModel model)
        {
            var service = new LiquidacionService();

            try
            {
                var data = service.getConductor(model).FirstOrDefault();

                return Ok(new
                {
                    data = data,
                    message = ""
                });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("api/Liquidacion/GetEstatus")]
        public IHttpActionResult GetEstatus()
        {
            var service = new LiquidacionService();

            try
            {
                var data = service.GetEstatus();

                return Ok(new
                {
                    data = data,
                    message = ""
                });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [Route("api/Liquidacion/ReporteSeguimiento")]
        public IHttpActionResult ReporteSeguimiento(LiquidacionModel model)
        {
            try
            {
                LiquidacionService service = new LiquidacionService();

                return Ok(new
                {
                    data = service.Seguimiento(model),
                    error = "0",
                    exists = "0"
                });

            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [Route("api/Liquidacion/GetCatalogos")]
        public IHttpActionResult GetCatalogos(UserModel model)
        {
            LiquidacionService service = new LiquidacionService();

            try
            {

                return Ok(new
                {
                    data = service.GetCatalogs(model),
                    message = "",
                    error="0",
                }) ;
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            finally
            {
                GC.Collect();
            }
        }


        [HttpPost]
        [Route("api/Liquidacion/ActualizarRechazos")]
        public IHttpActionResult ActualizarRechazos(LiquidacionModel model)
        {
            var accountService = new AccountService();

            try
            {
                var mensaje = accountService.RechazarFolio(model.FolioLiquidacion, model.Motivos);

                if (mensaje.Equals("Folio rechazado."))
                    return Ok(new {
                        data = new
                        {
                            FolioLiquidacion = mensaje
                        }, error = "0", exists = "1" });
                else
                {
                    return Ok(new {
                        data = new
                        {
                            FolioLiquidacion = mensaje
                        }, error = "1", exists = "0" });
                }

            }
            catch (Exception ex)
            {
                return Ok(new { data = ex.ToString(), error = "1" });
            }
        }

        [HttpPost]
        [Route("api/Liquidacion/IniciarLiquidacion")]
        public IHttpActionResult IniciarLiquidacion(LiquidacionModel model)
        {
            LiquidacionService service = new LiquidacionService();

            try
            {
                return Ok(new { data = service.IniciarLiquidacion(model), error = "0", exists = "0" });
            }
            catch (Exception ex)
            {
                return Ok(new { data = ex.ToString(), error = "1" });
            }
        }

        private bool SendMail(UserModel mUser, string ticket, string comments)
        {
            try
            {
                var mail = new MailService();

                List<Addressee> lstAddressee = new List<Addressee>();

                lstAddressee.Add(new Addressee("TRS", "ealvarez@tracusa.com.mx"));
                lstAddressee.Add(new Addressee("TRS", "fsaucedo@tracusa.com.mx"));

                if (mail.SendMail("Ticket de liquidación: " + ticket,
                             "<h1>Se ha generado un nuevo ticket de liquidación con la siguiente información:</h1> <br/><br/>" +
                             "Folio ticket: <b>" + ticket + "</b>. <br/>" +
                             "Conductor: <b>" + mUser.Nombre + "</b>. <br/>" +
                             "Clave de empleado: <b>" + mUser.IDUsuario + "</b>. <br/>" +
                             "Número celular 1: <b>" + mUser.Phone + "</b>. <br/>" +
                             "Número celular 2: <b>" + mUser.Phone2 + "</b>. <br/>" +
                             "Base: <b>" + mUser.Base + "</b>. <br/>" +
                             "Circuito: <b>" + mUser.Circuit + "</b>. <br/>" +
                             "Comentario: <b>" + comments + "</b>. <br/>",
                             false,
                             lstAddressee.ToArray()))
                    return false;

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}