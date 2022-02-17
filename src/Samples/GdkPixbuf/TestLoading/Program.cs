using System;
using GdkPixbuf;

Console.WriteLine("Testing Pixbuf");

try
{
    var pixbuf = Pixbuf.NewFromFile("test.bmp");

    Console.WriteLine("Loaded Pixbuf at address " + pixbuf.Handle);
    Console.WriteLine("Width: " + pixbuf.Width);
    Console.WriteLine("Height: " + pixbuf.Height);
    Console.WriteLine("Has Alpha: " + pixbuf.HasAlpha);
    Console.WriteLine("Channels: " + pixbuf.NChannels);

    Console.WriteLine("Done!");
}
catch (Exception e)
{
    Console.WriteLine($"Test Failed: {e.Message}");
}
