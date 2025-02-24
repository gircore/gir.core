using Generator.Model;

namespace Generator.Fixer.Class;

internal class InterfaceMethodsCollidingWithClassConstructorsFixer : Fixer<GirModel.Class>
{
    public void Fixup(GirModel.Class @class)
    {
        foreach (var constructor in @class.Constructors)
        {
            var duplicateMethods = Model.Class.DuplicateMethods(@class, constructor);
            foreach (var duplicateMethod in duplicateMethods)
            {
                if (duplicateMethod.Parent is GirModel.Interface)
                {
                    Method.SetImplementExplicitly(duplicateMethod);
                }
                else
                {
                    Method.Disable(duplicateMethod);
                    Log.Warning($"Disabled method {duplicateMethod.Parent.Name}.{duplicateMethod.Name} for class {@class.Name} as there is a conflicting constructor on the class.");
                }
            }
        }
    }
}
