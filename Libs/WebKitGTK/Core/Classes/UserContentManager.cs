using System;

namespace WebKitGTK.Core
{
    public class UserContentManager : GObject.Core.GObject
    {
        internal UserContentManager(IntPtr handle) : base(handle)
        {
            RegisterEvent("script-message-received", OnMessageReceived);
        }

        private void OnMessageReceived()
        {
            Console.WriteLine("REE");
        }

        public bool RegisterScriptMessageHandler(string name) => WebKit2.UserContentManager.register_script_message_handler(this, name);
    }
}