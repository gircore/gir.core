namespace cairo.Internal
{
    public partial class ContextOwnedHandle : ContextHandle
    {
        protected override partial bool ReleaseHandle()
        {
            Context.Destroy(this);
            return true;
        }
    }
}

