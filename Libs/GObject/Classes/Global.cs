namespace GObject
{
    public static partial class Global
    {
        public static Type GetParentType(Type t)
            => new Type(Native.type_parent(t.Value));
    }
}
