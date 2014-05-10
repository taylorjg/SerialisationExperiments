using System;
using System.Runtime.Serialization;

namespace SerialisationTests.Things
{
    [Serializable]
    public class ThingWithCustomSerialisation : ISerializable
    {
        public int Property1 { get; set; }
        public string Property2 { get; set; }

        public bool ProtectedConstructorWasInvoked
        {
            get { return _protectedConstructorWasInvoked; }
        }

        public bool GetObjectDataWasInvoked
        {
            get { return _getObjectDataWasInvoked; }
        }

        // ReSharper disable EmptyConstructor
        public ThingWithCustomSerialisation()
        {
        }
        // ReSharper restore EmptyConstructor

        protected ThingWithCustomSerialisation(SerializationInfo info, StreamingContext context)
        {
            _protectedConstructorWasInvoked = true;
            Property1 = (int)info.GetValue("Property1", typeof(int));
            Property2 = (string)info.GetValue("Property2", typeof(string));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            _getObjectDataWasInvoked = true;
            info.AddValue("Property1", Property1, typeof(int));
            info.AddValue("Property2", Property2, typeof(string));
        }

        [NonSerialized]
        private bool _getObjectDataWasInvoked;

        [NonSerialized]
        private readonly bool _protectedConstructorWasInvoked;
    }
}
