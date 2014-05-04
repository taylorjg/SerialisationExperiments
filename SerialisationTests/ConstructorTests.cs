using NUnit.Framework;

namespace SerialisationTests
{
    [TestFixture]
    internal class ConstructorTests
    {
        [TestCase(SerialiserType.NewtonsoftJson)]
        [TestCase(SerialiserType.XmlSerialiser)]
        public void DefaultConstructorWillBeInvoked(SerialiserType serialiserType)
        {
            var before = SerialisationUtils.MakeThing();
            var after = SerialisationUtils.SerialiseAndDeserialise(serialiserType, before);
            SerialisationUtils.AssertBeforeAndAfterHaveSamePropertyValues(before, after);
            Assert.That(before.DefaultConstructorWasInvoked, Is.True);
            Assert.That(after.DefaultConstructorWasInvoked, Is.True);
        }

        [TestCase(SerialiserType.BinaryFormatter)]
        public void DefaultConstructorWillNotBeInvoked(SerialiserType serialiserType)
        {
            var before = SerialisationUtils.MakeThing();
            var after = SerialisationUtils.SerialiseAndDeserialise(serialiserType, before);
            SerialisationUtils.AssertBeforeAndAfterHaveSamePropertyValues(before, after);
            Assert.That(before.DefaultConstructorWasInvoked, Is.True);
            Assert.That(after.DefaultConstructorWasInvoked, Is.False);
        }
    }
}
