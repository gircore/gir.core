using GObject;

namespace Gtk
{
    public partial class Application
    {
        #region Constructors
        
        public Application(string applicationId) 
            : this(ConstructParameter.With(ApplicationIdProperty, applicationId)) { }
        
        #endregion
    }
}
