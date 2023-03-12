#include "girtest-rename-to-tester.h"

/**
 * GirTestRenameToTester:
 *
 * Contains functions for testing functions shadowing each other
 */

struct _GirTestRenameToTester
{
    GObject parent_instance;
};

G_DEFINE_TYPE(GirTestRenameToTester, girtest_rename_to_tester, G_TYPE_OBJECT)

static void
girtest_rename_to_tester_init(GirTestRenameToTester *value)
{
}

static void
girtest_rename_to_tester_class_init(GirTestRenameToTesterClass *class)
{
}

/**
 * girtest_rename_to_tester_get:
 * @i: Integer to mirror to the return value
 *
 * Simple test to verify renaming of functions.
 *
 * Returns: The input value.
 */
gint
girtest_rename_to_tester_get(gint i)
{
    return i;
}

/**
 * girtest_rename_to_tester_get2: (rename-to girtest_rename_to_tester_get)
 * @i1: Integer to add to the return value
 * @i2: Integer to add to the return value
 *
 * Simple test to verify renaming of functions.
 *
 * Returns: The sum of the input values.
 */
gint
girtest_rename_to_tester_get2(gint i1, gint i2)
{
    return i1 + i2;
}
