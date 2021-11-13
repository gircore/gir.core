using System.Collections.Generic;
using System.Linq;
using Generator3.Model.Native;

namespace Generator3.Generation.Record
{
    public class NativeDelegatesModel
    {
        private readonly GirModel.Record _record;

        public string Name => _record.Name;
        public string NamespaceName => _record.Namespace.GetNativeName();
        public IEnumerable<Model.Native.Callback> Callbacks { get; }
        public NativeDelegatesModel(GirModel.Record record)
        {
            _record = record;

            Callbacks = _record.Fields
                .Select(x => x.AnyTypeOrCallback)
                .Where(x => x.IsT1)
                .Select(x => new Model.Native.Callback(x.AsT1));
        }
    }
}
