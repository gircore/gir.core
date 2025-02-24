using Generator.Model;

namespace Generator.Generator.Internal;

internal class CallbackAsyncHandler : Generator<GirModel.Callback>
{
    private readonly Publisher _publisher;

    public CallbackAsyncHandler(Publisher publisher)
    {
        _publisher = publisher;
    }

    public void Generate(GirModel.Callback callback)
    {
        var source = Renderer.Internal.CallbackAsyncHandler.Render(callback);
        var codeUnit = new CodeUnit(
            Project: Namespace.GetCanonicalName(callback.Namespace),
            Name: $"{callback.Name}.AsyncHandler",
            Source: source,
            IsInternal: true
        );

        _publisher.Publish(codeUnit);
    }
}
