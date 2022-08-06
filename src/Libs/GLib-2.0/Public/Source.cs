namespace GLib
{
    public partial class Source
    {
        public static void Remove(uint tag) => Internal.Functions.SourceRemove(tag);
    }
}
