
using System;
using System.IO;
using System.Reflection;

namespace GdkPixbuf.Core
{
    public partial class GPixbufLoader : GObject.Core.GObject
    {
        public GPixbufLoader() : this(GdkPixbuf.PixbufLoader.@new()) {}
        internal GPixbufLoader(IntPtr handle) : base(handle) { }

        public bool Write(string imageResourceName) => Write(imageResourceName, Assembly.GetCallingAssembly());
        internal bool Write(string imageResourceName, Assembly assembly)
        {
            using var stream = assembly.GetManifestResourceStream(imageResourceName);

            if(stream is null)
                throw new Exception ($"Cannot get image resource '{imageResourceName}'");

            using var ms = new MemoryStream();
            stream.CopyTo(ms);
            var buffer = ms.ToArray();
            
            return GdkPixbuf.PixbufLoader.write(this, buffer, (ulong)buffer.LongLength, out var error);
        }

        private Pixbuf? pixbuf;
        public Pixbuf? GetPixbuf()
        {
            if(pixbuf is null)
            {
                var ret = GdkPixbuf.PixbufLoader.get_pixbuf(this);
                
                if(ret != IntPtr.Zero)
                    pixbuf = new Pixbuf(ret);
            }

            return pixbuf;
        } 
    }
}