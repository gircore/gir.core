using Generator.Model;

namespace Generator.Generator.Internal;

internal class CallbackNotifiedHandler : Generator<GirModel.Callback>
{
    private readonly Publisher _publisher;

    public CallbackNotifiedHandler(Publisher publisher)
    {
        _publisher = publisher;
    }

    public void Generate(GirModel.Callback callback)
    {
        var source = Renderer.Internal.CallbackNotifiedHandler.Render(callback);
        var codeUnit = new CodeUnit(
            Project: Namespace.GetCanonicalName(callback.Namespace),
            Name: $"{callback.Name}.NotifiedHandler",
            Source: source,
            IsInternal: true
        );

        _publisher.Publish(codeUnit);
    }
}
