using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Shared.NuGet
{
    public static class ObjectExtensions
    {
        private static readonly JsonSerializerSettings _jsonSerializationOptions = new JsonSerializerSettings()
        {
            NullValueHandling = NullValueHandling.Ignore,
            DateFormatHandling = DateFormatHandling.IsoDateFormat,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
        };

        public static string ToJson(this object objectForSerialization)
        {
            if (objectForSerialization == null)
            {
                return "{}";
            }
            return JsonConvert.SerializeObject(objectForSerialization, _jsonSerializationOptions);
        }

        public static T ToObject<T>(this string json) where T: class
        {
            if (string.IsNullOrWhiteSpace(json))
            {
                return null;
            }
            return JsonConvert.DeserializeObject<T>(json, _jsonSerializationOptions);
        }

        public static string ToFormattedExceptionDescription(this Exception exception)
        {
            var stackTrace = exception.StackTrace;
            var errorMessage = exception.Message;

            while (exception.InnerException != null)
            {
                exception = exception.InnerException;
                errorMessage += "; " + exception.Message;
            }
            return $"Exception message: {errorMessage}. {Environment.NewLine}" +
                   $"Stack-trace: {stackTrace}. {Environment.NewLine}";
        }
    }
}
