using System;

namespace Generator3.Model.Native
{
    public class StringParameter : Parameter
    {
        public override string NullableTypeName => Model.AnyType.Match(
            type => type switch
            {
                // Marshal as a UTF-8 encoded string
                GirModel.Utf8String => GetNullableTypeName(type, "[MarshalAs(UnmanagedType.LPUTF8Str)] "),
                
                // Marshal as a null-terminated array of ANSI characters
                // TODO: This is likely incorrect:
                //  - GObject introspection specifies that Windows should use
                //    UTF-8 and Unix should use ANSI. Does using ANSI for
                //    everything cause problems here?
                GirModel.PlatformString => GetNullableTypeName(type, "[MarshalAs(UnmanagedType.LPStr)] "),
                
                _ => GetNullableTypeName(type)
            },
            _ => throw new Exception($"{nameof(StringParameter)} is not supporting arrays.")
        );

        public override string Direction => Model switch
        {
            { Direction: GirModel.Direction.InOut } => ParameterDirection.Ref,
            { Direction: GirModel.Direction.Out, CallerAllocates: true } => ParameterDirection.Ref,
            { Direction: GirModel.Direction.Out } => ParameterDirection.Out,
            _ => ParameterDirection.In
        };

        protected internal StringParameter(GirModel.Parameter parameter) : base(parameter)
        {
            parameter.AnyType.VerifyType<GirModel.String>();
        }

        private string GetNullableTypeName(GirModel.Type type, string attribute = "") => attribute + type.GetName() + GetDefaultNullable();
    }
}
