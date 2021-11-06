using System.Collections.Generic;
using System.IO;

namespace Build
{
    // TODO: Instead of declaring the folder here, maybe we should use LoadedProject?
    // Alternatively, we might want to choose where certain projects are generated,
    // e.g. to put GLib and GObject in a separate dedicated folder.
    public record Project(string GirFile, string Folder)
    {
        public static Project FromName(string name)
        {
            var file = Path.Combine(Projects.GirPath, $"{name}.gir");
            var folder = Path.Combine(Projects.ProjectPath, name);

            return new Project(file, folder);
        }
    }

    public static class Projects
    {
        #region Constants

        public const string ProjectPath = "../../Libs";
        public const string GirPath = "../../../ext/gir-files";

        public const string SolutionDirectory = "../../";

        private const string SAMPLE = "../../Samples/";

        private const string DBUS_SAMPLE = SAMPLE + "DBus/";
        private const string GSTREAMER_SAMPLE = SAMPLE + "GStreamer/";
        private const string GDK_PIXBUF_TEST_LOADING = SAMPLE + "GdkPixbuf/TestLoading";
        private const string GDK_PIXBUF_TEST_MEMORY_LEAKS = SAMPLE + "GdkPixbuf/TestMemoryLeaks";
        private const string GTK3_WINDOW = SAMPLE + "Gtk3/Window";
        private const string GTK3_DECLARATIVE_UI = SAMPLE + "Gtk3/DeclarativeUi";
        private const string GTK3_ABOUT_DIALOG = SAMPLE + "Gtk3/AboutDialog";
        private const string GTK3_QUICKSTART = SAMPLE + "Gtk3/QuickStart";
        private const string GTK3_DRAWINGAREA = SAMPLE + "Gtk3/DrawingArea";
        private const string GTK3_TEXTEDITOR = SAMPLE + "Gtk3/TextEditor";

        // private const string DBUS_SAMPLE = SAMPLE + "DBus/";
        // private const string GSTREAMER_SAMPLE = SAMPLE + "GStreamer/";
        // private const string GTK3_APP_SAMPLE = SAMPLE + "Gtk3/GtkApp/";
        // private const string GTK3_BUILDER_SAMPLE = SAMPLE + "Gtk3/Builder";

        // private const string GTK3_COMPOSITE_TEMPLATE_SOURCEGENERATOR = SAMPLE + "Gtk3/CompositeTemplates/UsingSourceGenerator";
        // private const string GTK3_COMPOSITE_TEMPLATE_NO_SOURCEGENERATOR = SAMPLE + "Gtk3/CompositeTemplates/NoSourceGenerator";
        // private const string GTK4_SIMPLE_WINDOW_SAMPLE = SAMPLE + "Gtk4/SimpleWindow/";

        private const string INTEGRATION = "../../Integration/";

        #endregion

        #region Fields

        public static readonly string[] IntegrationProjects =
        {
            INTEGRATION
        };

        public static readonly string[] SampleProjects =
        {
            DBUS_SAMPLE,
            GSTREAMER_SAMPLE,
            GDK_PIXBUF_TEST_LOADING,
            GDK_PIXBUF_TEST_MEMORY_LEAKS,
            GTK3_WINDOW,
            GTK3_DECLARATIVE_UI,
            GTK3_ABOUT_DIALOG,
            GTK3_QUICKSTART,
            GTK3_DRAWINGAREA,
            GTK3_TEXTEDITOR
        };

        public static readonly Project[] AllLibraries = new[]
        {
            // Runtime
            Project.FromName("GLib-2.0"),
            Project.FromName("GObject-2.0"),
            Project.FromName("Gio-2.0"),
            
            // GTK Stack
            Project.FromName("cairo-1.0"),
            Project.FromName("Pango-1.0"),
            Project.FromName("Gdk-3.0"),
            Project.FromName("GdkPixbuf-2.0"),
            Project.FromName("Atk-1.0"),
            Project.FromName("Gtk-3.0"),
            
            // GStreamer Stack
            Project.FromName("Gst-1.0"),
            Project.FromName("GstBase-1.0"),
            Project.FromName("GstAudio-1.0"),
            Project.FromName("GstVideo-1.0"),
            Project.FromName("GstPbutils-1.0"),
        };

        #endregion
    }
}
