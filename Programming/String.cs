﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Programming
{
    public class String
    {
        public string AddDelimiterBetweenStringCharacters(string text, string delimiter)
        {
            if (string.IsNullOrWhiteSpace(text))
                throw new ArgumentNullException("text");

            if (string.IsNullOrEmpty(delimiter))
                throw new ArgumentNullException("delimiter");

            return string.Join(delimiter, text.Select(c => c.ToString()).ToArray());
        }

        public string CountTextOcurrences(string text, string textToCount)
        {
            int count = Regex.Matches(text, textToCount).Count;

            return count.ToString();
        }

        public bool IsStringUpperCase(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                throw new ArgumentNullException("text");

            // Verifica se a string é númerica. Se sim, retorna false.
            if (text.All(char.IsDigit))
            {
                return false;
            }
            else
            {
                return text == text.ToUpper();
            }
        }

        public bool StringStartsWith(string text, string valor)
        {
            return text.StartsWith(valor);
        }

        public string normalizeString(string text)
        {
            StringBuilder sbReturn = new StringBuilder();
            var arrayText = text.Normalize(NormalizationForm.FormD).ToCharArray();
            foreach (char letter in arrayText)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(letter) != UnicodeCategory.NonSpacingMark)
                    sbReturn.Append(letter);
            }
            return sbReturn.Replace("\n", "").Replace("\r", "").ToString().ToUpper();
        }

        public bool checkDuplicatesOnList(string text, string delimiter, string ignore)
        {
            char d = char.Parse(delimiter);

            List<string> list = text.Split(d).ToList();

            bool dupes = list
                .Where(item => item != null && !string.IsNullOrWhiteSpace(item) && !item.Equals(ignore))
                .GroupBy(item => item)
                .Any(g => g.Count() > 1);

            return dupes;
        }
    }
}
