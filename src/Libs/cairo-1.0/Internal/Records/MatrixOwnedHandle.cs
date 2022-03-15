namespace cairo.Internal
{
    public partial class MatrixOwnedHandle : MatrixHandle
    {
        protected override partial bool ReleaseHandle()
        {
            // There isn't any cleanup method for Cairo.Matrix. These are always
            // allocated and owned by the caller (i.e. MatrixManagedHandle)
            throw new System.Exception("Can't free native handle of type \"cairo.Internal.MatrixOwnedHandle\".");
        }
    }
}

