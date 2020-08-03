using System;
using GObject;

namespace Gtk
{
    public class Revealer : Bin
    {
        public Property<uint> TransitionDuration { get; }
        public Property<RevealerTransitionType> TransitionType { get; }
        public Property<bool> Reveal { get; }

        public Revealer() : this (Sys.Revealer.@new()) { }
        internal Revealer(IntPtr handle) : base(handle) 
        { 
            TransitionDuration = PropertyOfUint("transition-duration");
            TransitionType = Property("transition-type",
                get : GetEnum<RevealerTransitionType>,
                set : SetEnum
            );
            Reveal = PropertyOfBool("reveal-child");
        }
    }
}