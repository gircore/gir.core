namespace Gtk
{
    public partial class Widget
    {
        protected override void Dispose(bool disposing)
        {
            if (!Disposed)
            {
                
                if(typeof(Window).IsAssignableFrom(GetType()))
                {
                    Sys.Widget.destroy(this);
                }

                if(builder is {})
                {
                    builder.Dispose();
                    builder = null;
                }

                base.Dispose(disposing);
            }
        }
    }
}