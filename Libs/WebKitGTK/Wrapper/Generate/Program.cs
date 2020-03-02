using Gir;

namespace Generate
{
    class Program
    {
        static void Main(string[] args)
        {
            var girWrapper = new GirCWrapper("../../../../gir-files/WebKit2-4.0.gir", "../Generated/", "\"libwebkit2gtk-4.0.so.37\"", "../../../../gir-files/GLib-2.0.gir");
            girWrapper.CreateClasses();
            girWrapper.CreateInterfaces();
            girWrapper.CreateEnums();
            girWrapper.CreateStructs();
            girWrapper.CreateDelegates();
            girWrapper.CreateMethods();
        }
    }
}
