using Newtonsoft.Json;

namespace SerialisationTests
{
    public class NewtonsoftJsonSerialiser<T> : ISerialiser<T>
    {
        public IDeserialiser<T> Serialise(T obj)
        {
            var json = JsonConvert.SerializeObject(obj);
            return new NewtonsoftJsonDeserialiser<T>(json);
        }

        public void Dispose()
        {
        }
    }
}
