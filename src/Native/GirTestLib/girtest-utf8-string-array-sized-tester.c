#include "girtest-utf8-string-array-sized-tester.h"

/**
 * GirTestUtf8StringArraySizedTester:
 *
 * Contains functions for testing bindings with string arrays.
 */

const char* sized_data[] = { "FOO", "BAR", NULL };

struct _GirTestUtf8StringArraySizedTester
{
    GObject parent_instance;
};

G_DEFINE_TYPE(GirTestUtf8StringArraySizedTester, girtest_utf8_string_array_sized_tester, G_TYPE_OBJECT)

static void
girtest_utf8_string_array_sized_tester_init(GirTestUtf8StringArraySizedTester *value)
{
}

static void
girtest_utf8_string_array_sized_tester_class_init(GirTestUtf8StringArraySizedTesterClass *class)
{
}

/**
 * girtest_utf8_string_array_sized_tester_parameter_inout_with_length_parameter_transfer_full:
 * @argc: (inout) (optional): pointer to application's argc
 * @argv: (inout) (transfer full) (array length=argc) (nullable): pointer to application's argv
 *
 * Returns an array with content 'hello', 'world'.
 */
void 
girtest_utf8_string_array_sized_tester_parameter_inout_with_length_parameter_transfer_full(int* argc, char*** argv)
{
    *argc = 2;
    
    g_autoptr(GStrvBuilder) builder = g_strv_builder_new ();
    g_strv_builder_add (builder, "hello");
    g_strv_builder_add (builder, "world");

    *argv = g_strv_builder_end (builder);
}

/**
 * girtest_utf8_string_array_sized_tester_parameter_in_with_length_parameter_transfer_none:
 * @elements: number of array elements
 * @array: (array length=elements) (transfer none) (nullable): array
 * 
 * Returns: (nullable) (transfer full): The first element of the given array or NULL if the array is empty or NULL.
 *
 * String array parameter with length.
 */
char*
girtest_utf8_string_array_sized_tester_parameter_in_with_length_parameter_transfer_none(int elements, char** array)
{
    if (elements <= 0 || array == NULL)
        return NULL;

   return g_strdup(array[elements - 1]);
}

/**
 * girtest_utf8_string_array_sized_tester_parameter_in_with_length_parameter_transfer_full:
 * @elements: number of array elements
 * @array: (array length=elements) (transfer full) (nullable): array
 * 
 * Returns: (nullable) (transfer full): The first element of the given array or NULL if the array is empty or NULL.
 *
 * String array parameter with length.
 */
char*
girtest_utf8_string_array_sized_tester_parameter_in_with_length_parameter_transfer_full(int elements, char** array)
{
    if (elements <= 0 || array == NULL)
        return NULL;

    char* result = g_strdup(array[elements - 1]);
    
    for (int i = 0; i< elements; i++)
    {
        g_free(array[i]);
    }
    g_free(array);
    
    return result;
}

/**
 * girtest_utf8_string_array_sized_tester_parameter_out_with_length_parameter_transfer_none:
 * @returnNull: wether NULL should be passed to the output parameters
 * @elements: (out): number of array elements
 * @array: (array length=elements) (transfer none) (nullable) (out) (optional): array
 *
 * String array parameter with length.
 */
void
girtest_utf8_string_array_sized_tester_parameter_out_with_length_parameter_transfer_none(gboolean returnNull, int* elements, const char*** array)
{
    if (returnNull)
    {
        elements = 0;
        array = NULL;
        return;    
    }
    
    *elements = 2;
    *array = sized_data;
}

/**
 * girtest_utf8_string_array_sized_tester_parameter_out_with_length_parameter_transfer_full:
 * @returnNull: wether NULL should be passed to the output parameters
 * @elements: (out): number of array elements
 * @array: (array length=elements) (transfer full) (nullable) (out) (optional): array
 *
 * String array parameter with length.
 */
void
girtest_utf8_string_array_sized_tester_parameter_out_with_length_parameter_transfer_full(gboolean returnNull, int* elements, char*** array)
{
    if (returnNull)
    {
        elements = 0;
        array = NULL;
        return;    
    }
    
    *elements = 2;

    g_autoptr(GStrvBuilder) builder = g_strv_builder_new ();
    g_strv_builder_add (builder, "hello");
    g_strv_builder_add (builder, "world");

    char** result = g_strv_builder_end (builder);
   
    if (array)
        *array = result;
}