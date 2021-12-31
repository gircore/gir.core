using System.Collections.Generic;
using Generator3.Generation.Functions;
using Generator3.Publication;

namespace Generator3
{
    public static class Functions
    {
        public static void Generate(this IEnumerable<GirModel.Function> functions)
        {
            var generator = new InternalGenerator(
                template: new InternalTemplate(),
                publisher: new InternalClassFilePublisher()
            );

            generator.Generate(functions);
        }
    }
}
