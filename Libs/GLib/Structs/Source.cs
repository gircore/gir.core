namespace GLib
{
    public partial struct Source
    {
        public static void Remove(uint tag) => Global.Native.source_remove(tag);
    }
}
