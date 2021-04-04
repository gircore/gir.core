namespace GObject
{
    public partial class InitiallyUnowned
    {
        protected override void Initialize()
        {
             Native.Object.Instance.Methods.RefSink(Handle);
        }
    }
}
