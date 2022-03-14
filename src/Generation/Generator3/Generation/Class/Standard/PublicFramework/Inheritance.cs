using System.Collections.Generic;
using System.Linq;

namespace Generator3.Generation.Class.Standard;

public static class Inheritance
{
    public static string RenderInheritance(this PublicFrameworkModel model)
    {
        var parentClass = model.ParentClass?.GetFullyQualified();
        var interfaces = model.Implements.Select(x => x.GetFullyQualified());

        var elements = new List<string>(interfaces);

        if (parentClass is not null)
            elements.Insert(0, parentClass);

        return elements.Count == 0
            ? string.Empty
            : $": {string.Join(", ", elements)}";
    }
}
