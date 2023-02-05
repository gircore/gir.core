#include "girtest-returning-signal-tester.h"

/**
 * GirTestReturningSignalTester:
 *
 * Contains functions to test signals which return a value.
 */

enum {
    RETURN_BOOL_SIGNAL,
    N_SIGNALS
};

struct _GirTestReturningSignalTester
{
    GObject parent_instance;
};

G_DEFINE_TYPE(GirTestReturningSignalTester, girtest_returning_signal_tester, G_TYPE_OBJECT)

static guint tester_signals[N_SIGNALS] = { 0 };

static void
girtest_returning_signal_tester_init(GirTestReturningSignalTester *value)
{
}

static void
girtest_returning_signal_tester_class_init(GirTestReturningSignalTesterClass *class)
{
    tester_signals[RETURN_BOOL_SIGNAL] =
      g_signal_new ("return-bool", G_TYPE_FROM_CLASS (class), G_SIGNAL_RUN_LAST, 0, NULL, NULL, NULL, G_TYPE_BOOLEAN, 0);
}


/**
 * girtest_returning_signal_tester_new:
 *
 * Creates a new `GirTestReturningSignalTester`.
 *
 * Returns: The newly created `GirTestReturningSignalTester`.
 */
GirTestReturningSignalTester*
girtest_returning_signal_tester_new (void)
{
    return g_object_new (GIRTEST_TYPE_RETURNING_SIGNAL_TESTER, NULL);
}

/**
 * girtest_returning_signal_tester_emit_return_bool:
 * @tester: a `ReturningSignalTester`
 *
 * Emits the `return-bool` signal
 *
 * Returns: The value returned by the signal handler
 */
gboolean
girtest_returning_signal_tester_emit_return_bool (GirTestReturningSignalTester *tester)
{
    gboolean retval = FALSE;

    g_signal_emit (tester, tester_signals[RETURN_BOOL_SIGNAL], 0, &retval);

    return retval;
}

