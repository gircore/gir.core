using System;

namespace WebKit2;

public partial class UserContentManager
{
    //TODO: Is this method obsolete and the users can write it themself?
    //As JavaScriptResult is a record this method must be kept as long as public API for records is not generated
    public bool RegisterScriptMessageHandler(string name, Action<JavaScriptCore.Value> callback)
    {
        void OnMessageReceived(UserContentManager obj, ScriptMessageReceivedSignalArgs args)
        {
            var jsValue = Internal.JavascriptResult.GetJsValue(args.JsResult.Handle);
            var value = GObject.Internal.ObjectWrapper.WrapHandle<JavaScriptCore.Value>(jsValue, false);

            callback(value);
        }

        ScriptMessageReceivedSignal.Connect(this, OnMessageReceived, false, name);

        if (RegisterScriptMessageHandler(name))
            return true;

        ScriptMessageReceivedSignal.Disconnect(this, OnMessageReceived);
        return false;
    }
}
