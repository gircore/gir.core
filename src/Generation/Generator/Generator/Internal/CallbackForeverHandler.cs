using Generator.Model;

namespace Generator.Generator.Internal;

internal class CallbackForeverHandler : Generator<GirModel.Callback>
{
    private readonly Publisher _publisher;

    public CallbackForeverHandler(Publisher publisher)
    {
        _publisher = publisher;
    }

    public void Generate(GirModel.Callback callback)
    {
        var source = Renderer.Internal.CallbackForeverHandler.Render(callback);
        var codeUnit = new CodeUnit(
            Project: Namespace.GetCanonicalName(callback.Namespace),
            Name: $"{callback.Name}.ForeverHandler",
            Source: source,
            IsInternal: true
        );

        _publisher.Publish(codeUnit);
    }
}
