using System;
using GirModel;

namespace GirLoader.PlatformSupport;

public partial class Constant : PlatformDependent
{
    private readonly GirModel.Constant? _linuxConstant;
    private readonly GirModel.Constant? _macosConstant;
    private readonly GirModel.Constant? _windowsConstant;
    private readonly GirModel.Constant _constant;

    public bool SupportsLinux => _linuxConstant is not null;
    public bool SupportsMacos => _macosConstant is not null;
    public bool SupportsWindows => _windowsConstant is not null;

    public Constant(GirModel.Constant? linuxConstant, GirModel.Constant? macosConstant, GirModel.Constant? windowsConstant)
    {
        _linuxConstant = linuxConstant;
        _macosConstant = macosConstant;
        _windowsConstant = windowsConstant;

        _constant = linuxConstant ?? macosConstant ?? windowsConstant ?? throw new Exception("Please supply at least one constant.");
    }

    public override string ToString()
    {
        return _constant.Name;
    }
}
