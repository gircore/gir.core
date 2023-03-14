using System;
using GirModel;

namespace GirLoader.PlatformSupport;

public partial class Alias : PlatformDependent
{
    private readonly GirModel.Alias? _linuxAlias;
    private readonly GirModel.Alias? _macosAlias;
    private readonly GirModel.Alias? _windowsAlias;
    private readonly GirModel.Alias _alias;

    public bool SupportsLinux => _linuxAlias is not null;
    public bool SupportsMacos => _macosAlias is not null;
    public bool SupportsWindows => _windowsAlias is not null;

    public Alias(GirModel.Alias? linuxAlias, GirModel.Alias? macosAlias, GirModel.Alias? windowsAlias)
    {
        _linuxAlias = linuxAlias;
        _macosAlias = macosAlias;
        _windowsAlias = windowsAlias;

        _alias = linuxAlias ?? macosAlias ?? windowsAlias ?? throw new Exception("Please supply at least one alias.");
    }

    public override string ToString()
    {
        return _alias.Name;
    }
}
