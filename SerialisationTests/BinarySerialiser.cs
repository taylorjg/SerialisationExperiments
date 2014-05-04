using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace SerialisationTests
{
    public class BinarySerialiser<T> : ISerialiser<T>
    {
        public IDeserialiser<T> Serialise(T obj)
        {
            using (var stream = new MemoryStream())
            {
                var binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(stream, obj);
                return new BinaryDeserialiser<T>(stream.ToArray());
            }
        }
    }
}
