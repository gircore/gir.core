using System.Collections.Generic;
using Generator3.Generation.Union;
using Generator3.Publication;

namespace Generator3
{
    public static class Unions
    {
        public static void Generate(this IEnumerable<GirModel.Union> unions)
        {
            var internalStructGenerator = new InternalStructGenerator(
                template: new InternalStructTemplate(),
                publisher: new InternalUnionFilePublisher()
            );

            var internalMethodsGenerator = new InternalMethodsGenerator(
                template: new InternalMethodsTemplate(),
                publisher: new InternalUnionFilePublisher()
            );

            foreach (var union in unions)
            {
                internalStructGenerator.Generate(union);
                internalMethodsGenerator.Generate(union);
            }
        }
    }
}
