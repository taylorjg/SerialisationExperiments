using NUnit.Framework;

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

        private static Thing SerialiseAndDeserialise(ISerialiser<Thing> serialiser, Thing thingBefore)
        {
            var deserialiser = serialiser.Serialise(thingBefore);
            var thingAfter = deserialiser.Deserialise();
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
            var serialiser = new NewtonsoftJsonSerialiser<Thing>();
            var thingBefore = MakeThing();
            var thingAfter = SerialiseAndDeserialise(serialiser, thingBefore);
            AssertBeforeAndAfterHaveSamePropertyValues(thingBefore, thingAfter);
            Assert.That(thingBefore.OnDeserializationWasInvoked, Is.False);
            Assert.That(thingAfter.OnDeserializationWasInvoked, Is.True);
        }

        [Test]
        public void BasicSerialiseThenDeserialiseUsingXmlSerializer()
        {
            var serialiser = new XmlSerialiser<Thing>();
            var thingBefore = MakeThing();
            var thingAfter = SerialiseAndDeserialise(serialiser, thingBefore);
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
