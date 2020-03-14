using System;
using System.Runtime.InteropServices;
using GObject.Core;
using JavaScriptCore.Core;
using WebKit2;

namespace WebKitGTK.Core
{
    public class UserContentManager : GObject.Core.GObject
    {
        internal UserContentManager(IntPtr handle) : base(handle) { }

        public bool RegisterScriptMessageHandler(string name, Action<JavaScriptCore.Core.Value> callback)
        {
            var ret = WebKit2.UserContentManager.register_script_message_handler(this, name);

            if(ret)
            {
                ActionRefValues onMessageReceived = (ref GObject.Value[] values) => {
                    var result = values[1].GetBoxed();
                    var jsValue = JavascriptResult.get_js_value(result);
                    var value = new JavaScriptCore.Core.Value(jsValue);
                    callback(value);
                };

                RegisterEvent($"script-message-received::{name}", onMessageReceived);
            }
            return ret;
        }
    }
}