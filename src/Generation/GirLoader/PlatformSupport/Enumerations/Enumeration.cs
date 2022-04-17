using System;
using GirModel;

namespace GirLoader.PlatformSupport;

public partial class Enumeration : PlatformDependent
{
    private readonly GirModel.Enumeration? _linuxEnumeration;
    private readonly GirModel.Enumeration? _macosEnumeration;
    private readonly GirModel.Enumeration? _windowEnumeration;
    private readonly GirModel.Enumeration _enumeration;

    public bool SupportsLinux => _linuxEnumeration is not null;
    public bool SupportsMacos => _macosEnumeration is not null;
    public bool SupportsWindows => _windowEnumeration is not null;

    public Enumeration(GirModel.Enumeration? linuxEnumeration, GirModel.Enumeration? macosEnumeration, GirModel.Enumeration? windowsEnumeration)
    {
        _linuxEnumeration = linuxEnumeration;
        _macosEnumeration = macosEnumeration;
        _windowEnumeration = windowsEnumeration;

        _enumeration = linuxEnumeration ?? macosEnumeration ?? windowsEnumeration ?? throw new Exception("Please supply at least one enumeration.");
    }

    public override string ToString()
    {
        return _enumeration.Name;
    }
}
