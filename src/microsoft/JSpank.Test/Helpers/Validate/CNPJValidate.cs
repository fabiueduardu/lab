
namespace JSpank.Test.Helpers.Validate
{
    public  class CNPJValidate
    {
        public static bool IsValid(string number)
        {
            if (string.IsNullOrEmpty(number))
                return false;

            int[] multiplier1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplier = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int sum;
            int remainder;
            string digit;
            string tempnumber;
            number = number.Trim();
            number = number.Replace(".", "").Replace("-", "").Replace("/", "");
            if (number.Length != 14)
                return false;
            tempnumber = number.Substring(0, 12);
            sum = 0;
            for (int i = 0; i < 12; i++)
                sum += int.Parse(tempnumber[i].ToString()) * multiplier1[i];
            remainder = (sum % 11);
            if (remainder < 2)
                remainder = 0;
            else
                remainder = 11 - remainder;
            digit = remainder.ToString();
            tempnumber = tempnumber + digit;
            sum = 0;
            for (int i = 0; i < 13; i++)
                sum += int.Parse(tempnumber[i].ToString()) * multiplier[i];
            remainder = (sum % 11);
            if (remainder < 2)
                remainder = 0;
            else
                remainder = 11 - remainder;
            digit = digit + remainder.ToString();
            return number.EndsWith(digit);
        }
    }
}
