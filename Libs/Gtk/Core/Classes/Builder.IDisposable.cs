namespace Gtk.Core
{
    internal partial class GBuilder
    {
        protected override void Dispose(bool dispsing)
        {
            if(dispsing)
            {
                objects.Clear();
            }

            base.Dispose(dispsing);
        }
    }
}