using static Bullseye.Targets;
using static SimpleExec.Command;
using static Targets;
using static Commands;
using static Projects;

class Program
{
    private static string configuration = confDebug;
    private const string confRelease = "Release";
    private const string confDebug = "Debug";

    static void Main(string[] args)
    {
        Target(generate_wrapper, 
            ForEach(
                CWRAPPER, 
                GLIB_WRAPPER,
                CAIRO_WRAPPER,
                XLIB_WRAPPER,
                PANGO_WRAPPER,
                GDK_WRAPPER,
                GIO_WRAPPER,
                GOBJECT_WRAPPER,
                GDK_PIXBUF_WRAPPER,
                GTK_WRAPPER),
            project => GenerateProject(project)
        );

        Target(build_gdkpixbuf_core, DependsOn(generate_wrapper), () => DotNetBuild(GDK_PIXBUF_CORE));

        Target(build_gtk_core, DependsOn(generate_wrapper), () => DotNetBuild(GTK_CORE));

        Target(build_handy_core, DependsOn(build_gtk_core), () => {
            GenerateProject(HANDY_WRAPPER);
            DotNetBuild(HANDY_CORE);
        });

        Target(build_webkitgtk_core, DependsOn(generate_wrapper), () => {
            GenerateProject(WEBKITGTK_WRAPPER);
            GenerateProject(JAVASCRIPT_CORE_WRAPPER);
            
            DotNetBuild(WEBKITGTK_CORE);
        });

        Target(build_webkit2webextensions_core, () => {
            GenerateProject(WEBKIT2WEBEXTENSION_WRAPPER);
            DotNetBuild(WEBKIT2WEBEXTENSION_CORE);
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
            (project) => DotNetClean(project));
        
        Target(release, () => configuration = confRelease);
        Target(debug, () => configuration = confDebug);

        Target("default", DependsOn(Targets.build));
        RunTargetsAndExit(args);
    }

    private static void DotNetBuild(string project) {
        Run(dotnet,$"{restore} {project}");
        Run(dotnet, $"{Commands.build} --no-restore --nologo -c {configuration} {project}");
    } 
    private static void DotNetRun(string project) => Run(dotnet, $"{run} -c {configuration} {project}", project);
    private static void DotNetClean(string project) => Run(dotnet, $"{clean} --nologo -c {configuration}", project);

    private static void GenerateProject(string path)
    {
        path += "Generate/";

        DotNetBuild(path);
        DotNetRun(path);
    }
}