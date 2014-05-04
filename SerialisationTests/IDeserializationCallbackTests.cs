using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using NUnit.Framework;
using Newtonsoft.Json;

namespace SerialisationTests
{
    // ReSharper disable InconsistentNaming

    public interface ISerialiser<T> : IDisposable
    {
        void Serialise(T obj);
        T Deserialise();
    }

    public class BinarySerialiser<T> : ISerialiser<T>
    {
        public void Serialise(T obj)
        {
            var binaryFormatter = new BinaryFormatter();
            _state = new MemoryStream();
            binaryFormatter.Serialize(_state, obj);
        }

        public T Deserialise()
        {
            var binaryFormatter = new BinaryFormatter();
            _state.Seek(0, SeekOrigin.Begin);
            var obj = (T) binaryFormatter.Deserialize(_state);
            return obj;
        }

        public void Dispose()
        {
            if (_state != null)
            {
                _state.Close();
            }
        }

        private MemoryStream _state = null;
    }

    [Serializable]
    public class Thing : IDeserializationCallback
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

    [TestFixture]
    internal class IDeserializationCallbackTests
    {
        private static Thing MakeThing()
        {
            return new Thing
                {
                    Property1 = 42,
                    Property2 = "Jon"
                };
        }

        private static Thing SerialiseAndDeserialise(ISerialiser<Thing> serialiser, Thing thingBefore)
        {
            serialiser.Serialise(thingBefore);
            var thingAfter = serialiser.Deserialise();
            return thingAfter;
        }

        [Test]
        public void BasicSerialiseThenDeserialiseUsingBinaryFormatter()
        {
            var serialiser = new BinarySerialiser<Thing>();

            var thingBefore = MakeThing();
            var thingAfter = SerialiseAndDeserialise(serialiser, thingBefore);

            AssertBeforeAndAfterHaveSamePropertyValues(thingBefore, thingAfter);
            Assert.That(thingBefore.OnDeserializationWasInvoked, Is.False);
            Assert.That(thingAfter.OnDeserializationWasInvoked, Is.True);
        }

        [Test]
        public void BasicSerialiseThenDeserialiseUsingNewtonsoftJson()
        {
            var thingBefore = MakeThing();

            var json = JsonConvert.SerializeObject(thingBefore);
            var thingAfter = JsonConvert.DeserializeObject<Thing>(json);

            AssertBeforeAndAfterHaveSamePropertyValues(thingBefore, thingAfter);
            Assert.That(thingBefore.OnDeserializationWasInvoked, Is.False);
            Assert.That(thingAfter.OnDeserializationWasInvoked, Is.True);
        }

        [Test]
        public void BasicSerialiseThenDeserialiseUsingXmlSerializer()
        {
            var thingBefore = MakeThing();

            var xmlSerializer = new XmlSerializer(typeof(Thing));
            var memoryStream = new MemoryStream();
            xmlSerializer.Serialize(memoryStream, thingBefore);
            memoryStream.Seek(0, SeekOrigin.Begin);
            var thingAfter = (Thing)xmlSerializer.Deserialize(memoryStream);
            memoryStream.Close();

            AssertBeforeAndAfterHaveSamePropertyValues(thingBefore, thingAfter);
            Assert.That(thingBefore.OnDeserializationWasInvoked, Is.False);
            Assert.That(thingAfter.OnDeserializationWasInvoked, Is.True);
        }

        private static void AssertBeforeAndAfterHaveSamePropertyValues(Thing thingBefore, Thing thingAfter)
        {
            Assert.That(thingAfter.Property1, Is.EqualTo(thingBefore.Property1));
            Assert.That(thingAfter.Property2, Is.EqualTo(thingBefore.Property2));
        }
    }
}
