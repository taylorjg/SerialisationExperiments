using System;

namespace SerialisationTests
{
    public interface ISerialiser<T>
    {
        IDeserialiser<T> Serialise(T obj);
    }
}
