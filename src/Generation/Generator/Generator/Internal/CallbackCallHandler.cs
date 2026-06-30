using Generator.Model;

namespace Generator.Generator.Internal;

internal class CallbackCallHandler(Publisher publisher) : Generator<GirModel.Callback>
{
    public void Generate(GirModel.Callback callback)
    {
        if (!Callback.IsEnabled(callback))
            return;

        var source = Renderer.Internal.CallbackCallHandler.RenderFile(callback);
        var codeUnit = new CodeUnit(
            Project: Namespace.GetCanonicalName(callback.Namespace),
            Name: $"{callback.Name}.CallHandler",
            Source: source,
            IsInternal: true
        );

        publisher.Publish(codeUnit);
    }
}
