using Gir;

namespace Generate
{
    class Program
    {
        static void Main(string[] args)
        {
            var girWrapper = new GirCWrapper("../../../../gir-files/Gtk-3.0.gir", "../Generated/", "\"libgtk-3.so.0\"", "../../../../gir-files/GLib-2.0.gir");
            girWrapper.CreateClasses();
            girWrapper.CreateInterfaces();
            girWrapper.CreateEnums();
            girWrapper.CreateStructs();
            girWrapper.CreateDelegates();
            girWrapper.CreateMethods();
        }
    }
}
