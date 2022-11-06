using System;
using System.Collections.Generic;
using System.Linq;

namespace GirLoader.PlatformSupport;

public class Namespace : GirModel.Namespace
{
    private readonly PlatformHandler _handler;

    public string Name { get; }
    public string Version { get; }
    public string? SharedLibrary => null; //Null because differnt platforms have different shared libraries

    public IEnumerable<GirModel.Enumeration> Enumerations { get; }
    public IEnumerable<GirModel.Bitfield> Bitfields { get; }
    public IEnumerable<GirModel.Record> Records { get; }
    public IEnumerable<GirModel.Union> Unions { get; }
    public IEnumerable<GirModel.Callback> Callbacks { get; }
    public IEnumerable<GirModel.Function> Functions { get; }
    public IEnumerable<GirModel.Constant> Constants { get; }
    public IEnumerable<GirModel.Interface> Interfaces { get; }
    public IEnumerable<GirModel.Class> Classes { get; }

    public Namespace(PlatformHandler handler)
    {
        _handler = handler;

        Name = GetVerified(ns => ns.Name);
        Version = GetVerified(ns => ns.Version);

        Enumerations = handler.GetEnumerations().ToList();
        Bitfields = handler.GetBitfields().ToList();
        Records = handler.GetRecords().ToList();
        Unions = handler.GetUnions().ToList();
        Callbacks = handler.GetCallbacks().ToList();
        Functions = handler.GetFunctions().ToList();
        Constants = handler.GetConstants().ToList();
        Interfaces = handler.GetInterfaces().ToList();
        Classes = handler.GetClasses().ToList();
    }

    private string GetVerified(Func<GirModel.Namespace, string> func)
    {
        var data = new HashSet<string>();

        if (_handler.LinuxNamespace is not null)
            data.Add(func(_handler.LinuxNamespace));

        if (_handler.MacosNamespace is not null)
            data.Add(func(_handler.MacosNamespace));

        if (_handler.WindowsNamespace is not null)
            data.Add(func(_handler.WindowsNamespace));

        return data.Count switch
        {
            0 => throw new Exception("No data"),
            1 => data.First(),
            _ => throw new Exception("Ambigious data")
        };
    }

    public PlatformHandler GetPlatformHandler() => _handler;

    public override string ToString()
    {
        return Name;
    }
}
