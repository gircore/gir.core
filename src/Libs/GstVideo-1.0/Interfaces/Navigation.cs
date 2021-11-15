using System;
using System.Runtime.InteropServices;
using GLib;
using Gst;
using Object = GObject.Object;

namespace GstVideo
{
    public partial interface Navigation
    {
        public void SendMouseEvent(string @event, int button, double x, double y)
        {
            throw new NotImplementedException();
            // // TODO: Can we do this?
            // Object obj = (Object) this;
            // Native.Navigation.Instance.Methods.SendMouseEvent(obj.Handle, @event, button, x, y);
        }

        public void SendCommand(NavigationCommand command)
        {
            throw new NotImplementedException();
            // Object obj = (Object) this;
            // Native.Navigation.Instance.Methods.SendCommand(obj.Handle, command);
        }

        public static NavigationMessageType MessageGetType(Message message)
        {
            throw new NotImplementedException();
            // return MarshalHelper.ToPtrAndFree(message, (messagePtr) =>
            // {
            //     var result = Native.Functions.NavigationMessageGetType(messagePtr);
            //
            //     // Update message structure
            //     // TODO: Not necessary?
            //     Marshal.PtrToStructure<Message>(messagePtr, message);
            //
            //     return result;
            // });
        }
    }
}
