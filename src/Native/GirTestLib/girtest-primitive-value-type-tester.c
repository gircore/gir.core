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

/**
 * girtest_primitive_value_type_tester_int_in_out:
 * @val: (inout): An integer value to multiply by 2
 *
 * Simple test for an in/out integer parameter.
 */
void girtest_primitive_value_type_tester_int_in_out(int *val)
{
    *val *= 2;
}

/**
 * girtest_primitive_value_type_tester_int_in_out_nullable:
 * @val: (inout) (nullable): An optional integer value to multiply by 2
 *
 * Simple test for a nullable in/out integer parameter.
 */
void girtest_primitive_value_type_tester_int_in_out_nullable(int *val)
{
    if (val)
        *val *= 2;
}

/**
 * girtest_primitive_value_type_tester_int_out:
 * @val: (out caller-allocates): An integer value to write to.
 *
 * Simple test for an out integer parameter.
 */
void girtest_primitive_value_type_tester_int_out(int *result)
{
    *result = 42;
}

/**
 * girtest_primitive_value_type_tester_int_out_nullable:
 * @val: (out) (nullable): An optional integer value to write to.
 *
 * Simple test for a nullable out integer parameter.
 */
void girtest_primitive_value_type_tester_int_out_nullable(int *result)
{
    if (result)
        *result = 42;
}
