using System;
using GObject.Core;

namespace Gtk.Core
{
    public class GRevealer : GBin
    {
        public Property<uint> TransitionDuration { get; }
        public Property<RevealerTransitionType> TransitionType { get; }
        public Property<bool> Reveal { get; }

        public GRevealer() : this (Gtk.Revealer.@new()) { }
        internal GRevealer(IntPtr handle) : base(handle) 
        { 
            TransitionDuration = Property<uint>("transition-duration",
                get: GetUInt,
                set: Set
            );

            TransitionType = Property<RevealerTransitionType, Gtk.RevealerTransitionType>("transition-type",
                get : GetEnum<Gtk.RevealerTransitionType>,
                set : SetEnum<Gtk.RevealerTransitionType>,
                to: RevealerTransitionTypeConverter.ToGtk,
                from: RevealerTransitionTypeConverter.FromGtk
            );

            Reveal = Property<bool>("reveal-child",
                get: GetBool,
                set: Set
            );
        }
    }
}