namespace Cairo
{
    public partial class Context
    {
        public void Translate(double tx, double ty)
            => Internal.Context.Translate(Handle, tx, ty);

        public void Scale(double sx, double sy)
            => Internal.Context.Scale(Handle, sx, sy);

        public void Rotate(double angle)
            => Internal.Context.Rotate(Handle, angle);

        public void Transform(Matrix matrix)
            => Internal.Context.Transform(Handle, matrix.Handle);

        public void SetMatrix(Matrix matrix)
            => Internal.Context.SetMatrix(Handle, matrix.Handle);

        public void GetMatrix(Matrix matrix)
            => Internal.Context.GetMatrix(Handle, matrix.Handle);

        public void IdentityMatrix()
            => Internal.Context.IdentityMatrix(Handle);

        public void UserToDevice(ref double x, ref double y)
            => Internal.Context.UserToDevice(Handle, ref x, ref y);

        public void UserToDeviceDistance(ref double dx, ref double dy)
            => Internal.Context.UserToDeviceDistance(Handle, ref dx, ref dy);

        public void DeviceToUser(ref double x, ref double y)
            => Internal.Context.DeviceToUser(Handle, ref x, ref y);

        public void DeviceToUserDistance(ref double dx, ref double dy)
            => Internal.Context.DeviceToUserDistance(Handle, ref dx, ref dy);
    }
}
