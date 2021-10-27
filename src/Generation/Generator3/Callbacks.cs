using System.Collections.Generic;
using Generator3.Generation.Callback;
using Generator3.Publication;

namespace Generator3
{
    public static class Callbacks
    {
        public static void Generate(this IEnumerable<GirModel.Callback> callbacks, string project)
        {
            var generator = new NativeGenerator(
                template: new NativeTemplate(),
                publisher: new NativeDelegateFilePublisher()
            );
        
            foreach(var callback in callbacks)
                generator.Generate(project, callback);
        }
    }
}
