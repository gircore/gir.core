#include "primitivevaluetype.h"

/**
 * GirTestPrimitiveValueType:
 *
 * Contains functions for testing bindings with primitive value types.
 */

struct _GirTestPrimitiveValueType
{
    GObject parent_instance;
};

G_DEFINE_TYPE(GirTestPrimitiveValueType, girtest_primitive_value_type,
              G_TYPE_OBJECT)

static void
girtest_primitive_value_type_init(GirTestPrimitiveValueType *value)
{
}

static void
girtest_primitive_value_type_class_init(GirTestPrimitiveValueTypeClass *class)
{
}

/**
 * girtest_primitive_value_type_int_in:
 * @val: (in): An integer value
 *
 * Simple test for an input integer parameter.
 *
 * Returns: The input value multiplied by 2
 */
int girtest_primitive_value_type_int_in(int val)
{
    return 2 * val;
}
