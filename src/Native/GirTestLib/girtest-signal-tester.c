#include "girtest-signal-tester.h"

/**
 * GirTestSignalTester:
 *
 * Contains functions to test signals.
 */

enum {
    MY_SIGNAL,
    N_SIGNALS
};

struct _GirTestSignalTester
{
    GObject parent_instance;
};

G_DEFINE_TYPE(GirTestSignalTester, girtest_signal_tester, G_TYPE_OBJECT)

static guint tester_signals[N_SIGNALS] = { 0 };

static void
girtest_signal_tester_init(GirTestSignalTester *value)
{
}

static void
girtest_signal_tester_class_init(GirTestSignalTesterClass *class)
{
    tester_signals[MY_SIGNAL] =
      g_signal_new ("my-signal", G_TYPE_FROM_CLASS (class), G_SIGNAL_RUN_LAST | G_SIGNAL_DETAILED, 0, NULL, NULL, NULL, G_TYPE_NONE, 0);
}


/**
 * girtest_signal_tester_new:
 *
 * Creates a new `GirTestSignalTester`.
 *
 * Returns: The newly created `GirTestSignalTester`.
 */
GirTestSignalTester*
girtest_signal_tester_new (void)
{
    return g_object_new (GIRTEST_TYPE_SIGNAL_TESTER, NULL);
}

/**
 * girtest_signal_tester_emit_my_signal_named:
 * @tester: a `SignalTester`
 *
 * Emits the `my-signal::fubar` signal
 */
void
girtest_signal_tester_emit_my_signal_fubar(GirTestSignalTester *tester)
{
    GQuark quark = g_quark_from_string("fubar");
    g_signal_emit (tester, tester_signals[MY_SIGNAL], quark);
}

