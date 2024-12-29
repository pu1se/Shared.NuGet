using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Shared.NuGet
{
    public static partial class EnumHelper
    {
        public static List<T> ToList<T>() where T : struct, IConvertible
        {
            return Enum.GetValues(typeof(T)).Cast<T>().ToList();
        }

        public static string GetDescription<TEnum>(TEnum value) where TEnum : struct, IConvertible
        {
            var fieldInfo = value.GetType().GetField(value.ToString());

            var attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes.Length > 0)
            {
                return attributes[0].Description;
            }
            return value.ToString();
        }

        private static T GetEnumItemByDescription<T>(string description) where T : struct, IConvertible
        {
            var enumItems = ToList<T>().ToDictionary(x => x, x => GetDescription(x));
            var enumItem = enumItems.FirstOrDefault(item => item.Value.ToUpper() == description.Trim().ToUpper());
            if (enumItem.Equals(default(KeyValuePair<T, string>)))
            {
                throw new ArgumentException($"Enum item with description '{description}' not found for enum {typeof(T).Name}.");
            }
            return (T)Enum.ToObject(typeof(T), enumItem.Key);
        }

        public static T Parse<T>(string value) where T : struct, IConvertible
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException("value", "Parsing enum value must not be null.");
            }
            var wasParsed = Enum.TryParse(value.Trim(), out T result);
            if (wasParsed)
            {
                return result;
            }

            return GetEnumItemByDescription<T>(value);
        }

        public static T? TryParse<T>(string value) where T : struct, IConvertible
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }
            try
            {
                return Parse<T>(value);
            }
            catch (Exception)
            {
                // ignored
            }

            return null;
        }

        public static Dictionary<string, string> ToDictionary<T>() where  T : struct, IConvertible
        {
            var result = new Dictionary<string, string>();
            foreach (var item in EnumHelper.ToList<T>().OrderBy(x=>x))
            {
                result.Add(item.ToString(), GetDescription(item));
            }

            return result;
        }
    }
}
