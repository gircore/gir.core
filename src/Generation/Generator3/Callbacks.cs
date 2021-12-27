using System.Collections.Generic;
using System.Linq;
using Generator3.Generation.Callback;
using Generator3.Publication;

namespace Generator3
{
    public static class Callbacks
    {
        public static void Generate(this IEnumerable<GirModel.Callback> callbacks)
        {
            callbacks = callbacks.Where(x => x.Introspectable);
            var internalDelegateGenerator = new InternalDelegateGenerator(
                template: new InternalDelegateTemplate(),
                publisher: new InternalDelegateFilePublisher()
            );

            var delegateGenerator = new PublicDelegateGenerator(
                template: new PublicDelegateTemplate(),
                publisher: new PublicDelegateFilePublisher()
            );

            var handlerGenerator = new PublicHandlerGenerator(
                template: new PublicHandlerTemplate(),
                publisher: new PublicDelegateFilePublisher()
            );

            var asyncHandlerGenerator = new PublicAsyncHandlerGenerator(
                template: new PublicAsyncHandlerTemplate(),
                publisher: new PublicDelegateFilePublisher()
            );

            var notifiedHandlerGenerator = new PublicNotifiedHandlerGenerator(
                template: new PublicNotifiedHandlerTemplate(),
                publisher: new PublicDelegateFilePublisher()
            );

            foreach (var callback in callbacks)
            {
                internalDelegateGenerator.Generate(callback);
                delegateGenerator.Generate(callback);
                handlerGenerator.Generate(callback);
                asyncHandlerGenerator.Generate(callback);
                notifiedHandlerGenerator.Generate(callback);
            }
        }
    }
}
