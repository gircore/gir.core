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
 * girtest_primitive_value_type_tester_int_pointer_in:
 * @val: (in): An integer value
 *
 * Simple test for an input pointed integer parameter.
 * The pointed value is multiplied by 2.
 */
void
girtest_primitive_value_type_tester_int_pointer_in(int *val)
{
    *val = *val * 2;
}

/**
 * girtest_primitive_value_type_tester_int_in_out:
 * @val: (inout): An integer value to multiply by 2
 *
 * Simple test for an in/out integer parameter.
 */
void
girtest_primitive_value_type_tester_int_in_out(int *val)
{
    *val *= 2;
}

/**
 * girtest_primitive_value_type_tester_int_in_out_optional:
 * @val: (inout) (optional): An optional integer value to multiply by 2
 *
 * Simple test for a optional in/out integer parameter.
 */
void
girtest_primitive_value_type_tester_int_in_out_optional(int *val)
{
    if (val)
        *val *= 2;
}

/**
 * girtest_primitive_value_type_tester_int_out:
 * @result: (out): An integer value to write to.
 *
 * Simple test for an out integer parameter.
 */
void
girtest_primitive_value_type_tester_int_out(int *result)
{
    *result = 42;
}

/**
 * girtest_primitive_value_type_tester_int_out_optional:
 * @result: (out) (optional): An optional integer value to write to.
 *
 * Simple test for a optional out integer parameter.
 */
void
girtest_primitive_value_type_tester_int_out_optional(int *result)
{
    if (result)
        *result = 42;
}
