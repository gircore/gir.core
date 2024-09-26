#include "girtest-integer-array-tester.h"

/**
 * GirTestIntegerArrayTester:
 *
 * Contains functions for testing bindings with integer arrays.
 */

struct _GirTestIntegerArrayTester
{
    GObject parent_instance;
};

G_DEFINE_TYPE(GirTestIntegerArrayTester, girtest_integer_array_tester, G_TYPE_OBJECT)

static gint data[] = { 1, 2, 3 };

static void
girtest_integer_array_tester_init(GirTestIntegerArrayTester *value)
{
}

static void
girtest_integer_array_tester_class_init(GirTestIntegerArrayTesterClass *class)
{
}

/**
 * girtest_integer_array_tester_data_return:
 *
 * Simple test for an array return value.
 *
 * Returns: a read-only pointer to the raw data
 */
const gint*
girtest_integer_array_tester_data_return()
{
    return &data[0];
}

/**
 * girtest_integer_array_tester_get_data_count:
 *
 * Returns the count of the data
 *
 * Returns: count of the data
 */
gsize
girtest_integer_array_tester_get_data_count()
{
    return sizeof(data) / sizeof(gint);
}

/**
 * girtest_integer_array_tester_data_out_caller_allocates:
 * @buffer: (array length=count) (element-type gint) (out caller-allocates):
 *     a buffer to read data into (which should be at least count bytes long).
 * @count: (in): the number of bytes that will be read
 *
 * Simple test for an caller allocated out array
 *
 * Returns: Number of bytes read, or -1 on error, or 0 on end of file.
 */
gssize
girtest_integer_array_tester_data_out_caller_allocates(void *buffer, gsize count)
{
	g_return_val_if_fail (buffer != NULL, 0);

	if (count == 0)
    	return 0;

	if(count > (sizeof(data) / sizeof(gint)))
		count = (sizeof(data) / sizeof(gint));

	gint* ptr = buffer;

	for(int i = 0; i < count; i++)
	{
		*ptr = data[i];
		ptr++;
	}


    return ptr - (gint*) buffer;
}

/**
 * girtest_integer_array_tester_invoke_callback:
 * @callback: (scope call): a function that is called to get integer data
 *
 * Calls the callback to transfer data.
 **/
void
girtest_integer_array_tester_invoke_callback (GirTestIntegerArrayTesterCallback callback)
{
    callback(&data[0], sizeof(data)/sizeof(gint)); 
}

/**
 * girtest_integer_array_tester_invoke_callback_no_length:
 * @callback: (scope call): a function that is called to get integer data
 *
 * Calls the callback to transfer data.
 **/
void
girtest_integer_array_tester_invoke_callback_no_length(GirTestIntegerArrayTesterCallbackNoLength callback)
{
    callback(&data[0]);
}


/**
 * girtest_integer_array_tester_get_data_from_const_pointer:
 * @data: (transfer none) (array length=size) (element-type gint) (nullable):
 *        the data to be used
 * @size: the size of @data
 *
 * Returns: The last integer of the given data.
 */
gint
girtest_integer_array_tester_get_data_from_const_pointer(gconstpointer data, gsize size)
{
    gint* p = (gint*) data;
    return *(p + (size -1));
}

/**
 * girtest_integer_array_tester_get_data_with_gint64_size:
 * @data: (transfer none) (array length=size) (element-type gint):
 *        the data to be used
 * @size: the size of @data
 *
 * Returns: The last integer of the given data.
 */
gint
girtest_integer_array_tester_get_data_with_gint64_size(gconstpointer data, gint64 size)
{
    gint* p = (gint*) data;
    return *(p + (size -1));
}

/**
 * girtest_integer_array_tester_clear_data:
 * @data: (array length=count) (element-type gint) (inout):
 *     data which will be set to 0
 * @count: (in): the number of elements of data.
 *
 * Sets all data to 0
 */
void
girtest_integer_array_tester_clear_data(void *data, gsize count)
{
    gint* ptr = data;
    for(int i = 0; i < count; i++)
    {
        *ptr = 0;
        ptr++;
    }
}