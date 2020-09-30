namespace GObject
{
    public partial class InitiallyUnowned
    {
        protected override void Initialize()
        {
            Object.ref_sink(Handle);
        }
    }
}