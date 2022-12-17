using Generator.Model;

namespace Generator.Fixer.Class;

internal class PublicMethodsColldingWithPropertiesFixer : Fixer<GirModel.Class>
{
    public void Fixup(GirModel.Class @class)
    {
        foreach (var property in @class.Properties)
        {
            foreach (var method in @class.Methods)
            {
                if (Property.GetName(property) == Method.GetPublicName(method))
                {
                    if (method.ReturnType.AnyType.Is<GirModel.Void>())
                    {
                        Log.Warning($"{@class.Namespace.Name}.{@class.Name}: Property {Property.GetName(property)} is named like a method but returns no value. It makes no sense to prefix the method with 'Get'. Thus it will be ignored.");
                        Method.Disable(method);
                    }
                    else
                    {
                        var newName = $"Get{Method.GetPublicName(method)}";
                        Log.Warning($"{@class.Namespace.Name}.{@class.Name}: Property {Property.GetName(property)} collides with method {Method.GetPublicName(method)} and is renamed to {newName}.");

                        Method.SetPublicName(method, newName);
                    }
                }
            }
        }
    }
}
