using GObject;

namespace Gtk
{
    public partial class ApplicationWindow
    {
        #region Constructors
        public ApplicationWindow(Application application) 
            : this(ConstructParameter.With(ApplicationProperty, application)) { }
        
        #endregion
    }
}
