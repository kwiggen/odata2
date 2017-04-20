using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace odata2.Lib
{
    public static class StringUtilities
    {
        public static int InvariantInsensitive(string one, string two)
        {
            return StringComparer.InvariantCultureIgnoreCase.Compare(one, two);
        }
    }
}