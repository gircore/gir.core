using System;

namespace Gtk
{
    public class FileImage : Image
    {
        public FileImage(string fileName) : this(Sys.Image.new_from_file(fileName)) {}
        internal FileImage(IntPtr handle) : base(handle) { }
    }
}