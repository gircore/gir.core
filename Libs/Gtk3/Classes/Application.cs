using Gio;

namespace Gtk
{
    public partial class Application
    {
        #region Constructors
        
        public Application(string applicationId, ApplicationFlags flags = ApplicationFlags.FlagsNone) 
            : this(Native.@new(applicationId, flags)) { }
        
        #endregion
    }
}
