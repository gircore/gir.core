using Generator3.Converter;

namespace Generator3.Model.Internal
{
    public class StringParameter : Parameter
    {
        public override string NullableTypeName => Model.AnyType.AsT0.GetName() + GetDefaultNullable();

        public override string Attribute => Model.AnyType.AsT0 switch
        {
            // Marshal as a UTF-8 encoded string
            GirModel.Utf8String => MarshalAs.UnmanagedLpUtf8String(),

            // Marshal as a null-terminated array of ANSI characters
            // TODO: This is likely incorrect:
            //  - GObject introspection specifies that Windows should use
            //    UTF-8 and Unix should use ANSI. Does using ANSI for
            //    everything cause problems here?
            GirModel.PlatformString => MarshalAs.UnmanagedLpString(),

            _ => ""
        };

        public override string Direction => Model.GetDirection(
            @in: ParameterDirection.In,
            @out: ParameterDirection.Out,
            @outCallerAllocates: ParameterDirection.Ref,
            @inout: ParameterDirection.Ref
        );

        protected internal StringParameter(GirModel.Parameter parameter) : base(parameter)
        {
            parameter.AnyType.VerifyType<GirModel.String>();
        }
    }
}
