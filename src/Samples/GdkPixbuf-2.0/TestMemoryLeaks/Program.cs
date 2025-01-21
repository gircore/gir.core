using System;
using System.IO;
using System.Threading.Tasks;
using GdkPixbuf;
using GLib;

namespace TestMemoryLeaks;

public static class Program
{
    // This is NOT a demo of the Pixbuf library. The intended users of this sample are developers.
    //
    // This uses pixbufs to load a lot of images and discard them afterwards. It tests if the C# garbage
    // collector is freeing the managed and unmanaged resources properly. To verify it is necessary
    // to check the used system resources.

    public static void Main(string[] args)
    {
        GdkPixbuf.Module.Initialize();

        var cycles = 10000;
        var fileName = "test.bmp";

        var imageBytes = File.ReadAllBytes(fileName);
        Task[] tasks = new Task[cycles];

        Console.WriteLine("Concurrent file finalizer: Memory can go up. GC.Collect() is called in the end which must free everything up.");
        for (int i = 0; i < cycles; i++)
        {
            tasks[i] = Task.Run(() =>
            {
                FromBytes(imageBytes);
            });
        }
        Task.WaitAll(tasks);
        Done();

        Console.WriteLine("Concurrent bytes finalizer: Memory can go up. GC.Collect() is called in the end which must free everything up.");
        for (int i = 0; i < cycles; i++)
        {
            tasks[i] = Task.Run(() =>
            {
                Pixbuf.NewFromFile(fileName);
            });
        }
        Task.WaitAll(tasks);
        Done();

        Console.WriteLine("File finalizer: Memory can go up. GC.Collect() is called in the end which must free everything up.");
        for (int i = 0; i < cycles; i++)
        {
            var a = Pixbuf.NewFromFile(fileName);
        }
        Done();

        Console.WriteLine("Bytes finalizer: Memory can go up. GC.Collect() is called in the end which must free everything up.");
        for (int i = 0; i < cycles; i++)
        {
            var p = FromBytes(imageBytes);
        }
        Done();

        Console.WriteLine("File dispose: Memory should not go up as it is freed explicity via Dispose().");

        for (int i = 0; i < cycles; i++)
        {
            var a = Pixbuf.NewFromFile(fileName);
            a.Dispose();
        }
        Done();

        Console.WriteLine("Bytes dispose: Memory should not go up as it is freed explicity via Dispose().");

        for (int i = 0; i < cycles; i++)
        {
            var p = FromBytes(imageBytes);
            p.Dispose();
        }
        Done();
    }

    private static Pixbuf FromBytes(byte[] data)
    {
        using var bytes = Bytes.New(data);
        var loader = PixbufLoader.New();
        loader.WriteBytes(bytes);
        loader.Close();

        return loader.GetPixbuf() ?? throw new Exception("No Pixbuf created.");

    }

    private static void Done()
    {
        Console.WriteLine("DONE.");
        Console.ReadLine();
    }
}
