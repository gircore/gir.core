using System;
using GirLoader.Output.Model;
using String = GirLoader.Output.Model.String;
using Type = GirLoader.Output.Model.Type;

namespace Generator
{
    internal static class Convert
    {
        internal static string ManagedToNative(TransferableAnyType transferable, string fromParam, Namespace currentNamespace)
        {
            Transfer transfer = transferable.Transfer;
            Type type = transferable.TypeReference.Type
                        ?? throw new NullReferenceException($"Error: Conversion of '{fromParam}' to {nameof(Target.Native)} failed - {transferable.GetType().Name} does not have a type");

            var qualifiedNativeType = type.Write(Target.Native, currentNamespace);
            var qualifiedManagedType = type.Write(Target.Managed, currentNamespace);

            return transferable.TypeReference switch
            {
                // String Array Handling
                ArrayTypeReference { Length: null, Type: String } when transfer == Transfer.None => $"new GLib.Native.StringArrayNullTerminatedSafeHandle({fromParam}).DangerousGetHandle()",

                // String Handling
                ResolveableTypeReference { Type: String } when (transfer == Transfer.None) && (transferable is ReturnValue) => $"GLib.Native.StringHelper.StringToHGlobalUTF8({fromParam})",
                ResolveableTypeReference { Type: String } => fromParam,

                //Record Array Conversions 
                ArrayTypeReference { Type: Record, TypeReference: { CTypeReference: { IsPointer: true } } } => $"{fromParam}.Select(x => x.Handle.DangerousGetHandle()).ToArray()",
                ArrayTypeReference { Type: Record r, TypeReference: { CTypeReference: { IsPointer: false } } } => $"({r.Repository.Namespace}.Native.{r.GetMetadataString("StructRefName")}[]) default!; //TODO: Fixme",
                ArrayTypeReference { Type: Record r, TypeReference: { CTypeReference: null } } => $"({r.Repository.Namespace}.Native.{r.GetMetadataString("StructRefName")}[]) default!; //TODO: Fixme",

                //Record Conversions
                ResolveableTypeReference { Type: Record, CTypeReference: { IsPointer: true } } => $"{fromParam}.Handle",
                ResolveableTypeReference { Type: Record r, CTypeReference: { IsPointer: false } } => $"({r.Repository.Namespace}.Native.{r.GetMetadataString("StructRefName")}) default!; //TODO: Fixme",

                // Class Array Conversions
                ArrayTypeReference { Type: Class, TypeReference: { CTypeReference: { IsPointer: true } } } => throw new NotImplementedException($"Can't create delegate for argument {fromParam}"),
                ArrayTypeReference { Type: Class } => $"{fromParam}.Select(cls => cls.Handle).ToArray()",

                // Class Conversions
                ResolveableTypeReference { Type: Class { IsFundamental: true }, CTypeReference: { IsPointer: true } } => $"{qualifiedManagedType}.To({fromParam})",
                ResolveableTypeReference { Type: Class, CTypeReference: { IsPointer: true } } => $"{fromParam}.Handle",

                // Interface Array Conversions
                ArrayTypeReference { Type: Interface } => $"{fromParam}.Select(iface => (iface as GObject.Object).Handle).ToArray()",

                // Interface Array Conversions
                ResolveableTypeReference { Type: Interface } => $"({fromParam} as GObject.Object).Handle",

                // Other -> Try a brute-force cast
                ArrayTypeReference { } => $"({qualifiedNativeType}[]){fromParam}",
                _ => $"({qualifiedNativeType}){fromParam}"
            };
        }

        internal static string NativeToManaged(TransferableAnyType transferable, string fromParam, Namespace currentNamespace, bool useSafeHandle = true)
        {
            Transfer transfer = transferable.Transfer;
            Type type = transferable.TypeReference.Type
                        ?? throw new NullReferenceException($"Error: Conversion of '{fromParam}' to {nameof(Target.Managed)} failed - {transferable.GetType().Name} does not have a type");

            var qualifiedType = type.Write(Target.Managed, currentNamespace);

            return transferable.TypeReference switch
            {
                // String Array Handling
                ArrayTypeReference { Length: null, Type: String } when transfer == Transfer.None => $"GLib.Native.StringHelper.ToStringArrayUtf8({fromParam})",

                // String Handling
                { Type: String } when (transfer == Transfer.None) && (transferable is ReturnValue) => $"GLib.Native.StringHelper.ToStringUtf8({fromParam})",

                // Record Conversions (safe handles)
                ArrayTypeReference { Type: Record r, TypeReference: { CTypeReference: { IsPointer: true } } } when useSafeHandle => $"{fromParam}.Select(x => new {r.Write(Target.Managed, currentNamespace)}(x)).ToArray()",
                ResolveableTypeReference { Type: Record r, CTypeReference: { IsPointer: true } } when useSafeHandle => $"new {r.Write(Target.Managed, currentNamespace)}({fromParam})",
               
                // Record Conversions (raw pointers)
                ArrayTypeReference { Type: Record r, TypeReference: { CTypeReference: { IsPointer: true } } } when !useSafeHandle => $"{fromParam}.Select(x => new {r.Write(Target.Managed, currentNamespace)}(new {SafeHandleFromRecord(r)}(x))).ToArray()",
                ResolveableTypeReference { Type: Record r, CTypeReference: { IsPointer: true } } when !useSafeHandle => $"new {r.Write(Target.Managed, currentNamespace)}(new {SafeHandleFromRecord(r)}({fromParam}))",

                //Record Conversions without pointers are not working yet
                ArrayTypeReference { Type: Record r, TypeReference: { CTypeReference: { IsPointer: false } } } => $"({qualifiedType}[]) {fromParam}.Select(x => new {qualifiedType}({SafeHandleFromRecord(r, true)}(x))).ToArray();",
                ResolveableTypeReference { Type: Record r, CTypeReference: { IsPointer: false } } => $"({r.Write(Target.Managed, currentNamespace)}) default!; //TODO: Fixme",

                // Class Conversions
                ArrayTypeReference { Type: Class, CTypeReference: { IsPointer: true } } => throw new NotImplementedException($"Can't create delegate for argument '{fromParam}'"),
                ResolveableTypeReference { Type: Class { IsFundamental: true }, CTypeReference: { IsPointer: true } } => $"{qualifiedType}.From({fromParam})",
                ResolveableTypeReference { Type: Class, CTypeReference: { IsPointer: true } } => $"GObject.Native.ObjectWrapper.WrapHandle<{qualifiedType}>({fromParam}, {transfer.IsOwnedRef().ToString().ToLower()})",

                // Misc
                ResolveableTypeReference { Type: Interface } => $"GObject.Native.ObjectWrapper.WrapHandle<{qualifiedType}>({fromParam}, {transfer.IsOwnedRef().ToString().ToLower()})",
                ResolveableTypeReference { Type: Union } => $"",

                // Other -> Try a brute-force cast
                ArrayTypeReference { } => $"({qualifiedType}[]){fromParam}",
                _ => $"({qualifiedType}){fromParam}"
            };
        }

        private static string SafeHandleFromRecord(Record r, bool managedHandle = false)
        {
            var type = r.GetMetadataString(managedHandle ? "SafeHandleRefManagedFunc" : "SafeHandleRefName");
            var nspace = $"{r.Repository.Namespace}.Native";
            return nspace + "." + type;
        }
    }
}
