using GirModel;

namespace Generator3.Generation.Code.Native
{
    public class StringReturnValue : ReturnValue
    {
        public override string NullableTypeName => _returnValue switch
        {
            // Return values which return a string without transferring ownership to us can not be marshalled automatically
            // as the marshaller want's to free the unmanaged memory which is not allowed if the ownership is not transferred
            { Transfer: Transfer.None } => "IntPtr",
            _ => _returnValue.Type.GetName() + GetDefaultNullable()
        };
        
        protected internal StringReturnValue(GirModel.ReturnValue returnValue) : base(returnValue) { }
    }
}
