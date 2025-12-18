using System;
using System.Runtime.InteropServices;

namespace GObject.Internal;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct ObjectClassUnmanaged
{
    public nuint GType; //public GObject.Internal.TypeClassData GTypeClass;
    public IntPtr ConstructProperties;
    public delegate* unmanaged<nuint, uint, IntPtr, IntPtr> Constructor;
    public delegate* unmanaged<IntPtr, uint, IntPtr, IntPtr, void> SetProperty;
    public delegate* unmanaged<IntPtr, uint, IntPtr, IntPtr, void> GetProperty;
    public delegate* unmanaged<IntPtr, void> Dispose;
    public delegate* unmanaged<IntPtr, void> Finalize;
    public delegate* unmanaged<IntPtr, uint, IntPtr, void> DispatchPropertiesChanged;
    public delegate* unmanaged<IntPtr, IntPtr, void> Notify;
    public delegate* unmanaged<IntPtr, void> Constructed;
    public nuint Flags;
    public nuint NConstructProperties;
    public IntPtr Pspecs;
    public nuint NPspecs;

    public IntPtr Pdummy1;
    public IntPtr Pdummy2;
    public IntPtr Pdummy3;
}
