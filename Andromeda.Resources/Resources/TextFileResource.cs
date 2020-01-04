using System.IO;

namespace Andromeda.Resources
{
    public class TextFileResource : IResource
    {
        public ResourceType Type => ResourceType.Text;

        public string Source { get; }

        internal TextFileResource(Stream stream)
        {
            var reader = new StreamReader(stream);
            Source = reader.ReadToEnd();

            reader.Close();
            stream.Close();
        }
    }
}
