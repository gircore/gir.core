using GirModel;

namespace Generator3.Converter
{
    public static partial class ReturnTypeConverter
    {
        public static string ToNative(this ReturnType from, string fromVariableName)
        {
            if (from.AnyType.Is<Pointer>())
                return fromVariableName;

            if (from.AnyType.Is<Bitfield>())
                return fromVariableName;

            if (from.AnyType.Is<Enumeration>())
                return fromVariableName;

            if (from.AnyType.Is<PrimitiveValueType>())
                return fromVariableName;

            if (from.AnyType.Is<String>())
            {
                return from.Transfer == Transfer.None 
                    ? $"GLib.Native.StringHelper.StringToHGlobalUTF8({fromVariableName})" 
                    : fromVariableName;
            }

            if(from.AnyType.Is<Record>())
                return fromVariableName + ".Handle";
            
            throw new System.NotImplementedException($"Can't convert from return type {from} to native");
        }
    }
}
