namespace Gtk
{
    public class Functions
    {
        public static void Init()
        {
            var argc = 0;
            Native.Functions.Init(ref argc, new string[0]);
        }

        public static void Main() => Native.Functions.Main();

        public static void MainQuit() => Native.Functions.MainQuit();
    }
}
