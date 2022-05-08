﻿using Generator.Model;
using Generator.Renderer;

namespace Generator.Generator.Internal;

internal class ClassStruct : Generator<GirModel.Class>
{
    private readonly Publisher _publisher;

    public ClassStruct(Publisher publisher)
    {
        _publisher = publisher;
    }

    public void Generate(GirModel.Class obj)
    {
        if (obj.IsFundamental)
            return;

        var source = Renderer.Internal.ClassStruct.Render(obj);
        var codeUnit = new CodeUnit(
            Project: Namespace.GetCanonicalName(obj.Namespace),
            Name: Class.GetInternalStructName(obj),
            Source: source,
            IsInternal: true
        );

        _publisher.Publish(codeUnit);
    }
}
