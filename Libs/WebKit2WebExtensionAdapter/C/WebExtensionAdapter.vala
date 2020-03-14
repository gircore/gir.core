// BUILD
//valac --pkg webkit2gtk-web-extension-4.0 --library=WebExtension --gir=WebExtensionAdapter-1.0.gir WebExtensionAdapter.vala  -X -fPIC -X -shared -o webextension.so -X -w

using WebKit;
namespace WebExtension{
    public class Adapter : Object {
        public signal void web_extension_initialize (WebKit.WebExtension extension);
        
        public Adapter() {
        }

        internal void on_web_extension_initialize(WebKit.WebExtension extension)
        {
            web_extension_initialize (extension);
        }
    }
    private Adapter adapter;
    public void set_adapter(Adapter a)
    {
        adapter = a;
    }
}

private void webkit_web_extension_initialize(WebKit.WebExtension extension) {
    WebExtension.adapter.on_web_extension_initialize(extension);
    //extension.page_created.connect((extension, page) =>{
    //    page.document_loaded.connect((page) => {
    //        WebMusic.Webextension.Controler.DocumentLoaded(page);
    //    });
    //});
}