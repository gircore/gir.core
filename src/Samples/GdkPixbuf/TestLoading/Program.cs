using System;
using System.Diagnostics;
using GdkPixbuf;

Console.WriteLine("Testing Pixbuf");

try
{
    var pixbuf = Pixbuf.NewFromFile("test.bmp");

    Console.WriteLine("Loaded Pixbuf at address " + pixbuf.Handle);
    Console.WriteLine("Width: " + pixbuf.GetWidth());
    Console.WriteLine("Height: " + pixbuf.GetHeight());
    Console.WriteLine("Has Alpha: " + pixbuf.GetHasAlpha());
    Console.WriteLine("Channels: " + pixbuf.GetNChannels());

    Console.WriteLine("Done!");
}
catch (Exception e)
{
    Console.WriteLine($"Test Failed: {e.Message}");
}
