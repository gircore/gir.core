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
        
        /// <summary>
        /// TODO: This code must be auto generated.
        /// Indexer to connect with a SignalHandler&lt;Button&gt;
        /// </summary>
        public SignalHandler<Button> this[Signal<Button> signal]
        {
            set => signal.Connect(this, value, true);
        }
    }
}
