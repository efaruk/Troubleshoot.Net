using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Troubleshoot.Common
{
    public static class Helper
    {
        public static string EscapeSingleQuato(this string text)
        {
            text = text.Replace("'", "''");
            return text;
        }
    }
}
