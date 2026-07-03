#include "girtest-subclass-tester.h"

/**
 * GirTestSubClassTester:
 *
 * Contains functions for testing bindings with subclass types.
 */

struct _GirTestSubClassTester
{
    GObject parent_instance;
    int counter;
};

G_DEFINE_TYPE(GirTestSubClassTester, girtest_subclass_tester, G_TYPE_OBJECT)

static void
girtest_subclass_tester_init(GirTestSubClassTester *value)
{
}

static void
girtest_subclass_tester_class_init(GirTestSubClassTesterClass *class)
{
}

/**
 * girtest_subclass_tester_increase_counter:
 * @self: The instance
 *
 * Increase the counter by one.
 */
void
girtest_subclass_tester_increase_counter(GirTestSubClassTester* self)
{
    self->counter++;
}

/**
 * girtest_subclass_tester_get_counter:
 * @self: The instance
 *
 * Get the counter value.
 */
int
girtest_subclass_tester_get_counter(GirTestSubClassTester* self)
{
    return self->counter;
}