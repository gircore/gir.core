using System.Collections.Generic;
using Generator3.Generation.Enumeration;
using Generator3.Publication;

namespace Generator3
{
    public static class Enumerations
    {
        public static void Generate(this IEnumerable<GirModel.Enumeration> enumerations)
        {
            var generator = new Generator(
                template: new Template(),
                publisher: new EnumFilePublisher()
            );
        
            foreach(var enumeration in enumerations)
                generator.Generate(enumeration);
        }
    }
}
