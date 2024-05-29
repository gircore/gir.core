#include "girtest-long-tester.h"

/**
 * GirTestLongTester:
 *
 * Test records with long fields
 */

gsize girtest_long_tester_get_sizeof_l(GirTestLongTester* record)
{
    return sizeof(record->l);
}

glong girtest_long_tester_get_max_long_value()
{
    return LONG_MAX;
}

gboolean girtest_long_tester_is_max_long_value(glong value)
{
    if(value == LONG_MAX)
        return TRUE;

    return FALSE;
}

glong girtest_long_tester_get_min_long_value()
{
   return LONG_MIN;
}

gboolean girtest_long_tester_is_min_long_value(glong value)
{
    if(value == LONG_MIN)
        return TRUE;

    return FALSE;
}

/**
 * girtest_long_tester_run_callback:
 * @value: The long value which should be passed to the callback
 * @callback: (scope call): a function that is called receives a long and should return a long
 *
 * Calls the callback and returns the data from the callback.
 * 
 * Returns: The result of the callback.
 **/
glong girtest_long_tester_run_callback(glong value, GirTestLongCallback callback)
{
	return callback(value);
}