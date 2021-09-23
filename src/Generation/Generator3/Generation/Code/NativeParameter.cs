using GirModel;
using String = GirModel.String;

namespace Generator3.Generation
{
    public class NativeParameter
    {
        private readonly Parameter _parameter;

        public string Name => _parameter.Name;
        public string Type => GetNullableType();
        public string TypeName => _parameter.Type.GetName();
        public string Direction => GetDirection();
        public string Code => GetCode();

        public NativeParameter(Parameter parameter)
        {
            _parameter = parameter;
        }

        private string GetNullableType()
        {
            return GetType() + (IsNullable() ? "?" : "");
        }
        
        private bool IsNullable() => _parameter switch
        {
            //IntPtr can't be nullable they can be "nulled" via IntPtr.Zero
            { Type: Pointer } => false,

            //Native arrays can not be nullable
            { Type: ArrayType } => false,
            
            //Native classes are represented as IntPtr and should not be nullable
            { Type: Class } => false,
            
            //Native records are represented as SafeHandles and are not nullable
            { Type: Record } => false,
            
            _ => _parameter.Nullable
        };
        
        private string GetType() => _parameter switch
        {
            // Arrays of string which do not transfer ownership and have no length index can not be marshalled automatically
            { Type: ArrayType {Type: String, Length: null}, Transfer: Transfer.None} => "IntPtr",

            {Type: ArrayType } => TypeName + "[]",
            _ => TypeName,
        };
        
        private string GetDirection() => _parameter switch
        {
            // Arrays are automatically marshalled correctly. They don't need any direction
            { Direction: GirModel.Direction.InOut, Type: ArrayType} => "",
            
            //Native records (SafeHandles) are not supporting ref
            { Direction: GirModel.Direction.InOut, Type: Record } => "",
            { Direction: GirModel.Direction.Out, CallerAllocates: true, Type: Record } => "",

            { Direction: GirModel.Direction.InOut } => "ref ",
            { Direction: GirModel.Direction.Out, CallerAllocates: true } => "ref ",
            { Direction: GirModel.Direction.Out } => "out ",
            _ => ""
        };

        private string GetCode() => $@"{Direction}{ Type } { Name }";
    }
}
