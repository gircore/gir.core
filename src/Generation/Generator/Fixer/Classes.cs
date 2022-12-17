using System.Collections.Generic;
using Generator.Fixer.Class;

namespace Generator.Fixer;

public static class Classes
{
    private static readonly List<Fixer<GirModel.Class>> Fixers = new()
    {
        new PublicMethodsColldingWithPropertiesFixer(),
        new InternalMethodsNamedLikeClassFixer(),
        new PropertyNamedLikeClassFixer(),
        new PropertyLikeInterfacePropertyFixer(),
        new InterfaceMethodsCollidingWithClassMethodsFixer()
    };

    public static void Fixup(IEnumerable<GirModel.Class> classes)
    {
        foreach (var @class in classes)
            foreach (var fixer in Fixers)
                fixer.Fixup(@class);
    }
}
