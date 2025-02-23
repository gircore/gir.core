using System;
using GirModel;

namespace GirLoader.PlatformSupport;

public partial class Union : PlatformDependent
{
    private readonly GirModel.Union? _linuxUnion;
    private readonly GirModel.Union? _macosUnion;
    private readonly GirModel.Union? _windowsUnion;
    private readonly GirModel.Union _union;

    public bool SupportsLinux => _linuxUnion is not null;
    public bool SupportsMacos => _macosUnion is not null;
    public bool SupportsWindows => _windowsUnion is not null;

    public Union(GirModel.Union? linuxUnion, GirModel.Union? macosUnion, GirModel.Union? windowsUnion)
    {
        _linuxUnion = linuxUnion;
        _macosUnion = macosUnion;
        _windowsUnion = windowsUnion;

        _union = linuxUnion ?? macosUnion ?? windowsUnion ?? throw new Exception("Please supply at least one union.");
    }

    public override string ToString()
    {
        return _union.Name;
    }
}
