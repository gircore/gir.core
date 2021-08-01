using System;

namespace GirLoader.Output.Model
{
    public abstract class PrimitiveValueType : PrimitiveType
    {
        protected PrimitiveValueType(CType ctype, TypeName name) : base(ctype, name)
        {
        }

        internal override bool Matches(TypeReference typeReference)
        {
            return typeReference switch
            {
                { CTypeReference: { } cr } => cr.CType == CType,
                { SymbolNameReference: { SymbolName: { } sn } } => sn.Value == CType?.Value,
                _ => throw new Exception($"Can't match {GetType().Name} with {nameof(TypeReference)} {typeReference}")
            };
        }
    }
}
