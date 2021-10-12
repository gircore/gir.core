using System.Collections.Generic;
using System.Linq;

namespace Generator3.Generation.Unit.NativeFunctions
{
    public class Generator
    {
        private readonly Renderer _renderer;

        public Generator(Renderer renderer)
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
