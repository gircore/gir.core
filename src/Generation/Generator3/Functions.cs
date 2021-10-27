using System.Collections.Generic;
using Generator3.Generation.Functions;
using Generator3.Publication;

namespace Generator3
{
    public static class Functions
    {
        public static void Generate(this IEnumerable<GirModel.Method> functions, string project)
        {
            var generator = new NativeGenerator(
                template: new NativeTemplate(),
                publisher: new NativeClassFilePublisher()
            );
        
            generator.Generate(project, functions);
        }
    }
}
