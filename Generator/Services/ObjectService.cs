using Generator.Analysis;
using Generator.Introspection;

namespace Generator.Services
{
    public class ObjectService : Service
    {
        public string WriteInheritance(ObjectSymbol obj)
        {
            ClassInfo classInfo = obj.ClassInfo;
            if (classInfo.Parent is null)
                return string.Empty;

            // Parent Object
            var result = $": {classInfo.Parent}";

            // Interfaces
            foreach (ImplementInfo impl in classInfo.Implements)
            {
                var ifaceSymbol = (InterfaceSymbol) TypeDict.LookupSymbol(impl.Name);
                
                result += $", {ifaceSymbol.ManagedName}";
            }

            return result;
        }
    }
}
