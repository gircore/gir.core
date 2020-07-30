using static Bullseye.Targets;
using static Targets;
using static DotNet;
using static Projects;
using System.Collections.Generic;
using System.IO;

class Program
{
    private static string configuration = confDebug;
    private const string confRelease = "Release";
    private const string confDebug = "Debug";

    private static string[] allProjects = {
        GLIB_WRAPPER,
        CAIRO_WRAPPER,
        XLIB_WRAPPER,
        PANGO_WRAPPER,
        GDK3_WRAPPER,
        GIO_WRAPPER,
        GOBJECT_WRAPPER,
        GDK_PIXBUF_WRAPPER,
        GTK3_WRAPPER,
        WEBKIT2WEBEXTENSION_WRAPPER,
        CLUTTER_WRAPPER,
        GTKCLUTTER_WRAPPER,
        CHAMPLAIN_WRAPPER,
        GTKCHAMPLAIN_WRAPPER,
        GST_WRAPPER,
        GDK4_WRAPPER, //GTK4
        GSK4_WRAPPER, //GTK4
        GTK4_WRAPPER, //GTK4
        GOBJECT_CORE,
        GDK_PIXBUF_CORE,
        GLIB_CORE,
        GIO_CORE,
        GTK3_CORE,
        GTK4_CORE,
        HANDY_CORE,
        WEBKITGTK_CORE,
        JAVASCRIPT_CORE_CORE,
        WEBKIT2WEBEXTENSION_CORE,
        CLUTTER_CORE,
        GTKCLUTTER_CORE,
        CHAMPLAIN_CORE,
        GTKCHAMPLAIN_CORE,
        GST_CORE
    };

    static void Main(string[] args)
    {        
        Target<(string project, string girFile, string import, bool addAlias)>(generate, 
            ForEach(
                (GLIB_WRAPPER, "GLib-2.0.gir", "libglib-2.0.so.0", false),
                (GOBJECT_WRAPPER, "GObject-2.0.gir", "libgobject-2.0.so.0", true),
                (GIO_WRAPPER, "Gio-2.0.gir", "libgio-2.0.so.0", true),
                (CAIRO_WRAPPER, "cairo-1.0.gir", "TODO", false),
                (XLIB_WRAPPER, "xlib-2.0.gir", "TODO", false),
                (PANGO_WRAPPER, "Pango-1.0.gir", "TODO", false),
                (GDK3_WRAPPER, "Gdk-3.0.gir", "TODO", true),
                (GDK_PIXBUF_WRAPPER, "GdkPixbuf-2.0.gir", "libgdk_pixbuf-2.0.so.0", true),
                (GTK3_WRAPPER, "Gtk-3.0.gir", "libgtk-3.so.0", true),
                (JAVASCRIPT_CORE_WRAPPER, "JavaScriptCore-4.0.gir", "javascriptcoregtk-4.0.so", false),
                (HANDY_WRAPPER, "Handy-0.0.gir", "libhandy-0.0.so.0", false),
                (WEBKITGTK_WRAPPER, "WebKit2-4.0.gir", "libwebkit2gtk-4.0.so.37", true),
                (WEBKIT2WEBEXTENSION_WRAPPER, "WebKit2WebExtension-4.0.gir", "WEBEXTENSION", true),
                (CLUTTER_WRAPPER, "Clutter-1.0.gir", "libclutter-1.0.so.0", false),
                (GTKCLUTTER_WRAPPER, "GtkClutter-1.0.gir", "libclutter-gtk-1.0.so.0", false),
                (CHAMPLAIN_WRAPPER, "Champlain-0.12.gir", "libchamplain-0.12", false),
                (GTKCHAMPLAIN_WRAPPER, "GtkChamplain-0.12.gir", "libchamplain-gtk-0.12.so.0", false),
                (GST_WRAPPER, "Gst-1.0.gir", "libgstreamer-1.0.so.0", true),
                (GDK4_WRAPPER, "Gdk-4.0.gir", "libgtk-4.so.0", true),//GTK4
                (GSK4_WRAPPER, "Gsk-4.0.gir", "libgtk-4.so.0", true),//GTK4
                (GTK4_WRAPPER, "Gtk-4.0.gir", "libgtk-4.so.0", true) //GTK4
                ),
            (x) => Generate(x.project, x.girFile, x.import, x.addAlias)
        );

        Target<string>(Targets.build, DependsOn(generate),
            ForEach(allProjects),
            (project) => Build(project, configuration)
        );

        Target(Targets.clean, 
            ForEach(allProjects), 
            (project) => CleanUp(project, configuration)
        );
        
        Target(Targets.release, () => configuration = confRelease);
        Target(Targets.debug, () => configuration = confDebug);

        Target("default", DependsOn(Targets.build));
        RunTargetsAndExit(args);
    }

    private static void CleanUp(string project, string configuration)
    {
        if(project.EndsWith("Wrapper/"))
            Directory.Delete(project + "Generated", true);

        Clean(project, configuration);
    }

    private static void Generate(string project, string girFile, string import, bool addGlibAliases)
    {
        girFile = $"../gir-files/{girFile}";
        var outputDir = project + "Generated";

        var list = new List<string>();
        if(addGlibAliases)
            list.Add("../gir-files/GLib-2.0.gir");

        var g = new Generator.Generator(girFile, outputDir, import, list);
        g.Generate();
    }
}