using Generator.Model;

namespace Generator.Fixer.Class;

internal class PropertyLikeInterfacePropertyFixer : Fixer<GirModel.Class>
{
    public void Fixup(GirModel.Class @class)
    {
        foreach (var @interface in @class.Implements)
        {
            foreach (var interfaceProperty in @interface.Properties)
            {
                foreach (var property in @class.Properties)
                {
                    if (Property.GetName(property) == Property.GetName(interfaceProperty))
                    {
                        Log.Warning($"Disabling {@class.Namespace.Name}.{@class.Name}.{Property.GetName(property)} becuase it conflcits with a property of an interface ({@interface.Namespace.Name}.{@interface.Name}.{Property.GetName(interfaceProperty)})");
                        Property.Disable(property);
                    }
                }
            }
        }
    }
}
