#include "girtest-culong-record-tester.h"

/**
 * GirTestCULongRecordTester:
 *
 * Test records with unsigned long fields
 */

gsize girtest_cu_long_record_tester_get_sizeof_ul(GirTestCULongRecordTester* record)
{
    return sizeof(record->ul);
}

gulong girtest_cu_long_record_tester_get_max_unsigned_long_value()
{
    return ULONG_MAX;
}

gboolean girtest_cu_long_record_tester_is_max_unsigned_long_value(gulong value)
{
    if(value == ULONG_MAX)
        return TRUE;

    return FALSE;
}

/**
 * girtest_cu_long_record_tester_run_callback:
 * @value: The long value which should be passed to the callback
 * @callback: (scope call): a function that is called receives a ulong and should return a ulong
 *
 * Calls the callback and returns the data from the callback.
 * 
 * Returns: The result of the callback.
 **/
gulong girtest_cu_long_record_tester_run_callback(gulong value, GirTestCULongCallback callback)
{
    return callback(value);
}