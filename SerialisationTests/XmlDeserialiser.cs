using System.IO;
using System.Xml.Serialization;

namespace SerialisationTests
{
    public class XmlDeserialiser<T> : IDeserialiser<T>
    {
        public XmlDeserialiser(byte[] bytes)
        {
            _bytes = bytes;
        }

        public T Deserialise()
        {
            using (var stream = new MemoryStream(_bytes))
            {
                var xmlSerializer = new XmlSerializer(typeof(T));
                return (T)xmlSerializer.Deserialize(stream);
            }
        }

        private readonly byte[] _bytes;
    }
}
