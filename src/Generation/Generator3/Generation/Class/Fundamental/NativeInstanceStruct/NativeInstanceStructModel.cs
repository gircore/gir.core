using System.Collections.Generic;
using System.Linq;
using Generator3.Model;

namespace Generator3.Generation.Class.Fundamental
{
    public class NativeInstanceStructModel
    {
        private readonly GirModel.Class _class;

        public string Name => _class.Name;
        public string NamespaceName => _class.Namespace.Name + ".Native";
        public IEnumerable<Field> Fields { get; }

        public NativeInstanceStructModel(GirModel.Class @class)
        {
            _class = @class;

            Fields = @class
                .Fields
                .CreateNativeModels()
                .ToList();
        }
    }
}
