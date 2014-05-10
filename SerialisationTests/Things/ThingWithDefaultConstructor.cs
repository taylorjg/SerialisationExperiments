using System;

namespace SerialisationTests.Things
{
    [Serializable]
    public class ThingWithDefaultConstructor
    {
        public int Property1 { get; set; }
        public string Property2 { get; set; }

        public bool DefaultConstructorWasInvoked
        {
            get { return _defaultConstructorWasInvoked; }
        }

        public ThingWithDefaultConstructor()
        {
            _defaultConstructorWasInvoked = true;
        }

        [NonSerialized]
        private readonly bool _defaultConstructorWasInvoked;
    }
}
