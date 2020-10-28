using Gst;

namespace Sample
{
    public class Gst
    {
        public static void Play()
        {
            Application.Init();
            Element ret = Parse.Launch("playbin uri=playbin uri=http://ftp.halifax.rwth-aachen.de/blender/demo/movies/ToS/tears_of_steel_720p.mov");
            ret.SetState(State.Playing);
            Bus bus = ret.GetBus();
            bus.WaitForEndOrError();
            ret.SetState(State.@null);
        }
    }
}
