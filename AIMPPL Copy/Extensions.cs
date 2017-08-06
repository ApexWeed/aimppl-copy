using System;

namespace AIMPPL_Copy
{
    public static class Extensions
    {
        public static bool IsNullOrDBNull(this object obj)
            => obj == null || obj is DBNull;
    }
}
