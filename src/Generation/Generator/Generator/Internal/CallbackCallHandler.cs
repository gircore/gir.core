using Generator.Model;

namespace Generator.Generator.Internal;

internal class CallbackCallHandler : Generator<GirModel.Callback>
{
    private readonly Publisher _publisher;

    public CallbackCallHandler(Publisher publisher)
    {
        _publisher = publisher;
    }

    public void Generate(GirModel.Callback callback)
    {
        var source = Renderer.Internal.CallbackCallHandler.Render(callback);
        var codeUnit = new CodeUnit(
            Project: Namespace.GetCanonicalName(callback.Namespace),
            Name: $"{callback.Name}.CallHandler",
            Source: source,
            IsInternal: true
        );

        _publisher.Publish(codeUnit);
    }
}
