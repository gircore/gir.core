using System;
using GObject;

namespace Gio
{
    public partial class Application
    {
        #region Properties

        public static readonly Property<string> ApplicationIdProperty = Property<string>.Register<Application>(
            Properties.ApplicationId,
            nameof(Application),
            get: (o) => o.ApplicationId,
            set: (o, v) => o.ApplicationId = v
        );

        public string ApplicationId
        {
            get => GetProperty(ApplicationIdProperty);
            set => SetProperty(ApplicationIdProperty, value);
        }

        #endregion

        #region Methods

        public int Run()
        {
            return Native.Instance.Methods.Run(Handle, 0, new string[0]);
        }

        #endregion
    }
}
