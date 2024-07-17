#include "girtest-method-hiding.h"

/**
 * GirTestMethodHidingBase:
 *
 * Base class for testing subclasses with method names that are the same as a parent class method
 */

G_DEFINE_TYPE(GirTestMethodHidingBase, girtest_method_hiding_base, G_TYPE_OBJECT)

static void
girtest_method_hiding_base_init(GirTestMethodHidingBase *value)
{
}

static void
girtest_method_hiding_base_class_init(GirTestMethodHidingBaseClass *class)
{
}

gchar *
girtest_method_hiding_base_to_string(GirTestMethodHidingBase *instance)
{
  return g_strdup("New to_string");
}

gchar *
girtest_method_hiding_base_custom_string(GirTestMethodHidingBase *instance)
{
  return g_strdup("Base class custom string");
}

/**
 * GirTestMethodHidingSubclass:
 *
 * Subclass for testing method names that are the same as a parent class method
 */

struct _GirTestMethodHidingSubclass
{
    GirTestMethodHidingBase parent_instance;
};

G_DEFINE_TYPE(GirTestMethodHidingSubclass, girtest_method_hiding_subclass, GIRTEST_TYPE_METHOD_HIDING_BASE)

static void
girtest_method_hiding_subclass_init(GirTestMethodHidingSubclass *value)
{
}

static void
girtest_method_hiding_subclass_class_init(GirTestMethodHidingSubclassClass *class)
{
}

/**
 * girtest_method_hiding_subclass_new:
 *
 * Creates a new `GirTestMethodHidingSubclass`.
 *
 * Returns: The newly created `MethodHidingSubclass`.
 */
GirTestMethodHidingSubclass*
girtest_method_hiding_subclass_new(void)
{
    return g_object_new (GIRTEST_TYPE_METHOD_HIDING_SUBCLASS, NULL);
}

gchar *
girtest_method_hiding_subclass_custom_string(GirTestMethodHidingSubclass *instance)
{
  return g_strdup("Subclass custom string");
}
