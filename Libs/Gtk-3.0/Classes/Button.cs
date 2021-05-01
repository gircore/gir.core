using GObject;

namespace Gtk
{
    public partial class Button
    {
        /// <summary>
        /// Creates a new instance of <see cref="Button"/>.
        /// </summary>
        /// <param name="label">The button text.</param>
        public Button(string label)
            : this(ConstructArgument.With("label", label))
        {
        }
    }
}
