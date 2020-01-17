using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programming
{
    public class String
    {
        public string AddDelimiterBetweenStringCharacters (string text, string delimiter)
        {
            if (string.IsNullOrWhiteSpace(text))
                throw new ArgumentNullException("text");

            if (string.IsNullOrEmpty(delimiter))
                throw new ArgumentNullException("delimiter");

            return string.Join(delimiter, text.Select(c => c.ToString()).ToArray());
        }
    }
}
