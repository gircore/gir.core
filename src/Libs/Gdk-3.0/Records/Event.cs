using System;
using System.Runtime.InteropServices;

namespace Gdk
{
    public partial class Event
    {
        // TODO: Workaround as long as we need GDK3 or
        // do not create instances from safe handles
        // see usage of Attribute GObject.Construct
        [GObject.Construct]
        public static Event FromPointer(IntPtr ptr, bool ownsHandle)
        {
            var ev = Marshal.PtrToStructure<Internal.EventAnyData>(ptr);
            switch (ev.Type)
            {
                case EventType.Expose:
                    return new EventExpose(ptr, ownsHandle);
                case EventType.VisibilityNotify:
                    return new EventVisibility(ptr, ownsHandle);
                case EventType.MotionNotify:
                    return new EventMotion(ptr, ownsHandle);
                case EventType.ButtonPress:
                case EventType.TwoButtonPress:
                case EventType.ThreeButtonPress:
                case EventType.ButtonRelease:
                    return new EventButton(ptr, ownsHandle);
                case EventType.TouchBegin:
                    return new EventTouch(ptr, ownsHandle);
                case EventType.Scroll:
                    return new EventScroll(ptr, ownsHandle);
                case EventType.KeyPress:
                case EventType.KeyRelease:
                    return new EventKey(ptr, ownsHandle);
                case EventType.EnterNotify:
                case EventType.LeaveNotify:
                    return new EventCrossing(ptr, ownsHandle);
                case EventType.FocusChange:
                    return new EventFocus(ptr, ownsHandle);
                case EventType.Configure:
                    return new EventConfigure(ptr, ownsHandle);
                case EventType.PropertyNotify:
                    return new EventProperty(ptr, ownsHandle);
                case EventType.SelectionClear:
                case EventType.SelectionNotify:
                case EventType.SelectionRequest:
                    return new EventSelection(ptr, ownsHandle);
                case EventType.OwnerChange:
                    return new EventOwnerChange(ptr, ownsHandle);
                case EventType.ProximityIn:
                case EventType.ProximityOut:
                    return new EventProximity(ptr, ownsHandle);
                case EventType.DragEnter:
                case EventType.DragLeave:
                case EventType.DragMotion:
                case EventType.DragStatus:
                case EventType.DropStart:
                case EventType.DropFinished:
                    return new EventDND(ptr, ownsHandle);
                case EventType.WindowState:
                    return new EventWindowState(ptr, ownsHandle);
                case EventType.Setting:
                    return new EventSetting(ptr, ownsHandle);
                case EventType.GrabBroken:
                    return new EventGrabBroken(ptr, ownsHandle);
                case EventType.TouchpadSwipe:
                    return new EventTouchpadSwipe(ptr, ownsHandle);
                case EventType.TouchpadPinch:
                    return new EventTouchpadPinch(ptr, ownsHandle);
                case EventType.PadButtonPress:
                case EventType.PadButtonRelease:
                    return new EventPadButton(ptr, ownsHandle);
                case EventType.PadRing:
                case EventType.PadStrip:
                    return new EventPadAxis(ptr, ownsHandle);
                case EventType.PadGroupMode:
                    return new EventPadGroupMode(ptr, ownsHandle);
                // Default/Not Implemented
                case EventType.Map:
                case EventType.Unmap:
                case EventType.Delete:
                case EventType.Destroy:
                default:
                    throw new NotImplementedException("Event Type not yet supported");
            }
        }
    }
}
