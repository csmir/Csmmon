using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Csmmon
{
    public static class Assert
    {
        public static void IsTrue(bool condition, string failureMessage)
        {
            if (!condition)
                throw new InvalidOperationException(failureMessage);
        }

        public static void IsFalse(bool condition, string failureMessage)
        {
            if (condition)
                throw new InvalidOperationException(failureMessage);
        }

        public static void IsNotNull(object value, [CallerArgumentExpression("value")] string? caller = null)
        {
            if (value == null)
                throw new ArgumentNullException(paramName: caller, "Argument is null.");
        }

        public static void IsNotEmpty(string value, [CallerArgumentExpression("value")] string? caller = null)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException(paramName: caller, "Argument is null or empty.");
        }

        public static void IsNotWhitespace(string value, [CallerArgumentExpression("value")] string? caller = null)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException(paramName: caller, "Argument is null or whitespace.");
        }
    }
}
