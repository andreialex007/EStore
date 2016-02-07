using System;
using System.Globalization;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace EStore.Web.Code
{
    public class JsonNetResult : JsonResult
    {
        public JsonSerializerSettings JsonSerializerSettings { get; set; }

        public JsonNetResult()
        {
            JsonSerializerSettings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            };
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            if (Data == null)
                return;

            var response = context.HttpContext.Response;
            response.ContentType = !String.IsNullOrEmpty(ContentType) ? ContentType : "application/json";

            if (ContentEncoding != null)
                response.ContentEncoding = ContentEncoding;

            JsonSerializerSettings.Converters.Add(new IsoDateTimeConverter
            {
                Culture = new CultureInfo("en-AU"),
                DateTimeFormat = "dd/MM/yyyy HH:mm:ss"
            });

            var serializedObject = JsonConvert.SerializeObject(Data, Formatting.Indented, JsonSerializerSettings);
            response.Write(serializedObject);
        }
    }
}