using static Bullseye.Targets;
using static Targets;
using static DotNet;
using static Projects;
using Gir;

class Program
{
    private static string configuration = confDebug;
    private const string confRelease = "Release";
    private const string confDebug = "Debug";

    static void Main(string[] args)
    {
        Target<(string project, string girFile, string import)>(generate_wrapper, 
            ForEach(
                (CWRAPPER, "", ""), 
                (GLIB_WRAPPER, "GLib-2.0.gir", "libglib-2.0.so.0"),
                (CAIRO_WRAPPER, "cairo-1.0.gir", "TODO"),
                (XLIB_WRAPPER, "xlib-2.0.gir", "TODO"),
                (PANGO_WRAPPER, "Pango-1.0.gir", "TODO"),
                (GDK_WRAPPER, "Gdk-3.0.gir", "TODO"),
                (GIO_WRAPPER, "Gio-2.0.gir", "libgio-2.0.so.0"),
                (GOBJECT_WRAPPER, "GObject-2.0.gir", "libgobject-2.0.so.0"),
                (GDK_PIXBUF_WRAPPER, "GdkPixbuf-2.0.gir", "libgdk_pixbuf-2.0.so.0"),
                (GTK_WRAPPER, "Gtk-3.0.gir", "libgtk-3.so.0")),
            (x) => GenerateAndBuildProject(x.project, x.girFile, x.import)
        );

        Target(build_gdkpixbuf_core, DependsOn(generate_wrapper), () => Build(GDK_PIXBUF_CORE, configuration));

        Target(build_gtk_core, DependsOn(generate_wrapper), () => Build(GTK_CORE, configuration));

        Target(build_handy_core, DependsOn(build_gtk_core), () => {
            GenerateAndBuildProject(HANDY_WRAPPER);
            Build(HANDY_CORE, configuration);
        });

        Target(build_webkitgtk_core, DependsOn(generate_wrapper), () => {
            GenerateAndBuildProject(WEBKITGTK_WRAPPER);
            GenerateAndBuildProject(JAVASCRIPT_CORE_WRAPPER);
            
            Build(WEBKITGTK_CORE, configuration);
        });

        Target(build_webkit2webextensions_core, () => {
            GenerateAndBuildProject(WEBKIT2WEBEXTENSION_WRAPPER);
            Build(WEBKIT2WEBEXTENSION_CORE, configuration);
        });

        Target(Targets.build, DependsOn(build_gdkpixbuf_core, build_handy_core, build_gtk_core, build_webkitgtk_core, build_webkit2webextensions_core));

        Target(clean, ForEach(GLIB_WRAPPER,
                CAIRO_WRAPPER,
                XLIB_WRAPPER,
                PANGO_WRAPPER,
                GDK_WRAPPER,
                GIO_WRAPPER,
                GOBJECT_WRAPPER,
                GDK_PIXBUF_WRAPPER,
                GTK_WRAPPER,
                GOBJECT_CORE,
                GDK_PIXBUF_CORE,
                GLIB_CORE,
                GIO_CORE,
                GTK_CORE,
                HANDY_CORE,
                WEBKITGTK_CORE,
                JAVASCRIPT_CORE_CORE),
            (project) => Clean(project, configuration));
        
        Target(release, () => configuration = confRelease);
        Target(debug, () => configuration = confDebug);

        Target("default", DependsOn(Targets.build));
        RunTargetsAndExit(args);
    }

    private static void GenerateAndBuildProject(string project, string girFile, string import)
    {
        var girPath = $"../gir-files/{girFile}";
        var outputDir = project + "Generated";

        GenerateProject(outputDir, girPath, import);
        Build(project, configuration);
    }

    private static void GenerateProject(string outputDir, string girFile, string import)
    {
        var girWrapper = new GirCWrapper(girFile, outputDir, $"\"{import}\"", "../gir-files/GLib-2.0.gir");
        girWrapper.CreateClasses();
        girWrapper.CreateInterfaces();
        girWrapper.CreateEnums();
        girWrapper.CreateStructs();
        girWrapper.CreateDelegates();
        girWrapper.CreateMethods();
    }
}