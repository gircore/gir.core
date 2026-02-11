#include "girtest-enum-tester.h"

/**
 * GirTestEnumTester:
 *
 * Contains functions for testing enumerations.
 */

struct _GirTestEnumTester
{
    GObject parent_instance;
};

G_DEFINE_TYPE(GirTestEnumTester, girtest_enum_tester, G_TYPE_OBJECT)

static void
girtest_enum_tester_init(GirTestEnumTester *value)
{
}

static void
girtest_enum_tester_class_init(GirTestEnumTesterClass *class)
{
}

/**
 * girtest_enum_tester_out_parameter:
 * @out_enum: (out): location to store a #GirTestEnumTesterSimpleEnum or %NULL
 **/
void girtest_enum_tester_out_parameter(GirTestEnumTesterSimpleEnum *out_enum)
{
	if (out_enum)
		*out_enum = SIMPLE_ENUM_A;
}

/**
 * girtest_enum_tester_ref_parameter:
 * @ref_enum: (inout): location to store a #GirTestEnumTesterSimpleEnum or %NULL
 **/
void girtest_enum_tester_ref_parameter(GirTestEnumTesterSimpleEnum *ref_enum)
{
	// Change A (input) to B (output)
	if (ref_enum && *ref_enum == SIMPLE_ENUM_A)
		*ref_enum = SIMPLE_ENUM_B;
}