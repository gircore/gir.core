#include "girtest-class-tester.h"

/**
 * GirTestClassTester:
 *
 * Contains functions for testing bindings with class types.
 */

struct _GirTestClassTester
{
    GObject parent_instance;
    GirTestExecutor* executor;
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
 * girtest_class_tester_new:
 *
 * Creates a new `GirTestClassTester`.
 *
 * Returns: The newly created `GirTestClassTester`.
 */
GirTestClassTester*
girtest_class_tester_new (void)
{
    return g_object_new (GIRTEST_TYPE_CLASS_TESTER, NULL);
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

/**
 * girtest_class_tester_create_hidden_instance:
 *
 * Creates a new `girtest_class_tester_new` disguised in a GObject.
 *
 * Returns: (transfer full): The newly created `girtest_class_tester` casted as GObject.
 */
GObject*
girtest_class_tester_create_hidden_instance (void)
{
    return g_object_new (GIRTEST_TYPE_CLASS_TESTER, NULL);
}

/**
 * girtest_class_tester_take_executor:
 * @self: The instance
 * @executor: (transfer full): The executor which is stored in the class tester
 *
 * Takes ownership of the given executor.
 */
void
girtest_class_tester_take_executor(GirTestClassTester* self, GirTestExecutor* executor)
{
    self->executor = executor;
}

/**
 * girtest_class_tester_free_executor:
 * @self: The instance
 *
 * Frees the saved executor if one is available. Otherwise nothing happens.
 */
void
girtest_class_tester_free_executor(GirTestClassTester* self)
{
    g_clear_object(&self->executor);
}