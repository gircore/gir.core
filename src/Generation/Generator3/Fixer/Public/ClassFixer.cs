using Generator3.Converter;

namespace Generator3.Fixer.Public;

public static class ClassFixer
{
    public static void Fixup(this GirModel.Class @class)
    {
        FixMethodsColldingWithProperties(@class);
    }

    private static void FixMethodsColldingWithProperties(GirModel.Class @class)
    {
        foreach (var property in @class.Properties)
        {
            foreach (var method in @class.Methods)
            {
                if (property.GetPublicName() == method.GetPublicName())
                {
                    var newName = $"Get{method.GetPublicName()}";
                    Log.Warning($"{@class.Namespace.Name}.{@class.Name}: Property {property.GetPublicName()} collides with method {method.GetPublicName()} and is renamed to {newName}.");

                    method.SetPublicName(newName);
                }
            }
        }
    }
}
