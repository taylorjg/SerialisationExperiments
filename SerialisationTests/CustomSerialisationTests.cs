using NUnit.Framework;
using SerialisationTests.Serialisation;

namespace SerialisationTests
{
    [TestFixture]
    internal class CustomSerialisationTests
    {
        [TestCase(SerialiserType.BinaryFormatter)]
        [TestCase(SerialiserType.NewtonsoftJson)]
        public void CustomSerialisationIsHonoured(SerialiserType serialiserType)
        {
            var before = SerialisationUtils.MakeThingWithCustomSerialisation();
            var after = SerialisationUtils.SerialiseAndDeserialise(serialiserType, before);
            SerialisationUtils.AssertBeforeAndAfterHaveSamePropertyValues(before, after);
            Assert.That(before.GetObjectDataWasInvoked, Is.True);
            Assert.That(before.ProtectedConstructorWasInvoked, Is.False);
            Assert.That(after.GetObjectDataWasInvoked, Is.False);
            Assert.That(after.ProtectedConstructorWasInvoked, Is.True);
        }

        [TestCase(SerialiserType.XmlSerialiser)]
        public void CustomSerialisationIsIgnored(SerialiserType serialiserType)
        {
            var before = SerialisationUtils.MakeThingWithCustomSerialisation();
            var after = SerialisationUtils.SerialiseAndDeserialise(serialiserType, before);
            SerialisationUtils.AssertBeforeAndAfterHaveSamePropertyValues(before, after);
            Assert.That(before.GetObjectDataWasInvoked, Is.False);
            Assert.That(before.ProtectedConstructorWasInvoked, Is.False);
            Assert.That(after.GetObjectDataWasInvoked, Is.False);
            Assert.That(after.ProtectedConstructorWasInvoked, Is.False);
        }
    }
}
