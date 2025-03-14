using System.Net.Mail;
using System.Net;

namespace guslinks.Components.Uteis
{
    public static class Utilitarios
    {
        public static string EnvioEmail(List<string> To, string Body, string Subject, bool isBodyHtml, List<Attachment> lstAttachments)
        {
            try
            {
                SmtpClient smtp = new SmtpClient
                {
                    Host = "smtp.hostinger.com",
                    Port = 587,
                    EnableSsl = true,
                    UseDefaultCredentials = false,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new NetworkCredential("no-replay@gus.app.br", "A2gt21uo221@")
                };

                MailMessage mail = new MailMessage();

                mail.From = new MailAddress("no-replay@gus.app.br");

                foreach (string s in To)
                {
                    mail.To.Add(s);
                }

                mail.Body = Body;
                mail.Subject = Subject;
                mail.IsBodyHtml = isBodyHtml;

                foreach (Attachment attachment in lstAttachments)
                {
                    mail.Attachments.Add(attachment);
                }

                smtp.Send(mail);

                return "Teoricamente enviou o e-mail.";
            }
            catch (Exception ex)
            {
                return $"Erro ao enviar e-mail: {ex.Message}";
            }
        }

        public static string GeraToken()
        {
            DateTime data = DateTime.Now;
            var dia = data.Day;
            var mes = data.Month;
            var ano = data.Year;
            var hora = data.Hour;
            var minuto = data.Minute;
            var segundo = data.Second;

            Random random = new Random();
            int numeroAleatorio = random.Next(10000000, 99999999);

            string token = $"{dia}{mes}{ano}{hora}{minuto}{segundo}{numeroAleatorio}";

            return token;
        }
    }
}
