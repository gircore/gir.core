﻿using System.Collections.Generic;
using Generator3.Publication;
using Fundamental = Generator3.Generation.Class.Fundamental;
using Standard = Generator3.Generation.Class.Standard;

namespace Generator3
{
    public static class Classes
    {
        public static void Generate(this IEnumerable<GirModel.Class> classes)
        {
            foreach (var @class in classes)
            {
                if (@class.IsFundamental)
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

            var fundamentalPublicFrameworkGenerator = new Fundamental.PublicFrameworkGenerator(
                template: new Fundamental.PublicFrameworkTemplate(),
                publisher: new PublicClassFilePublisher()
            );

            var fundamentalPublicMethodsGenerator = new Fundamental.PublicMethodsGenerator(
                template: new Fundamental.PublicMethodsTemplate(),
                publisher: new PublicClassFilePublisher()
            );

            fundamentalInternalInstanceStructGenerator.Generate(@class);
            fundamentalInternalInstanceMethodsGenerator.Generate(@class);
            fundamentalPublicFrameworkGenerator.Generate(@class);
            fundamentalPublicMethodsGenerator.Generate(@class);
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

            var standardPublicFrameworkGenerator = new Standard.PublicFrameworkGenerator(
                template: new Standard.PublicFrameworkTemplate(),
                publisher: new PublicClassFilePublisher()
            );

            var standardPublicPropertiesGenerator = new Standard.PublicPropertiesGenerator(
                template: new Standard.PublicPropertiesTemplate(),
                publisher: new PublicClassFilePublisher()
            );

            var standardPublicSignalsGenerator = new Standard.PublicSignalsGenerator(
                template: new Standard.PublicSignalsTemplate(),
                publisher: new PublicClassFilePublisher()
            );

            standardInternalMethodsGenerator.Generate(@class);
            standardInternalStructGenerator.Generate(@class);
            standardPublicClassMethodsGenerator.Generate(@class);
            standardPublicFrameworkGenerator.Generate(@class);
            standardPublicPropertiesGenerator.Generate(@class);
            standardPublicSignalsGenerator.Generate(@class);
        }
    }
}
