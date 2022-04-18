using Generator.Model;
using Generator.Renderer;

namespace Generator.Generator.Public;

internal class CallbackCallHandler : Generator<GirModel.Callback>
{
    private readonly Publisher _publisher;

    public CallbackCallHandler(Publisher publisher)
    {
        _publisher = publisher;
    }

    public void Generate(GirModel.Callback callback)
    {
        var source = Renderer.Public.CallbackCallHandler.Render(callback);
        var codeUnit = new CodeUnit(
            Project: Namespace.GetCanonicalName(callback.Namespace),
            Name: $"{callback.Name}.CallHandler",
            Source: source,
            IsInternal: false
        );

        _publisher.Publish(codeUnit);
    }
}
