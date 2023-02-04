#include "classtype.h"

/**
 * GirTestClassType:
 *
 * Contains functions for testing bindings with class types.
 */

struct _GirTestClassType
{
    GObject parent_instance;
};

G_DEFINE_TYPE(GirTestClassType, girtest_class_type,
              G_TYPE_OBJECT)

static void
girtest_class_type_init(GirTestClassType *value)
{
}

static void
girtest_class_type_class_init(GirTestClassTypeClass *class)
{
}

/**
 * girtest_class_type_transfer_ownership_full_and_unref:
 * @object: (transfer full): Any object
 *
 * Simple test to transfer ownership to C. The object is unrefed immediately.
 */
void girtest_class_type_transfer_ownership_full_and_unref(GObject *object)
{
    g_object_unref(object);
}
