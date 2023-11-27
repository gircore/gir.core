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