using System;
using GirModel;

namespace GirLoader.PlatformSupport;

public partial class Record : PlatformDependent
{
    private readonly GirModel.Record? _linuxRecord;
    private readonly GirModel.Record? _macosRecord;
    private readonly GirModel.Record? _windowsRecord;
    private readonly GirModel.Record _record;

    public bool SupportsLinux => _linuxRecord is not null;
    public bool SupportsMacos => _macosRecord is not null;
    public bool SupportsWindows => _windowsRecord is not null;

    public Record(GirModel.Record? linuxRecord, GirModel.Record? macosRecord, GirModel.Record? windowsRecord)
    {
        _linuxRecord = linuxRecord;
        _macosRecord = macosRecord;
        _windowsRecord = windowsRecord;

        _record = linuxRecord ?? macosRecord ?? windowsRecord ?? throw new Exception("Please supply at least one record.");
    }

    public override string ToString()
    {
        return _record.Name;
    }
}
