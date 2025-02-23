using Generator.Model;

namespace Generator.Generator.Internal;

internal class ClassHandle : Generator<GirModel.Class>
{
    private readonly Publisher _publisher;

    public ClassHandle(Publisher publisher)
    {
        _publisher = publisher;
    }

    public void Generate(GirModel.Class obj)
    {
        if (obj.Fundamental)
            return;

        if (obj.Parent is null)
            return; //Do not generate a handle for GObject.Object itself

        var source = Renderer.Internal.ClassHandle.Render(obj);
        var codeUnit = new CodeUnit(
            Project: Namespace.GetCanonicalName(obj.Namespace),
            Name: Class.GetInternalHandleName(obj),
            Source: source,
            IsInternal: true
        );

        _publisher.Publish(codeUnit);
    }
}
