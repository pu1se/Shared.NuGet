using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shared.NuGet
{
    public static class StringExtensions
    {
        public static TEnum AsEnum<TEnum>(this string value) where TEnum : struct, IConvertible
        {
            return EnumHelper.Parse<TEnum>(value);
        }

        public static string Join(this IEnumerable<string> list, string separator)
        {
            return string.Join(separator, list);
        }

        public static bool IsNullOrEmpty(this string st)
        {
            return String.IsNullOrEmpty(st);
        }
    }
}
