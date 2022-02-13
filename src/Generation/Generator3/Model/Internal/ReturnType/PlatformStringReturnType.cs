using Generator3.Converter;
using GirModel;

namespace Generator3.Model.Internal
{
    public class PlatformStringReturnType : ReturnType
    {
        public override string NullableTypeName => Model switch
        {
            // Return values which return a string without transferring ownership to us can not be marshalled automatically
            // as the marshaller want's to free the unmanaged memory which is not allowed if the ownership is not transferred
            { Transfer: Transfer.None } => TypeNameExtension.Pointer,
            _ => Model.AnyType.AsT0.GetName() + GetDefaultNullable()
        };

        protected internal PlatformStringReturnType(GirModel.ReturnType returnValue) : base(returnValue)
        {
            returnValue.AnyType.VerifyType<PlatformString>();
        }
    }
}
