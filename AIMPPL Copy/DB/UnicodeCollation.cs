using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMPPL_Copy.DB
{
    [SQLiteFunction(FuncType = FunctionType.Collation, Name = "UNICODE")]
    class UnicodeCollation : SQLiteFunction
    {
        private static CultureInfo _cultureInfo = CultureInfo.CurrentCulture;

        public override int Compare(string param1, string param2)
        {
            return string.CompareOrdinal(param1, param2);
        }
    }
}
