using Generator.Model;
using Generator.Renderer;

namespace Generator.Generator.Public;

internal class CallbackForeverHandler : Generator<GirModel.Callback>
{
    private readonly Publisher _publisher;

    public CallbackForeverHandler(Publisher publisher)
    {
        _publisher = publisher;
    }

    public void Generate(GirModel.Callback callback)
    {
        var source = Renderer.Public.CallbackForeverHandler.Render(callback);
        var codeUnit = new CodeUnit(
            Project: Namespace.GetCanonicalName(callback.Namespace),
            Name: $"{callback.Name}.ForeverHandler",
            Source: source,
            IsInternal: false
        );

        _publisher.Publish(codeUnit);
    }
}
