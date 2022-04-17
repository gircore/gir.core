using System.Collections.Generic;
using System.Linq;
using Generator3.Converter;

namespace Generator3.Generation.Record
{
    public class InternalOwnedHandleModel
    {
        public string HandleName => Record.GetInternalHandleName();
        public string OwnedHandleName => Record.GetInternalOwnedHandleName();
        public string InternalNamespaceName => Record.Namespace.GetInternalName();
        public bool Foreign => Record.Foreign;
        public GirModel.Record Record { get; }
        public GirModel.Method? FreeMethod => GetFreeOrUnrefMethod(Record.Methods);
        public GirModel.PlatformDependent? PlatformDependent => Record as GirModel.PlatformDependent;

        public InternalOwnedHandleModel(GirModel.Record record)
        {
            Record = record;
        }

        private static GirModel.Method? GetFreeOrUnrefMethod(IEnumerable<GirModel.Method> methods)
            //Unref functions takes precedense over free function
            => methods.FirstOrDefault(function => function.IsUnref())
               ?? methods.FirstOrDefault(function => function.IsFree());
    }
}
