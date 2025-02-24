using System;
using GirModel;

namespace GirLoader.PlatformSupport;

public partial class Class : PlatformDependent
{
    private readonly GirModel.Class? _linuxClass;
    private readonly GirModel.Class? _macosClass;
    private readonly GirModel.Class? _windowsClass;
    private readonly GirModel.Class _class;

    public bool SupportsLinux => _linuxClass is not null;
    public bool SupportsMacos => _macosClass is not null;
    public bool SupportsWindows => _windowsClass is not null;

    public Class(GirModel.Class? linuxClass, GirModel.Class? macosClass, GirModel.Class? windowsClass)
    {
        _linuxClass = linuxClass;
        _macosClass = macosClass;
        _windowsClass = windowsClass;

        _class = linuxClass ?? macosClass ?? windowsClass ?? throw new Exception("Please supply at least one class.");
    }

    public override string ToString()
    {
        return _class.Name;
    }
}
