using System;
using WebKit2;

namespace WebKitGTK.Core
{
    public class UserScript
    {
        private readonly string script;

        private IntPtr webkitScript;
        internal IntPtr WebKitScript
        {
            get
            {
                if(webkitScript == IntPtr.Zero)
                {
                    var zero = IntPtr.Zero;

                    webkitScript = WebKit2.UserScript.@new(script, UserContentInjectedFrames.all_frames, UserScriptInjectionTime.end, ref zero, ref zero);
                }

                return webkitScript;
            }
        }

        public UserScript(string script)
        {
            this.script = script ?? throw new System.ArgumentNullException(nameof(script));
        }
    }
}