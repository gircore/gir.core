using System.Collections.Generic;

namespace GirLoader.PlatformSupport;

public static class BitfieldProvider
{
    public static IEnumerable<GirModel.Bitfield> GetBitfields(this PlatformHandler handler)
    {
        var dataStore = new PlatformDataStore<GirModel.Bitfield>();

        if (handler.LinuxNamespace is not null)
            foreach (var bitfield in handler.LinuxNamespace.Bitfields)
                dataStore.AddLinuxElement(bitfield.Name, bitfield);

        if (handler.MacosNamespace is not null)
            foreach (var bitfield in handler.MacosNamespace.Bitfields)
                dataStore.AddMacosElement(bitfield.Name, bitfield);

        if (handler.WindowsNamespace is not null)
            foreach (var bitfield in handler.WindowsNamespace.Bitfields)
                dataStore.AddWindowsElement(bitfield.Name, bitfield);

        return dataStore.Select(x => new Bitfield(
            linuxBitfield: x.Linux,
            macosBitfield: x.Macos,
            windowsBitfield: x.Windows
        ));
    }
}
