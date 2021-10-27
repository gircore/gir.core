using System.Collections.Generic;
using Generator3.Generation.Constants;
using Generator3.Publication;

namespace Generator3
{
    public static class Constants
    {
        public static void Generate(this IEnumerable<GirModel.Constant> constants, string project)
        {
            var generator = new Generator(
                template: new Template(),
                publisher: new ClassFilePublisher()
            );
        
            generator.Generate(project, constants);
        }
    }
}
