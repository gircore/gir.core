using System;
using WebKit2;

namespace WebKitGTK.Core
{
    public class StringUserScript : UserScript
    {
        public string Script { get; }

        public StringUserScript(string script)
        {
            Script = script ?? throw new ArgumentNullException(nameof(script));
        }
    }
}