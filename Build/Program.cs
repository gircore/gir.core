using System;
using static Bullseye.Targets;
using static Build.Projects;
using System.IO;
using Generator;

namespace Build
{
    public static class Program
    {
        #region Fields

        private static string Configuration = ConfDebug;
        private static bool GenerateComments = false;

        private static readonly string[] TestProjects =
        {
            GOBJECT_TEST
        };
        
        private static readonly string[] SampleProjects =
        {
            DBUS_SAMPLE,
            GSTREAMER_SAMPLE,
            //GTK3_APP_SAMPLE,
            GTK3_MINIMAL_SAMPLE,
            GTK3_QUICKSTART,
            //GTK4_SIMPLE_WINDOW_SAMPLE
        };

        private static readonly (Project Project, Type Type)[] LibraryProjects =
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
            /*(GDK4, "Gdk-4.0.gir", "libgtk-4.so.0", true),//GTK4
            (GSK4, "Gsk-4.0.gir", "libgtk-4.so.0", true),//GTK4
            (GTK4, GTK4_GIR, "libgtk-4.so.0", true) //GTK4*/
        };

        #endregion

        #region Constants

        private const string ConfRelease = "Release";
        private const string ConfDebug = "Debug";

        #endregion

        #region Methods

        public static void Main(string[] args)
        {
            Target(Targets.Generate,
                ForEach(LibraryProjects),
                (l) => Generate(l.Project, l.Type)
            );

            Target(Targets.Build,
                DependsOn(Targets.Generate),
                ForEach(LibraryProjects),
                (x) => DotNet.Build(x.Project.Folder, Configuration)
            );

            Target(Targets.CleanLibs,
                ForEach(LibraryProjects),
                (x) => CleanUp(x.Project.Folder, Configuration)
            );

            Target(Targets.CleanSamples,
                ForEach(SampleProjects),
                (project) => CleanUp(project, Configuration)
            );

            Target(Targets.Clean, DependsOn(Targets.CleanLibs, Targets.CleanSamples));

            Target(Targets.Samples,
                DependsOn(Targets.Build),
                ForEach(SampleProjects),
                (project) => DotNet.Build(project, Configuration)
            );
            
            Target(Targets.Test,
                DependsOn(Targets.Build),
                ForEach(TestProjects),
                (project) => DotNet.Test(project, Configuration)
            );

            Target(Targets.Comments, () => GenerateComments = true);
            
            Target(Targets.Release, () => Configuration = ConfRelease);
            Target(Targets.Debug,
                DependsOn(Targets.Comments),
                () => Configuration = ConfDebug
            );

            Target("default", DependsOn(Targets.Debug, Targets.Build));
            RunTargetsAndExit(args);
        }

        private static void CleanUp(string project, string configuration)
        {
            if (Directory.Exists(project))
            {
                foreach (var d in Directory.EnumerateDirectories(project))
                {
                    foreach (var file in Directory.EnumerateFiles(d))
                    {
                        if (file.Contains(".Generated."))
                            File.Delete(file);
                    }
                }
            }

            DotNet.Clean(project, configuration);
        }

        private static void Generate(Project project, Type type)
        {
            project.Gir = $"../gir-files/{project.Gir}";

            if (Activator.CreateInstance(type, project) is IGenerator generator)
            {
                generator.GenerateComments = GenerateComments;
                generator.Generate();
            }
            else
                throw new Exception($"{type.Name} is not a generator");
        }

        #endregion
    }
}
