using Generator.Model;

namespace Generator.Generator.Public;

internal class CallbackDelegate : Generator<GirModel.Callback>
{
    private readonly Publisher _publisher;

    public CallbackDelegate(Publisher publisher)
    {
        _publisher = publisher;
    }

    public void Generate(GirModel.Callback callback)
    {
        var source = Renderer.Public.CallbackDelegate.Render(callback);
        var codeUnit = new CodeUnit(
            Project: Namespace.GetCanonicalName(callback.Namespace),
            Name: callback.Name,
            Source: source,
            IsInternal: false
        );

        _publisher.Publish(codeUnit);
    }
}
