using System.Collections.Generic;
using System.Linq;

namespace Generator3.Generation.Constants
{
    public class Generator
    {
        private readonly Renderer _renderer;

        public Generator(Renderer renderer)
        {
            _renderer = renderer;
        }

        public string Generate(IEnumerable<GirModel.Constant> constants)
        {
            var data = new Data(
                namespaceName: constants.First().NamespaceName
            );

            foreach (var constant in constants)
                data.Add(new Constant(constant));

            return _renderer.Render(data);
        }
    }
}
