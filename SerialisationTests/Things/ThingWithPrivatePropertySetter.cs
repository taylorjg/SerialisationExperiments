using System;

namespace SerialisationTests.Things
{
    [Serializable]
    public class ThingWithPrivatePropertySetter
    {
        public int Property1 { get; set; }
        public string Property2 { get; private set; }

        // ReSharper disable EmptyConstructor
        public ThingWithPrivatePropertySetter()
        {
        }
        // ReSharper restore EmptyConstructor

        public static ThingWithPrivatePropertySetter MakeThing(int property1, string property2)
        {
            return new ThingWithPrivatePropertySetter
                {
                    Property1 = property1,
                    Property2 = property2
                };
        }
    }
}
