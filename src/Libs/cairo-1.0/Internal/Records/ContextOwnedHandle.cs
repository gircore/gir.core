namespace Cairo.Internal
{
    public partial class ContextOwnedHandle : ContextHandle
    {
        protected override partial bool ReleaseHandle()
        {
            Context.Destroy(handle);
            return true;
        }
    }
}

