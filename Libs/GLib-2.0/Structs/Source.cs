namespace GLib
{
    public partial struct Source
    {
        public static void Remove(uint tag) => Functions.Native.SourceRemove(tag);
    }
}
