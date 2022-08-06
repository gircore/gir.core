using Generator.Model;
using Generator.Renderer;

namespace Generator.Generator.Public;

internal class CallbackNotifiedHandler : Generator<GirModel.Callback>
{
    private readonly Publisher _publisher;

    public CallbackNotifiedHandler(Publisher publisher)
    {
        _publisher = publisher;
    }

    public void Generate(GirModel.Callback callback)
    {
        var source = Renderer.Public.CallbackNotifiedHandler.Render(callback);
        var codeUnit = new CodeUnit(
            Project: Namespace.GetCanonicalName(callback.Namespace),
            Name: $"{callback.Name}.NotifiedHandler",
            Source: source,
            IsInternal: false
        );

        _publisher.Publish(codeUnit);
    }
}
