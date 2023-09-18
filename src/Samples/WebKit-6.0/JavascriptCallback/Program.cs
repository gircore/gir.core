#pragma warning disable CA1416

WebKit.Module.Initialize();

var application = Gtk.Application.New("org.gir.core", Gio.ApplicationFlags.FlagsNone);
application.OnActivate += (sender, args) =>
{
    var webView = WebKit.WebView.New();
    webView.HeightRequest = 500;
    webView.WidthRequest = 500;

    var ucm = webView.GetUserContentManager();
    ucm.AddScript(WebKit.UserScript.New(
        source: """
            (function(globalContext) {
                globalContext.document.getElementById("inputId").onclick = function () {
                    var message = { myProp : "Success" }; //Message is a new object
                    window.webkit.messageHandlers.handlerid.postMessage(message);
                };
            })(this)
            """,
        injectedFrames: WebKit.UserContentInjectedFrames.AllFrames,
        injectionTime: WebKit.UserScriptInjectionTime.End)
    );

    WebKit.UserContentManager.ScriptMessageReceivedSignal.Connect(ucm, (manager, signalArgs) =>
    {
        System.Diagnostics.Debug.Assert(signalArgs.Value.IsObject(), "Returned value is expected to be an object");

        var result = signalArgs.Value.ObjectGetProperty("myProp");
        System.Console.WriteLine(result.ToString()); //Writes "Success" into the console
    }, true, "handlerid");

    if (!ucm.RegisterScriptMessageHandler("handlerid", null))
        throw new System.Exception("Could not register script message handler");

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
    window.Present();
};
return application.RunWithSynchronizationContext(null);

#pragma warning restore CA1416
