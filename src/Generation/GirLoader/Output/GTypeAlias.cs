using System;
using System.Collections.Generic;

namespace GirLoader.Output;

/// <summary>
/// This is an alias definition for "GType" in GLib. The GLib package is dependent on this
/// type but the type is only defined in GObject. Thus the alisas is added manually here and
/// the GObject implementation get's disabled in a Generator fixer.
/// </summary>
public class GTypeAlias : Type, GirModel.Alias
{
    public GirModel.Namespace Namespace { get; } = new GObjectNamespace();
    public string Name => "Type";
    public GirModel.Type Type { get; } = new NativeUnsignedInteger("gsize");

    internal GTypeAlias() : base("GType")
    {
    }

    internal override bool Matches(TypeReference typeReference)
    {
        if (typeReference.CTypeReference?.CType is not null)
            return typeReference.CTypeReference.CType == CType;

        return false;
    }

    private class GObjectNamespace : GirModel.Namespace
    {
        public string Name => "GObject";
        public string Version => "2.0";
        public string? SharedLibrary { get; }
        public IEnumerable<GirModel.Alias> Aliases => throw new Exception();
        public IEnumerable<GirModel.Enumeration> Enumerations => throw new Exception();
        public IEnumerable<GirModel.Bitfield> Bitfields => throw new Exception();
        public IEnumerable<GirModel.Record> Records => throw new Exception();
        public IEnumerable<GirModel.Union> Unions => throw new Exception();
        public IEnumerable<GirModel.Callback> Callbacks => throw new Exception();
        public IEnumerable<GirModel.Function> Functions => throw new Exception();
        public IEnumerable<GirModel.Constant> Constants => throw new Exception();
        public IEnumerable<GirModel.Interface> Interfaces => throw new Exception();
        public IEnumerable<GirModel.Class> Classes => throw new Exception();
    }
}
