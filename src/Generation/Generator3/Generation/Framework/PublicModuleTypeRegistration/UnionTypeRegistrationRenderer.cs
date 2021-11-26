using System;
using System.Collections.Generic;
using System.Linq;

namespace Generator3.Generation.Framework
{
    public static class UnionTypeRegistrationRenderer
    {
        public static string Render(this IEnumerable<GirModel.Union> unions)
        {
            return unions
                .Select(Render)
                .Join(Environment.NewLine);
        }

        private static string Render(this GirModel.Union union)
        {
            return $@"
try
{{
    GObject.Internal.TypeDictionary.Add(typeof({union.Name}), new GObject.Type(Internal.{union.Name}.Methods.GetGType()));
}}
catch(Exception e)
{{
    Console.WriteLine($""Could not register union type '{union.Name}': {{e.Message}}"");
}}";
        }
    }
}
