namespace GObject
{
    public partial class InitiallyUnowned
    {
        protected override void Initialize()
        {
            Object.Native.ref_sink(Handle);
        }
    }
}