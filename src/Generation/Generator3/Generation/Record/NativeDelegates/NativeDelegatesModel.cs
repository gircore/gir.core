using System.Collections.Generic;
using System.Linq;
using Generator3.Model;

namespace Generator3.Generation.Record
{
    public class NativeDelegatesModel
    {
        private readonly GirModel.Record _record;

        public string Name => _record.Name;
        public string NamespaceName => _record.Namespace.GetNativeName();
        public IEnumerable<NativeCallback> Callbacks { get; }
        public NativeDelegatesModel(GirModel.Record record)
        {
            _record = record;

            Callbacks = _record.Fields
                .Select(x => x.AnyTypeOrCallback)
                .Where(x => x.IsT1)
                .Select(x => new NativeCallback(x.AsT1));
        }
    }
}
