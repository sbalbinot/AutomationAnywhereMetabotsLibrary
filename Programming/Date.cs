using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programming
{
    class Date
    {
        public string AddDay(string date, string format, int number)
        {
            DateTime newDate = DateTime.Parse(date, CultureInfo.InvariantCulture);

            newDate.AddDays(number);

            return newDate.ToString(format);
        }

        public string AddMonth(string date, string format, int number)
        {
            DateTime newDate = DateTime.Parse(date, CultureInfo.InvariantCulture);

            newDate.AddDays(number);

            return newDate.ToString(format);
        }

        public string AddYear(string date, string format, int number)
        {
            DateTime newDate = DateTime.Parse(date, CultureInfo.InvariantCulture);

            newDate.AddDays(number);

            return newDate.ToString(format);
        }

        public int GetDay(string date)
        {
            DateTime newDate = DateTime.Parse(date, CultureInfo.InvariantCulture);
            return newDate.Day;
        }

        public int GetMonth(string date)
        {
            DateTime newDate = DateTime.Parse(date, CultureInfo.InvariantCulture);
            return newDate.Month;
        }

        public int GetYear(string date)
        {
            DateTime newDate = DateTime.Parse(date, CultureInfo.InvariantCulture);
            return newDate.Year;
        }

        public string GetDateNow(string format)
        {
            return DateTime.Now.ToString(format);
        }

        public int GetLastDayOfMonth(string date)
        {
            DateTime newDate = DateTime.Parse(date, CultureInfo.InvariantCulture);
            return DateTime.DaysInMonth(newDate.Year, newDate.Month);
        }

        public string GetFirstDayOfMonthAsDate(string date, string format)
        {
            DateTime newDate = DateTime.Parse(date, CultureInfo.InvariantCulture);
            newDate = new DateTime(newDate.Year, newDate.Month, 1);
            return newDate.ToString(format);
        }

        public string GetLastDayOfMonthAsDate(string date, string format)
        {
            DateTime newDate = DateTime.Parse(date, CultureInfo.InvariantCulture);
            int lastDay = DateTime.DaysInMonth(newDate.Year, newDate.Month);
            newDate = new DateTime(newDate.Year, newDate.Month, lastDay);
            return newDate.ToString(format);
        }

        public int GetDaysBetweenDates(string start, string end)
        {
            DateTime startDate = DateTime.Parse(start, CultureInfo.InvariantCulture);
            DateTime endDate = DateTime.Parse(end, CultureInfo.InvariantCulture);

            TimeSpan difference = endDate - startDate;

            return difference.Days;
        }
    }
}
