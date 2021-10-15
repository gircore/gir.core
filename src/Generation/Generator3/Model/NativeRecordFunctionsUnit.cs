using System.Collections.Generic;
using System.Linq;

namespace Generator3.Model
{
    public class NativeRecordFunctionsUnit
    {
        private readonly GirModel.Record _record;

        public string Name => _record.Name;
        public string NamespaceName => _record.NamespaceName + ".Native";
        public IEnumerable<NativeFunction> Functions { get; }
        public NativeFunction? TypeFunction { get; }

        public NativeRecordFunctionsUnit(GirModel.Record record)
        {
            _record = record;
            Functions = record.Functions
                .Select(function => new NativeFunction(function))
                .ToList();

            TypeFunction = record.TypeFunction is not null
                ? new NativeFunction(record.TypeFunction)
                : null;
        }
    }
}
