using GObject;

namespace Gst
{
    public partial class Pipeline
    {
        public Pipeline(string name) : this(ConstructParameter.With(NameProperty, name)) { }
    }
}
