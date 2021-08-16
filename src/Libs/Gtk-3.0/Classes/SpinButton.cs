namespace Gtk
{
    public partial class SpinButton
    {
        public static SpinButton NewWithRange(double min, double max, double step)
            => new SpinButton(Native.SpinButton.Instance.Methods.NewWithRange(min, max, step), false);
    }
}
