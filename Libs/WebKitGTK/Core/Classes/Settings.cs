using System;
using GObject;

namespace WebKit2
{
    public partial class Settings
    {
        #region Properties
        public Property<bool> AllowFileAccessFromFileUrls { get; }
        public Property<bool> AllowUniversalAccessFromFileUrls { get; }
        public Property<bool> AllowModalDialogs { get; }
        public Property<bool> EnableDeveloperExtras { get; }
        public Property<string> UserAgent { get; }
        #endregion
    }
}