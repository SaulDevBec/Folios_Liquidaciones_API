using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace Folio_Liquidacion_API.Services
{
    public class MailService
    {
        public string _Emisor { get; set; }
        public string _EmailEmisor { get; set; }
        public string _PasswordEmisor { get; set; }
        public string _Destinatario { get; set; }
        public string _EmailDestinatario { get; set; }
        public string _Asunto { get; set; }
        public string _Mensaje { get; set; }
        public string _ServidorSMTP { get; set; }
        public Int32? _PuertoSMTP { get; set; }
        public string _Error { get; set; }

        public MailService()
        {
            _Emisor = "Desarrollo Tracusa Bajio";
            _EmailEmisor = "fsaucedo@tracusa.com.mx";
            _PasswordEmisor = "bbfsa27";
            _ServidorSMTP = "smtp.alestraune.net.mx";
            _PuertoSMTP = 587;
        }

        public MailService(string Emisor, string EmailEmisor, string Password, string ServidorSMTP, Int32? PuertoSMTP)
        {
            _Emisor = Emisor;
            _EmailEmisor = EmailEmisor;
            _PasswordEmisor = Password;
            _ServidorSMTP = ServidorSMTP;
            _PuertoSMTP = PuertoSMTP;
        }

        public bool SendMail(string Asunto, string Mensaje, bool SSL, params Addressee[] Destinatarios)
        {
            try
            {
                //Definir Correo
                MailMessage correo = new MailMessage();
                correo.From = new System.Net.Mail.MailAddress(_EmailEmisor, _Emisor);

                foreach (Addressee dest in Destinatarios)
                    correo.To.Add(new MailAddress(dest.Email, dest.Nombre));

                //correo.CC.Add();
                correo.Subject = Asunto;

                correo.Body = Mensaje;
                correo.IsBodyHtml = true;
                correo.Priority = MailPriority.Normal;

                //Definir Estancia SMTP
                SmtpClient smtp = new SmtpClient();
                //El servidor anfrition que enviara el correo electrónico
                smtp.Host = _ServidorSMTP;
                smtp.Port = int.Parse(_PuertoSMTP.ToString());

                // Indicador si se utilizaran los credenciales predeterminados
                smtp.UseDefaultCredentials = false;
                //Credenciales a utilizar para enviar el correo electrónico por medio del protocolo
                smtp.Credentials = new NetworkCredential(_EmailEmisor, _PasswordEmisor);
                // Indicador si esta habilitado el certificado SSL
                smtp.EnableSsl = SSL;


                //Enviar correo
                smtp.Send(correo);
                correo.Dispose();
                //MessageBox.Show("Corre electrónico fue enviado satisfactoriamente.", "Envio de correo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            catch (Exception ex)
            {
                _Error = ex.Message;
                return false;
                //throw new Exception("Error enviando correo electrónico: " + ex.Message);
                //MessageBox.Show("Error enviando correo electrónico: " + ex.Message, "Envio de correo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    public struct Addressee
    {
        public string Nombre;
        public string Email;

        public Addressee(string nombre, string email)
        {
            Nombre = nombre;
            Email = email;
        }
    }
}