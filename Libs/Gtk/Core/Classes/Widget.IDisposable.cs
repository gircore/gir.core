namespace Gtk.Core
{
    public partial class GWidget
    {
        protected override void Dispose(bool disposing)
        {
            if (!Disposed)
            {
                
                if(typeof(GWindow).IsAssignableFrom(GetType()))
                {
                    Gtk.Widget.destroy(this);
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