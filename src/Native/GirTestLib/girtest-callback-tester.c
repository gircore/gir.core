#include "girtest-callback-tester.h"

/**
 * GirTestCallbackTester:
 *
 * Contains functions for testing bindings with callbacks.
 */

struct _GirTestCallbackTester
{
    GObject parent_instance;

    GirTestIntCallback notified_callback;
    gpointer notified_data;
    GDestroyNotify notified_destroy;
};

G_DEFINE_TYPE(GirTestCallbackTester, girtest_callback_tester, G_TYPE_OBJECT)

static void
girtest_callback_tester_init(GirTestCallbackTester *tester)
{
    tester->notified_callback = NULL;
    tester->notified_data = NULL;
    tester->notified_destroy = NULL;
}

static void
girtest_callback_tester_class_init(GirTestCallbackTesterClass *class)
{
}

/**
 * girtest_callback_tester_new:
 *
 * Creates a new `GirTestCallbackTester`.
 *
 * Returns: The newly created `GirTestCallbackTester`.
 */
GirTestCallbackTester*
girtest_callback_tester_new (void)
{
    return g_object_new (GIRTEST_TYPE_CALLBACK_TESTER, NULL);
}

/**
 * girtest_callback_tester_set_notified_callback:
 * @callback: (scope notified): a function to set as the notified callback.
 * @data: data to pass to @notify.
 * @notify: (nullable): function to call when the callback is removed, or %NULL.
 *
 * Assigns a callback with 'notified' scope, which can later be called with
 * girtest_callback_tester_run_notified_callback().
 */
void
girtest_callback_tester_set_notified_callback(GirTestCallbackTester *tester,
                                              GirTestIntCallback callback,
                                              gpointer data,
                                              GDestroyNotify notify)
{
    tester->notified_callback = callback;
    tester->notified_data = data;
    tester->notified_destroy = notify;
}

/**
 * girtest_callback_tester_run_notified_callback:
 * @value: value to pass to the callback.
 * @done: If true, runs the GDestroyNotify callback.
 *
 * Runs the notified callback with the provided value, or returns -1 if the
 * callback was not set or has been destroyed.
 */
int
girtest_callback_tester_run_notified_callback(GirTestCallbackTester *tester,
                                              int value,
                                              gboolean done)
{
    if (!tester->notified_callback)
        return -1;

    int result = tester->notified_callback(value);

    if (done)
    {
        if (tester->notified_destroy)
            tester->notified_destroy(tester->notified_data);

        tester->notified_callback = NULL;
        tester->notified_data = NULL;
        tester->notified_destroy = NULL;
    }

    return result;
}
