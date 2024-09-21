#include "girtest-alias-tester.h"

/**
 * GirTestAliasTester:
 *
 * Contains functions for testing aliases.
 */

struct _GirTestAliasTester
{
    GObject parent_instance;
};

G_DEFINE_TYPE(GirTestAliasTester, girtest_alias_tester, G_TYPE_OBJECT)

static void
girtest_alias_tester_init(GirTestAliasTester *value)
{
}

static void
girtest_alias_tester_class_init(GirTestAliasTesterClass *class)
{
}

/**
 * girtest_alias_tester_to_int:
 * @alias: An IntAlias
 *
 * Converts a given IntAlias into its coresponding gint.
 */
int
girtest_alias_tester_to_int(GirTestIntAlias alias)
{
	return (int) alias;
}

/**
 * girtest_alias_tester_to_int_alias:
 * @i: A gint value
 *
 * Converts a given gint into its corresponding IntAlias.
 */
GirTestIntAlias
girtest_alias_tester_to_int_alias(int i)
{
	return (GirTestIntAlias) i;
}

/**
 * girtest_alias_tester_alias_out:
 * @alias: (out): IntAlias(42)
 *
 * Outputs an IntAlias with value 42.
 */
void
girtest_alias_tester_alias_out(GirTestIntAlias* alias)
{
	*alias = (GirTestIntAlias) 42;
}

/**
 * girtest_alias_tester_alias_in_out:
 * @alias: (inout): IntAlias
 *
 * Doubles the given IntAlias
 */
void
girtest_alias_tester_alias_in_out(GirTestIntAlias* alias)
{
	*alias = ((int) *alias) * 2;
}

/**
 * girtest_alias_tester_to_pointer:
 * @alias: A pointer alias
 *
 * Converts a given pointer alias into its coresponding gpointer.
 */
gpointer
girtest_alias_tester_to_pointer(GirTestPointerAlias alias)
{
    return (gpointer) alias;
}

/**
 * girtest_alias_tester_to_pointer_alias:
 * @pointer: A pointer value
 *
 * Converts a given gpointer into its corresponding pointer alias.
 */
GirTestPointerAlias
girtest_alias_tester_to_pointer_alias(gpointer pointer)
{
    return (GirTestPointerAlias) pointer;
}

/**
 * girtest_alias_tester_get_array_transfer_full:
 * @n_elements: (out) (optional): location for the length of the array or %NULL
 *
 * Always returns 3 IntAlias values from 1 to 3;
 *
 * Returns: (array length=n_elements) (transfer full): The array
 */
GirTestIntAlias*
girtest_alias_tester_get_array_transfer_full (guint *n_elements)
{
    if(n_elements)
        *n_elements = 3;

    GirTestIntAlias* array = g_new(GirTestIntAlias, 4);
    
    for(int i = 0; i <3; i++)
    {
        array[i] = i+1;
    }

    array[3] = 0;

    return array;
}
