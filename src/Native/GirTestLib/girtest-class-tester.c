#include "girtest-class-tester.h"

/**
 * GirTestClassTester:
 *
 * Contains functions for testing bindings with class types.
 */

struct _GirTestClassTester
{
    GObject parent_instance;
};

G_DEFINE_TYPE(GirTestClassTester, girtest_class_tester, G_TYPE_OBJECT)

static void
girtest_class_tester_init(GirTestClassTester *value)
{
}

static void
girtest_class_tester_class_init(GirTestClassTesterClass *class)
{
}

/**
 * girtest_class_tester_transfer_ownership_full_and_unref:
 * @object: (transfer full): Any object
 *
 * Simple test to transfer ownership to C. The object is unrefed immediately.
 */
void
girtest_class_tester_transfer_ownership_full_and_unref(GObject *object)
{
    g_object_unref(object);
}
