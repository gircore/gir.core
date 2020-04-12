using System;
using Gtk.Core;
using Handy.Core;
using WebKitGTK.Core;

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
        private GPaginator paginator;
        private GLabel b;

        private GtkChamplain.Core.Embed map;

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
            var ret = ucm.RegisterScriptMessageHandler("foobar", JsCallback);

            if(ret)
            {
                var code = @"                    
                    (function(globalContext) {
                        globalContext.document.getElementById(""clickMe"").onclick = function () 
                        {
                            var message = {
                                myProp : ""5"",
                            };
                            window.webkit.messageHandlers[""foobar""].postMessage(message);
                        };
                    })(this)

                    function test() { return 'test' }
                    ";
                ucm.AddScript(new StringUserScript(code));
            }

            Console.WriteLine(System.IO.Directory.GetCurrentDirectory());
            webView.LoadUri("file:///mnt/daten/Programmierung/test.html");
            webView.HeightRequest.Value = 500;
            webView.WidthRequest.Value = 500;

            paginator = new GPaginator();
            paginator.AllowMouseDrag.Value = true;
            paginator.Append(webView);
            paginator.IndicatorStyle.Value = PaginatorIndicatorStyle.Lines;

            b = new GLabel("label");
            paginator.Append(b);

            map = new GtkChamplain.Core.Embed();
            map.WidthRequest.Value = 500;
            map.HeightRequest.Value = 500;
            paginator.Append(map);

            notebook.InsertPage("Paginator", paginator, 2);
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

        private void JsCallback(JavaScriptCore.Core.Value value)
        {
            if(value.IsString())
            {
                Console.WriteLine(value.GetString());
            }
            if(value.IsObject())
            {
                var p = value.GetProperty("myProp");
                Console.WriteLine(p.GetString());
            }
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

            webView.RunJavascript("test()", (value) => Console.WriteLine(value.GetString()));
        } 
    }
}