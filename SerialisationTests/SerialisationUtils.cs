using System;
using NUnit.Framework;

namespace SerialisationTests
{
    public static class SerialisationUtils
    {
        public static SerialisableThing SerialiseAndDeserialise(SerialiserType serialiserType, SerialisableThing before)
        {
            var serialiser = MakeSerialiser<SerialisableThing>(serialiserType);
            var deserialiser = serialiser.Serialise(before);
            var after = deserialiser.Deserialise();
            return after;
        }

        public static SerialisableThing MakeThing()
        {
            return new SerialisableThing
                {
                    Property1 = 42,
                    Property2 = "Jon"
                };
        }

        public static void AssertBeforeAndAfterHaveSamePropertyValues(SerialisableThing before, SerialisableThing after)
        {
            Assert.That(after.Property1, Is.EqualTo(before.Property1));
            Assert.That(after.Property2, Is.EqualTo(before.Property2));
        }

        private static ISerialiser<T> MakeSerialiser<T>(SerialiserType serialiserType)
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
