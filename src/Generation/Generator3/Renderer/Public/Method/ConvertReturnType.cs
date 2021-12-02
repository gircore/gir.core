using System;
using Generator3.Model.Public;

namespace Generator3.Renderer.Public
{
    public static class ConvertReturnType
    {
        public static string ToPublicFromVariable(this Model.Internal.ReturnType from, string variableName)
        {
            var to = from.Model.CreatePublicModel();

            return from switch
            {
                //** Enumerations **
                Model.Internal.BitfieldReturnType => variableName,
                Model.Internal.EnumerationReturnType => variableName,

                //** Primitives **
                Model.Internal.PrimitiveValueReturnType { IsPointer: false } => variableName,

                //** Strings **
                //If ownership is transfered the internal return type is encoded as a string as the
                //marshaller will handle the ownership transfer automatically
                Model.Internal.Utf8StringReturnType { IsOwnedRef: true } => variableName,
                Model.Internal.PlatformStringReturnType { IsOwnedRef: true } => variableName,

                Model.Internal.Utf8StringReturnType => $"GLib.Internal.StringHelper.ToStringUtf8({variableName})",
                Model.Internal.PlatformStringReturnType => $"GLib.Internal.StringHelper.ToStringUtf8({variableName})",

                //** Classes **
                Model.Internal.ClassReturnType { IsPointer: true } 
                    => $"GObject.Internal.ObjectWrapper.WrapHandle<{to.NullableTypeName}>({variableName}, {from.IsOwnedRef.ToString().ToLower()})",

                //** Records **
                Model.Internal.RecordReturnType { IsPointer: true } => $"new {to.NullableTypeName}({variableName})",

                _ => throw new NotImplementedException($"Convert from internal return type {from.NullableTypeName} to public")
            };
        }
    }
}
