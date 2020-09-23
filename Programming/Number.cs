﻿using System.Globalization;

namespace Programming
{
    public class Number
    {
        public string GetDifference(double number, double number2, string format)
        {
            double difference = number - number2;

            return System.String.Format(CultureInfo.CurrentCulture, format, difference);
        }

        public string SumDecimal(string decimal1, string decimal2)
        {
            return decimal.Add(decimal.Parse(decimal1), decimal.Parse(decimal2)).ToString();
        }
    }
}
