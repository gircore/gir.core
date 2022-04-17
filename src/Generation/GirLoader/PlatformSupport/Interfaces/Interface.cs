using System;
using GirModel;

namespace GirLoader.PlatformSupport;

public partial class Interface : PlatformDependent
{
    private readonly GirModel.Interface? _linuxInterface;
    private readonly GirModel.Interface? _macosInterface;
    private readonly GirModel.Interface? _windowsInterface;
    private readonly GirModel.Interface _interface;

    public bool SupportsLinux => _linuxInterface is not null;
    public bool SupportsMacos => _macosInterface is not null;
    public bool SupportsWindows => _windowsInterface is not null;

    public Interface(GirModel.Interface? linuxInterface, GirModel.Interface? macosInterface, GirModel.Interface? windowsInterface)
    {
        _linuxInterface = linuxInterface;
        _macosInterface = macosInterface;
        _windowsInterface = windowsInterface;

        _interface = linuxInterface ?? macosInterface ?? windowsInterface ?? throw new Exception("Please supply at least one interface.");
    }

    public override string ToString()
    {
        return _interface.Name;
    }
}
