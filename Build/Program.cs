using static Bullseye.Targets;
using static DotNet;
using static Projects;
using System.Collections.Generic;
using System.IO;
using Generator;

class Program
{
    private static string configuration = confDebug;
    private const string confRelease = "Release";
    private const string confDebug = "Debug";

    private static readonly string[] sampleProjects =
    {
        DBUS_SAMPLE,
        GST_SAMPLE,
        GTK3_APP_SAMPLE,
        GTK3_MINIMAL_SAMPLE,
        GTK4_SIMPLE_WINDOW_SAMPLE
    };

    private static readonly Project[] libraryProjects = 
    {
        (GLIB, "GLib-2.0.gir", "libglib-2.0", "0", false),
        /*(GOBJECT, "GObject-2.0.gir", "libgobject-2.0.so.0", true),
        (GIO, GIO_GIR, "libgio-2.0.so.0", true),
        (CAIRO, "cairo-1.0.gir", "TODO", false),
        (XLIB, "xlib-2.0.gir", "TODO", false),
        (PANGO, "Pango-1.0.gir", "TODO", false),
        (GDK3, "Gdk-3.0.gir", "TODO", true),
        (GDK_PIXBUF, GDK_PIXBUF_GIR, "libgdk_pixbuf-2.0.so.0", true),
        (GTK3, GTK3_GIR, "libgtk-3.so.0", true),
        (JAVASCRIPT_CORE, JAVASCRIPT_CORE_GIR, "javascriptcoregtk-4.0.so", false),
        (HANDY, HANDY_GIR, "libhandy-0.0.so.0", false),
        (WEBKITGTK, WEBKITGTK_GIR, "libwebkit2gtk-4.0.so.37", true),
        (WEBKIT2WEBEXTENSION, WEBKIT2WEBEXTENSION_GIR, "WEBEXTENSION", true),
        (CLUTTER, "Clutter-1.0.gir", "libclutter-1.0.so.0", false),
        (GTKCLUTTER, "GtkClutter-1.0.gir", "libclutter-gtk-1.0.so.0", false),
        (CHAMPLAIN, "Champlain-0.12.gir", "libchamplain-0.12", false),
        (GTKCHAMPLAIN, "GtkChamplain-0.12.gir", "libchamplain-gtk-0.12.so.0", false),
        (GST, "Gst-1.0.gir", "libgstreamer-1.0.so.0", true),
        (GDK4, "Gdk-4.0.gir", "libgtk-4.so.0", true),//GTK4
        (GSK4, "Gsk-4.0.gir", "libgtk-4.so.0", true),//GTK4
        (GTK4, GTK4_GIR, "libgtk-4.so.0", true) //GTK4*/
    };

    static void Main(string[] args)
    {        
        Target(Targets.Generate, 
            ForEach(libraryProjects),
            Generate
        );

        Target(Targets.Build, DependsOn(Targets.Generate),
            ForEach(libraryProjects),
            (x) => Build(x.Folder, configuration)
        );

        Target(Targets.CleanLibs, 
            ForEach(libraryProjects), 
            (x) => CleanUp(x.Folder, configuration)
        );
        
        Target(Targets.CleanSamples, 
            ForEach(sampleProjects), 
            (project) => CleanUp(project, configuration)
        );
        
        Target(Targets.Clean, DependsOn(Targets.CleanLibs, Targets.CleanSamples));
        
        Target(Targets.Samples, DependsOn(Targets.Build), 
            ForEach(sampleProjects),
            (project) => Build(project, configuration)
        );
        
        Target(Targets.Release, () => configuration = confRelease);
        Target(Targets.Debug, () => configuration = confDebug);

        Target("default", DependsOn(Targets.Build));
        RunTargetsAndExit(args);
    }

    private static void CleanUp(string project, string configuration)
    {
        if(Directory.Exists(project))
            foreach(var file in Directory.EnumerateFiles(project))
                if (file.Contains(".Generated."))
                    File.Delete(file);
            
        Clean(project, configuration);
    }

    private static void Generate(Project project)
    {
        project.Gir = $"../gir-files/{project.Gir}";
        
        var g = new CoreGenerator(project);
        g.Generate();
    }
}