using Gir.Core.Gst;

namespace Sample.Gst
{
    public class Play
    {
        public static void Start()
        {
            Application.Init();
            var ret = Parse.Launch("playbin uri=playbin uri=http://ftp.halifax.rwth-aachen.de/blender/demo/movies/ToS/tears_of_steel_720p.mov");
            ret.SetState(State.Playing);
            var bus = ret.GetBus();
            bus.TimedPopFiltered(18446744073709551615);
            ret.SetState(State.Null);
        }
    }
}