// BUILD
//valac --pkg webkit2gtk-web-extension-4.0 --library=WebExtension --gir=WebExtensionAdapter-1.0.gir WebExtensionAdapter.vala  -X -fPIC -X -shared -o webextension.so -X -w

using WebKit;

namespace WebExtension{

}

private void webkit_web_extension_initialize(WebKit.WebExtension extension) {
    

    if(extension != null) {
        print("extension ok\n");
        extension.user_message_received.connect (message_received);
    }


    //extension.send_message_to_context(null, null, null);

    //extension.page_created.connect((extension, page) =>{
    //    page.document_loaded.connect((page) => {
    //        WebMusic.Webextension.Controler.DocumentLoaded(page);
    //    });
    //});
}

private void message_received (WebKit.WebExtension extension, WebKit.UserMessage message) {
    stdout.printf ("Callback A\n");
}