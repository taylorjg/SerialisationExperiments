using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace SerialisationTests.Serialisation.Implementations
{
    public class BinaryDeserialiser<T> : IDeserialiser<T>
    {
        public BinaryDeserialiser(Byte[] bytes)
        {
            _bytes = bytes;
        }

        public T Deserialise()
        {
            using (var stream = new MemoryStream(_bytes))
            {
                var binaryFormatter = new BinaryFormatter();
                return (T)binaryFormatter.Deserialize(stream);
            }
        }

        private readonly byte[] _bytes;
    }
}
