using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SerialisationTests
{
    [Serializable]
    public class SerialisableThing : IDeserializationCallback
    {
        public int Property1 { get; set; }
        public string Property2 { get; set; }

        [XmlIgnore]
        public bool OnDeserializationWasInvoked { get; private set; }

        public void OnDeserialization(object _)
        {
            OnDeserializationWasInvoked = true;
        }
    }
}
