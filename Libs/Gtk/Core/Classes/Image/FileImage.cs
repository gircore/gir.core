using System;

namespace Gtk.Core
{
    public class FileImage : GImage
    {
        public FileImage(string fileName) : this(Gtk.Image.new_from_file(fileName)) {}
        internal FileImage(IntPtr handle) : base(handle) { }
    }
}