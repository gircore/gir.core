namespace Generator3.Generation.Model.Native
{
    public class StringParameter : Parameter
    {
        public override string NullableTypeName => Model switch
        {
            // Marshal as a UTF-8 encoded string
            { Type: GirModel.Utf8String } => GetNullableTypeName("[MarshalAs(UnmanagedType.LPUTF8Str)] "),

            // Marshal as a null-terminated array of ANSI characters
            // TODO: This is likely incorrect:
            //  - GObject introspection specifies that Windows should use
            //    UTF-8 and Unix should use ANSI. Does using ANSI for
            //    everything cause problems here?
            { Type: GirModel.PlatformString } => GetNullableTypeName("[MarshalAs(UnmanagedType.LPStr)] "),

            _ => GetNullableTypeName()
        };
        public override string Direction => Model switch
        {
            { Direction: GirModel.Direction.InOut } => ParameterDirection.Ref,
            { Direction: GirModel.Direction.Out, CallerAllocates: true } => ParameterDirection.Ref,
            { Direction: GirModel.Direction.Out } => ParameterDirection.Out,
            _ => ParameterDirection.In
        };

        protected internal StringParameter(GirModel.Parameter parameter) : base(parameter) { }

        private string GetNullableTypeName(string attribute = "") => attribute + Model.Type.GetName() + GetDefaultNullable();
    }
}
