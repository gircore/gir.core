namespace GObject
{
    public class InitallyUnowned : Object
    {
        protected override void Constructed()
        {
            base.Constructed();
            Sys.Object.ref_sink(handle);
        }
    }
}