namespace GObject
{
    public partial class InitiallyUnowned
    {
        protected override void Initialize()
        {
            // TODO Verify if this call is needed. It should not be needed as the object class
            // always requires the information if the ref is owned by us or not.
            // In case of "g_object_new_with_properties" it is checked if the created
            // ref is floating. If so it is assumed that the reference is not owned by us
            // which will result in a "ref_sink".
            //
            //Native.Object.Instance.Methods.RefSink(Handle);
        }
    }
}
