using System;
using System.Collections.Generic;

namespace GirLoader.Output;

/// <summary>
/// This is an alias definition for "GType" in the GObject repository. As cairo does not include GObject
/// the alias "GObject.Type" is unknown which leads to missing GetGType methods for cairo. Adding
/// a typ named "GType" results in the alias being available as a global fallback which helps cairo. 
///
/// TODO: Only needed until https://gitlab.gnome.org/GNOME/gobject-introspection/-/merge_requests/396 is merged
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
