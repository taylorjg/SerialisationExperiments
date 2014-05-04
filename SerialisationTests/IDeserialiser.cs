using System;

namespace SerialisationTests
{
    public interface IDeserialiser<out T>
    {
        T Deserialise();
    }
}
