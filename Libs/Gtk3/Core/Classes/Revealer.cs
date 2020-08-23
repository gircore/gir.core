using System;
using GObject;

namespace Gtk
{
    public partial class Revealer
    {
        #region Properties
        public Property<uint> TransitionDuration { get; }
        public Property<RevealerTransitionType> TransitionType { get; }
        public Property<bool> Reveal { get; }
        #endregion

        public Revealer() : this (Sys.Revealer.@new()) { }
    }
}