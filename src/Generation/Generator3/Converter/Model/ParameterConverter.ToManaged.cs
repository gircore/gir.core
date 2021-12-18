﻿using System;
using GirModel;

namespace Generator3.Converter
{
    public static partial class ParameterConverter
    {
        public static string? ToManaged(this GirModel.Parameter from, out string variableName)
        {
            #region Primitivevaluetype

            if (from.AnyType.Is<GirModel.PrimitiveValueType>())
            {
                if (from.IsPointer)
                    throw new NotImplementedException($"{from.AnyType}: Pointed primitive value types can not yet be converted to managed");

                if (from.Direction != GirModel.Direction.In)
                    throw new NotImplementedException($"{from.AnyType}: Primitive value type with direction != in not yet supported");

                //We don't need any conversion for native parameters
                variableName = from.GetPublicName();
                return null;
            }

            #endregion

            #region Enumeration

            if (from.AnyType.Is<GirModel.Enumeration>())
            {
                if (from.Direction != GirModel.Direction.In)
                    throw new NotImplementedException($"{from.AnyType}: Enumeration with direction != in not yet supported");

                //We don't need any conversion for enumerations
                variableName = from.GetPublicName();
                return null;
            }

            #endregion

            #region Bitfield

            if (from.AnyType.Is<GirModel.Bitfield>())
            {
                if (from.Direction != GirModel.Direction.In)
                    throw new NotImplementedException($"{from.AnyType}: Bitfield with direction != in not yet supported");

                //We don't need any conversion for bitfields
                variableName = from.GetPublicName();
                return null;
            }

            #endregion

            #region Pointer

            if (from.AnyType.Is<GirModel.Pointer>())
            {
                if (from.Direction != GirModel.Direction.In)
                    throw new NotImplementedException($"{from.AnyType}: Pointer with direction != in not yet supported");

                //We don't need any conversion for bitfields
                variableName = from.GetPublicName();
                return null;
            }

            #endregion

            #region String array

            if (from.AnyType.IsArray<GirModel.String>())
            {
                var arrayType = from.AnyType.AsT1;
                if (from.Transfer == GirModel.Transfer.None && arrayType.Length == null)
                {
                    variableName = from.GetConvertedName();
                    return $"var {variableName} = new GLib.Internal.StringArrayNullTerminatedSafeHandle({from.GetPublicName()}).DangerousGetHandle();";
                }
                else
                {
                    //We don't need any conversion for string[]
                    variableName = from.GetPublicName();
                    return null;
                }
            }

            #endregion

            #region Record

            if (from.AnyType.Is<GirModel.Record>())
            {
                if (from.Direction != GirModel.Direction.In)
                    throw new NotImplementedException($"{from.AnyType}: record with direction != in not yet supported");

                if (!from.IsPointer)
                    throw new NotImplementedException($"Unpointed record parameter {from.Name} ({from.AnyType}) can not yet be converted to managed");

                var record = (GirModel.Record) from.AnyType.AsT0;

                variableName = from.GetConvertedName();
                return $"var {variableName} = new {record.GetFullyQualifiedPublicClassName()}(new {record.GetFullyQualifiedInternalHandle()}({from.GetPublicName()}));";
            }

            #endregion

            #region Record array

            if (from.AnyType.IsArray<GirModel.Record>())
            {
                var arrayType = from.AnyType.AsT1;
                var record = (GirModel.Record) arrayType.AnyType.AsT0;
                variableName = from.GetConvertedName();

                if (arrayType.IsPointer)
                {
                    return $"var {variableName} = {from.GetPublicName()}.Select(x => new {record.GetFullyQualifiedPublicClassName()}(x)).ToArray();";
                }
                else
                {
                    return $"var {variableName} = ({record.GetFullyQualifiedPublicClassName()}[]){from.GetPublicName()}.Select(x => new {record.GetFullyQualifiedPublicClassName()}({record.GetFullyQualifiedInternalManagedHandleCreateMethod()}(x))).ToArray();";
                }
            }

            #endregion

            throw new NotImplementedException($"Can't convert from parameter {from.Name} ({from.AnyType}) to managed");
        }
    }
}