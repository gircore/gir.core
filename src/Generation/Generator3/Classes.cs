using System.Collections.Generic;
using Generator3.Generation.Class.Fundamental;
using Generator3.Publication;

namespace Generator3
{
    public static class Classes
    {
        public static void Generate(this IEnumerable<GirModel.Class> classes)
        {
            var fundamentalNativeInstanceStructGenerator = new NativeInstanceStructGenerator(
                template: new NativeInstanceStructTemplate(),
                publisher: new NativeClassFilePublisher()
            );

            foreach (var @class in classes)
            {
                if(@class.Fundamental)
                {
                    fundamentalNativeInstanceStructGenerator.Generate(@class);
                }
            }
        }
    }
}
