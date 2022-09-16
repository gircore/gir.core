using System.Drawing;

namespace Gtk.Extensions.DependencyInjection;

public partial class ApplicationBuilder
{
    private class WindowSettings
    {
        public string? Title { get; set; }
        public Size? Size { get; set; }
    }

    public ApplicationBuilder SetWindowTitle(string? title)
    {
        _windowSettings.Title = title;
        return this;
    }

    public ApplicationBuilder SetWindowDefaultSize(Size? size)
    {
        _windowSettings.Size = size;
        return this;
    }
}
