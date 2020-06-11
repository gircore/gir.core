using static Bullseye.Targets;
using static Targets;
using static DotNet;
using static Projects;
using System.Collections.Generic;

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
        GDK_WRAPPER,
        GIO_WRAPPER,
        GOBJECT_WRAPPER,
        GDK_PIXBUF_WRAPPER,
        GTK_WRAPPER,
        WEBKIT2WEBEXTENSION_WRAPPER,
        CLUTTER_WRAPPER,
        GTKCLUTTER_WRAPPER,
        CHAMPLAIN_WRAPPER,
        GTKCHAMPLAIN_WRAPPER,
        GOBJECT_CORE,
        GDK_PIXBUF_CORE,
        GLIB_CORE,
        GIO_CORE,
        GTK_CORE,
        HANDY_CORE,
        WEBKITGTK_CORE,
        JAVASCRIPT_CORE_CORE,
        WEBKIT2WEBEXTENSION_CORE,
        CLUTTER_CORE,
        GTKCLUTTER_CORE,
        CHAMPLAIN_CORE,
        GTKCHAMPLAIN_CORE
    };

    static void Main(string[] args)
    {        
        Target<(string project, string girFile, string import, bool addAlias)>(generate_wrapper, 
            ForEach(
                (GLIB_WRAPPER, "GLib-2.0.gir", "libglib-2.0.so.0", false),
                (GOBJECT_WRAPPER, "GObject-2.0.gir", "libgobject-2.0.so.0", true),
                (GIO_WRAPPER, "Gio-2.0.gir", "libgio-2.0.so.0", true),
                (CAIRO_WRAPPER, "cairo-1.0.gir", "TODO", false),
                (XLIB_WRAPPER, "xlib-2.0.gir", "TODO", false),
                (PANGO_WRAPPER, "Pango-1.0.gir", "TODO", false),
                (GDK_WRAPPER, "Gdk-3.0.gir", "TODO", true),
                (GDK_PIXBUF_WRAPPER, "GdkPixbuf-2.0.gir", "libgdk_pixbuf-2.0.so.0", true),
                (GTK_WRAPPER, "Gtk-3.0.gir", "libgtk-3.so.0", true),
                (JAVASCRIPT_CORE_WRAPPER, "JavaScriptCore-4.0.gir", "javascriptcoregtk-4.0.so", false),
                (HANDY_WRAPPER, "Handy-0.0.gir", "libhandy-0.0.so.0", false),
                (WEBKITGTK_WRAPPER, "WebKit2-4.0.gir", "libwebkit2gtk-4.0.so.37", true),
                (WEBKIT2WEBEXTENSION_WRAPPER, "WebKit2WebExtension-4.0.gir", "WEBEXTENSION", true),
                (CLUTTER_WRAPPER, "Clutter-1.0.gir", "libclutter-1.0.so", false),
                (GTKCLUTTER_WRAPPER, "GtkClutter-1.0.gir", "libclutter-gtk-1.0.so", false),
                (CHAMPLAIN_WRAPPER, "Champlain-0.12.gir", "libchamplain-0.12", false),
                (GTKCHAMPLAIN_WRAPPER, "GtkChamplain-0.12.gir", "libchamplain-gtk-0.12.so.0", false)
                ),
            (x) => GenerateAndBuildProject(x.project, x.girFile, x.import, x.addAlias)
        );

        Target(build_gdkpixbuf_core, DependsOn(generate_wrapper), () => Build(GDK_PIXBUF_CORE, configuration));
        Target(build_gtk_core, DependsOn(generate_wrapper), () => Build(GTK_CORE, configuration));
        Target(build_handy_core, DependsOn(build_gtk_core), () => Build(HANDY_CORE, configuration));
        Target(build_webkitgtk_core, DependsOn(generate_wrapper), () => Build(WEBKITGTK_CORE, configuration));
        Target(build_webkit2webextensions_core, DependsOn(generate_wrapper), () => Build(WEBKIT2WEBEXTENSION_CORE, configuration));
        Target(build_gtkclutter_core, DependsOn(generate_wrapper), () => Build(GTKCLUTTER_CORE, configuration));
        Target(build_gtkchamplain_core, DependsOn(generate_wrapper), () => Build(GTKCHAMPLAIN_CORE, configuration));

        Target(Targets.build, DependsOn(build_gdkpixbuf_core, build_handy_core, build_gtk_core, build_webkitgtk_core, build_webkit2webextensions_core, build_gtkclutter_core, build_gtkchamplain_core));
        Target(Targets.clean, ForEach(allProjects), (project) => Clean(project, configuration));
        
        Target(Targets.release, () => configuration = confRelease);
        Target(Targets.debug, () => configuration = confDebug);

        Target("default", DependsOn(Targets.build));
        RunTargetsAndExit(args);
    }

    private static void GenerateAndBuildProject(string project, string girFile, string import, bool addGlibAliases)
    {
        var girPath = $"../gir-files/{girFile}";
        var outputDir = project + "Generated";

        GenerateProject(outputDir, girPath, import, addGlibAliases);
        Build(project, configuration);
    }

    private static void GenerateProject(string outputDir, string girFile, string import, bool addGlibAliases)
    {
        var list = new List<string>();
        if(addGlibAliases)
            list.Add("../gir-files/GLib-2.0.gir");

        var g = new Generator.Generator(girFile, outputDir, import, list);
        g.Generate();
    }
}