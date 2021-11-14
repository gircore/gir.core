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
            foreach (var @class in classes)
            {
                if(@class.IsFundamental)
                    GenerateFundamentalClass(@class);
                else
                    GenerateStandardClass(@class);
            }
        }

        private static void GenerateFundamentalClass(GirModel.Class @class)
        {
            var fundamentalNativeInstanceStructGenerator = new Fundamental.NativeInstanceStructGenerator(
                template: new Fundamental.NativeInstanceStructTemplate(),
                publisher: new NativeClassFilePublisher()
            );

            var fundamentalNativeInstanceMethodsGenerator = new Fundamental.NativeInstanceMethodsGenerator(
                template: new Fundamental.NativeInstanceMethodsTemplate(),
                publisher: new NativeClassFilePublisher()
            );


            fundamentalNativeInstanceStructGenerator.Generate(@class);
            fundamentalNativeInstanceMethodsGenerator.Generate(@class);
        }

        private static void GenerateStandardClass(GirModel.Class @class)
        {
            var standardNativeMethodsGenerator = new Standard.NativeMethodsGenerator(
                template: new Standard.NativeMethodsTemplate(),
                publisher: new NativeClassFilePublisher()
            );
            
            var standardNativeStructGenerator = new Standard.NativeStructGenerator(
                template: new Standard.NativeStructTemplate(),
                publisher: new NativeClassFilePublisher()
            );

            var standardPublicCLassMethodsGenerator = new Standard.PublicMethodsGenerator(
                template: new Standard.PublicMethodsTemplate(),
                publisher: new PublicClassFilePublisher()
            );
            
            standardNativeMethodsGenerator.Generate(@class);
            standardNativeStructGenerator.Generate(@class);
            standardPublicCLassMethodsGenerator.Generate(@class);
        }
    }
}
