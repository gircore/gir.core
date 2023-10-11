#pragma warning disable CA1416

WebKit.Module.Initialize();

var application = Gtk.Application.New("org.gir.core", Gio.ApplicationFlags.FlagsNone);
application.OnActivate += (sender, _) =>
{
    var webView = WebKit.WebView.New();
    webView.HeightRequest = 500;
    webView.WidthRequest = 500;

    var ucm = webView.GetUserContentManager();
    ucm.AddScript(WebKit.UserScript.New(
        source: "function testFunc() { return 'Success' }",
        injectedFrames: WebKit.UserContentInjectedFrames.AllFrames,
        injectionTime: WebKit.UserScriptInjectionTime.End,
        allowList: null,
        blockList: null)
    );

    webView.OnLoadChanged += async (view, signalArgs) =>
    {
        if (signalArgs.LoadEvent != WebKit.LoadEvent.Finished)
            return;

        var result = await webView.EvaluateJavascriptAsync("testFunc();");
        System.Console.WriteLine(result.ToString()); //Writes "Success" into the console
    };

    webView.LoadHtml(
        content: """
            <!DOCTYPE html>
            <html>
                <head><title>Javascript Demo</title></head>
                <body>Demo content</body>
            </html>
            """,
        baseUri: null
    );

    var window = Gtk.ApplicationWindow.New((Gtk.Application) sender);
    window.Title = "WebKit Demo";
    window.SetChild(webView);
    window.Show();
};
return application.RunWithSynchronizationContext(null);

#pragma warning restore CA1416
