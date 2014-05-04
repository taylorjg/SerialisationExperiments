using System.IO;
using System.Xml.Serialization;

namespace SerialisationTests
{
    public class XmlDeserialiser<T> : IDeserialiser<T>
    {
        public XmlDeserialiser(byte[] bytes)
        {
            _stream = new MemoryStream(bytes);
        }

        public T Deserialise()
        {
            var xmlSerializer = new XmlSerializer(typeof(T));
            _stream.Seek(0, SeekOrigin.Begin);
            return (T)xmlSerializer.Deserialize(_stream);
        }

        public void Dispose()
        {
            _stream.Close();
        }

        private readonly Stream _stream;
    }
}
