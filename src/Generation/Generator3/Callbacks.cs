using System.Collections.Generic;
using Generator3.Generation.Callback;
using Generator3.Publication;

namespace Generator3
{
    public static class Callbacks
    {
        public static void Generate(this IEnumerable<GirModel.Callback> callbacks)
        {
            var nativeDelegateGenerator = new NativeDelegateGenerator(
                template: new NativeDelegateTemplate(),
                publisher: new NativeDelegateFilePublisher()
            );

            var delegateGenerator = new PublicDelegateGenerator(
                template: new PublicDelegateTemplate(),
                publisher: new DelegateFilePublisher()
            );

            var asyncHandlerGenerator = new PublicAsyncHandlerGenerator(
                template: new PublicAsyncHandlerTemplate(),
                publisher: new DelegateFilePublisher()
            );
            
            var notifiedHandlerGenerator = new PublicNotifiedHandlerGenerator(
                template: new PublicNotifiedHandlerTemplate(),
                publisher: new DelegateFilePublisher()
            );

            foreach (var callback in callbacks)
            {
                nativeDelegateGenerator.Generate(callback);
                delegateGenerator.Generate(callback);
                asyncHandlerGenerator.Generate(callback);
                notifiedHandlerGenerator.Generate(callback);
            }
        }
    }
}
