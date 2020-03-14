using System;
using Gtk.Core;
using WebKitGTK.Core;
using WebKit2WebExtension.Core;

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
        private GTextCombobox textCombobox;
        private GCheckButton checkButton;
        private GNotebook notebook;
        private WebView webView;
        private WebContext context;

        public MyWindow(Gtk.Core.GApplication application) : base(application, "ui.glade") 
        { 
            notebook = new GNotebook();

            button = new GButton("Test");

            button.Text.Value = "NEW TEXT";
            button.Clicked += (obj, args) => image.Clear();

            image = new StockImage("folder", IconSize.Button);

            notebook.InsertPage("Image", (GWidget)image, 0);
            notebook.InsertPage("Box", innerBox, 1);
            
            context = new WebContext();
            context.InitializeWebExtensions += OnInitializeWebExtension;
            webView = new WebView(context);
            
            var settings = webView.GetSettings();
            settings.AllowModalDialogs.Value = true;
            settings.EnableDeveloperExtras.Value = true;

            var ucm = webView.GetUserContentManager();
            var ret = ucm.RegisterScriptMessageHandler("foobar");

            Console.WriteLine(System.IO.Directory.GetCurrentDirectory());
            webView.LoadUri("file:///mnt/daten/Programmierung/test.html");
            webView.HeightRequest.Value = 500;
            webView.WidthRequest.Value = 500;


            notebook.InsertPage("WebKit", webView, 2);
            Box.Add(notebook);

            checkButton = new GCheckButton("Check");
            checkButton.Toggled += (s, o) => Console.WriteLine("Toggled");
            Box.Add(checkButton);

            r = new TextLabelExpander("<span fgcolor='red'>Te_st</span>");
            r.UseMarkup.Value = true;
            r.UseUnderline.Value = true;

            textCombobox = new GTextCombobox("combobox.glade");
            textCombobox.AppendText("t3", "Test 3");
            textCombobox.AppendText("t4", "Test 4");

            revealer = new GRevealer();
            revealer.TransitionType.Value = RevealerTransitionType.Crossfade;
            revealer.Add(textCombobox);
            Box.Add(revealer);

            r.Add(new GLabel("test"));
            Box.Add(r);

            action = new SimpleCommand((o) => Console.WriteLine("Do it!"));
            application.AddAction("do", action);
        }

        private void OnInitializeWebExtension(object? sender, EventArgs args)
        {
            Console.WriteLine("INIT");
            Console.WriteLine(System.IO.Directory.GetCurrentDirectory());
        }

        private void button_clicked(object obj, EventArgs args)
        {
            revealer.Reveal.Value = !revealer.Reveal.Value;
            action.SetCanExecute(!action.CanExecute(default));

            var inspector = webView.GetInspector();
            inspector.Show();
        } 

    }
}