using System;
using System.Runtime.InteropServices;

namespace Gdk
{
    public partial class Event
    {
        // TODO: Workaround as long as we need GDK3 or
        // do not create instances from safe handles
        [GObject.Construct]
        private static Event __FactoryNew(IntPtr ptr)
        {
            var ev = Marshal.PtrToStructure<Internal.EventAny.Struct>(ptr);
            switch (ev.Type)
            {
                case EventType.Expose:
                    return new EventExpose(ptr);
                case EventType.VisibilityNotify:
                    return new EventVisibility(ptr);
                case EventType.MotionNotify:
                    return new EventMotion(ptr);
                case EventType.ButtonPress:
                case EventType.TwoButtonPress:
                case EventType.ThreeButtonPress:
                case EventType.ButtonRelease:
                    return new EventButton(ptr);
                case EventType.TouchBegin:
                    return new EventTouch(ptr);
                case EventType.Scroll:
                    return new EventScroll(ptr);
                case EventType.KeyPress:
                case EventType.KeyRelease:
                    return new EventKey(ptr);
                case EventType.EnterNotify:
                case EventType.LeaveNotify:
                    return new EventCrossing(ptr);
                case EventType.FocusChange:
                    return new EventFocus(ptr);
                case EventType.Configure:
                    return new EventConfigure(ptr);
                case EventType.PropertyNotify:
                    return new EventProperty(ptr);
                case EventType.SelectionClear:
                case EventType.SelectionNotify:
                case EventType.SelectionRequest:
                    return new EventSelection(ptr);
                case EventType.OwnerChange:
                    return new EventOwnerChange(ptr);
                case EventType.ProximityIn:
                case EventType.ProximityOut:
                    return new EventProximity(ptr);
                case EventType.DragEnter:
                case EventType.DragLeave:
                case EventType.DragMotion:
                case EventType.DragStatus:
                case EventType.DropStart:
                case EventType.DropFinished:
                    return new EventDND(ptr);
                case EventType.WindowState:
                    return new EventWindowState(ptr);
                case EventType.Setting:
                    return new EventSetting(ptr);
                case EventType.GrabBroken:
                    return new EventGrabBroken(ptr);
                case EventType.TouchpadSwipe:
                    return new EventTouchpadSwipe(ptr);
                case EventType.TouchpadPinch:
                    return new EventTouchpadPinch(ptr);
                case EventType.PadButtonPress:
                case EventType.PadButtonRelease:
                    return new EventPadButton(ptr);
                case EventType.PadRing:
                case EventType.PadStrip:
                    return new EventPadAxis(ptr);
                case EventType.PadGroupMode:
                    return new EventPadGroupMode(ptr);
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
