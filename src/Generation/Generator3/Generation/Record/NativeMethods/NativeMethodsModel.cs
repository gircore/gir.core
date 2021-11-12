using System.Collections.Generic;
using System.Linq;
using Generator3.Model;
using GirModel;

namespace Generator3.Generation.Record
{
    public class NativeMethodsModel
    {
        private readonly GirModel.Record _record;

        public string Name => _record.Name;
        public string NamespaceName => _record.Namespace.GetNativeName();
        public IEnumerable<NativeFunction> Functions { get; }
        public IEnumerable<NativeMethod> Methods { get; }
        public IEnumerable<NativeConstructor> Constructors { get; }
        public NativeFunction? TypeFunction { get; }

        public NativeMethodsModel(GirModel.Record record)
        {
            _record = record;

            Functions = record.Functions
                .Select(function => new NativeFunction(function))
                .ToList();

            Methods = record.Methods
                .Select(method => new NativeMethod(method))
                .ToList();

            Constructors = record.Constructors
                .Select(method => new NativeConstructor(method))
                .ToList();

            TypeFunction = record.TypeFunction is not null
                ? new NativeFunction(record.TypeFunction)
                : null;
        }
    }
}
