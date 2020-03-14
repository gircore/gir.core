using System;
using System.Runtime.InteropServices;
using WebKit2;

namespace WebKitGTK.Core
{
    public class UserContentManager : GObject.Core.GObject
    {
        internal UserContentManager(IntPtr handle) : base(handle)
        {
            RegisterEvent("script-message-received", OnMessageReceived);
        }

        private void OnMessageReceived(ref GObject.Value[] values)
        {
            var result = (IntPtr)values[1];
            var jsValue = JavascriptResult.get_js_value(result);
            var value = new JavaScriptCore.Core.Value(jsValue);
           // var i = value.ToInt();
            Console.WriteLine("REE");
        }

        public bool RegisterScriptMessageHandler(string name) => WebKit2.UserContentManager.register_script_message_handler(this, name);
    }
}