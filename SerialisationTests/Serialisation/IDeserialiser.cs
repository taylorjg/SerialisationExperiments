namespace SerialisationTests.Serialisation
{
    public interface IDeserialiser<out T>
    {
        T Deserialise();
    }
}
