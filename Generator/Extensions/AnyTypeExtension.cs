﻿using System;
using GirLoader.Output.Model;
using String = GirLoader.Output.Model.String;

namespace Generator
{
    internal static class AnyTypeExtension
    {
        internal static string WriteType(this AnyType anyType, Target target, Namespace currentNamespace, bool useSafeHandle = true)
        {
            return target switch
            {
                Target.Managed => WriteManagedType(anyType, currentNamespace, useSafeHandle),
                Target.Native => WriteNativeType(anyType, currentNamespace, useSafeHandle)
            };
        }

        private static string WriteNativeType(AnyType anyType, Namespace currentNamespace, bool useSafeHandle = true)
        {
            return anyType switch
            {
                { TypeReference: { Type: Callback c } } => c.Write(Target.Native, currentNamespace),

                // Return values which return a string without transferring ownership to us can not be marshalled automatically
                // as the marshaller want's to free the unmanaged memory which is not allowed if the ownership is not transferred
                ReturnValue { Transfer: Transfer.None, TypeReference: { Type: String } } => "IntPtr",

                // Arrays of string which do not transfer ownership and have no length index can not be marshalled automatically
                TransferableAnyType { TypeReference: ArrayTypeReference { Length: null }, TypeReference: { Type: String }, Transfer: Transfer.None } => "IntPtr",

                // Arrays of string can be marshalled automatically, no IntPtr needed
                { TypeReference: ArrayTypeReference { Type: String } } => "string[]",

                // Arrays of byte can be marshalled automatically, no IntPtr needed
                { TypeReference: ArrayTypeReference { Type: { } s } } when s.Name == "byte" => "byte[]",

                // References to records which are not using a pointer
                { TypeReference: ArrayTypeReference { Type: Record r, TypeReference: { CTypeReference: { IsPointer: false } } } } => GetStructName(r, currentNamespace) + "[]",
                { TypeReference: ArrayTypeReference { Type: Record r, TypeReference: { CTypeReference: null } } } => GetStructName(r, currentNamespace) + "[]",
                { TypeReference: { Type: Record r, CTypeReference: { IsPointer: false } } } => GetStructName(r, currentNamespace),

                // References to records which are using a pointer
                { TypeReference: ArrayTypeReference { Type: Record, TypeReference: { CTypeReference: { IsPointer: true } } } } => "IntPtr[]", //Array of SafeHandle not supported by runtime
                { TypeReference: ResolveableTypeReference { Type: Record r, CTypeReference: { IsPointer: true } } } => GetSafeHandleName(r, currentNamespace, useSafeHandle),

                // References to unions which are not using a pointer
                { TypeReference: ArrayTypeReference { Type: Union u, TypeReference: { CTypeReference: { IsPointer: false } } } } => GetStructName(u, currentNamespace) + "[]",
                { TypeReference: ArrayTypeReference { Type: Union u, TypeReference: { CTypeReference: null } } } => GetStructName(u, currentNamespace) + "[]",
                { TypeReference: { Type: Union u, CTypeReference: { IsPointer: false } } } => GetStructName(u, currentNamespace),

                // References to unions which are using a pointer
                { TypeReference: ArrayTypeReference { Type: Union, TypeReference: { CTypeReference: { IsPointer: true } } } } => "IntPtr[]", //Array of SafeHandle not supported by runtime
                { TypeReference: ResolveableTypeReference { Type: Union, CTypeReference: { IsPointer: true } } } => "IntPtr",

                // Primitives - Marshal directly
                { TypeReference: ArrayTypeReference { Type: PrimitiveValueType s } } => s.Write(Target.Native, currentNamespace) + "[]",
                { TypeReference: { Type: PrimitiveValueType s } } => s.Write(Target.Native, currentNamespace),

                // Enumerations - Marshal directly
                { TypeReference: ArrayTypeReference { Type: Enumeration } } => anyType.TypeReference.Type.Write(Target.Native, currentNamespace) + "[]",
                { TypeReference: { Type: Enumeration } } => anyType.TypeReference.Type.Write(Target.Native, currentNamespace),

                // Short path for strings as strings are pointers which should not be handled as pointers
                { TypeReference: { Type: String r } } => r.Name,

                // Use IntPtr for all types where a pointer is expected
                { TypeReference: ArrayTypeReference { Length: not null, TypeReference: { CTypeReference: { IsPointer: true } } } } => "IntPtr[]",
                { TypeReference: ResolveableTypeReference { CTypeReference: { IsPointer: true } } } => "IntPtr",

                { TypeReference: ArrayTypeReference } => anyType.TypeReference.Type.Write(Target.Native, currentNamespace) + "[]",
                _ => anyType.TypeReference.Type.Write(Target.Native, currentNamespace)
            };
        }

        private static string GetStructName(Record r, Namespace currentNamespace)
        {
            return AddNamespace(currentNamespace, r.Repository.Namespace, r.GetMetadataString("StructRefName"), Target.Native);
        }

        private static string GetStructName(Union u, Namespace currentNamespace)
        {
            return AddNamespace(currentNamespace, u.Repository.Namespace, u.GetMetadataString("StructRefName"), Target.Native);
        }

        private static string GetSafeHandleName(Record r, Namespace currentNamespace, bool useSafeHandle)
        {
            if (useSafeHandle)
                return AddNamespace(currentNamespace, r.Repository.Namespace, r.GetMetadataString("SafeHandleRefName"), Target.Native);

            return "IntPtr";
        }

        private static string WriteManagedType(AnyType anyType, Namespace currentNamespace, bool useSafeHandle = true)
        {
            var result = anyType switch
            {
                { TypeReference: { Type: Callback c } } => AddNamespace(currentNamespace, c.Repository.Namespace, c.GetMetadataString("ManagedName"), Target.Managed),
                { TypeReference: { Type: Record r } } => AddNamespace(currentNamespace, r.Repository.Namespace, r.GetMetadataString("Name"), Target.Managed),
                { TypeReference: { Type: Union u } } => AddNamespace(currentNamespace, u.Repository.Namespace, u.GetMetadataString("Name"), Target.Managed),

                _ => anyType.TypeReference.Type.Write(Target.Managed, currentNamespace)
            };

            if (anyType.TypeReference is ArrayTypeReference)
                result += "[]";

            return result;
        }

        private static string AddNamespace(Namespace currentNamespace, Namespace? targetNamespace, string str, Target target)
        {
            if (!currentNamespace.IsForeignTo(targetNamespace))
                return str;

            if (targetNamespace is null)
                throw new Exception("Target namespace is missing");

            return targetNamespace.GetName(target) + "." + str;
        }
    }
}
