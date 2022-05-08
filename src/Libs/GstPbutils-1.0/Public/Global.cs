using System;
using System.Runtime.InteropServices;
using GLib;

namespace GstPbutils
{
    public static partial class Global
    {
        public static bool IsMissingPluginMessage(Gst.Message msg)
        {
            throw new NotImplementedException();
            // return MarshalHelper.ToPtrAndFree(msg, (msgPtr) =>
            // {
            //     var result = Native.is_missing_plugin_message(msgPtr);
            //
            //     // Update this structure
            //     Marshal.PtrToStructure<Gst.Message>(msgPtr, msg);
            //
            //     return result;
            // });
        }

        public static string? MissingPluginMessageGetInstallerDetail(Gst.Message msg)
        {
            throw new NotImplementedException();
            // return MarshalHelper.ToPtrAndFree(msg, (msgPtr) =>
            // {
            //     IntPtr result = Native.missing_plugin_message_get_installer_detail(msgPtr);
            //
            //     // Update this structure
            //     Marshal.PtrToStructure<Gst.Message>(msgPtr, msg);
            //
            //     return StringHelper.ToAnsiStringAndFree(result);
            // });
        }

        public static string? MissingPluginMessageGetDescription(Gst.Message msg)
        {
            throw new NotImplementedException();
            // return MarshalHelper.ToPtrAndFree(msg, (msgPtr) =>
            // {
            //     IntPtr result = Native.missing_plugin_message_get_description(msgPtr);
            //
            //     // Update this structure
            //     Marshal.PtrToStructure<Gst.Message>(msgPtr, msg);
            //
            //     return StringHelper.ToAnsiStringAndFree(result);
            // });
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
