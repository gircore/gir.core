using System;
using System.Linq;
using System.Collections.Generic;
using Gir;
using Scriban.Runtime;

namespace Generator
{
    public class GLibGenerator : Generator<GLibTemplateLoader>
    {
        #region Constructors

        public GLibGenerator(Project project) : base(project) { }

        #endregion

        #region Methods

        protected override void GenerateDelegates(IEnumerable<GCallback> delegates, string @namespace)
        {
            foreach (GCallback? dlg in delegates)
            {
                ScriptObject? scriptObject = GetScriptObject();
                scriptObject.Import(dlg);

                Generate(
                    templateName: "delegate",
                    subfolder: "Delegates",
                    fileName: dlg.Name,
                    scriptObject: scriptObject
                );
            }
        }
        
        protected override void GenerateGlobals(IEnumerable<GMethod> methods, string @namespace)
        {
            ScriptObject? scriptObject = GetScriptObject();
            scriptObject.Add("methods", methods.Where(x => !x.HasVariadicParameter()));

            Generate(
                templateName: "global",
                subfolder: "Classes",
                fileName: "Global",
                scriptObject: scriptObject
            );
        }

        protected override void GenerateStructs(IEnumerable<GRecord> records, string @namespace)
        {
            foreach (GRecord? record in records)
            {
                // By calling GetStructType(), we determine whether the struct is
                // readable or opaque and generate it accordingly. See GetStructType()
                // for details.

                (var templateName, var subfolder) = GetStructType(record) switch
                {
                    StructType.RefStruct => ("struct", "Structs"),
                    StructType.OpaqueStruct => ("struct_as_class", "Classes"),
                    _ => throw new NotImplementedException($"Cannot generate struct {record.Name} - Skipping"),
                };

                ScriptObject? scriptObject = GetScriptObject();
                scriptObject.Import(record);

                Generate(
                    templateName: templateName,
                    subfolder: subfolder,
                    fileName: record.Name,
                    scriptObject: scriptObject
                );
            }
        }

        protected override void GenerateEnums(IEnumerable<GEnumeration> enums, string @namespace, bool hasFlags)
        {
            foreach (GEnumeration? obj in enums)
            {
                ScriptObject? scriptObject = GetScriptObject();
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

        #endregion
    }
}
