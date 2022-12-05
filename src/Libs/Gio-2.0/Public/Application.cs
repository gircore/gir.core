namespace Gio;

public partial class Application
{
    static Application()
    {
        Module.Initialize();
    }

    public int Run()
    {
        return Internal.Application.Run(Handle, 0, new string[0]);
    }
}
