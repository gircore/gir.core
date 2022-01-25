using System;
using System.Runtime.InteropServices;
using Gdk.Internal;

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
                case EventType.VisibilityNotify:
                    return ownsHandle
                        ? new EventVisibility(new EventVisibilityOwnedHandle(ptr))
                        : new EventVisibility(new EventVisibilityUnownedHandle(ptr));
                case EventType.MotionNotify:
                    return ownsHandle
                        ? new EventMotion(new EventMotionOwnedHandle(ptr))
                        : new EventMotion(new EventMotionUnownedHandle(ptr));
                case EventType.ButtonPress:
                case EventType.TwoButtonPress:
                case EventType.ThreeButtonPress:
                case EventType.ButtonRelease:
                    return ownsHandle
                        ? new EventButton(new EventButtonOwnedHandle(ptr))
                        : new EventButton(new EventButtonUnownedHandle(ptr));
                case EventType.TouchBegin:
                    return ownsHandle
                        ? new EventTouch(new EventTouchOwnedHandle(ptr))
                        : new EventTouch(new EventTouchUnownedHandle(ptr));
                case EventType.Scroll:
                    return ownsHandle
                        ? new EventScroll(new EventScrollOwnedHandle(ptr))
                        : new EventScroll(new EventScrollUnownedHandle(ptr));
                case EventType.KeyPress:
                case EventType.KeyRelease:
                    return ownsHandle
                        ? new EventKey(new EventKeyOwnedHandle(ptr))
                        : new EventKey(new EventKeyUnownedHandle(ptr));
                case EventType.EnterNotify:
                case EventType.LeaveNotify:
                    return ownsHandle
                        ? new EventCrossing(new EventCrossingOwnedHandle(ptr))
                        : new EventCrossing(new EventCrossingUnownedHandle(ptr));
                case EventType.FocusChange:
                    return ownsHandle
                        ? new EventFocus(new EventFocusOwnedHandle(ptr))
                        : new EventFocus(new EventFocusUnownedHandle(ptr));
                case EventType.Configure:
                    return ownsHandle
                        ? new EventConfigure(new EventConfigureOwnedHandle(ptr))
                        : new EventConfigure(new EventConfigureUnownedHandle(ptr));
                case EventType.PropertyNotify:
                    return ownsHandle
                        ? new EventProperty(new EventPropertyOwnedHandle(ptr))
                        : new EventProperty(new EventPropertyUnownedHandle(ptr));
                case EventType.SelectionClear:
                case EventType.SelectionNotify:
                case EventType.SelectionRequest:
                    return ownsHandle
                        ? new EventSelection(new EventSelectionOwnedHandle(ptr))
                        : new EventSelection(new EventSelectionUnownedHandle(ptr));
                case EventType.OwnerChange:
                    return ownsHandle
                        ? new EventOwnerChange(new EventOwnerChangeOwnedHandle(ptr))
                        : new EventOwnerChange(new EventOwnerChangeUnownedHandle(ptr));
                case EventType.ProximityIn:
                case EventType.ProximityOut:
                    return ownsHandle
                        ? new EventProximity(new EventProximityOwnedHandle(ptr))
                        : new EventProximity(new EventProximityUnownedHandle(ptr));
                case EventType.DragEnter:
                case EventType.DragLeave:
                case EventType.DragMotion:
                case EventType.DragStatus:
                case EventType.DropStart:
                case EventType.DropFinished:
                    return ownsHandle
                        ? new EventDND(new EventDNDOwnedHandle(ptr))
                        : new EventDND(new EventDNDUnownedHandle(ptr));
                case EventType.WindowState:
                    return ownsHandle
                        ? new EventWindowState(new EventWindowStateOwnedHandle(ptr))
                        : new EventWindowState(new EventWindowStateUnownedHandle(ptr));
                case EventType.Setting:
                    return ownsHandle
                        ? new EventSetting(new EventSettingOwnedHandle(ptr))
                        : new EventSetting(new EventSettingUnownedHandle(ptr));
                case EventType.GrabBroken:
                    return ownsHandle
                        ? new EventGrabBroken(new EventGrabBrokenOwnedHandle(ptr))
                        : new EventGrabBroken(new EventGrabBrokenUnownedHandle(ptr));
                case EventType.TouchpadSwipe:
                    return ownsHandle
                        ? new EventTouchpadSwipe(new EventTouchpadSwipeOwnedHandle(ptr))
                        : new EventTouchpadSwipe(new EventTouchpadSwipeUnownedHandle(ptr));
                case EventType.TouchpadPinch:
                    return ownsHandle
                        ? new EventTouchpadPinch(new EventTouchpadPinchOwnedHandle(ptr))
                        : new EventTouchpadPinch(new EventTouchpadPinchUnownedHandle(ptr));
                case EventType.PadButtonPress:
                case EventType.PadButtonRelease:
                    return ownsHandle
                        ? new EventPadButton(new EventPadButtonOwnedHandle(ptr))
                        : new EventPadButton(new EventPadButtonUnownedHandle(ptr));
                case EventType.PadRing:
                case EventType.PadStrip:
                    return ownsHandle
                        ? new EventPadAxis(new EventPadAxisOwnedHandle(ptr))
                        : new EventPadAxis(new EventPadAxisUnownedHandle(ptr));
                case EventType.PadGroupMode:
                    return ownsHandle
                        ? new EventPadGroupMode(new EventPadGroupModeOwnedHandle(ptr))
                        : new EventPadGroupMode(new EventPadGroupModeUnownedHandle(ptr));
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
