using System;
using System.Runtime.Serialization;

namespace SerialisationTests.Things
{
    [Serializable]
    public class ThingWithDeserializationCallback : IDeserializationCallback
    {
        public int Property1 { get; set; }
        public string Property2 { get; set; }

        public bool OnDeserializationWasInvoked
        {
            get { return _onDeserializationWasInvoked; }
        }

        void IDeserializationCallback.OnDeserialization(object _)
        {
            _onDeserializationWasInvoked = true;
        }

        [NonSerialized]
        private bool _onDeserializationWasInvoked;
    }
}
