using System.Linq;
using Generator.Model;

namespace Generator.Fixer.Class;

internal class PropertyLikeInterfacePropertyFixer : Fixer<GirModel.Class>
{
    public void Fixup(GirModel.Class @class)
    {
        foreach (var interfaceProperty in @class.Implements.SelectMany(x => x.Properties))
            foreach (var property in @class.Properties)
                if (Property.GetName(property) == Property.GetName(interfaceProperty))
                    Property.SetImplementExplicitly(interfaceProperty);
    }
}
