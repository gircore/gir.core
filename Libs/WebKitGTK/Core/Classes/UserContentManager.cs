using System;
using GObject;
using GObject.Sys;

namespace WebKit2
{
    public class UserContentManager : GObject.Object
    {
        internal UserContentManager(IntPtr handle, bool isInitiallyUnowned = false) : base(handle, isInitiallyUnowned) {   }

        public bool RegisterScriptMessageHandler(string name, Action<JavaScriptCore.Value> callback)
        {
            if(!Sys.UserContentManager.register_script_message_handler(this, name))
                return false;

            void OnMessageReceived(ref Value[] values)
            {
                var result = values[1].GetBoxed();
                result = Sys.JavascriptResult.@ref(result);
                var jsValue = Sys.JavascriptResult.get_js_value(result);
                var value = new JavaScriptCore.Value(jsValue);
                callback(value);
            }

            RegisterEvent($"script-message-received::{name}", (ActionRefValues) OnMessageReceived);
            return true;
        }

        public void AddScript(UserScript script)
        { 
             var zero = IntPtr.Zero;
             var webkitScript = Sys.UserScript.@new(script.Script, Sys.UserContentInjectedFrames.all_frames, Sys.UserScriptInjectionTime.end, zero, zero);
            Sys.UserContentManager.add_script(this, webkitScript);
        }
    }
}