using System;
using Generator3.Model.Internal;

namespace Generator3.Renderer.Converter
{
    public static class ConvertInternalReturnType
    {
        public static string ToPublicFromVariable(this ReturnType from, string variableName)
        {
            var to = Model.Public.ReturnTypeFactory.CreatePublicModel(from.Model);

            return from switch
            {
                //** Pointer **
                PointerReturnType => variableName,
                
                //** Enumerations **
                BitfieldReturnType => variableName,
                EnumerationReturnType => variableName,

                //** Primitives **
                PrimitiveValueReturnType { IsPointer: false } => variableName,
                PrimitiveValueReturnType { IsPointer: true } => variableName,

                //** Strings **
                //If ownership is transfered the internal return type is encoded as a string as the
                //marshaller will handle the ownership transfer automatically
                Utf8StringReturnType { IsOwnedRef: true } => variableName,
                PlatformStringReturnType { IsOwnedRef: true } => variableName,

                Utf8StringReturnType => $"GLib.Internal.StringHelper.ToStringUtf8({variableName})",
                PlatformStringReturnType => $"GLib.Internal.StringHelper.ToStringUtf8({variableName})",

                //** Classes **
                ClassReturnType { IsPointer: true } 
                    => $"GObject.Internal.ObjectWrapper.WrapHandle<{to.NullableTypeName}>({variableName}, {from.IsOwnedRef.ToString().ToLower()})",

                //** Interfaces **
                InterfaceReturnType {IsPointer: true }
                    => $"GObject.Internal.ObjectWrapper.WrapHandle<{to.NullableTypeName}>({variableName}, {from.IsOwnedRef.ToString().ToLower()})",
                
                //** Records **
                RecordReturnType { IsPointer: true } => $"new {to.NullableTypeName}({variableName})",
                RecordReturnType { IsPointer: false } 
                    => throw new NotImplementedException("Can't convert from internal records which are returnd by value to public available. This is not supported in current development branch, too."),

                //** Array of primitive value types **
                ArrayPrimitiveValueReturnType => variableName,
                
                //** Array of String **
                ArrayStringReturnType { IsMarshalAble: true } => variableName,
                ArrayStringReturnType { IsMarshalAble: false } => $"GLib.Internal.StringHelper.ToStringArrayUtf8({variableName})",
                
                _ => throw new NotImplementedException($"Can't convert from internal return type {from.NullableTypeName} to public")
            };
        }
    }
}
