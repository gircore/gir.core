using System.Collections.Generic;
using Generator3.Publication;
using Generator3.Generation.Interface;

namespace Generator3
{
    public static class Interfaces
    {
        public static void Generate(this IEnumerable<GirModel.Interface> interfaces)
        {
            var internalMethodsGenerator = new InternalMethodsGenerator(
                template: new InternalMethodsTemplate(),
                publisher: new InternalInterfaceFilePublisher()
            );

            foreach (var record in interfaces)
            {
                internalMethodsGenerator.Generate(@record);
            }
        }
    }
}
