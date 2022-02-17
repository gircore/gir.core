using System;
using System.IO;
using System.Reflection;
using GdkPixbuf;
using Gtk;

namespace AboutDialog
{
    /// <summary>
    /// An 'About' dialog window which shows various information about
    /// a program. This is used to test Gir.Core's string handling
    /// across multiple platforms. 
    /// </summary>
    public static class Program
    {
        public static void Main(string[] args)
        {
            // Make sure to initialise Gtk beforehand
            Functions.Init();

            // Create the about dialog
            SampleDialog dialog = new SampleDialog("Custom AboutDialog");
            dialog.OnClose += (dlg, args) => Functions.MainQuit();

            // And run!
            dialog.Run();
        }
    }
}
