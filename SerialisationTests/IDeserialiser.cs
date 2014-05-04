using System;

namespace SerialisationTests
{
    public interface IDeserialiser<out T> : IDisposable
    {
        T Deserialise();
    }
}
