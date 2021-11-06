using System.Collections.Generic;
using Generator3.Generation.Functions;
using Generator3.Publication;

namespace Generator3
{
    public static class Functions
    {
        public static void Generate(this IEnumerable<GirModel.Method> functions)
        {
            var generator = new NativeGenerator(
                template: new NativeTemplate(),
                publisher: new NativeClassFilePublisher()
            );
        
            generator.Generate(functions);
        }
    }
}
