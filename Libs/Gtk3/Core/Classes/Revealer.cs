using System;
using GObject;

namespace Gtk
{
    public partial class Revealer
    {
        #region Properties
        public IProperty<uint> TransitionDuration { get; }
        public IProperty<RevealerTransitionType> TransitionType { get; }
        public IProperty<bool> Reveal { get; }
        #endregion

        public Revealer() : this(Sys.Revealer.@new()) { }
    }
}