using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace JSpank.Test.Helpers.Validate
{
    public class CPFValidate
    {
        public static bool IsValid(string number)
        {
            if (string.IsNullOrEmpty(number))
                return false;

            var reg = new Regex(@"[^0-9]");
            number = reg.Replace(number, string.Empty);

            if (number.Length > 11)
                return false;

            while (number.Length != 11)
                number = '0' + number;

            bool equals = true;
            for (int i = 1; i < 11 && equals; i++)
                if (number[i] != number[0])
                    equals = false;

            if (equals || number == "12345678909")
                return false;

            int[] numbers = new int[11];

            for (int i = 0; i < 11; i++)
                numbers[i] = int.Parse(number[i].ToString());

            int sum = 0;
            for (int i = 0; i < 9; i++)
                sum += (10 - i) * numbers[i];

            int result = sum % 11;

            if (result == 1 || result == 0)
            {
                if (numbers[9] != 0)
                    return false;
            }
            else if (numbers[9] != 11 - result)
                return false;

            sum = 0;
            for (int i = 0; i < 10; i++)
                sum += (11 - i) * numbers[i];

            result = sum % 11;

            if (result == 1 || result == 0)
            {
                if (numbers[10] != 0)
                    return false;
            }
            else if (numbers[10] != 11 - result)
                return false;
            return true;
        }
    }
}
