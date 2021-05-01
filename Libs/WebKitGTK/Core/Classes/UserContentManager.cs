using System;
using GObject;

namespace WebKit2
{
    public partial class UserContentManager
    {
        public bool RegisterScriptMessageHandler(string name, Action<JavaScriptCore.Value> callback)
        {
            if (!Sys.UserContentManager.register_script_message_handler(Handle, name))
                return false;

            void OnMessageReceived(ref GObject.Sys.Value[] values)
            {
                var result = values[1].GetBoxed();
                result = Sys.JavascriptResult.@ref(result);
                var jsValue = Sys.JavascriptResult.get_js_value(result);
                var value = JavaScriptCore.Value.Create(jsValue);
                callback(value);
            }

            RegisterEvent($"script-message-received::{name}", (ActionRefValues) OnMessageReceived);
            return true;
        }

        public void AddScript(UserScript script)
        {
            var zero = IntPtr.Zero;
            var webkitScript = Sys.UserScript.@new(script.Script, Sys.UserContentInjectedFrames.all_frames, Sys.UserScriptInjectionTime.end, zero, zero);
            Sys.UserContentManager.add_script(Handle, webkitScript);
        }
    }
}
