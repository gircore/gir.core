namespace Gir.Output.Model
{
    public class Pointer : Type
    {
        public Pointer(string nativeName) : base(nativeName, "IntPtr") { }
    }
}
