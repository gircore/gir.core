using System;
using GObject.Core;

namespace WebKitGTK.Core
{
    public class Settings : GObject.Core.GObject
    {
        public Property<bool> AllowFileAccessFromFileUrls { get; }
        public Property<bool> AllowUniversalAccessFromFileUrls { get; }
        public Property<bool> AllowModalDialogs { get; }
        public Property<bool> EnableDeveloperExtras { get; }
        internal Settings(IntPtr handle) : base(handle)
        {
            AllowFileAccessFromFileUrls = Property<bool>("allow-file-access-from-file-urls",
                get : GetBool,
                set: Set
            );

            AllowUniversalAccessFromFileUrls = Property<bool>("allow-universal-access-from-file-urls",
                get : GetBool,
                set : Set
            );

            AllowModalDialogs = Property<bool>("allow-modal-dialogs",
                get : GetBool,
                set : Set
            );

            EnableDeveloperExtras = Property<bool>("enable-developer-extras",
                get : GetBool,
                set : Set
            );
        }
    }
}