using Newtonsoft.Json;

namespace SerialisationTests
{
    public class NewtonsoftJsonDeserialiser<T> : IDeserialiser<T>
    {
        public NewtonsoftJsonDeserialiser(string json)
        {
            _json = json;
        }

        public T Deserialise()
        {
            return JsonConvert.DeserializeObject<T>(_json);
        }

        public void Dispose()
        {
        }

        private readonly string _json;
    }
}
