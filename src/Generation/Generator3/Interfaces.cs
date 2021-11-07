using System.Collections.Generic;
using Generator3.Publication;
using Generator3.Generation.Interface;

namespace Generator3
{
    public static class Interfaces
    {
        public static void Generate(this IEnumerable<GirModel.Interface> interfaces)
        {
            var nativeMethodsGenerator = new NativeMethodsGenerator(
                template: new NativeMethodsTemplate(),
                publisher: new NativeInterfaceFilePublisher()
            );

            foreach (var record in interfaces)
            {
                nativeMethodsGenerator.Generate(@record);
            }
        }
    }
}
