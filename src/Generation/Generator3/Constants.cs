using System.Collections.Generic;
using System.Linq;
using Generator3.Generation.Constants;
using Generator3.Publication;

namespace Generator3
{
    public static class Constants
    {
        public static void Generate(this IEnumerable<GirModel.Constant> constants)
        {
            constants = constants.Where(x => x.Introspectable);
            var generator = new Generator(
                template: new Template(),
                publisher: new PublicClassFilePublisher()
            );

            generator.Generate(constants);
        }
    }
}
