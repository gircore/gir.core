using System.Collections.Generic;
using System.Linq;
using Generator3.Model.Public;

namespace Generator3.Generation.Class.Standard
{
    public class PublicSignalsModel
    {
        private readonly GirModel.Class _class;

        public string Name => _class.Name;
        public string NamespaceName => _class.Namespace.Name;
        public IEnumerable<Signal> Signals { get; }

        public PublicSignalsModel(GirModel.Class @class)
        {
            _class = @class;
            Signals = _class.Signals.Select(signal => new Signal(signal, Name));
        }
    }
}
