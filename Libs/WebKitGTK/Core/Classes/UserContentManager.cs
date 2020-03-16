using System;
using GObject.Core;
using WebKit2;

namespace WebKitGTK.Core
{
    public class UserContentManager : GObject.Core.GObject
    {
        internal UserContentManager(IntPtr handle, bool isInitiallyUnowned = false) : base(handle, isInitiallyUnowned) {   }

        public bool RegisterScriptMessageHandler(string name, Action<JavaScriptCore.Core.Value> callback)
        {
            var ret = WebKit2.UserContentManager.register_script_message_handler(this, name);

            if(ret)
            {
                ActionRefValues onMessageReceived = (ref GObject.Value[] values) => {
                    var result = values[1].GetBoxed();
                    result = JavascriptResult.@ref(result);
                    var jsValue = JavascriptResult.get_js_value(result);
                    var value = new JavaScriptCore.Core.Value(jsValue);
                    callback(value);
                };

                RegisterEvent($"script-message-received::{name}", onMessageReceived);
            }
            return ret;
        }

        public void AddScript(UserScript script)
        { 
             var zero = IntPtr.Zero;
             var webkitScript = WebKit2.UserScript.@new(script.Script, UserContentInjectedFrames.all_frames, UserScriptInjectionTime.end, ref zero, ref zero);
            WebKit2.UserContentManager.add_script(this, webkitScript);
        }
    }
}