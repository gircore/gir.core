using GObject;

namespace Gtk
{
    public partial class Application
    {
        #region Constructors

        public Application() : base(owned: true) { }

        //TODO
        //public Application(string applicationId)
        //    : this(ConstructArgument.With(ApplicationIdProperty, applicationId)) { }

        #endregion
    }
}
