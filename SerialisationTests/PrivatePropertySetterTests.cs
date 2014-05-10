using NUnit.Framework;
using SerialisationTests.Serialisation;

namespace SerialisationTests
{
    [TestFixture]
    internal class PrivatePropertySetterTests
    {
        [TestCase(SerialiserType.BinaryFormatter)]
        public void CanSerialiseAndDeserialiseWithPrivatePropertySetter(SerialiserType serialiserType)
        {
            var before = SerialisationUtils.MakeThingWithPrivatePropertySetter();
            var after = SerialisationUtils.SerialiseAndDeserialise(serialiserType, before);
            SerialisationUtils.AssertBeforeAndAfterHaveSamePropertyValues(before, after);
        }

        [TestCase(SerialiserType.NewtonsoftJson)]
        public void DeserialiseWithPrivatePropertySetterDoesNotSetProperty(SerialiserType serialiserType)
        {
            var before = SerialisationUtils.MakeThingWithPrivatePropertySetter();
            var after = SerialisationUtils.SerialiseAndDeserialise(serialiserType, before);
            Assert.That(after.Property1, Is.EqualTo(before.Property1));
            Assert.That(after.Property2, Is.Not.EqualTo(before.Property2));
        }

        [TestCase(SerialiserType.XmlSerialiser)]
        public void CannotSerialiseWithPrivatePropertySetter(SerialiserType serialiserType)
        {
            var before = SerialisationUtils.MakeThingWithPrivatePropertySetter();
            Assert.Throws<System.InvalidOperationException>(() => SerialisationUtils.SerialiseAndDeserialise(serialiserType, before));
        }
    }
}
