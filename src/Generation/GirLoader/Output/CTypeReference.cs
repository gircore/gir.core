namespace GirLoader.Output
{
    public class CTypeReference
    {
        public string CType { get; }
        public bool IsPointer { get; }
        public bool IsConst { get; }
        public bool IsVolatile { get; }

        public CTypeReference(string cTypeReference)
        {
            IsPointer = cTypeReference.Contains("*") || cTypeReference == "gpointer";
            IsConst = cTypeReference.Contains("const ") || cTypeReference.Contains(" const");
            IsVolatile = cTypeReference.Contains("volatile ");

            cTypeReference = cTypeReference
                .Replace("*", "")
                .Replace("const ", "")
                .Replace(" const", "")
                .Replace("volatile ", "");

            CType = cTypeReference;
        }

        public override string ToString()
            => CType;
    }
}
