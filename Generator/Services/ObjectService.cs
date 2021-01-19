using Generator.Analysis;
using Generator.Introspection;

namespace Generator.Services
{
    public class ObjectService : Service
    {
        public string WriteInheritance(ObjectSymbol obj)
        {
            GClass classInfo = obj.ClassInfo;
            if (classInfo.Parent is null)
                return string.Empty;

            // Parent Object
            var result = $": {classInfo.Parent}";

            // Interfaces
            foreach (GImplement impl in classInfo.Implements)
            {
                InterfaceSymbol ifaceSymbol;
                
                // If the name contains a dot, it is qualified
                // TODO: Make a 'IsQualified()' method
                if (impl.Name.Contains('.'))
                    ifaceSymbol = (InterfaceSymbol) TypeDict.GetSymbol(impl.Name);
                else
                    ifaceSymbol = (InterfaceSymbol) TypeDict.GetSymbol(obj.NativeName.Namespace, impl.Name);
                
                result += $", {ifaceSymbol.ManagedName}";
            }

            return result;
        }
    }
}
