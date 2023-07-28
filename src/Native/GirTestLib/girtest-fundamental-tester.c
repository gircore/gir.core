#include "girtest-fundamental-tester.h"

#include <gobject/gvaluecollector.h>

/**
 * GirTestFundamentalTester:
 *
 * Unit tests for fundamental types.
 */

struct _GirTestFundamentalTester
{
    GObject parent_instance;
};

G_DEFINE_TYPE(GirTestFundamentalTester, girtest_fundamental_tester, G_TYPE_OBJECT)

static void
girtest_fundamental_tester_init(GirTestFundamentalTester *value)
{
}

static void
girtest_fundamental_tester_class_init(GirTestFundamentalTesterClass *class)
{
}

/**
 * girtest_fundamental_tester_create_fundamental:
 * @return_null: Iff true, NULL will be returned.
 *
 * Creates a new `GirTestInstantiatableFundamental`.
 *
 * Returns: (transfer full) (nullable): The newly created `GirTestInstantiatableFundamental`.
 */
GirTestInstantiatableFundamental*
girtest_fundamental_tester_create_fundamental (gboolean return_null)
{
    if (return_null) {
        return NULL;
    }
    GirTestInstantiatableFundamental *tester = girtest_instantiatable_fundamental_new();
    return tester;
}
