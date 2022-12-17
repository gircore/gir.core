using Generator.Model;

namespace Generator.Fixer.Class;

internal class InternalMethodsNamedLikeClassFixer : Fixer<GirModel.Class>
{
    public void Fixup(GirModel.Class @class)
    {
        foreach (var method in @class.Methods)
        {
            if (method.Name == @class.Name)
            {
                Log.Warning($"{@class.Name}: Method {method.Name} is named like its containing class. This is not allowed. The method should be created with a suffix and be rewritten to it's original name");
                Method.SetInternalName(method, method.Name + "1");
            }
        }
    }
}
