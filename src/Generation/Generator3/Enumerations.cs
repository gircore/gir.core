using System.Collections.Generic;
using System.Linq;
using Generator3.Generation.Enumeration;
using Generator3.Publication;

namespace Generator3
{
    public static class Enumerations
    {
        public static void Generate(this IEnumerable<GirModel.Enumeration> enumerations)
        {
            enumerations = enumerations.Where(x => x.Introspectable);
            var generator = new Generator(
                template: new Template(),
                publisher: new PublicEnumFilePublisher()
            );

            foreach (var enumeration in enumerations)
                generator.Generate(enumeration);
        }
    }
}
