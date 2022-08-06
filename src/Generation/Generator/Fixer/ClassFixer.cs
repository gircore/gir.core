using Generator.Model;

namespace Generator.Fixer;

internal static class ClassFixer
{
    public static void Fixup(GirModel.Class @class)
    {
        FixPublicMethodsColldingWithProperties(@class);
        FixInternalMethodsNamedLikeClass(@class);
        FixPropertyNamedLikeClass(@class);
    }

    private static void FixPublicMethodsColldingWithProperties(GirModel.Class @class)
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

    private static void FixInternalMethodsNamedLikeClass(GirModel.Class @class)
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

    private static void FixPropertyNamedLikeClass(GirModel.Class @class)
    {
        foreach (var property in @class.Properties)
        {
            if (Property.GetName(property) == @class.Name)
            {
                Property.Disable(property);
                Log.Warning($"Property {property.Name} is named like its containing class. This is not allowed. The property should be created with a suffix and be rewritten to it's original name");
            }
        }
    }
}
