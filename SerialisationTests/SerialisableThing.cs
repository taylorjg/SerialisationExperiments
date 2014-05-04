using System;
using System.Runtime.Serialization;

namespace SerialisationTests
{
    [Serializable]
    public class SerialisableThing : IDeserializationCallback
    {
        public int Property1 { get; set; }
        public string Property2 { get; set; }

        public bool DefaultConstructorWasInvoked {
            get { return _defaultConstructorWasInvoked; }
        }

        public bool OnDeserializationWasInvoked
        {
            get { return _onDeserializationWasInvoked; }
        }

        public SerialisableThing()
        {
            _defaultConstructorWasInvoked = true;
        }

        void IDeserializationCallback.OnDeserialization(object _)
        {
            _onDeserializationWasInvoked = true;
        }

        [NonSerialized]
        private readonly bool _defaultConstructorWasInvoked;

        [NonSerialized]
        private bool _onDeserializationWasInvoked;
    }
}
