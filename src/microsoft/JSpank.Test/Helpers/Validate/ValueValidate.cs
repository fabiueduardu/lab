using System.ComponentModel;
using System.Text.RegularExpressions;

namespace JSpank.Test.Helpers.Validate
{
    public static class ValueValidate
    {
        public static string GetNumbers(string value)
        {
            return string.IsNullOrEmpty(value) ? value : Regex.Replace(value, "[^0-9a-zA-Z]+", string.Empty);
        }

        public static T GetNumbers<T>(string value)
        {
            value = GetNumbers(value);

            var converter = TypeDescriptor.GetConverter(typeof(T));
            if (converter.CanConvertFrom(value.GetType()))
                return (T)converter.ConvertFrom(value);

            return default(T);
        }
    }
}
