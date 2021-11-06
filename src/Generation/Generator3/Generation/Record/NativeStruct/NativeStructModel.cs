using System.Collections.Generic;
using System.Linq;
using Generator3.Model;

namespace Generator3.Generation.Record
{
    public class NativeStructModel
    {
        private readonly GirModel.Record _record;

        public string Name => _record.Name;
        public string NamespaceName => _record.Namespace.Name + ".Native";
        public IEnumerable<Field> Fields { get; }

        public NativeStructModel(GirModel.Record record)
        {
            _record = record;

            Fields = record
                .Fields
                .CreateNativeModels()
                .ToList();
        }
    }
}
