using System;
using Repository.Model;
using Array = System.Array;
using String = Repository.Model.String;

namespace Generator
{
    internal static class Convert
    {
        internal static string ManagedToNative(string fromParam, Symbol symbol, TypeInformation typeInfo, Namespace currentNamespace)
        {
            // TODO: We need to support disguised structs (opaque types)
            var qualifiedNativeType = symbol.Write(Target.Native, currentNamespace);
            var qualifiedManagedType = symbol.Write(Target.Managed, currentNamespace);
            
            return (symbol, typeInfo) switch
            {
                (Record r, {IsPointer: true, Array: null}) => $"{fromParam}.Handle",
                (Record r, {IsPointer: true, Array:{}}) => $"{fromParam}.MarshalToStructure<{qualifiedNativeType}>()",
                (Class {IsFundamental: true} c, {IsPointer: true, Array: null}) => $"{qualifiedManagedType}.To({fromParam})",
                (Class c, {IsPointer: true, Array: null}) => $"{fromParam}.Handle",
                (Class c, {IsPointer: true, Array: {}}) => throw new NotImplementedException($"Can't create delegate for argument {fromParam}"),
                (Class c, { Array: {}}) => $"{fromParam}.Select(cls => cls.Handle).ToArray()",
                (Interface i, { Array: {}}) => $"{fromParam}.Select(iface => (iface as GObject.Object).Handle).ToArray()",
                (Interface i, _) => $"({fromParam} as GObject.Object).Handle",
                
                // Other -> Try a brute-force cast
                (_, {Array: {}}) => $"({qualifiedNativeType}[]){fromParam}",
                _ => $"({qualifiedNativeType}){fromParam}"
            };
        }
        
        internal static string NativeToManaged(string fromParam, Symbol symbol, TypeInformation typeInfo, Namespace currentNamespace, Transfer transfer = Transfer.Unknown)
        {
            // TODO: We need to support disguised structs (opaque types)
            var qualifiedType = symbol.Write(Target.Managed, currentNamespace);
            
            return (symbol, typeInfo) switch
            {
                // String Handling
                (String s, {Array: {}}) when transfer == Transfer.None => $"GLib.Native.StringHelper.ToStringArrayUtf8({fromParam})",
                (String s, _) when transfer == Transfer.None => $"GLib.Native.StringHelper.ToStringUtf8({fromParam})",

                // General Conversions
                (Record r, {IsPointer: true, Array: null}) => $"Marshal.PtrToStructure<{qualifiedType}>({fromParam})",
                (Record r, {IsPointer: true, Array:{}}) => $"{fromParam}.MarshalToStructure<{qualifiedType}>()",
                (Class {IsFundamental: true} c, {IsPointer: true, Array: null}) => $"{qualifiedType}.From({fromParam})",
                (Class c, {IsPointer: true, Array: null}) => $"GObject.Object.WrapHandle<{qualifiedType}>({fromParam}, {transfer.IsOwnedRef().ToString().ToLower()})",
                (Class c, {IsPointer: true, Array: {}}) => throw new NotImplementedException($"Can't create delegate for argument '{fromParam}'"),
                (Interface i, _) => $"GObject.Object.WrapHandle<{qualifiedType}>({fromParam}, {transfer.IsOwnedRef().ToString().ToLower()})",
                (Union u, _) => $"",
                
                // Other -> Try a brute-force cast
                (_, {Array: {}}) => $"({qualifiedType}[]){fromParam}",
                _ => $"({qualifiedType}){fromParam}"
            };
        }
    }
}
