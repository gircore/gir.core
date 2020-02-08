using System;
using Gtk.Core;

namespace GtkApp
{
    public class MyWindow : GApplicationWindow
    {
        [Connect]
        private GButton Button = default!;
        
        [Connect]
        private GBox Box = default!;

        private GBox innerBox = new MyBox();
        private GButton button;

        private Image image;
        private TextLabelExpander r;
        private GRevealer revealer;

        private SimpleCommand action;

        public MyWindow(Gtk.Core.GApplication application) : base(application, "ui.glade") 
        { 
            button = new GButton("Test");

            button.Text.Value = "NEW TEXT";
            button.Clicked += (obj, args) => image.Clear();

            image = new StockImage("folder", IconSize.Button);

            Box.Add(button);
            Box.Add(innerBox);
            Box.Add((GWidget)image);

            r = new TextLabelExpander("<span fgcolor='red'>Te_st</span>");
            r.UseMarkup.Value = true;
            r.UseUnderline.Value = true;

            revealer = new GRevealer();
            revealer.TransitionType.Value = RevealerTransitionType.Crossfade;
            revealer.Add(new GLabel("rev test"));
            Box.Add(revealer);

            r.Add(new GLabel("test"));
            Box.Add(r);

            action = new SimpleCommand((o) => Console.WriteLine("Do it!"));
            application.AddAction("do", action);
        }

        private void button_clicked(object obj, EventArgs args)
        {
            revealer.Reveal.Value = !revealer.Reveal.Value;
            action.SetCanExecute(!action.CanExecute(default));
        } 

    }
}