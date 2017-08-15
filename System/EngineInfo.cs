using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VorliasEngine2D.System
{
    public static class EngineInfo
    {
        const int DATE = 170816;
        const int MAJOR = 0;
        const int MINOR = 270;
        const int REVISION = 0;

        const int FULL = (MAJOR * 10000) + (MINOR * 100) + REVISION;

        public static string String
        {
            get
            {
                return string.Format("{0}.{1}.{2}", MAJOR, MINOR, REVISION);
            }
        }

        public static string FullString
        {
            get
            {
                return string.Format("{0}.{1}.{2}.{3}", MAJOR, MINOR, REVISION, DATE);
            }
        }
    }
}
