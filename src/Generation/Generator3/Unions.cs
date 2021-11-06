using System.Collections.Generic;
using Generator3.Publication;
using Generator3.Generation.Union;

namespace Generator3
{
    public static class Unions
    {
        public static void Generate(this IEnumerable<GirModel.Union> unions)
        {
            var nativeStructGenerator = new NativeStructGenerator (
                template: new NativeStructTemplate(),
                publisher: new NativeUnionFilePublisher()
            );
            
            foreach (var union in unions)
            {
                nativeStructGenerator.Generate(union);
            }
        }
    }
}
