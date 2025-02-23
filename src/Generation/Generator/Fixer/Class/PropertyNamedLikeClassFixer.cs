using Generator.Model;

namespace Generator.Fixer.Class;

internal class PropertyNamedLikeClassFixer : Fixer<GirModel.Class>
{
    public void Fixup(GirModel.Class @class)
    {
        foreach (var property in @class.Properties)
        {
            var name = Property.GetName(property);
            if (name == @class.Name)
            {
                Property.SetName(property, $"{name}_");
                Log.Information($"Property {property.Name} is named like its containing class {@class.Namespace.Name}.{@class.Name}. This is not allowed. The property should be created with a suffix and be rewritten to it's original name");
            }
        }

        foreach (var @interface in @class.Implements)
        {
            foreach (var interfaceProperty in @interface.Properties)
            {
                var name = Property.GetName(interfaceProperty);
                if (name == @class.Name)
                {
                    Property.SetName(interfaceProperty, $"{name}_");
                    Log.Information($"Property {interfaceProperty.Name} of interface {@interface.Namespace.Name}.{@interface.Name} is named like its containing class {@class.Namespace.Name}.{@class.Name}. This is not allowed. The property should be created with a suffix and be rewritten to it's original name");
                }
            }
        }
    }
}
