#include "girtest-primitive-value-type-tester.h"

/**
 * GirTestPrimitiveValueTypeTester:
 *
 * Contains functions for testing bindings with primitive value types.
 */

struct _GirTestPrimitiveValueTypeTester
{
    GObject parent_instance;
};

G_DEFINE_TYPE(GirTestPrimitiveValueTypeTester, girtest_primitive_value_type_tester, G_TYPE_OBJECT)

static void
girtest_primitive_value_type_tester_init(GirTestPrimitiveValueTypeTester *value)
{
}

static void
girtest_primitive_value_type_tester_class_init(GirTestPrimitiveValueTypeTesterClass *class)
{
}

/**
 * girtest_primitive_value_type_tester_int_in:
 * @val: (in): An integer value
 *
 * Simple test for an input integer parameter.
 *
 * Returns: The input value multiplied by 2
 */
int
girtest_primitive_value_type_tester_int_in(int val)
{
    return 2 * val;
}
