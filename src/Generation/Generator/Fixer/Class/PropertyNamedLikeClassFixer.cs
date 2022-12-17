using Generator.Model;

namespace Generator.Fixer.Class;

internal class PropertyNamedLikeClassFixer : Fixer<GirModel.Class>
{
    public void Fixup(GirModel.Class @class)
    {
        foreach (var property in @class.Properties)
        {
            if (Property.GetName(property) == @class.Name)
            {
                Property.Disable(property);
                Log.Warning($"Property {property.Name} is named like its containing class {@class.Namespace.Name}.{@class.Name}. This is not allowed. The property should be created with a suffix and be rewritten to it's original name");
            }
        }

        foreach (var @interface in @class.Implements)
        {
            foreach (var interfaceProperty in @interface.Properties)
            {
                if (Property.GetName(interfaceProperty) == @class.Name)
                {
                    Property.Disable(interfaceProperty);
                    Log.Warning($"Property {interfaceProperty.Name} of interface {@interface.Namespace.Name}.{@interface.Name} is named like its containing class {@class.Namespace.Name}.{@class.Name}. This is not allowed. The property should be created with a suffix and be rewritten to it's original name");
                }
            }
        }
    }
}
