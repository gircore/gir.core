using System.Collections.Generic;
using System.Linq;
using Generator3.Converter;

namespace Generator3.Generation.Record
{
    public class InternalSafeHandleModel
    {
        public string Name => Record.Name;
        public string HandleClassName => "Handle";
        public string InternalNamespaceName => Record.Namespace.GetInternalName();
        public string NamespaceName => Record.Namespace.Name;
        public GirModel.Record Record { get; }
        public GirModel.Method? FreeMethod => GetFreeOrUnrefMethod(Record.Methods);

        public InternalSafeHandleModel(GirModel.Record record)
        {
            Record = record;
        }
        
        private static GirModel.Method? GetFreeOrUnrefMethod(IEnumerable<GirModel.Method> methods)
            //Unref functions takes precedense over free function
            => methods.FirstOrDefault(function => function.IsUnref()) 
               ?? methods.FirstOrDefault(function => function.IsFree());
    }
}
