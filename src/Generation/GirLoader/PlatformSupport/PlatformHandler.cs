using System;

namespace GirLoader.PlatformSupport;

public class PlatformHandler
{
    public GirModel.Namespace? LinuxNamespace { get; }
    public GirModel.Namespace? MacosNamespace { get; }
    public GirModel.Namespace? WindowsNamespace { get; }

    public PlatformHandler(GirModel.Namespace? linuxNamespace, GirModel.Namespace? macosNamespace, GirModel.Namespace? windowsNamespace)
    {
        if (linuxNamespace is null && macosNamespace is null && windowsNamespace is null)
            throw new Exception("Please supply at least one namespace");

        LinuxNamespace = linuxNamespace;
        MacosNamespace = macosNamespace;
        WindowsNamespace = windowsNamespace;
    }
}
