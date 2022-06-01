using System;

namespace Cairo
{
    public partial class FontFace
    {
        public Status Status => Internal.FontFace.Status(Handle);
        public FontType FontType => Internal.FontFace.GetType(Handle);
    }
}
