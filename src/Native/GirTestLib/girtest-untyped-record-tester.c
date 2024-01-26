#include "girtest-untyped-record-tester.h"

/**
 * GirTestUntypedRecordTester:
 *
 * Test untyped records
 */

/**
 * girtest_untyped_record_tester_new_with_a: (constructor)
 *
 * Returns: (transfer full): a new `GirTestUntypedRecordTester`
 **/
GirTestUntypedRecordTester* girtest_untyped_record_tester_new_with_a(int a)
{
    GirTestUntypedRecordTester *result;
    result = g_new0 (GirTestUntypedRecordTester, 1);
    result->a = a;
    return result;
}

/**
 * girtest_untyped_record_tester_try_new:
 * @returnNull: TRUE to return null, FALSE to create a new instance.
 *
 * Returns: (transfer full) (nullable): a new `GirTestUntypedRecordTester` or NULL
 **/
GirTestUntypedRecordTester * girtest_untyped_record_tester_try_new(gboolean returnNull)
{
    if(returnNull)
        return NULL;

    return girtest_untyped_record_tester_new_with_a(0);
}

/**
 * girtest_untyped_record_tester_mirror:
 * @data: (transfer none): A `GirTestUntypedRecordTester`
 *
 * Returns: (transfer none): a new `GirTestUntypedRecordTester`
 **/
GirTestUntypedRecordTester* girtest_untyped_record_tester_mirror(GirTestUntypedRecordTester *data)
{
    return data;
}

/**
 * girtest_untyped_record_tester_nullable_mirror:
 * @data: a `GirTestUntypedRecordTester`
 * @mirror: true to mirror data, false to return NULL
 *
 * Mirrors the given data as the return value if @mirror is true. Ownership is not transferred.
 *
 * Returns: (transfer none) (nullable): the mirrored data or NULL.
 **/
GirTestUntypedRecordTester * girtest_untyped_record_tester_nullable_mirror(GirTestUntypedRecordTester *data, gboolean mirror)
{
    if(!mirror)
        return NULL;

    return data;
}

/**
 * girtest_untyped_record_tester_get_a:
 * @record: A record
 *
 * Returns: The value of a
 **/
int girtest_untyped_record_tester_get_a(GirTestUntypedRecordTester *record)
{
    return record->a;
}

/**
 * girtest_untyped_record_tester_get_a_nullable:
 * @fallback: a fallback value if record is NULL
 * @record: (nullable): A record
 *
 * Returns: The value of a or the fallback value in case of NULL
 **/
int girtest_untyped_record_tester_get_a_nullable(int fallback, GirTestUntypedRecordTester* record)
{
    if(!record)
        return fallback;

    return record->a;
}

/**
 * girtest_untyped_record_tester_out_parameter_caller_allocates:
 * @v: value to set to field "a"
 * @record: (out caller-allocates): a user provided structure that is filled with data
 *
 * Fills a given structure with a new untyped record tester
 */
void girtest_untyped_record_tester_out_parameter_caller_allocates(int v, GirTestUntypedRecordTester *record)
{
    record->a = v;
}

/**
 * girtest_untyped_record_tester_callback_out_parameter_caller_allocates:
 * @callback: (scope call): a function that is called to create a new tester instance
 *
 * Return: (transfer full): Fills a given structure with a new untyped record tester
 */
GirTestUntypedRecordTester*
girtest_untyped_record_tester_callback_out_parameter_caller_allocates(GirTestUntypedRecordCallbackOutParameterCallerAllocates callback)
{
    GirTestUntypedRecordTester *result;
    result = g_new0 (GirTestUntypedRecordTester, 1);

    callback(result);

    return result;
}


/**
 * girtest_untyped_record_tester_callback_out_parameter_callee_allocates:
 * @callback: (scope call): a function that is called to create a new tester instance
 *
 * Return: (transfer full): Fills a given structure with a new untyped record tester
 */
GirTestUntypedRecordTester*
girtest_untyped_record_tester_callback_out_parameter_callee_allocates(GirTestUntypedRecordCallbackOutParameterCalleeAllocates callback)
{
    GirTestUntypedRecordTester **result = g_malloc(sizeof(GirTestUntypedRecordTester*));

    callback(result);

    return *result;
}

/**
 * girtest_untyped_record_tester_get_a_from_last_element:
 * @array: (transfer none) (array length=length) (element-type GirTestUntypedRecordTester): an array of GirTestUntypedRecordTester
 * @length: Length of the array
 *
 * Return: The value of A of the last array element
 */
int girtest_untyped_record_tester_get_a_from_last_element(GirTestUntypedRecordTester* array, int length)
{
    GirTestUntypedRecordTester* result = array += (length-1);
    return result->a;
}

/**
 * girtest_untyped_record_tester_get_a_from_last_element_pointer:
 * @array: (transfer none) (array length=length): an array of GirTestUntypedRecordTester*
 * @length: Length of the array
 *
 * Return: The value of A of the last array element
 */
int girtest_untyped_record_tester_get_a_from_last_element_pointer(GirTestUntypedRecordTester** array, int length)
{
    GirTestUntypedRecordTester** result = array += (length-1);
    return (*result)->a;
}