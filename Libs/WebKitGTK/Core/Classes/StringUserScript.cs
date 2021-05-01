using System;

namespace WebKit2
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
