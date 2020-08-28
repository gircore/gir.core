using System;
using GObject;

namespace WebKit2
{
    public partial class Settings
    {
        #region Properties
        public IProperty<bool> AllowFileAccessFromFileUrls { get; }
        public IProperty<bool> AllowUniversalAccessFromFileUrls { get; }
        public IProperty<bool> AllowModalDialogs { get; }
        public IProperty<bool> EnableDeveloperExtras { get; }
        public IProperty<string> UserAgent { get; }
        #endregion
    }
}