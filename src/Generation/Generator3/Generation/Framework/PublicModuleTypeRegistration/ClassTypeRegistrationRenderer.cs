using System;
using System.Collections.Generic;
using System.Linq;
using Generator3.Renderer;

namespace Generator3.Generation.Framework
{
    public static class ClassTypeRegistrationRenderer
    {
        public static string Render(this IEnumerable<GirModel.Class> classes)
        {
            return classes
                .Select(Render)
                .Join(Environment.NewLine);
        }

        private static string Render(this GirModel.Class cls)
        {
            var platformDependent = cls as GirModel.PlatformDependent;
            return $@"
try
{{
    { platformDependent.RenderPlatformSupportCondition() }
    GObject.Internal.TypeDictionary.Add(typeof({cls.Name}), new GObject.Type(Internal.{cls.Name}.GetGType()));
}}
catch(Exception e)
{{
    Console.WriteLine($""Could not register class type '{cls.Name}': {{e.Message}}"");
}}";
        }
    }
}
