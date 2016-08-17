using System;
using System.Configuration;

namespace JSpank.Test.Helpers.Generics
{
    public class DateAndTime
    {
        public static DateTime Now
        {
            get
            {
                var dateNow = ToOrNow(ConfigurationManager.AppSettings["DateNow"] as string);
                return ToBrazilianDate(new DateTime(dateNow.Year, dateNow.Month, dateNow.Day, dateNow.Hour, dateNow.Minute, dateNow.Second));
            }
        }

        public static DateTime ToOrNow(string date)
        {
            var dateNow = DateTime.Now;

            if (!string.IsNullOrEmpty(date))
            {
                if (date.Length <= 10)
                    date += string.Format(" {0:00}:{1:00}:{2:00}", dateNow.Hour, dateNow.Minute, dateNow.Second);

                if (!DateTime.TryParseExact(date, "dd/MM/yyyy HH:mm:ss", null, System.Globalization.DateTimeStyles.None, out dateNow))
                    dateNow = DateTime.Now;
            }

            return dateNow;
        }

        public static int Age(DateTime birth)
        {
            double days;
            return Age(birth, Now, out days);
        }

        public static int Age(DateTime birth, DateTime to)
        {
            double days;
            return Age(birth, to, out days);
        }

        public static int Age(DateTime birth, DateTime to, out double days)
        {
            var nu_idade = 0;
            var dt_while = birth;

            while (dt_while <= to)
            {
                if (dt_while.AddYears(1) <= to)
                {
                    nu_idade++;
                    dt_while = dt_while.AddYears(1);
                }
                else
                    break;
            }

            days = (to - dt_while).TotalDays;
            return nu_idade;
        }

        static DateTime ToBrazilianDate(DateTime date)
        {
            var brazilianTimeZone = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
            var otherTimeZone = TimeZoneInfo.FindSystemTimeZoneById(TimeZoneInfo.Local.Id);

            return TimeZoneInfo.Local.Equals(brazilianTimeZone) ? date :
                TimeZoneInfo.ConvertTime(date, otherTimeZone, brazilianTimeZone);
        }
    }
}
