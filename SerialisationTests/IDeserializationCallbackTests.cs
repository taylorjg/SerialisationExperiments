using System.IO;
using System.Xml.Serialization;
using NUnit.Framework;
using Newtonsoft.Json;

namespace SerialisationTests
{
    // ReSharper disable InconsistentNaming

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

        private static Thing SerialiseAndDeserialise(Thing thingBefore)
        {
            var serialiser = new BinarySerialiser<Thing>();
            var deserialiser = serialiser.Serialise(thingBefore);
            var thingAfter = deserialiser.Deserialise();
            return thingAfter;
        }

        [Test]
        public void BasicSerialiseThenDeserialiseUsingBinaryFormatter()
        {
            var thingBefore = MakeThing();
            var thingAfter = SerialiseAndDeserialise(thingBefore);
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
