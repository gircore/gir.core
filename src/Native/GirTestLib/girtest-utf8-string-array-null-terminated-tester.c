#include "girtest-utf8-string-array-null-terminated-tester.h"

/**
 * GirTestUtf8StringArrayNullTerminatedTester:
 *
 * Contains functions for testing bindings with string arrays.
 */

struct _GirTestUtf8StringArrayNullTerminatedTester
{
    GObject parent_instance;
};

G_DEFINE_TYPE(GirTestUtf8StringArrayNullTerminatedTester, girtest_utf8_string_array_null_terminated_tester, G_TYPE_OBJECT)

static void
girtest_utf8_string_array_null_terminated_tester_init(GirTestUtf8StringArrayNullTerminatedTester *value)
{
}

static void
girtest_utf8_string_array_null_terminated_tester_class_init(GirTestUtf8StringArrayNullTerminatedTesterClass *class)
{
}

/**
 * girtest_utf8_string_array_null_terminated_tester_get_last_element_transfer_none:
 * @data: (transfer none) (array zero-terminated=1): data buffer
 *
 * Get's the last element of the utf8 zero terminated array
 *
 * Returns: (transfer none): the last element of the array
 */
gchar*
girtest_utf8_string_array_null_terminated_tester_get_last_element_transfer_none(gchar** data)
{
	guint length = g_strv_length(data);
    return data[length -1];
}

/**
 * girtest_utf8_string_array_null_terminated_tester_get_last_element_transfer_full:
 * @data: (transfer full) (array zero-terminated=1): data buffer
 *
 * Get's the last element of the utf8 zero terminated array
 *
 * Returns: (transfer full): the last element of the array
 */
gchar*
girtest_utf8_string_array_null_terminated_tester_get_last_element_transfer_full(gchar** data)
{
    guint length = g_strv_length(data);
    gchar* last_element = g_strdup(data[length -1]);
    g_strfreev(data);
    return last_element;
}

/**
 * girtest_utf8_string_array_null_terminated_tester_get_last_element_transfer_none_nullable:
 * @data: (transfer none) (array zero-terminated=1) (nullable): data buffer
 *
 * Get's the last element of the utf8 zero terminated array or NULL
 *
 * Returns: (transfer none) (nullable): the last element of the array or NULL
 */
gchar*
girtest_utf8_string_array_null_terminated_tester_get_last_element_transfer_none_nullable(gchar** data)
{
    if(!data)
        return NULL;

    guint length = g_strv_length(data);
    return data[length -1];
}

/**
 * girtest_utf8_string_array_null_terminated_tester_get_last_element_transfer_full_nullable:
 * @data: (transfer full) (array zero-terminated=1) (nullable): data buffer
 *
 * Get's the last element of the utf8 zero terminated array or NULL
 *
 * Returns: (transfer full) (nullable): the last element of the array or NULL
 */
gchar*
girtest_utf8_string_array_null_terminated_tester_get_last_element_transfer_full_nullable(gchar** data)
{
    if(!data)
        return NULL;

    guint length = g_strv_length(data);
    gchar* last_element = g_strdup(data[length -1]);
    g_strfreev(data);
    return last_element;
}

/**
 * girtest_utf8_string_array_null_terminated_tester_return_transfer_full:
 * @data: (transfer none) (array zero-terminated=1): data buffer
 *
 * Copies the string array and returns it
 *
 * Returns: (transfer full): a copy of the array.
 */
gchar**
girtest_utf8_string_array_null_terminated_tester_return_transfer_full(gchar** data)
{
    return g_strdupv(data);
}

/**
 * girtest_utf8_string_array_null_terminated_tester_return_transfer_none:
 * @data: (transfer none) (array zero-terminated=1): data buffer
 *
 * Returns the given string array
 *
 * Returns: (transfer none): the given string array
 */
gchar**
girtest_utf8_string_array_null_terminated_tester_return_transfer_none(gchar** data)
{
    return data;
}

/**
 * girtest_utf8_string_array_null_terminated_tester_return_transfer_container:
 * @first: (transfer none): First element of the array
 * @second: (transfer none): Second element of the array
 *
 * Returns a new array which contains elements which are not allowed to be freed.
 *
 * Returns: (transfer container): a new array with content which is not allowed to get freed.
 */
gchar**
girtest_utf8_string_array_null_terminated_tester_return_transfer_container(gchar* first, gchar* second)
{
    gchar** data = g_malloc(sizeof(gchar*) * 3);
    data[0] = first;
    data[1] = second;
    data[2] = NULL;

    return data;
}

/**
 * girtest_utf8_string_array_null_terminated_tester_return_transfer_full_nullable:
 * @data: (transfer none) (array zero-terminated=1) (nullable): data buffer
 *
 * Copies the string array and returns it.
 *
 * Returns: (transfer full) (nullable): a copy of the array or NULL if @data is NULL.
 */
gchar**
girtest_utf8_string_array_null_terminated_tester_return_transfer_full_nullable(gchar** data)
{
    return g_strdupv(data);
}

/**
 * girtest_utf8_string_array_null_terminated_tester_return_transfer_none_nullable:
 * @data: (transfer none) (array zero-terminated=1) (nullable): data buffer
 *
 * Returns the given string array
 *
 * Returns: (transfer none) (nullable): the given string array or NULL
 */
gchar**
girtest_utf8_string_array_null_terminated_tester_return_transfer_none_nullable(gchar** data)
{
    return data;
}

/**
 * girtest_utf8_string_array_null_terminated_tester_return_transfer_container_nullable:
 * @first: (transfer none) (nullable): First element of the array
 * @second: (transfer none) (nullable): Second element of the array
 * 
 * Returns a new array which contains elements which are not allowed to be freed. If any of the parameters is NULL,
 * NULL will be returned
 *
 * Returns: (transfer container) (nullable): a new array with content which is not allowed to get freed or NULL
 */
gchar**
girtest_utf8_string_array_null_terminated_tester_return_transfer_container_nullable(gchar* first, gchar* second)
{
    if(!first)
        return NULL;

    if(!second)
        return NULL;

    gchar** data = g_malloc(sizeof(gchar*) * 3);
    data[0] = first;
    data[1] = second;
    data[2] = NULL;

    return data;
}

/**
 * girtest_utf8_string_array_null_terminated_tester_parameter_out_transfer_full:
 * @data: (transfer none) (array zero-terminated=1): data buffer
 * @output: (transfer full) (array zero-terminated=1) (out): output buffer
 *
 * Copies the string array and returns it as output parameter
 */
void
girtest_utf8_string_array_null_terminated_tester_parameter_out_transfer_full(gchar** data, gchar*** output)
{
    *output = g_strdupv(data);
}

/**
 * girtest_utf8_string_array_null_terminated_tester_parameter_out_transfer_none:
 * @data: (transfer none) (array zero-terminated=1): data buffer
 * @output: (transfer none) (array zero-terminated=1) (out): output buffer
 *
 * Assigns @data to @output.
 */
void
girtest_utf8_string_array_null_terminated_tester_parameter_out_transfer_none(gchar** data, gchar*** output)
{
    *output = data;
}

/**
 * girtest_utf8_string_array_null_terminated_tester_parameter_out_transfer_container:
 * @first: (transfer none): First element of the array
 * @second: (transfer none): Second element of the array
 * @output: (transfer container) (array zero-terminated=1) (out): output buffer
 *
 * Creates a new array with content which is not allowed to get freed.
 */
void
girtest_utf8_string_array_null_terminated_tester_parameter_out_transfer_container(gchar* first, gchar* second, gchar*** output)
{
    gchar** data = g_malloc(sizeof(gchar*) * 3);
    data[0] = first;
    data[1] = second;
    data[2] = NULL;

    *output = data;
}

/**
 * girtest_utf8_string_array_null_terminated_tester_parameter_out_transfer_full_nullable:
 * @data: (transfer none) (array zero-terminated=1) (nullable): data buffer
 * @output: (transfer full) (array zero-terminated=1) (out) (nullable): output buffer
 *
 * Copies the string array and returns it as output parameter
 */
void
girtest_utf8_string_array_null_terminated_tester_parameter_out_transfer_full_nullable(gchar** data, gchar*** output)
{
    *output = g_strdupv(data);
}

/**
 * girtest_utf8_string_array_null_terminated_tester_parameter_out_transfer_none_nullable:
 * @data: (transfer none) (array zero-terminated=1) (nullable): data buffer
 * @output: (transfer none) (array zero-terminated=1) (out) (nullable): output buffer
 *
 * Assigns @data to @output.
 */
void
girtest_utf8_string_array_null_terminated_tester_parameter_out_transfer_none_nullable(gchar** data, gchar*** output)
{
    *output = data;
}

/**
 * girtest_utf8_string_array_null_terminated_tester_parameter_out_transfer_container_nullable:
 * @first: (transfer none) (nullable): First element of the array
 * @second: (transfer none) (nullable): Second element of the array
 * @output: (transfer container) (array zero-terminated=1) (out) (nullable): output buffer
 *
 * Creates a new array with content which is not allowed to get freed. If @first or @second is NULL *output will be NULL.
 */
void
girtest_utf8_string_array_null_terminated_tester_parameter_out_transfer_container_nullable(gchar* first, gchar* second, gchar*** output)
{
    if(!first || !second)
    {
        *output = NULL;
    }
    else
    {
        gchar** data = g_malloc(sizeof(gchar*) * 3);
        data[0] = first;
        data[1] = second;
        data[2] = NULL;

        *output = data;
    }
}