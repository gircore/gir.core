namespace GirLoader.Output.Model
{
    public class CTypeReference
    {
        public CType CType { get; }
        public bool IsPointer { get; }
        public bool IsConst { get; }
        public bool IsVolatile { get; }

        public CTypeReference(string cTypeReference)
        {
            cTypeReference = cTypeReference.Replace(" ", "");
            IsPointer = TryRemove(ref cTypeReference, "*");
            IsConst = TryRemove(ref cTypeReference, "const");
            IsVolatile = TryRemove(ref cTypeReference, "volatile");

            CType = new CType(cTypeReference);
        }

        private bool TryRemove(ref string value, string remove)
        {
            var originalLength = value.Length;
            value = value.Replace(remove, "");

            return originalLength != value.Length;
        }

        public override string ToString()
            => CType;
    }
}
