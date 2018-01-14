using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Andromeda.System
{
    public static class EngineInfo
    {
        const int DATE = 180104;
        const int MAJOR = 1;
        const int MINOR = 0;
        const int REVISION = 0;
        const string SUFFIX = "-beta";

        const int FULL = (MAJOR * 10000) + (MINOR * 100) + REVISION;

        public static string String
        {
            get
            {
                return string.Format("{0}.{1}.{2}{3}", MAJOR, MINOR, REVISION, SUFFIX);
            }
        }

        public static string FullString
        {
            get
            {
                return string.Format("{0}.{1}.{2}.{3}{4}", MAJOR, MINOR, REVISION, DATE, SUFFIX);
            }
        }
    }
}
