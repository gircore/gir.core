using System;
using Gtk;
using Handy;
using WebKit2;

namespace GtkApp
{
    public class MyWindow : ApplicationWindow
    {
        [Connect]
        private Button Button = default!;

        [Connect]
        private Box Box = default!;

        private Box innerBox = new MyBox();
        private Button button;

        private Image image;
        private TextLabelExpander r;
        private Revealer revealer;

        private SimpleCommand action;
        private TextCombobox textCombobox;
        private CheckButton checkButton;
        private Notebook notebook;
        private WebView webView;
        private WebContext context;
        private Paginator paginator;
        private Label b;

        private GtkChamplain.Embed map;

        public MyWindow(Application application) : base(application, "ui.glade")
        {
            notebook = new Notebook();

            button = new Button("Test");

            button.Text.Value = "NEW TEXT";
            button.Clicked += (obj, args) => image.Clear();

            image = new StockImage("folder", IconSize.Button);

            notebook.InsertPage("Image", image, 0);
            notebook.InsertPage("Box", innerBox, 1);

            context = new WebContext();
            context.InitializeWebExtensions += OnInitializeWebExtension;
            webView = new WebView(context);

            var settings = webView.GetSettings();
            settings.AllowModalDialogs.Value = true;
            settings.EnableDeveloperExtras.Value = true;

            var ucm = webView.GetUserContentManager();
            var ret = ucm.RegisterScriptMessageHandler("foobar", JsCallback);

            if (ret)
            {
                const string code = @"                    
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
            webView.LoadUri("file:///home/marcel/Programmieren/csharp/gircore/gir.core/GtkApp/test.html");
            webView.HeightRequest.Value = 500;
            webView.WidthRequest.Value = 500;

            paginator = new Paginator();
            paginator.AllowMouseDrag.Value = true;
            paginator.Append(webView);
            paginator.IndicatorStyle.Value = PaginatorIndicatorStyle.Lines;

            b = new Label("label");
            paginator.Append(b);

            map = new GtkChamplain.Embed();
            map.WidthRequest.Value = 500;
            map.HeightRequest.Value = 500;
            paginator.Append(map);

            notebook.InsertPage("Paginator", paginator, 2);
            Box.Add(notebook);

            checkButton = new CheckButton("Check");
            checkButton.Toggled += (s, o) => Console.WriteLine("Toggled");
            Box.Add(checkButton);

            r = new TextLabelExpander("<span fgcolor='red'>Te_st</span>");
            r.UseMarkup.Value = true;
            r.UseUnderline.Value = true;

            textCombobox = new TextCombobox("combobox.glade");
            textCombobox.AppendText("t3", "Test 3");
            textCombobox.AppendText("t4", "Test 4");

            revealer = new Revealer();
            revealer.TransitionType.Value = RevealerTransitionType.Crossfade;
            revealer.Add(textCombobox);
            Box.Add(revealer);

            r.Add(new Label("test"));
            Box.Add(r);

            action = new SimpleCommand((o) => Console.WriteLine("Do it!"));
            application.AddAction("do", action);
        }

        private void JsCallback(JavaScriptCore.Value value)
        {
            if (value.IsString())
                Console.WriteLine(value.GetString());

            if (!value.IsObject())
                return;

            var p = value.GetProperty("myProp");
            Console.WriteLine(p.GetString());
        }

        private void OnInitializeWebExtension(object? sender, EventArgs args)
        {
            Console.WriteLine("INIT");
            Console.WriteLine(System.IO.Directory.GetCurrentDirectory());
        }

        private async void button_clicked(object obj, EventArgs args)
        {
            revealer.Reveal.Value = !revealer.Reveal.Value;
            action.SetCanExecute(!action.CanExecute(default));

            var inspector = webView.GetInspector();
            inspector.Show();

            var value = await webView.RunJavascriptAsync("test()");
            Console.WriteLine(value.GetString());
        }
    }
}
