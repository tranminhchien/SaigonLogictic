using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Provider
{
    public static class StringProvider
    {
        public enum StatusEntity
        {
            ACTIVE,
            INACTIVE,
            DELETE
        }

        public static string NOTFOUND = "NotFound";
    }
}
