using System.Collections.Generic;

namespace GirLoader.Output
{
    public partial class Namespace : GirModel.Namespace
    {
        string GirModel.Namespace.Name => Name;
        string GirModel.Namespace.Version => Version;
        string? GirModel.Namespace.SharedLibrary => SharedLibrary;

        IEnumerable<GirModel.Enumeration> GirModel.Namespace.Enumerations => Enumerations;
        IEnumerable<GirModel.Bitfield> GirModel.Namespace.Bitfields => Bitfields;
        IEnumerable<GirModel.Record> GirModel.Namespace.Records => Records;
        IEnumerable<GirModel.Union> GirModel.Namespace.Unions => Unions;
        IEnumerable<GirModel.Callback> GirModel.Namespace.Callbacks => Callbacks;
        IEnumerable<GirModel.Function> GirModel.Namespace.Functions => Functions;
        IEnumerable<GirModel.Constant> GirModel.Namespace.Constants => Constants;
        IEnumerable<GirModel.Interface> GirModel.Namespace.Interfaces => Interfaces;
        IEnumerable<GirModel.Class> GirModel.Namespace.Classes => Classes;
    }
}
