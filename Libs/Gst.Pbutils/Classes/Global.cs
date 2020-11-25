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

        // TODO: Rework with C# async/await
        // TODO: Rework. None of this works
        public static InstallPluginsReturn InstallPluginsAsync(string[] details, InstallPluginsContext? context,
            InstallPluginsResultFunc func)
        {
            throw new NotImplementedException("This Function is not implemented");
            
            // FIXME: We currently cannot support InstallPluginsContext because it is an opaque/disguised struct
            // that is for some reason being generated as a struct. Therefore, marshalling will have no effect
            // (because there are no fields) and the entire thing will simply blow up. Fix this ASAP.
            /*if (context != null)
                throw new NotImplementedException("Install Contexts are not currently supported!");
            
            // Marshal this structure
            // IntPtr contextPtr = Marshal.AllocHGlobal(Marshal.SizeOf(context));
            // Marshal.StructureToPtr(context!, contextPtr, false);

            // We have properties
            // Prepare Construct Properties
            var detailsArray = new IntPtr[details.Length];

            // Populate arrays
            for (var i = 0; i < details.Length; i++)
            {
                // TODO: Marshal in a block, rather than one at a time
                // for performance reasons. ADD SOME KIND OF FUNCTION
                // THAT DOES THIS RELIABLY FOR US!
                detailsArray[i] = Marshal.StringToHGlobalAnsi(details[i]);
            }

            // Native Call
            InstallPluginsReturn result = Native.install_plugins_async(detailsArray[0], /*contextPtr#1# IntPtr.Zero, func, IntPtr.Zero);
            
            // Free strings
            foreach (IntPtr ptr in detailsArray)
                Marshal.FreeHGlobal(ptr);

            return result;*/
        }
    }
}
