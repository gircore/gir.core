namespace GLib
{
    public partial record Source
    {
        public static void Remove(uint tag) => Functions.Native.SourceRemove(tag);
    }
}
