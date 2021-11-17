namespace Generator3.Generation.Framework
{
    public class ModuleDllImportTemplate : Template<ModuleDllImportModel>
    {
        public string Render(ModuleDllImportModel model)
        {
            return $@"
namespace {model.NamespaceName}
{{
    internal partial class Module
    {{
        static partial void InitializeDllImport()
        {{
            Internal.DllImport.Initialize();
        }}
    }}
}}";
        }
    }
}
