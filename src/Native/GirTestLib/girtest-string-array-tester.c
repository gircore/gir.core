#include "girtest-string-array-tester.h"

/**
 * GirTestStringArrayTester:
 *
 * Contains functions for testing bindings with string arrays.
 */

const char* data[] = { "FOO", "BAR", NULL };

struct _GirTestStringArrayTester
{
    GObject parent_instance;
};

G_DEFINE_TYPE(GirTestStringArrayTester, girtest_string_array_tester, G_TYPE_OBJECT)

static void
girtest_string_array_tester_init(GirTestStringArrayTester *value)
{
}

static void
girtest_string_array_tester_class_init(GirTestStringArrayTesterClass *class)
{
}

/**
 * girtest_string_array_tester_utf8_return_transfer_none:
 *
 * Returns an array.
 *
 * Returns: (transfer none): The array
 */
const char** girtest_string_array_tester_utf8_return_transfer_none()
{
    return data;
}

/**
 * girtest_string_array_tester_filename_return_transfer_none:
 *
 * Returns an array.
 *
 * Returns: (transfer none) (element-type filename): The array
 */
const char** girtest_string_array_tester_filename_return_transfer_none()
{
    return data;
}

/**
 * girtest_string_array_tester_utf8_return_transfer_none_nullable:
 *
 * Returns an array.
 *
 * Returns: (transfer none) (nullable): The array
 */
const char** girtest_string_array_tester_utf8_return_transfer_none_nullable(gboolean return_null)
{
	if(return_null)
		return NULL;

    return data;
}

/**
 * girtest_string_array_tester_filename_return_transfer_none_nullable:
 *
 * Returns an array.
 *
 * Returns: (transfer none) (element-type filename) (nullable): The array
 */
const char** girtest_string_array_tester_filename_return_transfer_none_nullable(gboolean return_null)
{
	if(return_null)
		return NULL;

    return data;
}

/**
 * girtest_string_array_tester_utf8_return_element_parameter_null_terminated_transfer_none:
 * @data: (array zero-terminated=1) (element-type utf8) (transfer none): Array
 * @position: The index to return
 *
 * Returns the string at the given position.
 *
 * Returns: (transfer full): The string of the array from the given position
 */
gchar* girtest_string_array_tester_utf8_return_element_parameter_null_terminated_transfer_none(const gchar** data, int position)
{
    return g_strdup(data[position]);
}

/**
 * girtest_string_array_tester_utf8_return_element_parameter_null_terminated_transfer_none_nullable:
 * @data: (array zero-terminated=1) (element-type utf8) (transfer none) (nullable): Array
 * @position: The index to return
 *
 * Returns the string at the given position.
 *
 * Returns: (transfer full) (nullable): The string of the array from the given position
 */
gchar* girtest_string_array_tester_utf8_return_element_parameter_null_terminated_transfer_none_nullable(const gchar** data, int position)
{
	if(!data)
		return NULL;

    return g_strdup(data[position]);
}

/**
 * girtest_string_array_tester_utf8_return_element_parameter_transfer_none_nullable_with_size:
 * @data: (array length=data_size) (element-type utf8) (transfer none) (nullable): Array
 * @data_size: The number of values in @data
 * @position: The index to return
 *
 * Returns the string at the given position.
 *
 * Returns: (transfer full) (nullable): The string of the array from the given position
 */
gchar* girtest_string_array_tester_utf8_return_element_parameter_transfer_none_nullable_with_size(const gchar** data, int data_size, int position)
{
    if(!data)
        return NULL;

    return g_strdup(data[position]);
}

/**
 * girtest_string_array_tester_filename_return_element_parameter_null_terminated_transfer_none:
 * @data: (array zero-terminated=1) (element-type filename) (transfer none): Array
 * @position: The index to return
 *
 * Returns the string at the given position.
 *
 * Returns: (transfer full) (type filename): The string of the array from the given position
 */
gchar* girtest_string_array_tester_filename_return_element_parameter_null_terminated_transfer_none(const gchar** data, int position)
{
    return g_strdup(data[position]);
}

/**
 * girtest_string_array_tester_filename_return_element_parameter_null_terminated_transfer_none_nullable:
 * @data: (array zero-terminated=1) (element-type filename) (transfer none) (nullable): Array
 * @position: The index to return
 *
 * Returns the string at the given position.
 *
 * Returns: (transfer full) (type filename) (nullable): The string of the array from the given position
 */
gchar* girtest_string_array_tester_filename_return_element_parameter_null_terminated_transfer_none_nullable(const gchar** data, int position)
{
	if(!data)
		return NULL;

    return g_strdup(data[position]);
}