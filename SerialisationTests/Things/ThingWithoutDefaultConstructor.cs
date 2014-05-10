using System;

namespace SerialisationTests.Things
{
    [Serializable]
    public class ThingWithoutDefaultConstructor
    {
        public int Property1 { get; set; }
        public string Property2 { get; set; }

        public ThingWithoutDefaultConstructor(int property1, string property2)
        {
            Property1 = property1;
            Property2 = property2;
        }
    }
}
