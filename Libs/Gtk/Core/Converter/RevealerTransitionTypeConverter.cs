namespace Gtk.Core
{
    public static class RevealerTransitionTypeConverter
    {
        public static Gtk.RevealerTransitionType ToGtk(this RevealerTransitionType e) => e switch
        {
            RevealerTransitionType.Crossfade => Gtk.RevealerTransitionType.crossfade,
            RevealerTransitionType.None => Gtk.RevealerTransitionType.none,
            RevealerTransitionType.SlideDown => Gtk.RevealerTransitionType.slide_down,
            RevealerTransitionType.SlideLeft => Gtk.RevealerTransitionType.slide_left,
            RevealerTransitionType.SlideRight => Gtk.RevealerTransitionType.slide_right,
            RevealerTransitionType.SlideUp => Gtk.RevealerTransitionType.slide_up,
            _ => throw new System.Exception($"Unknown value of {nameof(RevealerTransitionType)}")
        };

        public static RevealerTransitionType FromGtk(this Gtk.RevealerTransitionType e) => e switch
        {
            Gtk.RevealerTransitionType.crossfade => RevealerTransitionType.Crossfade,
            Gtk.RevealerTransitionType.none => RevealerTransitionType.None,
            Gtk.RevealerTransitionType.slide_down => RevealerTransitionType.SlideDown,
            Gtk.RevealerTransitionType.slide_left => RevealerTransitionType.SlideLeft,
            Gtk.RevealerTransitionType.slide_right => RevealerTransitionType.SlideRight,
            Gtk.RevealerTransitionType.slide_up => RevealerTransitionType.SlideUp,
            _ => throw new System.Exception($"Unknown value of {nameof(Gtk)}.{nameof(RevealerTransitionType)}")
        };
    }
}