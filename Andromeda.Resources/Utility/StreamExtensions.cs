using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Andromeda.Resources.Utility
{
    /// <summary>
    /// Utilties for the stream
    /// </summary>
    public static class StreamExtensions
    {
        /// <summary>
        /// Gets the buffer writer utility for this Stream
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static AStreamWriter GetWriter(this Stream stream)
        {
            return new AStreamWriter(stream);
        }

        /// <summary>
        /// Gets the buffer reader utility for this Stream
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static AStreamReader GetReader(this Stream stream)
        {
            return new AStreamReader(stream);
        }
    }
}
