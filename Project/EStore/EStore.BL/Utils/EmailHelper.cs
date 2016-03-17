using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Web.Hosting;

// ReSharper disable AssignNullToNotNullAttribute

namespace EStore.BL.Utils
{
    public static class EmailHelper
    {
        public static NameValueCollection AppSettings;
        private static string _SmtpServer;
        private static int _SmtpPort;
        private static string _AdminEmail;
        private static string _LoginEmail;
        private static string _LoginPassword;
        private static bool _EnableSSL;
        private static bool _HtmlFormat;

        private static string _BccAll;
        private static string _TestEmailFrom;
        private static string _TestEmailTo;

        public static void SetConfig(NameValueCollection appSettings)
        {
            AppSettings = appSettings;
            Init();
        }

        private static void Init()
        {
            _SmtpServer = AppSettings["SmtpServer"];
            _SmtpPort = Convert.ToInt32(AppSettings["SmtpPort"]);
            _LoginEmail = AppSettings["LoginEmail"];
            _AdminEmail = AppSettings["AdminEmail"];
            _LoginPassword = AppSettings["LoginPassword"];
            _EnableSSL = Convert.ToBoolean(AppSettings["EnableSSL"]);
            _HtmlFormat = Convert.ToBoolean(AppSettings["HtmlFormat"]);

            _BccAll = AppSettings["BccAll"];
            _TestEmailFrom = AppSettings["TestEmailFrom"];
            _TestEmailTo = AppSettings["TestEmailTo"];
        }

//        public static void SendEmail(SentEmailItem model)
//        {
//            var message = new MailMessage(model.From, model.To, model.Subject, model.Body) { IsBodyHtml = true };
//            model.EmailToList.Skip(1).ToList().ForEach(x => message.To.Add(x));
//            model.BccList.ToList().ForEach(x => message.Bcc.Add(x));
//            model.CcList.ToList().ForEach(x => message.CC.Add(x));
//
//            foreach (var attachment in model.Attachments)
//            {
//                message.Attachments.Add(new Attachment(HostingEnvironment.MapPath(attachment.Path)));
//            }
//            SendEmail(message, true);
//        }

//        public static void SendTicketCreatedEmail(SaveTicketResult result, string body)
//        {
//            var subject = "Ticket #" + result.TicketCreatedModel.TicketId + " created";
//            SendEmail(subject, body, _AdminEmail, result.CustomerEmail);
//        }

//        public static void SendEmail(string subject, string body, string from, string to)
//        {
//            var mailMessage = new MailMessage(from, to, subject, body) { IsBodyHtml = true };
//            SendEmail(mailMessage);
//        }
//
//        public static void SendEmail(MailMessage message, bool? isHtmlFormat = null)
//        {
//            var body = message.Body;
//            var attachments = ProcessEmail(ref body);
//            message.Body = body;
//            attachments.ForEach(x => message.Attachments.Add(x));
//
//            var objSmtpClient = new SmtpClient(_SmtpServer, _SmtpPort);
//
//            isHtmlFormat = isHtmlFormat ?? _HtmlFormat;
//            message.IsBodyHtml = isHtmlFormat.Value;
//
//            if (!String.IsNullOrEmpty(_BccAll))
//                message.Bcc.Add(_BccAll);
//
//            if (!String.IsNullOrEmpty(_TestEmailFrom))
//                message.From = new MailAddress(_TestEmailFrom);
//
//            if (!String.IsNullOrEmpty(_TestEmailTo))
//            {
//                message.To.Clear();
//                message.To.Add(new MailAddress(_TestEmailTo));
//            }
//
//            objSmtpClient.EnableSsl = _EnableSSL;
//            objSmtpClient.UseDefaultCredentials = false;
//
//            objSmtpClient.Credentials = new NetworkCredential(_LoginEmail, _LoginPassword);
//            objSmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
//
//            objSmtpClient.Send(message);
//        }

//        public static List<Attachment> ProcessEmail(ref string emailBody)
//        {
//            var attachments = new List<Attachment>();
//            var doc = new HtmlDocument();
//            doc.LoadHtml(emailBody);
//            var imgNodes = doc.DocumentNode.Descendants("img").ToArray();
//            foreach (var node in imgNodes)
//            {
//                var imageName = Guid.NewGuid().ToString().Replace("-", string.Empty);
//                var srcAttribute = node.Attributes["src"];
//                var imageSource = srcAttribute.Value;
//
//                if (imageSource.StartsWith("http"))
//                    continue;
//
//                var linkedResource = GetAttachment(imageSource, imageName);
//                srcAttribute.Value = string.Format("cid:{0}", imageName);
//                attachments.Add(linkedResource);
//            }
//            emailBody = doc.DocumentNode.InnerHtml;
//            return attachments;
//        }

        private static Attachment GetAttachment(string imageURL, string imageName)
        {
            var attachment = new Attachment(HostingEnvironment.MapPath(imageURL));
            attachment.ContentId = imageName;
            attachment.ContentDisposition.Inline = true;
            attachment.ContentDisposition.DispositionType = DispositionTypeNames.Inline;
            return attachment;
        }
    }
}