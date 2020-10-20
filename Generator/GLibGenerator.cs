using System;
using System.Collections.Generic;
using System.Linq;
using Gir;
using Scriban.Runtime;

namespace Generator
{
    public class GLibGenerator : Generator<GLibTemplateLoader>
    {
        public GLibGenerator(Project project) : base(project) { }

        protected override void GenerateDelegates(IEnumerable<GCallback> delegates, string @namespace)
        {
            foreach (var dlg in delegates)
            {
                var scriptObject = GetScriptObject();
                scriptObject.Import(dlg);
                
                Generate(
                    templateName: "delegate",
                    subfolder: "Delegates",
                    fileName: dlg.Name,
                    scriptObject: scriptObject
                );
            }
        }

        protected override void GenerateStructs(IEnumerable<GRecord> records, string @namespace)
        {
            foreach (var record in records)
            {
                // By calling GetStructType(), we determine whether the struct is
                // readable or opaque and generate it accordingly. See GetStructType()
                // for details.

                var (templateName, subfolder) = GetStructType(record) switch
                {
                    StructType.RefStruct => ("struct", "Structs"),
                    StructType.OpaqueStruct => ("struct_as_class", "Classes"),
                    _ => throw new NotImplementedException($"Cannot generate struct {record.Name} - Skipping"),
                };

                var scriptObject = GetScriptObject();
                scriptObject.Import(record);

                Generate(
                    templateName: templateName,
                    subfolder: subfolder,
                    fileName: record.Name,
                    scriptObject: scriptObject
                );
            }
        }

        protected override void GenerateClasses(IEnumerable<GClass> classes, string @namespace)
        {
            foreach (var cls in classes)
            {
                var scriptObject = GetScriptObject();
                scriptObject.Import(cls);

                Generate(
                    templateName: "class",
                    subfolder: "Classes",
                    fileName: cls.Name,
                    scriptObject: scriptObject
                );
            }
        }

        protected override void GenerateEnums(IEnumerable<GEnumeration> enums, string @namespace, bool hasFlags)
        {
            foreach (var obj in enums)
            {
                var scriptObject = GetScriptObject();
                scriptObject.Import(obj);
                scriptObject.Add("has_flags", hasFlags);

                Generate(
                    templateName: "enum",
                    subfolder: "Enums",
                    fileName: obj.Name,
                    scriptObject: scriptObject
                );
            }
        }
    }
}