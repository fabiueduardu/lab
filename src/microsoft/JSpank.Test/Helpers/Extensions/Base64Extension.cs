
namespace JSpank.Test.Helpers.Extensions
{
    public static class Base64Extension
    {
        public static string Base64Encode(string value)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(value);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(string value)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(value);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}
