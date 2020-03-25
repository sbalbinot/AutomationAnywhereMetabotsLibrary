using System.Globalization;

namespace Programming
{
    public class Number
    {
        public string GetDifference(double number, double number2, string format)
        {
            double difference = number - number2;

            return System.String.Format(CultureInfo.CurrentCulture, format, difference);
        }
    }
}
