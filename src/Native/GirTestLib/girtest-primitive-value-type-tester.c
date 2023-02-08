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
void
girtest_primitive_value_type_tester_int_in_out(int *val)
{
    *val *= 2;
}

/**
 * girtest_primitive_value_type_tester_int_in_out_nullable:
 * @val: (inout) (nullable): A nullable integer value.
 * If the value is null the parameter will be -1.
 * For all other cases the value is multiplied by 2. 
 *
 * Simple test for a nullable in/out integer parameter.
 */
void
girtest_primitive_value_type_tester_int_in_out_nullable(int *val)
{
    if (!val)
        *val = -1;
    else
        *val *= 2;
}

/**
 * girtest_primitive_value_type_tester_int_out:
 * @result: (out): An integer which outputs 42.
 *
 * Simple test for an out integer parameter.
 */
void
girtest_primitive_value_type_tester_int_out(int *result)
{
    *result = 42;
}

/**
 * girtest_primitive_value_type_tester_int_out_nullable:
 * @return_null: A boolean defining the value for "result".
 * @result: (out) (nullable): A nullable integer which returns 42 if "return_null" is false otherwise NULL.
 *
 * Simple test for a nullable out integer parameter.
 */
void
girtest_primitive_value_type_tester_int_out_nullable(gboolean return_null, int *result)
{
    if (return_null)
        result = NULL;
    else
        *result = 42;
}
