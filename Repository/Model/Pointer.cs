namespace Repository.Model
{
    public class Pointer : Symbol
    {
        public Pointer(string nativeName) : base(nativeName, "IntPtr") { }
    }
}
