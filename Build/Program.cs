using static Bullseye.Targets;
using static SimpleExec.Command;
using static Targets;
using static Commands;
using static Projects;

class Program
{

    static void Main(string[] args)
    {
        var GITHUB_TOKEN = args[0];

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

        Target(publish_alpha, DependsOn(build_core_projects), () => {

            var configuration = "--configuration Release";
            var pushParameter = $@"""bin/Release/*.nupkg"" -s ""github"" -k {GITHUB_TOKEN}";

            Run(dotnet, $"{pack} {configuration}");
            //Run(dotnet, $"{nuget} {push} {pushParameter}");
        });

        Target("default", DependsOn(build_core_projects));
        RunTargetsAndExit(args);
    }

    private static void GenerateProject(string path)
    {
        path += "Generate/";

        Run(dotnet, $"{build} {path}");
        Run(dotnet, $"{run} {path}", path);
    }
}