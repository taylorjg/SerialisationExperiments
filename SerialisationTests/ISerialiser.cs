using System;

namespace SerialisationTests
{
    public interface ISerialiser<T> : IDisposable
    {
        IDeserialiser<T> Serialise(T obj);
    }
}
