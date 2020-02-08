using Gir;

namespace Generate
{
    class Program
    {
        static void Main(string[] args)
        {
            var girWrapper = new GirCWrapper("../../../../gir-files/GObject-2.0.gir", "../Generated/", "\"libgobject-2.0.so.0\"", "../../../../gir-files/GLib-2.0.gir");
            girWrapper.CreateClasses();
            girWrapper.CreateInterfaces();
            girWrapper.CreateEnums();
            girWrapper.CreateStructs();
            girWrapper.CreateDelegates();
            girWrapper.CreateMethods();
        }
    }
}
