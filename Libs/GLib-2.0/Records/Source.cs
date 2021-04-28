namespace GLib
{
    public partial record Source
    {
        public static void Remove(uint tag) => Native.Functions.SourceRemove(tag);
    }
}
