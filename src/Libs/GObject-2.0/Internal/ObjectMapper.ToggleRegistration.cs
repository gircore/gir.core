using System;
using System.Runtime.InteropServices;

namespace GObject.Internal;

public partial class ObjectMapper
{
    private static unsafe class ToggleRegistration
    {
        internal static void AddToggleRef(IntPtr handle)
        {
            AddToggleRef(handle, &ToggleNotify, IntPtr.Zero);
            Internal.Object.Unref(handle);
        }

        internal static void RemoveToggleRef(IntPtr handle)
        {
            var sourceFunc = new GLib.Internal.SourceFuncAsyncHandler(() =>
            {
                RemoveToggleRef(handle, &ToggleNotify, IntPtr.Zero);
                return false;
            });
            GLib.Internal.MainContext.Invoke(GLib.Internal.MainContextUnownedHandle.NullHandle, sourceFunc.NativeCallback, IntPtr.Zero);
        }

        [DllImport(ImportResolver.Library, EntryPoint = "g_object_add_toggle_ref")]
        private static extern void AddToggleRef(IntPtr @object, delegate* unmanaged<IntPtr, IntPtr, int, void> toggleNotify, IntPtr data);

        [DllImport(ImportResolver.Library, EntryPoint = "g_object_remove_toggle_ref")]
        private static extern void RemoveToggleRef(IntPtr @object, delegate* unmanaged<IntPtr, IntPtr, int, void> toggleNotify, IntPtr data);
    }
}
