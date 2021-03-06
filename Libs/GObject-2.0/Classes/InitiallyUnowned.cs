namespace GObject
{
    public partial class InitiallyUnowned
    {
        protected override void Initialize()
        {
            Object.Native.Methods.RefSink(Handle);
        }
    }
}
