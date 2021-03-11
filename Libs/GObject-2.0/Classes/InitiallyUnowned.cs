namespace GObject
{
    public partial class InitiallyUnowned
    {
        protected override void Initialize()
        {
            ObjectInstance.Methods.RefSink(Handle);
        }
    }
}
