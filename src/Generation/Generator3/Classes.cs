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
            var fundamentalInternalInstanceStructGenerator = new Fundamental.InternalInstanceStructGenerator(
                template: new Fundamental.InternalInstanceStructTemplate(),
                publisher: new InternalClassFilePublisher()
            );

            var fundamentalInternalInstanceMethodsGenerator = new Fundamental.InternalInstanceMethodsGenerator(
                template: new Fundamental.InternalInstanceMethodsTemplate(),
                publisher: new InternalClassFilePublisher()
            );


            fundamentalInternalInstanceStructGenerator.Generate(@class);
            fundamentalInternalInstanceMethodsGenerator.Generate(@class);
        }

        private static void GenerateStandardClass(GirModel.Class @class)
        {
            var standardInternalMethodsGenerator = new Standard.InternalMethodsGenerator(
                template: new Standard.InternalMethodsTemplate(),
                publisher: new InternalClassFilePublisher()
            );
            
            var standardInternalStructGenerator = new Standard.InternalStructGenerator(
                template: new Standard.InternalStructTemplate(),
                publisher: new InternalClassFilePublisher()
            );

            var standardPublicClassMethodsGenerator = new Standard.PublicMethodsGenerator(
                template: new Standard.PublicMethodsTemplate(),
                publisher: new PublicClassFilePublisher()
            );
            
            standardInternalMethodsGenerator.Generate(@class);
            standardInternalStructGenerator.Generate(@class);
            standardPublicClassMethodsGenerator.Generate(@class);
        }
    }
}
