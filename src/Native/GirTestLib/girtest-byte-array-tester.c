#include "girtest-byte-array-tester.h"

/**
 * GirTestByteArrayTester:
 *
 * Contains functions for testing bindings with byte arrays.
 */

struct _GirTestByteArrayTester
{
    GObject parent_instance;
};

G_DEFINE_TYPE(GirTestByteArrayTester, girtest_byte_array_tester, G_TYPE_OBJECT)

static guchar data[] = { 0x00, 0x11, 0x22 };

static void
girtest_byte_array_tester_init(GirTestByteArrayTester *value)
{
}

static void
girtest_byte_array_tester_class_init(GirTestByteArrayTesterClass *class)
{
}

/**
 * girtest_byte_array_tester_data_return:
 *
 * Simple test for an array return value.
 *
 * Returns: a read-only pointer to the raw data
 */
const guchar*
girtest_byte_array_tester_data_return()
{
    return &data[0];
}

/**
 * girtest_byte_array_tester_get_data_size:
 *
 * Returns the size in byte of the data
 *
 * Returns: size of the data
 */
gsize
girtest_byte_array_tester_get_data_size()
{
    return sizeof(data);
}

/**
 * girtest_byte_array_tester_data_out_caller_allocates:
 * @buffer: (array length=count) (element-type guint8) (out caller-allocates):
 *     a buffer to read data into (which should be at least count bytes long).
 * @count: (in): the number of bytes that will be read
 *
 * Simple test for an caller allocated out array
 *
 * Returns: Number of bytes read, or -1 on error, or 0 on end of file.
 */
gssize
girtest_byte_array_tester_data_out_caller_allocates(void *buffer, gsize count)
{
	g_return_val_if_fail (buffer != NULL, 0);

	if (count == 0)
    	return 0;

	if(count > sizeof(data))
		count = sizeof(data);

	guchar* ptr = buffer;

	for(int i = 0; i < count; i++)
	{
		*ptr = data[i];
		ptr++;
	}


    return ptr - (guchar*) buffer;
}

/**
 * girtest_byte_array_tester_invoke_callback:
 * @callback: (scope call): a function that is called to get byte data
 *
 * Calls the callback to transfer data.
 **/
void
girtest_byte_array_tester_invoke_callback (GirTestByteArrayTesterCallback callback)
{
    callback(&data[0], sizeof(data)); 
}

/**
 * girtest_byte_array_tester_invoke_callback_no_length:
 * @callback: (scope call): a function that is called to get byte data
 *
 * Calls the callback to transfer data.
 **/
void
girtest_byte_array_tester_invoke_callback_no_length(GirTestByteArrayTesterCallbackNoLength callback)
{
    callback(&data[0]);
}


/**
 * girtest_byte_array_tester_get_data_from_const_pointer:
 * @data: (transfer none) (array length=size) (element-type guint8) (nullable):
 *        the data to be used
 * @size: the size of @data
 *
 * Returns: The last byte of the given data.
 */

guint8
girtest_byte_array_tester_get_data_from_const_pointer(gconstpointer data, gsize size)
{
    guint8* p = (guint8*) data;
    return *(p + (size -1));
}

/**
 * girtest_byte_array_tester_clear_data:
 * @data: (array length=count) (element-type guint8) (inout):
 *     data which will be set to 0
 * @count: (in): the number of elements of data.
 *
 * Sets all data to 0
 */
void
girtest_byte_array_tester_clear_data(void *data, gsize count)
{
    guchar* ptr = data;
    for(int i = 0; i < count; i++)
    {
        *ptr = 0x00;
        ptr++;
    }
}