using ReturnTypeFactory = Generator3.Model.Public.ReturnTypeFactory;
using GirModel;

namespace Generator3.Converter
{
    public static partial class ReturnTypeConverter
    {
        public static string ToManaged(this ReturnType from, string fromVariableName)
        {
            var to = ReturnTypeFactory.CreatePublicModel(from);
            
            if (from.AnyType.Is<Pointer>())
                return fromVariableName;

            if (from.AnyType.Is<Bitfield>())
                return fromVariableName;

            if (from.AnyType.Is<Enumeration>())
                return fromVariableName;

            if (from.AnyType.Is<PrimitiveValueType>())
                return fromVariableName; //Valid for IsPointer = true && IsPointer = false
            
            if (from.AnyType.Is<Utf8String>())
            {
                //If ownership is transfered the internal return type is encoded as a string as the
                //marshaller will handle the ownership transfer automatically
                return from.Transfer.IsOwnedRef()
                    ? fromVariableName
                    : $"GLib.Internal.StringHelper.ToStringUtf8({fromVariableName})";
            }

            if (from.AnyType.Is<PlatformString>())
            {
                //If ownership is transfered the internal return type is encoded as a string as the
                //marshaller will handle the ownership transfer automatically
                return from.Transfer.IsOwnedRef() 
                    ? fromVariableName 
                    : $"GLib.Internal.StringHelper.ToStringUtf8({fromVariableName})";
            }

            if (from.AnyType.Is<Class>() && from.IsPointer)
                return $"GObject.Internal.ObjectWrapper.WrapHandle<{to.NullableTypeName}>({fromVariableName}, {from.Transfer.IsOwnedRef().ToString().ToLower()})";
            
            if (from.AnyType.Is<Interface>() && from.IsPointer)
                return $"GObject.Internal.ObjectWrapper.WrapHandle<{to.NullableTypeName}>({fromVariableName}, {from.Transfer.IsOwnedRef().ToString().ToLower()})";

            if (from.AnyType.Is<Record>())
            {
                if(from.IsPointer)
                    return $"new {to.NullableTypeName}({fromVariableName})";
                
                throw new System.NotImplementedException("Can't convert from internal records which are returnd by value to public available. This is not supported in current development branch, too.");
            }

            if (from.AnyType.IsArray<PrimitiveValueType>())
                return fromVariableName;

            if (from.AnyType.IsArray<String>())
            {
                return from.Transfer == Transfer.None && from.AnyType.AsT1.Length == null
                    ? $"GLib.Internal.StringHelper.ToStringArrayUtf8({fromVariableName})" //variableName is a pointer to a string array 
                    : fromVariableName; //variableName is a string[]
            }
            
            throw new System.NotImplementedException($"Can't convert from internal return type {from} to public");
        }
    }
}
