namespace GirLoader;

public static class Win32Repository
{
    public static Input.Repository Get()
    {
        return new Input.Repository
        {
            Namespace = new Input.Namespace
            {
                Name = "win32",
                Version = "1.0",
                Aliases =
                {
                    new PointerAlias("Fence", "ID3D12Fence"),
                    new PointerAlias("Resource", "ID3D12Resource"),
                    new PointerAlias("HCURSOR", "HCURSOR"),
                    new PointerAlias("MSG", "MSG"),
                    new PointerAlias("HWND", "HWND"),
                }
            }
        };
    }
}
