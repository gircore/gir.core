using System;
using System.Collections.Generic;
using System.Linq;

namespace Generator3.Generation.Framework
{
    public static class RecordTypeRegistrationRenderer
    {
        public static string Render(this IEnumerable<GirModel.Record> records)
        {
            return records
                .Select(Render)
                .Join(Environment.NewLine);
        }

        private static string Render(this GirModel.Record record)
        {
            return $@"
try
{{
    GObject.Internal.TypeDictionary.Add(typeof({record.Name}), new GObject.Type(Internal.{record.Name}.GetGType()));
}}
catch(Exception e)
{{
    Console.WriteLine($""Could not register record type '{record.Name}': {{e.Message}}"");
}}";
        }
    }
}
