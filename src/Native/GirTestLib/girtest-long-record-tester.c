#include "girtest-long-record-tester.h"

/**
 * GirTestLongRecordTester:
 *
 * Test records with long (64 bit) fields
 */

gsize girtest_long_record_tester_get_sizeof_l(GirTestLongRecordTester* record)
{
    return sizeof(record->l);
}

gint64 girtest_long_record_tester_get_max_long_value()
{
    return G_MAXINT64;
}

gboolean girtest_long_record_tester_is_max_long_value(gint64 value)
{
    if(value == G_MAXINT64)
        return TRUE;

    return FALSE;
}

gint64 girtest_long_record_tester_get_min_long_value()
{
   return G_MININT64;
}

gboolean girtest_long_record_tester_is_min_long_value(gint64 value)
{
    if(value == G_MININT64)
        return TRUE;

    return FALSE;
}

/**
 * girtest_long_record_tester_run_callback:
 * @value: The long value which should be passed to the callback
 * @callback: (scope call): a function that is called receives a long and should return a long
 *
 * Calls the callback and returns the data from the callback.
 * 
 * Returns: The result of the callback.
 **/
gint64 girtest_long_record_tester_run_callback(gint64 value, GirTestLongCallback callback)
{
	return callback(value);
}