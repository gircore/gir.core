#include "girtest-platform-string-array-null-terminated-tester.h"

/**
 * GirTestPlatformStringArrayNullTerminatedTester:
 *
 * Contains functions for testing bindings with string arrays.
 */

struct _GirTestPlatformStringArrayNullTerminatedTester
{
    GObject parent_instance;
};

G_DEFINE_TYPE(GirTestPlatformStringArrayNullTerminatedTester, girtest_platform_string_array_null_terminated_tester, G_TYPE_OBJECT)

static void
girtest_platform_string_array_null_terminated_tester_init(GirTestPlatformStringArrayNullTerminatedTester *value)
{
}

static void
girtest_platform_string_array_null_terminated_tester_class_init(GirTestPlatformStringArrayNullTerminatedTesterClass *class)
{
}

/**
 * girtest_platform_string_array_null_terminated_tester_get_last_element_transfer_none:
 * @data: (transfer none) (array zero-terminated=1) (element-type filename): data buffer
 *
 * Get's the last element of the utf8 zero terminated array
 *
 * Returns: (transfer none) (type filename): the last element of the array
 */
gchar*
girtest_platform_string_array_null_terminated_tester_get_last_element_transfer_none(gchar** data)
{
	guint length = g_strv_length(data);
    return data[length -1];
}

/**
 * girtest_platform_string_array_null_terminated_tester_get_last_element_transfer_full:
 * @data: (transfer full) (array zero-terminated=1) (element-type filename): data buffer
 *
 * Get's the last element of the utf8 zero terminated array
 *
 * Returns: (transfer full) (type filename): the last element of the array
 */
gchar*
girtest_platform_string_array_null_terminated_tester_get_last_element_transfer_full(gchar** data)
{
    guint length = g_strv_length(data);
    gchar* last_element = g_strdup(data[length -1]);
    g_strfreev(data);
    return last_element;
}

/**
 * girtest_platform_string_array_null_terminated_tester_get_last_element_transfer_none_nullable:
 * @data: (transfer none) (array zero-terminated=1) (nullable) (element-type filename): data buffer
 *
 * Get's the last element of the utf8 zero terminated array or NULL
 *
 * Returns: (transfer none) (nullable) (type filename): the last element of the array or NULL
 */
gchar*
girtest_platform_string_array_null_terminated_tester_get_last_element_transfer_none_nullable(gchar** data)
{
    if(!data)
        return NULL;

    guint length = g_strv_length(data);
    return data[length -1];
}

/**
 * girtest_platform_string_array_null_terminated_tester_get_last_element_transfer_full_nullable:
 * @data: (transfer full) (array zero-terminated=1) (nullable) (element-type filename): data buffer
 *
 * Get's the last element of the utf8 zero terminated array or NULL
 *
 * Returns: (transfer full) (nullable) (type filename): the last element of the array or NULL
 */
gchar*
girtest_platform_string_array_null_terminated_tester_get_last_element_transfer_full_nullable(gchar** data)
{
    if(!data)
        return NULL;

    guint length = g_strv_length(data);
    gchar* last_element = g_strdup(data[length -1]);
    g_strfreev(data);
    return last_element;
}

/**
 * girtest_platform_string_array_null_terminated_tester_return_transfer_full:
 * @data: (transfer none) (array zero-terminated=1) (element-type filename): data buffer
 *
 * Copies the string array and returns it
 *
 * Returns: (transfer full) (element-type filename): a copy of the array.
 */
gchar**
girtest_platform_string_array_null_terminated_tester_return_transfer_full(gchar** data)
{
    return g_strdupv(data);
}

/**
 * girtest_platform_string_array_null_terminated_tester_return_transfer_none:
 * @data: (transfer none) (array zero-terminated=1) (element-type filename): data buffer
 *
 * Returns the given string array
 *
 * Returns: (transfer none) (element-type filename): the given string array
 */
gchar**
girtest_platform_string_array_null_terminated_tester_return_transfer_none(gchar** data)
{
    return data;
}

/**
 * girtest_platform_string_array_null_terminated_tester_return_transfer_container:
 * @first: (transfer none) (type filename): First element of the array
 * @second: (transfer none) (type filename): Second element of the array
 *
 * Returns a new array which contains elements which are not allowed to be freed.
 *
 * Returns: (transfer container) (element-type filename): a new array with content which is not allowed to get freed.
 */
gchar**
girtest_platform_string_array_null_terminated_tester_return_transfer_container(gchar* first, gchar* second)
{
    gchar** data = g_malloc(sizeof(gchar*) * 3);
    data[0] = first;
    data[1] = second;
    data[2] = NULL;

    return data;
}

/**
 * girtest_platform_string_array_null_terminated_tester_return_transfer_full_nullable:
 * @data: (transfer none) (array zero-terminated=1) (nullable) (element-type filename): data buffer
 *
 * Copies the string array and returns it.
 *
 * Returns: (transfer full) (nullable) (element-type filename): a copy of the array or NULL if @data is NULL.
 */
gchar**
girtest_platform_string_array_null_terminated_tester_return_transfer_full_nullable(gchar** data)
{
    return g_strdupv(data);
}

/**
 * girtest_platform_string_array_null_terminated_tester_return_transfer_none_nullable:
 * @data: (transfer none) (array zero-terminated=1) (nullable) (element-type filename): data buffer
 *
 * Returns the given string array
 *
 * Returns: (transfer none) (nullable) (element-type filename): the given string array or NULL
 */
gchar**
girtest_platform_string_array_null_terminated_tester_return_transfer_none_nullable(gchar** data)
{
    return data;
}

/**
 * girtest_platform_string_array_null_terminated_tester_return_transfer_container_nullable:
 * @first: (transfer none) (type filename) (nullable): First element of the array
 * @second: (transfer none) (type filename) (nullable): Second element of the array
 * 
 * Returns a new array which contains elements which are not allowed to be freed. If any of the parameters is NULL,
 * NULL will be returned
 *
 * Returns: (transfer container) (nullable) (element-type filename): a new array with content which is not allowed to get freed or NULL
 */
gchar**
girtest_platform_string_array_null_terminated_tester_return_transfer_container_nullable(gchar* first, gchar* second)
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
 * girtest_platform_string_array_null_terminated_tester_optional_size_out_transfer_full:
 * @data: (transfer none) (array zero-terminated=1) (element-type filename): data
 * @size: (out) (optional): the length of the array or %NULL
 *
 * The returned array is implicitly NULL terminated. The API definition corresponds to
 * g_application_command_line_get_arguments in the GIO project.
 *
 * Returns: (array length=size) (element-type filename) (transfer full): the array
 */
gchar** 
girtest_platform_string_array_null_terminated_tester_optional_size_out_transfer_full(gchar** data, int *size)
{
    if(size)
        *size = g_strv_length(data);

    return g_strdupv(data);
}

/**
 * girtest_platform_string_array_null_terminated_tester_size_out_transfer_full:
 * @data: (transfer none) (array zero-terminated=1) (element-type filename): data
 * @size: (out): the length of the array
 *
 * The returned array is implicitly NULL terminated.
 *
 * Returns: (array length=size) (element-type filename) (transfer full): the array
 */
gchar** 
girtest_platform_string_array_null_terminated_tester_size_out_transfer_full(gchar** data, int *size)
{
    *size = g_strv_length(data);
    return g_strdupv(data);
}

/**
 * girtest_platform_string_array_null_terminated_tester_optional_gsize_parameter_out_transfer_full:
 * @data: (transfer none) (array zero-terminated=1) (element-type filename): data
 * @size: (out) (optional): the length of the array
 *
 * The returned array is implicitly NULL terminated.
 *
 * Returns: (array length=size) (transfer full) (element-type filename): the array
 */
gchar **
girtest_platform_string_array_null_terminated_tester_optional_gsize_parameter_out_transfer_full(gchar** data, gsize *size)
{
    if(size)
        *size = g_strv_length(data);

    return g_strdupv(data);
}

/**
 * girtest_platform_string_array_null_terminated_tester_gsize_parameter_out_transfer_full:
 * @data: (transfer none) (array zero-terminated=1) (element-type filename): data
 * @size: (out): the length of the array
 *
 * The returned array is implicitly NULL terminated.
 *
 * Returns: (array length=size) (transfer full) (element-type filename): the array
 */
gchar **
girtest_platform_string_array_null_terminated_tester_gsize_parameter_out_transfer_full(gchar** data, gsize *size)
{
    *size = g_strv_length(data);
    return g_strdupv(data);
}

/**
 * girtest_platform_string_array_null_terminated_tester_parameter_out_transfer_full:
 * @data: (transfer none) (array zero-terminated=1) (element-type filename): data buffer
 * @output: (transfer full) (array zero-terminated=1) (out) (element-type filename): output buffer
 *
 * Copies the string array and returns it as output parameter
 */
void
girtest_platform_string_array_null_terminated_tester_parameter_out_transfer_full(gchar** data, gchar*** output)
{
    *output = g_strdupv(data);
}

/**
 * girtest_platform_string_array_null_terminated_tester_parameter_out_transfer_none:
 * @data: (transfer none) (array zero-terminated=1) (element-type filename): data buffer
 * @output: (transfer none) (array zero-terminated=1) (out) (element-type filename): output buffer
 *
 * Assigns @data to @output.
 */
void
girtest_platform_string_array_null_terminated_tester_parameter_out_transfer_none(gchar** data, gchar*** output)
{
    *output = data;
}

/**
 * girtest_platform_string_array_null_terminated_tester_parameter_out_transfer_container:
 * @first: (transfer none) (type filename): First element of the array
 * @second: (transfer none) (type filename): Second element of the array
 * @output: (transfer container) (array zero-terminated=1) (out) (element-type filename): output buffer
 *
 * Creates a new array with content which is not allowed to get freed.
 */
void
girtest_platform_string_array_null_terminated_tester_parameter_out_transfer_container(gchar* first, gchar* second, gchar*** output)
{
    gchar** data = g_malloc(sizeof(gchar*) * 3);
    data[0] = first;
    data[1] = second;
    data[2] = NULL;

    *output = data;
}

/**
 * girtest_platform_string_array_null_terminated_tester_parameter_out_transfer_full_nullable:
 * @data: (transfer none) (array zero-terminated=1) (nullable) (element-type filename): data buffer
 * @output: (transfer full) (array zero-terminated=1) (out) (nullable) (element-type filename): output buffer
 *
 * Copies the string array and returns it as output parameter
 */
void
girtest_platform_string_array_null_terminated_tester_parameter_out_transfer_full_nullable(gchar** data, gchar*** output)
{
    *output = g_strdupv(data);
}

/**
 * girtest_platform_string_array_null_terminated_tester_parameter_out_transfer_none_nullable:
 * @data: (transfer none) (array zero-terminated=1) (nullable) (element-type filename): data buffer
 * @output: (transfer none) (array zero-terminated=1) (out) (nullable) (element-type filename): output buffer
 *
 * Assigns @data to @output.
 */
void
girtest_platform_string_array_null_terminated_tester_parameter_out_transfer_none_nullable(gchar** data, gchar*** output)
{
    *output = data;
}

/**
 * girtest_platform_string_array_null_terminated_tester_parameter_out_transfer_container_nullable:
 * @first: (transfer none) (type filename) (nullable): First element of the array
 * @second: (transfer none) (type filename) (nullable): Second element of the array
 * @output: (transfer container) (array zero-terminated=1) (out) (nullable) (element-type filename): output buffer
 *
 * Creates a new array with content which is not allowed to get freed. If @first or @second is NULL *output will be NULL.
 */
void
girtest_platform_string_array_null_terminated_tester_parameter_out_transfer_container_nullable(gchar* first, gchar* second, gchar*** output)
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