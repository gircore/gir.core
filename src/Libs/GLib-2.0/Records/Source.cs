namespace GLib
{
    public partial class Source
    {
        public static void Remove(uint tag) => Native.Functions.SourceRemove(tag);
    }
}
