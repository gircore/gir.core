namespace GObject
{
    public partial class ParamSpecBoolean
    {
        //TODO: This is a sample implementation on how to create a boolean ParamSpec
        public ParamSpecBoolean(string name, string nick, string blurb, bool defaultValue, ParamFlags flags)
            : this(Internal.Functions.ParamSpecBoolean(name, nick, blurb, defaultValue, flags))
        {
        }
    }
}
