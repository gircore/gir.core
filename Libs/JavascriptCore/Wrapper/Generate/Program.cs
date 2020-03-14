using Gir;

namespace Generate
{
    class Program
    {
        static void Main(string[] args)
        {
            var girWrapper = new GirCWrapper("../../../../gir-files/JavaScriptCore-4.0.gir", "../Generated/", "\"javascriptcoregtk-4.0.so\"");
            girWrapper.CreateClasses();
            girWrapper.CreateInterfaces();
            girWrapper.CreateEnums();
            girWrapper.CreateStructs();
            girWrapper.CreateDelegates();
            girWrapper.CreateMethods();
        }
    }
}
