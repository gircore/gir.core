#include "girtest-ulong-tester.h"

/**
 * GirTestULongTester:
 *
 * Test records with unsigned long (64 bit) fields
 */

gsize girtest_ulong_tester_get_sizeof_l(GirTestULongTester* record)
{
    return sizeof(record->l);
}

guint64 girtest_ulong_tester_get_max_ulong_value()
{
    return G_MAXUINT64;
}

gboolean girtest_ulong_tester_is_max_ulong_value(guint64 value)
{
    if(value == G_MAXUINT64)
        return TRUE;

    return FALSE;
}

/**
 * girtest_ulong_tester_run_callback:
 * @value: The long value which should be passed to the callback
 * @callback: (scope call): a function that is called receives a long and should return a long
 *
 * Calls the callback and returns the data from the callback.
 * 
 * Returns: The result of the callback.
 **/
guint64 girtest_ulong_tester_run_callback(guint64 value, GirTestULongCallback callback)
{
	return callback(value);
}