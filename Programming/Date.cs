using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programming
{
    public class Date
    {
        public string AddDay(string date, string format, int number)
        {
            DateTime newDate = DateTime.Parse(date);

            return newDate.AddDays(number).ToString(format);
        }

        public string AddMonth(string date, string format, int number)
        {
            DateTime newDate = DateTime.Parse(date);

            return newDate.AddMonths(number).ToString(format);
        }

        public string AddYear(string date, string format, int number)
        {
            DateTime newDate = DateTime.Parse(date);

            return newDate.AddYears(number).ToString(format);
        }

        public int GetDay(string date, string format)
        {
            DateTime newDate = DateTime.ParseExact(date, format, CultureInfo.InvariantCulture);
            return newDate.Day;
        }

        public int GetMonth(string date, string format)
        {
            DateTime newDate = DateTime.ParseExact(date, format, CultureInfo.InvariantCulture);
            return newDate.Month;
        }

        public int GetYear(string date, string format)
        {
            DateTime newDate = DateTime.ParseExact(date, format, CultureInfo.InvariantCulture);
            return newDate.Year;
        }

        public string GetDateNow(string format)
        {
            return DateTime.Now.ToString(format);
        }

        public int GetLastDayOfMonth(string date, string format)
        {
            DateTime newDate = DateTime.ParseExact(date, format, CultureInfo.InvariantCulture);
            return DateTime.DaysInMonth(newDate.Year, newDate.Month);
        }

        public string GetFirstDayOfMonthAsDate(string date, string format)
        {
            DateTime newDate = DateTime.ParseExact(date, format, CultureInfo.InvariantCulture);
            newDate = new DateTime(newDate.Year, newDate.Month, 1);
            return newDate.ToString();
        }

        public string GetLastDayOfMonthAsDate(string date, string format)
        {
            DateTime newDate = DateTime.ParseExact(date, format, CultureInfo.InvariantCulture);
            int lastDay = DateTime.DaysInMonth(newDate.Year, newDate.Month);
            newDate = new DateTime(newDate.Year, newDate.Month, lastDay);
            return newDate.ToString();
        }

        public int GetDaysBetweenDates(string start, string end, string format)
        {
            DateTime startDate = DateTime.ParseExact(start, format, CultureInfo.InvariantCulture);
            DateTime endDate = DateTime.ParseExact(end, format, CultureInfo.InvariantCulture);

            TimeSpan difference = endDate - startDate;

            return difference.Days;
        }

        public int CalculateAge(string birthDate, string format)
        {
            DateTime today = DateTime.Today;
            DateTime birth = DateTime.ParseExact(birthDate, format, CultureInfo.InvariantCulture);

            int age = today.Year - birth.Year;

            if (birth.Date > today.AddYears(-age)) age--;

            return age;
        }

        public double DiffTotalHours(string start, string end, string format)
        {
            DateTime startDate = DateTime.ParseExact(start, format, CultureInfo.InvariantCulture);
            DateTime endDate = DateTime.ParseExact(end, format, CultureInfo.InvariantCulture);

            TimeSpan difference = endDate - startDate;

            return difference.TotalHours;
        }

        public string formatDate(string date, string format)
        {
            DateTime startDate = DateTime.Parse(date);

            return startDate.ToString(format);
        }
    }
}
