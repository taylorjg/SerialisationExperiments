using System;
using NUnit.Framework;
using SerialisationTests.Serialisation.Implementations;
using SerialisationTests.Things;

namespace SerialisationTests.Serialisation
{
    public static class SerialisationUtils
    {
        public static T SerialiseAndDeserialise<T>(SerialiserType serialiserType, T before)
        {
            var serialiser = GetSerialiserFor<T>(serialiserType);
            var deserialiser = serialiser.Serialise(before);
            var after = deserialiser.Deserialise();
            return after;
        }

        public static ThingWithDeserializationCallback MakeThingWithDeserializationCallback()
        {
            return new ThingWithDeserializationCallback
                {
                    Property1 = 42,
                    Property2 = "Jon"
                };
        }

        public static ThingWithDefaultConstructor MakeThingWithDefaultConstructor()
        {
            return new ThingWithDefaultConstructor
                {
                    Property1 = 42,
                    Property2 = "Jon"
                };
        }

        public static void AssertBeforeAndAfterHaveSamePropertyValues(ThingWithDeserializationCallback before, ThingWithDeserializationCallback after)
        {
            Assert.That(after.Property1, Is.EqualTo(before.Property1));
            Assert.That(after.Property2, Is.EqualTo(before.Property2));
        }

        public static void AssertBeforeAndAfterHaveSamePropertyValues(ThingWithDefaultConstructor before, ThingWithDefaultConstructor after)
        {
            Assert.That(after.Property1, Is.EqualTo(before.Property1));
            Assert.That(after.Property2, Is.EqualTo(before.Property2));
        }

        private static ISerialiser<T> GetSerialiserFor<T>(SerialiserType serialiserType)
        {
            switch (serialiserType)
            {
                case SerialiserType.BinaryFormatter:
                    return new BinarySerialiser<T>();

                case SerialiserType.NewtonsoftJson:
                    return new NewtonsoftJsonSerialiser<T>();

                case SerialiserType.XmlSerialiser:
                    return new XmlSerialiser<T>();

                default:
                    throw new InvalidOperationException(string.Format("Unknown serialiserType value: {0}.", serialiserType));
            }
        }
    }
}
