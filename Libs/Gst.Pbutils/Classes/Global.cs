using System;
using System.Runtime.InteropServices;

namespace GstPbutils
{
    public static partial class Global
    {
        public static bool IsMissingPluginMessage(Gst.Message msg)
        {
            // Marshal this structure
            IntPtr msgPtr = Marshal.AllocHGlobal(Marshal.SizeOf(msg));
            Marshal.StructureToPtr(msg, msgPtr, false);

            // Native Call
            var result = Native.is_missing_plugin_message(msgPtr);

            // Update this structure
            Marshal.PtrToStructure(msgPtr, msg);

            return result;
        }

        public static string? MissingPluginMessageGetInstallerDetail(Gst.Message msg)
        {
            // Marshal this structure
            IntPtr msgPtr = Marshal.AllocHGlobal(Marshal.SizeOf(msg));
            Marshal.StructureToPtr(msg, msgPtr, false);

            // Native Call
            IntPtr result = Native.missing_plugin_message_get_installer_detail(msgPtr);

            // Update this structure
            Marshal.PtrToStructure(msgPtr, msg);

            return Marshal.PtrToStringAnsi(result);
        }

        public static string? MissingPluginMessageGetDescription(Gst.Message msg)
        {
            // Marshal this structure
            IntPtr msgPtr = Marshal.AllocHGlobal(Marshal.SizeOf(msg));
            Marshal.StructureToPtr(msg, msgPtr, false);

            // Native Call
            IntPtr result = Native.missing_plugin_message_get_description(msgPtr);

            // Update this structure
            Marshal.PtrToStructure(msgPtr, msg);

            return Marshal.PtrToStringAnsi(result);
        }

        public static InstallPluginsReturn InstallPluginsAsync(string[] details, InstallPluginsContext? context,
            InstallPluginsResultFunc func)
        {
            // FIXME: We currently cannot support InstallPluginsContext because it is an opaque/disguised struct
            // that is for some reason being generated as a struct. Therefore, marshalling will have no effect
            // (because there are no fields) and the entire thing will simply blow up. Fix this ASAP.
            throw new NotImplementedException("This Function is not implemented");

            // We will want to also find some way of dealing with async/await for Asynchronous GLib methods.
        }
    }
}
