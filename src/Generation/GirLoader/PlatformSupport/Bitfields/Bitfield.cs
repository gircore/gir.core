using System;
using GirModel;

namespace GirLoader.PlatformSupport;

public partial class Bitfield : PlatformDependent
{
    private readonly GirModel.Bitfield? _linuxBitfield;
    private readonly GirModel.Bitfield? _macosBitfield;
    private readonly GirModel.Bitfield? _windowsBitfield;
    private readonly GirModel.Bitfield _bitfield;

    public bool SupportsLinux => _linuxBitfield is not null;
    public bool SupportsMacos => _macosBitfield is not null;
    public bool SupportsWindows => _windowsBitfield is not null;

    public Bitfield(GirModel.Bitfield? linuxBitfield, GirModel.Bitfield? macosBitfield, GirModel.Bitfield? windowsBitfield)
    {
        _linuxBitfield = linuxBitfield;
        _macosBitfield = macosBitfield;
        _windowsBitfield = windowsBitfield;

        _bitfield = linuxBitfield ?? macosBitfield ?? windowsBitfield ?? throw new Exception("Please supply at least one bitfield.");
    }

    public override string ToString()
    {
        return _bitfield.Name;
    }
}
