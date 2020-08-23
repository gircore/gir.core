namespace GObject
{
    public readonly struct Type
    {
        private readonly Sys.Type type;

        public Type(ulong type)
        {
            this.type = new Sys.Type(type);
        }
    }
}