using System;
using System.Collections.Generic;
using System.Linq;
using Gir;

namespace Generator
{
    public class GLibGenerator : Generator<GLibTemplateLoader>
    {
        enum StructType
        {
            RefStruct,          // Simple Marshal-able C-struct
            OpaqueStruct,       // Opaque struct, marshal as class + IntPtr
            PublicClassStruct,  // GObject type struct (special case)
            PrivateClassStruct  // Same as above, but opaque
        }

        public GLibGenerator(Project project) : base(project) { }

        protected override void GenerateDelegates(IEnumerable<GCallback> delegates, string @namespace)
        {
            foreach (var dele in delegates)
            {
                Generate(dele,
                    templateName: "delegate",
                    subfolder: "Delegates",
                    fileName: dele.Name,
                    scriptObject: ScriptObject
                );
            }
        }

        // Determine how we should generate a given record/struct based on
        // a simple set of rules.
        private StructType GetStructType(GRecord record)
        {
            switch (record)
            {
                // Disguised (private) Class Struct
                case GRecord r when r.GLibIsGTypeStructFor != null && r.Disguised == true:
                    return StructType.PrivateClassStruct;

                // Introspectable (public) Class Struct
                case GRecord r when r.GLibIsGTypeStructFor != null && r.Disguised == false:
                    return StructType.PublicClassStruct;

                // Disguised/Empty Struct
                case GRecord r when r.Disguised || r.Fields.Count == 0:
                    return StructType.OpaqueStruct;

                // Regular C-Style Structure
                default:
                    return StructType.RefStruct;
            }
        }

        protected override void GenerateStructs(IEnumerable<GRecord> records, string @namespace)
        {
            foreach (var record in records)
            {
                // There are structs which must be generated as classes. Currently
                // this is especially true for GLib. But there a structs which actually
                // are structs. The only distinction right now is that the "fake" structs
                // have no fields defined

                var (templateName, subfolder) = GetStructType(record) switch
                {
                    StructType.RefStruct => ("struct", "Structs"),
                    StructType.OpaqueStruct => ("struct_as_class", "Classes"),
                    _ => throw new NotImplementedException($"Cannot generate struct {record.Name} - Skipping"),
                };

                Generate(record,
                    templateName: templateName,
                    subfolder: subfolder,
                    fileName: record.Name,
                    scriptObject: ScriptObject
                );
            }
        }

        protected override void GenerateClasses(IEnumerable<GInterface> classes, string @namespace)
        {
            foreach (var cls in classes)
            {
                Generate(cls,
                    templateName: "class",
                    subfolder: "Classes",
                    fileName: cls.Name,
                    scriptObject: ScriptObject
                );   
            }
        }

        protected override void GenerateEnums(IEnumerable<GEnumeration> enums, string @namespace, bool hasFlags)
        {
            ScriptObject.Add("has_flags", hasFlags);
            
            foreach (var obj in enums)
            {
                Generate(obj,
                    templateName: "enum",
                    subfolder: "Enums",
                    fileName: obj.Name,
                    scriptObject: ScriptObject
                );   
            }
        }
    }
}