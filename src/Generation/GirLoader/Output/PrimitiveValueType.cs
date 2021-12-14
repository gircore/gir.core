using System;

namespace GirLoader.Output
{
    public abstract class PrimitiveValueType : Type
    {
        protected PrimitiveValueType(string ctype) : base(ctype)
        {
        }

        internal override bool Matches(TypeReference typeReference)
        {
            return typeReference switch
            {
                { SymbolNameReference: { SymbolName: { } sn } } => sn == CType,
                { CTypeReference: { } cr } => cr.CType == CType,
                _ => throw new Exception($"Can't match {GetType().Name} with {nameof(TypeReference)} {typeReference}")
            };
        }
    }
}
