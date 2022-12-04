namespace Samples;

public static class Program
{
    public static void Main(string[] args)
    {
        Gst.Module.Initialize();
        Sample.Gst.Play();
    }
}
