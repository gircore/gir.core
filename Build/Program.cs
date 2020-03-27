using static Bullseye.Targets;
using static SimpleExec.Command;
using static Targets;
using static Commands;
using static Projects;
using System;

class Program
{
    static void Main(string[] args)
    {
        Environment.SetEnvironmentVariable("MSBUILDSINGLELOADCONTEXT", "1");

        var TOKEN = args[0];
        var bullseyeArgs = args[1..];

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

        Target(build_gtk_core, DependsOn(generate_wrapper), () => {
            Run(dotnet, $"{build} {GTK_CORE}");
        });

        Target(build_handy_core, DependsOn(build_gtk_core), () => {
            GenerateProject(HANDY_WRAPPER);
            Run(dotnet, $"{build} {HANDY_CORE}");
        });

        Target(build_webkitgtk_core, DependsOn(generate_wrapper), () => {
            GenerateProject(WEBKITGTK_WRAPPER);
            GenerateProject(JAVASCRIPT_CORE_WRAPPER);
            
            Run(dotnet, $"{build} {WEBKITGTK_CORE}");
        });

        Target(build_webkit2webextensions_core, () => {
            GenerateProject(WEBKIT2WEBEXTENSION_WRAPPER);
            Run(dotnet, $"{build} {WEBKIT2WEBEXTENSION_CORE}");
        });

        Target(build_core_projects, DependsOn(build_handy_core, build_gtk_core, build_webkitgtk_core, build_webkit2webextensions_core));

        Target(publish_alpha, DependsOn(build_core_projects),
            ForEach(
                GLIB_WRAPPER,
                CAIRO_WRAPPER,
                XLIB_WRAPPER,
                PANGO_WRAPPER,
                GDK_WRAPPER,
                GIO_WRAPPER,
                GOBJECT_WRAPPER,
                GDK_PIXBUF_WRAPPER,
                GTK_WRAPPER), 
            (project) => {

                var configuration = "--configuration Release";
                var pushParameter = $@"""bin/Release/*.nupkg"" -s ""github"" -k {TOKEN}";

                Run(dotnet, $"{pack} {configuration}", project);
                Run(dotnet, $"{nuget} {push} {pushParameter}");
        });

        Target("default", DependsOn(build_core_projects));
        RunTargetsAndExit(bullseyeArgs);
    }

    private static void GenerateProject(string path)
    {
        path += "Generate/";

        Run(dotnet, $"{build} {path}");
        Run(dotnet, $"{run} {path}", path);
    }
}