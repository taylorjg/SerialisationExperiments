namespace SerialisationTests.Serialisation
{
    public interface ISerialiser<T>
    {
        IDeserialiser<T> Serialise(T obj);
    }
}
