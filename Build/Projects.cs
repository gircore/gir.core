using System;
using System.Collections.Generic;
using System.Linq;

namespace Build
{
    // TODO: Instead of declaring the folder here, maybe we should use LoadedProject?
    // Alternatively, we might want to choose where certain projects are generated,
    // e.g. to put GLib and GObject in a separate dedicated folder.
    public record Project(string GirFile, string Folder);
    
    public static class Projects
    {
        #region Constants

        private const string GTK4_GIR = "Gtk-4.0.gir";
        private const string HANDY_GIR = "Handy-0.0.gir";
        private const string WEBKITGTK_GIR = "WebKit2-4.0.gir";
        private const string WEBKIT2WEBEXTENSION_GIR = "WebKit2WebExtension-4.0.gir";
        private const string JAVASCRIPT_CORE_GIR = "JavaScriptCore-4.0.gir";

        private const string GTK3 = "../Libs/Gtk3/";
        private const string GTK4 = "../Libs/Gtk4/";
        private const string GDK4 = "../Libs/Gdk4/";
        private const string GSK4 = "../Libs/Gsk4/";
        private const string HANDY = "../Libs/Handy/";
        private const string WEBKITGTK = "../Libs/WebKitGTK/";
        private const string WEBKIT2WEBEXTENSION = "../Libs/WebKit2WebExtension/";
        private const string GIO = "../Libs/Gio/";
        private const string GOBJECT = "../Libs/GObject/";
        private const string GLIB = "../Libs/GLib/";
        private const string JAVASCRIPT_CORE = "../Libs/JavascriptCore/";
        private const string GDK_PIXBUF = "../Libs/GdkPixbuf/";
        private const string CHAMPLAIN = "../Libs/Champlain/";
        private const string GTKCHAMPLAIN = "../Libs/GtkChamplain/";
        private const string CLUTTER = "../Libs/Clutter/";
        private const string GTKCLUTTER = "../Libs/GtkClutter/";
        private const string CAIRO = "../Libs/Cairo/";
        private const string GDK3 = "../Libs/Gdk3/";
        private const string PANGO = "../Libs/Pango/";
        private const string XLIB = "../Libs/Xlib/";
        
        public const string GST = "../Libs/Gst/";
        public const string GST_BASE = "../Libs/Gst.Base/";
        public const string GST_AUDIO = "../Libs/Gst.Audio/";
        public const string GST_VIDEO = "../Libs/Gst.Video/";
        public const string GST_PBUTILS = "../Libs/Gst.Pbutils/";

        private const string BUILD_Test = "../Tests/Build.Tests/";
        private const string GOBJECT_TEST = "../Tests/Libs/GObject.Tests/";

        private const string SAMPLE = "../Samples/";
        private const string DBUS_SAMPLE = SAMPLE + "DBus/";
        private const string GSTREAMER_SAMPLE = SAMPLE + "GStreamer/";
        private const string GTK3_APP_SAMPLE = SAMPLE + "Gtk3/GtkApp/";
        private const string GTK3_BUILDER_SAMPLE = SAMPLE + "Gtk3/Builder";
        private const string GTK3_QUICKSTART = SAMPLE + "Gtk3/QuickStart";
        private const string GTK3_COMPOSITE_TEMPLATE_SOURCEGENERATOR = SAMPLE + "Gtk3/CompositeTemplates/UsingSourceGenerator";
        private const string GTK3_COMPOSITE_TEMPLATE_NO_SOURCEGENERATOR = SAMPLE + "Gtk3/CompositeTemplates/NoSourceGenerator";
        private const string GTK4_SIMPLE_WINDOW_SAMPLE = SAMPLE + "Gtk4/SimpleWindow/";
        private const string GDKPIXBUF_TEST_MEMORY_LEAKS = SAMPLE + "GdkPixbuf/TestMemoryLeaks";

        private const string INTEGRATION = "../Integration/";
        
        #endregion
        
        #region Fields

        public static readonly string[] IntegrationProjects =
        {
            INTEGRATION
        };
        
        public static readonly string[] TestProjects =
        {
            BUILD_Test,
            GOBJECT_TEST
        };

        public static readonly string[] SampleProjects =
        {
            DBUS_SAMPLE, 
            GSTREAMER_SAMPLE,
            //GTK3_APP_SAMPLE,
            GTK3_BUILDER_SAMPLE, 
            GTK3_QUICKSTART,
            GTK3_COMPOSITE_TEMPLATE_SOURCEGENERATOR,
            GTK3_COMPOSITE_TEMPLATE_NO_SOURCEGENERATOR,
            //GTK4_SIMPLE_WINDOW_SAMPLE
            GDKPIXBUF_TEST_MEMORY_LEAKS
        };

        public const string ProjectPath = "../Libs";
        public const string GirPath = "../gir-files";

        // TODO: Special-Case GLib and GObject
        public static readonly IEnumerable<Project> Base = new[]
        {
            new Project($"{GirPath}/GLib-2.0.gir", $"{ProjectPath}/GLib-2.0"),
            new Project($"{GirPath}/GObject-2.0.gir", $"{ProjectPath}/GObject-2.0"),
            new Project($"{GirPath}/Gio-2.0.gir", $"{ProjectPath}/Gio-2.0")
        };

        public static readonly IEnumerable<Project> Gtk3 = new[]
        {
            new Project($"{GirPath}/cairo-1.0.gir", $"{ProjectPath}/cairo-1.0"),
            new Project($"{GirPath}/Pango-1.0.gir", $"{ProjectPath}/Pango-1.0"),
            new Project($"{GirPath}/Gdk-3.0.gir", $"{ProjectPath}/Gdk-3.0"),
            new Project($"{GirPath}/GdkPixbuf-2.0.gir", $"{ProjectPath}/GdkPixbuf-2.0"),
            new Project($"{GirPath}/Gtk-3.0.gir", $"{ProjectPath}/Gtk-3.0")
        };
        
        public static readonly IEnumerable<Project> GStreamer = new[]
        {
            new Project($"{GirPath}/Gst-1.0.gir", $"{ProjectPath}/Gst-1.0"),
            new Project($"{GirPath}/GstAudio-1.0.gir", $"{ProjectPath}/GstAudio-1.0"),
            new Project($"{GirPath}/GstVideo-1.0.gir", $"{ProjectPath}/GstVideo-1.0"),
            new Project($"{GirPath}/GstPbutils-1.0.gir", $"{ProjectPath}/GstPbutils-1.0"),
            new Project($"{GirPath}/GstBase-1.0.gir", $"{ProjectPath}/GstBase-1.0")
        };
        
        public static readonly IEnumerable<Project> Misc = new[]
        {
            new Project($"{GirPath}/Handy-0.0.gir", $"{ProjectPath}/Handy-0.0")
            /*(JAVASCRIPT_CORE, JAVASCRIPT_CORE_GIR, "javascriptcoregtk-4.0.so", false),
            (WEBKITGTK, WEBKITGTK_GIR, "libwebkit2gtk-4.0.so.37", true),
            (WEBKIT2WEBEXTENSION, WEBKIT2WEBEXTENSION_GIR, "WEBEXTENSION", true),
            (GTKCLUTTER, "GtkClutter-1.0.gir", "libclutter-gtk-1.0.so.0", false),
            (CHAMPLAIN, "Champlain-0.12.gir", "libchamplain-0.12", false),
            (GTKCHAMPLAIN, "GtkChamplain-0.12.gir", "libchamplain-gtk-0.12.so.0", false),*/
        };

        public static IEnumerable<Project> AllLibraries
            => Base.Concat(Gtk3)
                .Concat(GStreamer);
                //.Concat(Misc);

        #endregion
    }
}
