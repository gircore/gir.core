using static Bullseye.Targets;
using static SimpleExec.Command;
using static Targets;
using static Commands;
using static Projects;
using System;

class Program
{
    private static string configuration = "Debug";
    private static string? NUGET_API_KEY = "";
    private const string release = "Release";

    static void Main(string[] args)
    {
        Environment.SetEnvironmentVariable("MSBUILDSINGLELOADCONTEXT", "1");
        NUGET_API_KEY = Environment.GetEnvironmentVariable("NUGET_API_KEY");

        Target(use_release_configuration, () =>{
            configuration = release;
        });

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

        Target(build_core_projects, DependsOn(build_gdkpixbuf_core, build_handy_core, build_gtk_core, build_webkitgtk_core, build_webkit2webextensions_core));

        Target(publish, DependsOn(use_release_configuration, build_core_projects),
            ForEach(
                GLIB_WRAPPER,
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
            (project) => {
                DotnetPack(project);
                DotNetNugetPush(project);
        });

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

        Target(clean_release, DependsOn(use_release_configuration, clean));

        Target("default", DependsOn(build_core_projects));
        RunTargetsAndExit(args);
    }

    private static string GetDisableParallel() => configuration switch
    {
        release => " --disable-parallel",
        _ => ""
    };

    private static void DotNetNugetPush(string project) => Run(dotnet, $@"{nuget} {push} ""bin/{configuration}/*.nupkg"" -s https://api.nuget.org/v3/index.json -k {NUGET_API_KEY} --skip-duplicate", project);
    private static void DotnetPack(string project) => Run(dotnet, $"{pack} --nologo --no-build -c {configuration}", project);
    private static void DotNetBuild(string project) {
        Run(dotnet,$"{restore}{GetDisableParallel()} {project}");
        Run(dotnet, $"{build} --no-restore --nologo -c {configuration}{GetDisableParallel()} {project}");
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