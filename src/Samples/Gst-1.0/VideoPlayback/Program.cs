using System;

namespace Samples;

public static class Program
{
    public static void Main(string[] args)
    {
        Gst.Module.Initialize();

        Console.WriteLine("Starting to play tears of steal. Please wait while file is beeing loaded...");

        Gst.Application.Init();
        Gst.Element ret = Gst.Functions.ParseLaunch("playbin uri=playbin uri=http://ftp.halifax.rwth-aachen.de/blender/demo/movies/ToS/tears_of_steel_720p.mov");
        ret.SetState(Gst.State.Playing);
        var bus = ret.GetBus();
        bus.TimedPopFiltered(Gst.Constants.CLOCK_TIME_NONE, Gst.MessageType.Eos | Gst.MessageType.Error);
        ret.SetState(Gst.State.Null);
    }
}
