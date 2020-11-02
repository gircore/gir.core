using System;
using GObject;

namespace Gio
{
    public partial class Application
    {
        #region Properties
        
        public static readonly Property<string> ApplicationProperty = Property<string>.Register<Application>(
            Native.ApplicationIdProperty,
            nameof(Application),
            get: (o) => o.ApplicationId,
            set: (o, v) => o.ApplicationId = v
        );

        public string ApplicationId
        {
            get => GetProperty(ApplicationProperty);
            set => SetProperty(ApplicationProperty, value);
        }
        
        #endregion
        
        #region Methods

        public int Run()
        {
            const int argc = 0;
            IntPtr ptr = IntPtr.Zero;
            return Native.run(Handle, argc, ref ptr);
        }

        #endregion
    }
}
