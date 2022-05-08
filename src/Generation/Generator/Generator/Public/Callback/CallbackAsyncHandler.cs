using Generator.Model;
using Generator.Renderer;

namespace Generator.Generator.Public;

internal class CallbackAsyncHandler : Generator<GirModel.Callback>
{
    private readonly Publisher _publisher;

    public CallbackAsyncHandler(Publisher publisher)
    {
        _publisher = publisher;
    }

    public void Generate(GirModel.Callback callback)
    {
        var source = Renderer.Public.CallbackAsyncHandler.Render(callback);
        var codeUnit = new CodeUnit(
            Project: Namespace.GetCanonicalName(callback.Namespace),
            Name: $"{callback.Name}.AsyncHandler",
            Source: source,
            IsInternal: false
        );

        _publisher.Publish(codeUnit);
    }
}
