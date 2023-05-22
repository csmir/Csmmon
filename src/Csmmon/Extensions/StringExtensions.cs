using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Csmmon
{
    public static class StringExtensions
    {
        public static string Reduce(this string input, int maxLength, bool killAtWhitespace = false, string finalizer = "...")
        {
            if (input is null)
                return string.Empty + finalizer;

            if (input.Length > maxLength)
            {
                maxLength -= (finalizer.Length + 1); // reduce the length of the finalizer + a single integer to convert to valid range.

                if (maxLength < 1)
                    throw new ArgumentOutOfRangeException(nameof(maxLength));

                if (killAtWhitespace)
                {
                    var range = input.Split(' ');
                    for (int i = 2; input.Length + finalizer.Length > maxLength; i++) // set i as 2, 1 for index reduction, 1 for initial word removal, then increment.
                        input = string.Join(' ', range[..(range.Length - i)]);

                    input += finalizer;
                }
                return input[..maxLength] + finalizer;
            }
            else return input;
        }

        public static bool IsAlphaNumeric(this string str)
            => Regex.IsMatch(str.Replace(" ", ""), "^[a-zA-Z0-9]+$");
    }
}
