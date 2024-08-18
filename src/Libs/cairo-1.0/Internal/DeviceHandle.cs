namespace Cairo.Internal;

public partial class DeviceOwnedHandle
{
    protected override bool ReleaseHandle()
    {
        Device.Destroy(handle);
        return true;
    }
}
