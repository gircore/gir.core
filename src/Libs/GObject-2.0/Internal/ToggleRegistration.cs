using System;
using System.Runtime.InteropServices;

namespace GObject.Internal;

internal static unsafe class ToggleRegistration
{
    internal static void AddToggleRef(IntPtr handle, delegate* unmanaged<IntPtr, IntPtr, int, void> toggleNotify)
    {
        Console.WriteLine("Adding toggle ref for {0}", handle);
        AddToggleRef(handle, toggleNotify, IntPtr.Zero);
    }

    internal static void RemoveToggleRef(IntPtr handle, delegate* unmanaged<IntPtr, IntPtr, int, void> toggleNotify)
    {
        Console.WriteLine("Removing toggle ref for {0}", handle);
        RemoveToggleRef(handle, toggleNotify, IntPtr.Zero);
    }

    [DllImport(ImportResolver.Library, EntryPoint = "g_object_add_toggle_ref")]
    private static extern void AddToggleRef(IntPtr @object, delegate* unmanaged<IntPtr, IntPtr, int, void> toggleNotify, IntPtr data);

    [DllImport(ImportResolver.Library, EntryPoint = "g_object_remove_toggle_ref")]
    private static extern void RemoveToggleRef(IntPtr @object, delegate* unmanaged<IntPtr, IntPtr, int, void> toggleNotify, IntPtr data);
}
