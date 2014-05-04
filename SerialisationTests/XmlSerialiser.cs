using System.IO;
using System.Xml.Serialization;

namespace SerialisationTests
{
    public class XmlSerialiser<T> : ISerialiser<T>
    {
        public IDeserialiser<T> Serialise(T obj)
        {
            using (var stream = new MemoryStream())
            {
                var xmlSerializer = new XmlSerializer(typeof(T));
                xmlSerializer.Serialize(stream, obj);
                return new XmlDeserialiser<T>(stream.ToArray());
            }
        }

        public void Dispose()
        {
        }
    }
}
