using System.Collections.Generic;
using System.Linq;

namespace Generator3.Generation.Unit.Native.Functions
{
    public class Generator
    {
        private readonly Renderer<Model> _renderer;

        public Generator(Renderer<Model> renderer)
        {
            _renderer = renderer;
        }

        public string Generate(IEnumerable<GirModel.Method> functions)
        {
            var data = new Model(
                namespaceName: functions.First().NamespaceName + ".Native"
            );

            foreach (var function in functions)
                data.Add(new Generation.Model.NativeFunction(function));

            return _renderer.Render(data);
        }
    }
}
