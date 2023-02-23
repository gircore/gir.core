using System;

namespace WebKit2;

public partial class UserScript
{
    //TODO: Should be generated automatically for records
    public static UserScript New(string source, UserContentInjectedFrames injectedFrames, UserScriptInjectionTime injectionTime)
    {
        return new UserScript(Internal.UserScript.New(GLib.Internal.NonNullableUtf8StringOwnedHandle.Create(source), injectedFrames, injectionTime, IntPtr.Zero, IntPtr.Zero));
    }
}
