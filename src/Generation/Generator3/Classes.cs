using System.Collections.Generic;
using Fundamental = Generator3.Generation.Class.Fundamental;
using Standard = Generator3.Generation.Class.Standard;
using Generator3.Publication;

namespace Generator3
{
    public static class Classes
    {
        public static void Generate(this IEnumerable<GirModel.Class> classes)
        {
            var fundamentalNativeInstanceStructGenerator = new Fundamental.NativeInstanceStructGenerator(
                template: new Fundamental.NativeInstanceStructTemplate(),
                publisher: new NativeClassFilePublisher()
            );

            var fundamentalNativeInstanceMethodsGenerator = new Fundamental.NativeInstanceMethodsGenerator(
                template: new Fundamental.NativeInstanceMethodsTemplate(),
                publisher: new NativeClassFilePublisher()
            );
            
            var standardNativeInstanceMethodsGenerator = new Standard.NativeInstanceMethodsGenerator(
                template: new Standard.NativeInstanceMethodsTemplate(),
                publisher: new NativeClassFilePublisher()
            );

            foreach (var @class in classes)
            {
                if(@class.IsFundamental)
                {
                    fundamentalNativeInstanceStructGenerator.Generate(@class);
                    fundamentalNativeInstanceMethodsGenerator.Generate(@class);
                }
                else
                {
                    standardNativeInstanceMethodsGenerator.Generate(@class);
                }
            }
        }
    }
}
