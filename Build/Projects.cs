using System;
using Generator;

namespace Build
{
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
        private const string GTK3_MINIMAL_SAMPLE = SAMPLE + "Gtk3/GtkMinimal";
        private const string GTK3_QUICKSTART = SAMPLE + "Gtk3/QuickStart";
        private const string GTK3_COMPOSITE_TEMPLATE = SAMPLE + "Gtk3/Template";
        private const string GTK4_SIMPLE_WINDOW_SAMPLE = SAMPLE + "Gtk4/SimpleWindow/";

        #endregion
        
        #region Fields
        
        public static readonly string[] TestProjects =
        {
            BUILD_Test,
            GOBJECT_TEST
        };

        public static readonly string[] SampleProjects =
        {
            DBUS_SAMPLE, GSTREAMER_SAMPLE,
            //GTK3_APP_SAMPLE,
            GTK3_MINIMAL_SAMPLE, 
            GTK3_QUICKSTART,
            GTK3_COMPOSITE_TEMPLATE,
            //GTK4_SIMPLE_WINDOW_SAMPLE
        };

        public static readonly (Project Project, Type Type)[] LibraryProjects =
        {
            (new Project(GLIB, "GLib-2.0.gir"), typeof(GLibGenerator)),
            (new Project(GOBJECT, "GObject-2.0.gir"), typeof(GObjectGenerator)),
            (new Project(GIO, "Gio-2.0.gir"), typeof(GObjectGenerator)),
            (new Project(CAIRO, "cairo-1.0.gir"), typeof(GObjectGenerator)),
            //(new Project(XLIB, "xlib-2.0.gir"), typeof(GObjectGenerator)),
            (new Project(PANGO, "Pango-1.0.gir"), typeof(GObjectGenerator)),
            //(new Project(CLUTTER, "Clutter-1.0.gir"), typeof(GObjectGenerator)),
            (new Project(GDK3, "Gdk-3.0.gir"), typeof(GObjectGenerator)),
            (new Project(GDK_PIXBUF, "GdkPixbuf-2.0.gir"), typeof(GObjectGenerator)),
            (new Project(GTK3, "Gtk-3.0.gir"), typeof(GObjectGenerator)),
            /*(JAVASCRIPT_CORE, JAVASCRIPT_CORE_GIR, "javascriptcoregtk-4.0.so", false),
            (HANDY, HANDY_GIR, "libhandy-0.0.so.0", false),
            (WEBKITGTK, WEBKITGTK_GIR, "libwebkit2gtk-4.0.so.37", true),
            (WEBKIT2WEBEXTENSION, WEBKIT2WEBEXTENSION_GIR, "WEBEXTENSION", true),
            (GTKCLUTTER, "GtkClutter-1.0.gir", "libclutter-gtk-1.0.so.0", false),
            (CHAMPLAIN, "Champlain-0.12.gir", "libchamplain-0.12", false),
            (GTKCHAMPLAIN, "GtkChamplain-0.12.gir", "libchamplain-gtk-0.12.so.0", false),*/
            (new Project(GST, "Gst-1.0.gir"), typeof(GObjectGenerator)),
            (new Project(GST_AUDIO, "GstAudio-1.0.gir"), typeof(GObjectGenerator)),
            (new Project(GST_VIDEO, "GstVideo-1.0.gir"), typeof(GObjectGenerator)),
            (new Project(GST_PBUTILS, "GstPbutils-1.0.gir"), typeof(GObjectGenerator)),
            (new Project(GST_BASE, "GstBase-1.0.gir"), typeof(GObjectGenerator))
            /*(GDK4, "Gdk-4.0.gir", "libgtk-4.so.0", true),//GTK4
            (GSK4, "Gsk-4.0.gir", "libgtk-4.so.0", true),//GTK4
            (GTK4, GTK4_GIR, "libgtk-4.so.0", true) //GTK4*/
        };
        
        #endregion
    }
}
