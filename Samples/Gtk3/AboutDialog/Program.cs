using System;
using System.IO;
using System.Reflection;
using GdkPixbuf;

namespace AboutDialog
{
    // For some reason, Gtk needs to be included
    // here otherwise we get a conflict with Gtk.Native?
    using Gtk;
    
    /// <summary>
    /// An 'About' dialog window which shows various information about
    /// a program. This is used to test Gir.Core's string handling
    /// across multiple platforms. 
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            // Make sure to initialise Gtk beforehand
            Functions.Init();
            
            // Create the about dialog
            var dialog = new SampleDialog("Custom AboutDialog Demo");
            dialog.OnClose += (dlg, args) => Functions.MainQuit();

            // And run!
            dialog.Run();
        }
    }
}
