// BUILD
//valac --pkg webkit2gtk-4.0 --library=CommunicatingWebView --gir=CommunicatingWebView-1.0.gir CommunicatingWebView.vala  -X -fPIC -X -shared -o libcommunicatingwebview.so -X -w

using WebKit;

namespace WebExtension{
    public class CommunicationWebKitWebView : WebView {
    }
}