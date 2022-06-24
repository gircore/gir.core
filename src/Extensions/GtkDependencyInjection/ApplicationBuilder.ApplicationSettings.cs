using Gio;

namespace Gtk.Extensions.DependencyInjection;

public partial class ApplicationBuilder
{
    private class ApplicationSettings
    {
        public string? Id { get; set; }
        public ApplicationFlags? Flags { get; set; }
    }

    public ApplicationBuilder SetApplicationId(string? id)
    {
        _applicationSettings.Id = id;
        return this;
    }

    public ApplicationBuilder SetApplicationFlags(ApplicationFlags? flags)
    {
        _applicationSettings.Flags = flags;
        return this;
    }
}
