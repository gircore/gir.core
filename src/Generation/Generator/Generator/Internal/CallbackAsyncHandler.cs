using Generator.Model;

namespace Generator.Generator.Internal;

internal class CallbackAsyncHandler(Publisher publisher) : Generator<GirModel.Callback>
{
    public void Generate(GirModel.Callback callback)
    {
        if (!Callback.IsEnabled(callback))
            return;

        var source = Renderer.Internal.CallbackAsyncHandler.RenderFile(callback);
        var codeUnit = new CodeUnit(
            Project: Namespace.GetCanonicalName(callback.Namespace),
            Name: $"{callback.Name}.AsyncHandler",
            Source: source,
            IsInternal: true
        );

        publisher.Publish(codeUnit);
    }
}
