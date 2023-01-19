#pragma warning disable CA1416

WebKit2.Module.Initialize();

var application = Gtk.Application.New("org.gir.core", Gio.ApplicationFlags.FlagsNone);
application.OnActivate += (sender, args) =>
{
    var webView = WebKit2.WebView.New();
    webView.HeightRequest = 500;
    webView.WidthRequest = 500;

    var ucm = webView.GetUserContentManager();
    ucm.AddScript(WebKit2.UserScript.New(
        source: """
            (function(globalContext) {
                globalContext.document.getElementById("inputId").onclick = function () {
                    var message = { myProp : "Success" }; //Message is a new object
                    window.webkit.messageHandlers["handlerId"].postMessage(message);
                };
            })(this)
            """,
        injectedFrames: WebKit2.UserContentInjectedFrames.AllFrames,
        injectionTime: WebKit2.UserScriptInjectionTime.End)
    );
    ucm.RegisterScriptMessageHandler("handlerId", value =>
    {
        System.Diagnostics.Debug.Assert(value.IsObject(), "Returned value is expected to be an object");

        var result = value.ObjectGetProperty("myProp");
        System.Console.WriteLine(result.ToString()); //Writes "Success" into the console
    });

    webView.LoadHtml(
        content: """
            <!DOCTYPE html>
            <html>
                <head><title>Javascript Demo</title></head>
                <body><input id="inputId" type="button" value="Click" /></body>
            </html>
            """,
        baseUri: null
    );

    var window = Gtk.ApplicationWindow.New((Gtk.Application) sender);
    window.Title = "WebKit Demo";
    window.SetChild(webView);
    window.Show();
};
return application.Run();

#pragma warning restore CA1416
