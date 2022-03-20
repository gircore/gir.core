namespace Cairo
{
    public partial class FontOptions
    {
        public FontOptions()
            : this(Internal.FontOptions.Create())
        {
        }

        public Status Status => Internal.FontOptions.Status(Handle);
    }
}
