using Generator.Model;

namespace Generator.Fixer.Class;

internal class InterfaceMethodsCollidingWithClassMethodsFixer : Fixer<GirModel.Class>
{
    public void Fixup(GirModel.Class @class)
    {
        //TODO: Replace with explicit interface implementation in case of signature collisions

        foreach (var method in @class.Methods)
        {
            var duplicateMethods = Model.Class.DuplicateMethods(@class, method);
            foreach (var duplicateMethod in duplicateMethods)
            {
                Method.Disable(duplicateMethod);
                Log.Warning($"Disabled method {duplicateMethod.Parent.Name}.{duplicateMethod.Name} for class {@class.Name} as there is a conflicting method on the class.");
            }
        }
    }
}
