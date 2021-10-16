using System.Collections.Generic;
using System.Linq;

namespace Generator3.Model
{
    public class NativeRecordStructUnit
    {
        private readonly GirModel.Record _record;

        public string Name => _record.Name;
        public string NamespaceName => _record.NamespaceName + ".Native";
        public IEnumerable<Field> Fields { get; }

        public NativeRecordStructUnit(GirModel.Record record)
        {
            _record = record;

            Fields = record.Fields
                .Select(Field.CreateNative)
                .ToList();
        }
    }
}
