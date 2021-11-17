using System.Collections.Generic;
using System.Linq;
using Generator3.Model.Internal;

namespace Generator3.Generation.Record
{
    public class InternalStructModel
    {
        private readonly GirModel.Record _record;

        public string Name => _record.Name;
        public string NamespaceName => _record.Namespace.GetInternalName();
        public IEnumerable<Field> Fields { get; }

        public InternalStructModel(GirModel.Record record)
        {
            _record = record;

            Fields = record
                .Fields
                .CreateInternalModels()
                .ToList();
        }
    }
}
