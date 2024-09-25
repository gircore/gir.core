#include "girtest-clong-tester.h"

/**
 * GirTestCLongTester:
 *
 * Test records with CLong fields
 */

gsize girtest_clong_tester_get_sizeof_l(GirTestCLongTester* record)
{
    return sizeof(record->l);
}

glong girtest_clong_tester_get_max_long_value()
{
    return LONG_MAX;
}

gboolean girtest_clong_tester_is_max_long_value(glong value)
{
    if(value == LONG_MAX)
        return TRUE;

    return FALSE;
}

glong girtest_clong_tester_get_min_long_value()
{
   return LONG_MIN;
}

gboolean girtest_clong_tester_is_min_long_value(glong value)
{
    if(value == LONG_MIN)
        return TRUE;

    return FALSE;
}

/**
 * girtest_clong_tester_run_callback:
 * @value: The long value which should be passed to the callback
 * @callback: (scope call): a function that is called receives a long and should return a long
 *
 * Calls the callback and returns the data from the callback.
 * 
 * Returns: The result of the callback.
 **/
glong girtest_clong_tester_run_callback(glong value, GirTestCLongCallback callback)
{
	return callback(value);
}