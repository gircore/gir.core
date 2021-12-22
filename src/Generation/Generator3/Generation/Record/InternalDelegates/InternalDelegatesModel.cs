using System.Collections.Generic;
using System.Linq;
using Generator3.Converter;
using Generator3.Model.Internal;

namespace Generator3.Generation.Record
{
    public class InternalDelegatesModel
    {
        private readonly GirModel.Record _record;

        public string Name => _record.Name;
        public string NamespaceName => _record.Namespace.GetInternalName();
        public IEnumerable<Model.Internal.Callback> Callbacks { get; }
        public InternalDelegatesModel(GirModel.Record record)
        {
            _record = record;

            Callbacks = _record.Fields
                .Select(x => x.AnyTypeOrCallback)
                .Where(x => x.IsT1)
                .Select(x => new Model.Internal.Callback(x.AsT1));
        }
    }
}
