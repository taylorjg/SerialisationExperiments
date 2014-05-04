using NUnit.Framework;

namespace SerialisationTests
{
    // ReSharper disable InconsistentNaming

    [TestFixture]
    internal class IDeserializationCallbackTests
    {
        [TestCase(SerialiserType.BinaryFormatter)]
        public void OnDeserializationWillBeInvoked(SerialiserType serialiserType)
        {
            var before = SerialisationUtils.MakeThing();
            var after = SerialisationUtils.SerialiseAndDeserialise(serialiserType, before);
            SerialisationUtils.AssertBeforeAndAfterHaveSamePropertyValues(before, after);
            Assert.That(before.OnDeserializationWasInvoked, Is.False);
            Assert.That(after.OnDeserializationWasInvoked, Is.True);
        }

        [TestCase(SerialiserType.NewtonsoftJson)]
        [TestCase(SerialiserType.XmlSerialiser)]
        public void OnDeserializationWillNotBeInvoked(SerialiserType serialiserType)
        {
            var before = SerialisationUtils.MakeThing();
            var after = SerialisationUtils.SerialiseAndDeserialise(serialiserType, before);
            SerialisationUtils.AssertBeforeAndAfterHaveSamePropertyValues(before, after);
            Assert.That(before.OnDeserializationWasInvoked, Is.False);
            Assert.That(after.OnDeserializationWasInvoked, Is.False);
        }
    }
}
