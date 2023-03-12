using Generator.Model;

namespace Generator.Fixer.Class;

internal class InternalMethodsNamedLikeClassFixer : Fixer<GirModel.Class>
{
    public void Fixup(GirModel.Class @class)
    {
        foreach (var method in @class.Methods)
        {
            if (Method.GetInternalName(method) == @class.Name)
            {
                Log.Warning($"{@class.Name}: Method {Method.GetInternalName(method)} is named like its containing class. This is not allowed. The method should be created with a suffix and be rewritten to it's original name. Adding suffix \"_\"");
                Method.SetInternalName(method, Method.GetInternalName(method) + "_");
            }
        }
    }
}
