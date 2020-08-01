
using System;
using System.IO;
using System.Reflection;

namespace GdkPixbuf
{
    public class PixbufLoader : GObject.Object
    {
        public PixbufLoader() : this(Sys.PixbufLoader.@new()) {}
        internal PixbufLoader(IntPtr handle) : base(handle) { }

        public bool Write(string imageResourceName) => Write(imageResourceName, Assembly.GetCallingAssembly());
        internal bool Write(string imageResourceName, Assembly assembly)
        {
            using var stream = assembly.GetManifestResourceStream(imageResourceName);

            if(stream is null)
                throw new Exception ($"Cannot get image resource '{imageResourceName}'");

            using var ms = new MemoryStream();
            stream.CopyTo(ms);
            var buffer = ms.ToArray();
            
            return Sys.PixbufLoader.write(this, buffer, (ulong)buffer.LongLength, out var error);
        }

        private Pixbuf? pixbuf;
        public Pixbuf? GetPixbuf()
        {
            if (!(pixbuf is null)) 
                return pixbuf;
            
            var ret = Sys.PixbufLoader.get_pixbuf(this);
                
            if(ret != IntPtr.Zero)
                pixbuf = new Pixbuf(ret);

            return pixbuf;
        } 
    }
}