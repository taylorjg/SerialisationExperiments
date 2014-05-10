using NUnit.Framework;
using SerialisationTests.Serialisation;

namespace SerialisationTests
{
    [TestFixture]
    internal class ConstructorTests
    {
        [TestCase(SerialiserType.NewtonsoftJson)]
        [TestCase(SerialiserType.XmlSerialiser)]
        public void DefaultConstructorWillBeInvoked(SerialiserType serialiserType)
        {
            var before = SerialisationUtils.MakeThingWithDefaultConstructor();
            var after = SerialisationUtils.SerialiseAndDeserialise(serialiserType, before);
            SerialisationUtils.AssertBeforeAndAfterHaveSamePropertyValues(before, after);
            Assert.That(before.DefaultConstructorWasInvoked, Is.True);
            Assert.That(after.DefaultConstructorWasInvoked, Is.True);
        }

        [TestCase(SerialiserType.BinaryFormatter)]
        public void DefaultConstructorWillNotBeInvoked(SerialiserType serialiserType)
        {
            var before = SerialisationUtils.MakeThingWithDefaultConstructor();
            var after = SerialisationUtils.SerialiseAndDeserialise(serialiserType, before);
            SerialisationUtils.AssertBeforeAndAfterHaveSamePropertyValues(before, after);
            Assert.That(before.DefaultConstructorWasInvoked, Is.True);
            Assert.That(after.DefaultConstructorWasInvoked, Is.False);
        }

        [TestCase(SerialiserType.NewtonsoftJson)]
        [TestCase(SerialiserType.BinaryFormatter)]
        public void CanSerialiseAndDeserialiseWithoutADefaultConstructor(SerialiserType serialiserType)
        {
            var before = SerialisationUtils.MakeThingWithoutDefaultConstructor();
            var after = SerialisationUtils.SerialiseAndDeserialise(serialiserType, before);
            SerialisationUtils.AssertBeforeAndAfterHaveSamePropertyValues(before, after);
        }

        [TestCase(SerialiserType.XmlSerialiser)]
        public void CannotSerialiseWithoutADefaultConstructor(SerialiserType serialiserType)
        {
            var before = SerialisationUtils.MakeThingWithoutDefaultConstructor();
            Assert.Throws<System.InvalidOperationException>(() => SerialisationUtils.SerialiseAndDeserialise(serialiserType, before));
        }
    }
}
