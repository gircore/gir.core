using System;
using GirModel;

namespace GirLoader.PlatformSupport;

public partial class Function : PlatformDependent
{
    private readonly GirModel.Function? _linuxFunction;
    private readonly GirModel.Function? _macosFunction;
    private readonly GirModel.Function? _windowsFunction;
    private readonly GirModel.Function _function;

    public bool SupportsLinux => _linuxFunction is not null;
    public bool SupportsMacos => _macosFunction is not null;
    public bool SupportsWindows => _windowsFunction is not null;

    public Function(GirModel.Function? linuxFunction, GirModel.Function? macosFunction, GirModel.Function? windowsFunction)
    {
        _linuxFunction = linuxFunction;
        _macosFunction = macosFunction;
        _windowsFunction = windowsFunction;

        _function = linuxFunction ?? macosFunction ?? windowsFunction ?? throw new Exception("Please supply at least one function.");
    }

    public override string ToString()
    {
        return _function.Name;
    }
}
