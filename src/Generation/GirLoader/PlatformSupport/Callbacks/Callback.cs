using System;
using GirModel;

namespace GirLoader.PlatformSupport;

public partial class Callback : PlatformDependent
{
    private readonly GirModel.Callback? _linuxCallback;
    private readonly GirModel.Callback? _macosCallback;
    private readonly GirModel.Callback? _windowsCallback;
    private readonly GirModel.Callback _callback;

    public bool SupportsLinux => _linuxCallback is not null;
    public bool SupportsMacos => _macosCallback is not null;
    public bool SupportsWindows => _windowsCallback is not null;

    public Callback(GirModel.Callback? linuxCallback, GirModel.Callback? macosCallback, GirModel.Callback? windowsCallback)
    {
        _linuxCallback = linuxCallback;
        _macosCallback = macosCallback;
        _windowsCallback = windowsCallback;

        _callback = linuxCallback ?? macosCallback ?? windowsCallback ?? throw new Exception("Please supply at least one callback.");
    }

    public override string ToString()
    {
        return _callback.Name;
    }
}
