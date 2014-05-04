using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace SerialisationTests
{
    public class BinaryDeserialiser<T> : IDeserialiser<T>
    {
        public BinaryDeserialiser(Byte[] bytes)
        {
            _stream = new MemoryStream(bytes);
        }

        public T Deserialise()
        {
            var binaryFormatter = new BinaryFormatter();
            _stream.Seek(0, SeekOrigin.Begin);
            return (T)binaryFormatter.Deserialize(_stream);
        }

        public void Dispose()
        {
            if (_stream != null)
            {
                _stream.Close();
            }
        }

        private readonly MemoryStream _stream;
    }
}
