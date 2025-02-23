namespace GObject;

public partial class ParamSpecBoolean
{
    //TODO: This is a sample implementation on how to create a boolean ParamSpec
    public ParamSpecBoolean(string name, string nick, string blurb, bool defaultValue, ParamFlags flags)
        : this(GObject.Internal.Functions.ParamSpecBoolean(
                GLib.Internal.NonNullableUtf8StringOwnedHandle.Create(name),
                GLib.Internal.NullableUtf8StringOwnedHandle.Create(nick),
                GLib.Internal.NullableUtf8StringOwnedHandle.Create(blurb),
                defaultValue, flags))
    {
    }
}
