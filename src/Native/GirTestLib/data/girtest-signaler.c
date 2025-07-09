#include "girtest-signaler.h"

enum {
    MY_SIGNAL,
    N_SIGNALS
};

G_DEFINE_INTERFACE (GirTestSignaler, girtest_signaler, G_TYPE_OBJECT)

static guint signaler_signals[N_SIGNALS] = { 0 };

static void
girtest_signaler_default_init (GirTestSignalerInterface *iface)
{
    signaler_signals[MY_SIGNAL] =
          g_signal_new ("my-signal", G_TYPE_FROM_INTERFACE (iface), G_SIGNAL_RUN_LAST | G_SIGNAL_DETAILED, 0, NULL, NULL, NULL, G_TYPE_NONE, 0);
}

void
girtest_signaler_my_signal (GirTestSignaler *self)
{
    g_return_if_fail (GIRTEST_IS_SIGNALER (self));

    GQuark quark = g_quark_from_string("fubar");
    g_signal_emit (self, signaler_signals[MY_SIGNAL], quark);
}

void
girtest_signaler_emit (GirTestSignaler *self)
{
    GirTestSignalerInterface *iface;

    g_return_if_fail (GIRTEST_IS_SIGNALER (self));

    iface = GIRTEST_SIGNALER_GET_IFACE (self);
    g_return_if_fail (iface->emit != NULL);
    iface->emit (self);
}
