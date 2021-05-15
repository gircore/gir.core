using System;
using Gir.Model;
using Array = System.Array;
using String = Gir.Model.String;
using Type = Gir.Model.Type;

namespace Generator
{
    internal static class Convert
    {
        internal static string ManagedToNative(TransferableAnyType transferable, string fromParam, Namespace currentNamespace)
        {
            Transfer transfer = transferable.Transfer;
            TypeInformation typeInfo = transferable.TypeInformation;
            Type type = transferable.TypeReference.ResolvedType
                        ?? throw new NullReferenceException($"Error: Conversion of '{fromParam}' to {nameof(Target.Native)} failed - {transferable.GetType().Name} does not have a type");

            var qualifiedNativeType = type.Write(Target.Native, currentNamespace);
            var qualifiedManagedType = type.Write(Target.Managed, currentNamespace);

            return (symbol: type, typeInfo) switch
            {
                // String Handling
                // String Arrays which do not have a length index need to be marshalled as IntPtr
                (String s, { Array: { Length: null } }) when transfer == Transfer.None => $"new GLib.Native.StringArrayNullTerminatedSafeHandle({fromParam}).DangerousGetHandle()",
                (String s, _) when (transfer == Transfer.None) && (transferable is ReturnValue) => $"GLib.Native.StringHelper.StringToHGlobalUTF8({fromParam})",

                // All other string types can be marshalled directly
                (String, _) => fromParam,

                // Record Conversions
                (Record r, { Array: null }) => $"{fromParam}.Handle",
                (Record r, { Array: { } }) => $"{fromParam}.Select(x => x.Handle.DangerousGetHandle()).ToArray()",
                
                // Class Conversions
                (Class { IsFundamental: true } c, { IsPointer: true, Array: null }) => $"{qualifiedManagedType}.To({fromParam})",
                (Class c, { IsPointer: true, Array: null }) => $"{fromParam}.Handle",
                (Class c, { IsPointer: true, Array: { } }) => throw new NotImplementedException($"Can't create delegate for argument {fromParam}"),
                (Class c, { Array: { } }) => $"{fromParam}.Select(cls => cls.Handle).ToArray()",
                
                // Interface Conversions
                (Interface i, { Array: { } }) => $"{fromParam}.Select(iface => (iface as GObject.Object).Handle).ToArray()",
                (Interface i, _) => $"({fromParam} as GObject.Object).Handle",

                // Other -> Try a brute-force cast
                (_, { Array: { } }) => $"({qualifiedNativeType}[]){fromParam}",
                _ => $"({qualifiedNativeType}){fromParam}"
            };
        }

        internal static string NativeToManaged(TransferableAnyType transferable, string fromParam, Namespace currentNamespace, bool useSafeHandle = true)
        {
            Transfer transfer = transferable.Transfer;
            TypeInformation typeInfo = transferable.TypeInformation;
            Type type = transferable.TypeReference.ResolvedType
                        ?? throw new NullReferenceException($"Error: Conversion of '{fromParam}' to {nameof(Target.Managed)} failed - {transferable.GetType().Name} does not have a type");
            
            var qualifiedType = type.Write(Target.Managed, currentNamespace);

            return (symbol: type, typeInfo) switch
            {
                // String Handling
                (String s, { Array: { Length: null } }) when transfer == Transfer.None => $"GLib.Native.StringHelper.ToStringArrayUtf8({fromParam})",
                (String s, _) when (transfer == Transfer.None) && (transferable is ReturnValue) => $"GLib.Native.StringHelper.ToStringUtf8({fromParam})",

                // Record Conversions (safe handles)
                (Record r, { Array: null }) when useSafeHandle => $"new {r.Write(Target.Managed, currentNamespace)}({fromParam})",
                (Record r, { Array: { } }) when useSafeHandle => $"{fromParam}.Select(x => new {r.Write(Target.Managed, currentNamespace)}(x)).ToArray()",
                
                // Record Conversions (raw pointers)
                (Record r, { Array: null }) when !useSafeHandle => $"new {r.Write(Target.Managed, currentNamespace)}(new {SafeHandleFromRecord(r)}({fromParam}))",
                (Record r, { Array: { } }) when !useSafeHandle => $"{fromParam}.Select(x => new {r.Write(Target.Managed, currentNamespace)}(new {SafeHandleFromRecord(r)}(x))).ToArray()",
                
                // Class Conversions
                (Class { IsFundamental: true } c, { IsPointer: true, Array: null }) => $"{qualifiedType}.From({fromParam})",
                (Class c, { IsPointer: true, Array: null }) => $"GObject.Native.ObjectWrapper.WrapHandle<{qualifiedType}>({fromParam}, {transfer.IsOwnedRef().ToString().ToLower()})",
                (Class c, { IsPointer: true, Array: { } }) => throw new NotImplementedException($"Can't create delegate for argument '{fromParam}'"),
                
                // Misc
                (Interface i, _) => $"GObject.Native.ObjectWrapper.WrapHandle<{qualifiedType}>({fromParam}, {transfer.IsOwnedRef().ToString().ToLower()})",
                (Union u, _) => $"",

                // Other -> Try a brute-force cast
                (_, { Array: { } }) => $"({qualifiedType}[]){fromParam}",
                _ => $"({qualifiedType}){fromParam}"
            };
        }

        private static string SafeHandleFromRecord(Record r)
        {
            var type = r.GetMetadataString("SafeHandleRefName");
            var nspace = $"{r.Namespace}.Native";
            return nspace + "." + type;
        }
    }
}
