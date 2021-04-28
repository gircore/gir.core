using System;
using GObject;

namespace Gio
{
    public partial class Application
    {
        #region Methods

        public int Run()
        {
            return Native.Application.Instance.Methods.Run(Handle, 0, new string[0]);
        }

        #endregion
    }
}
