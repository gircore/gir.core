using System.Collections.Generic;
using System.Linq;
using Generator3.Converter;

namespace Generator3.Generation.Record
{
    public class InternalHandleModel
    {
        public string HandleName => Record.GetInternalHandleName();
        public string NullHandleName => Record.GetInternalNullHandleName();
        public string OwnedHandleName => Record.GetInternalOwnedHandleName();
        public string UnownedHandleName => Record.GetInternalUnownedHandleName();
        public string InternalNamespaceName => Record.Namespace.GetInternalName();
        public string NamespaceName => Record.Namespace.Name;
        public GirModel.Record Record { get; }
        public GirModel.Method? FreeMethod => GetFreeOrUnrefMethod(Record.Methods);

        public InternalHandleModel(GirModel.Record record)
        {
            Record = record;
        }

        private static GirModel.Method? GetFreeOrUnrefMethod(IEnumerable<GirModel.Method> methods)
            //Unref functions takes precedense over free function
            => methods.FirstOrDefault(function => function.IsUnref())
               ?? methods.FirstOrDefault(function => function.IsFree());
    }
}
