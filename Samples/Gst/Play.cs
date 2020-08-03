using Gst;

namespace Sample
{
    public class Gst
    {
        public static void Play()
        {
            Application.Init();
            var ret = Parse.Launch("playbin uri=playbin uri=http://ftp.halifax.rwth-aachen.de/blender/demo/movies/ToS/tears_of_steel_720p.mov");
            ret.SetState(State.Playing);
            var bus = ret.GetBus();
            bus.WaitForEndOrError();
            ret.SetState(State.Null);
        }
    }
}