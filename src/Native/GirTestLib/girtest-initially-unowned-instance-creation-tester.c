#include "girtest-initially-unowned-instance-creation-tester.h"

/**
 * GirTestInitiallyUnownedInstanceCreationTester:
 *
 * Contains functions to verify instance creation in bindings.
 */

struct _GirTestInitiallyUnownedInstanceCreationTester
{
    GObject parent_instance;
    GObject* obj;
};

G_DEFINE_TYPE(GirTestInitiallyUnownedInstanceCreationTester, girtest_initially_unowned_instance_creation_tester, G_TYPE_INITIALLY_UNOWNED)

static void
girtest_initially_unowned_instance_creation_tester_dispose (GObject *gobject)
{
    GirTestInitiallyUnownedInstanceCreationTester *self = GIRTEST_INITIALLY_UNOWNED_INSTANCE_CREATION_TESTER (gobject);
    g_clear_object (&self->obj);

    G_OBJECT_CLASS (girtest_initially_unowned_instance_creation_tester_parent_class)->dispose (gobject);
}

static void
girtest_initially_unowned_instance_creation_tester_class_init(GirTestInitiallyUnownedInstanceCreationTesterClass *class)
{
    GObjectClass *object_class = G_OBJECT_CLASS (class);

    object_class->dispose = girtest_initially_unowned_instance_creation_tester_dispose;
}

static void
girtest_initially_unowned_instance_creation_tester_init(GirTestInitiallyUnownedInstanceCreationTester *value)
{
}

/**
 * girtest_initially_unowned_instance_creation_tester_new:
 *
 * Returns: A new instance
 */
GirTestInitiallyUnownedInstanceCreationTester* 
girtest_initially_unowned_instance_creation_tester_new()
{
    return g_object_new (GIRTEST_TYPE_INITIALLY_UNOWNED_INSTANCE_CREATION_TESTER, NULL);
}

/**
 * girtest_initially_unowned_instance_creation_tester_get_ref_count:
 *
 * Returns: The ref_count of the instance
 */
guint
girtest_initially_unowned_instance_creation_tester_get_ref_count(GirTestInitiallyUnownedInstanceCreationTester *instance)
{
    return ((GInitiallyUnowned *)instance)->ref_count;
}

/**
 * girtest_initially_unowned_instance_creation_tester_set_obj_transfer_none:
 * @instance: Instance
 * @obj: (transfer none): Object to store
 *
 */
void
girtest_initially_unowned_instance_creation_tester_set_obj_transfer_none(GirTestInitiallyUnownedInstanceCreationTester *instance, GObject *obj)
{
    g_object_ref(obj);
    instance->obj = obj;
}

/**
 * girtest_initially_unowned_instance_creation_tester_set_obj_transfer_full:
 * @instance: Instance
 * @obj: (transfer full): Object to store
 */
void
girtest_initially_unowned_instance_creation_tester_set_obj_transfer_full(GirTestInitiallyUnownedInstanceCreationTester *instance, GObject *obj)
{
    instance->obj = obj;
}

/**
 * girtest_initially_unowned_instance_creation_tester_create_transfer_full:
 * @type: The Type to create an instance of.
 *
 * Returns: (transfer full): Instance of the given type.
 */
GObject*
girtest_initially_unowned_instance_creation_tester_create_transfer_full(GType type)
{
    return g_object_new (type, NULL);
}

/**
 * girtest_initially_unowned_instance_creation_tester_create_transfer_none:
 * @instance: Instance
 * @type: The Type to create an instance of.
 * 
 * Returns: (transfer none): Instance of the given type.
 */
GObject*
girtest_initially_unowned_instance_creation_tester_create_transfer_none(GirTestInitiallyUnownedInstanceCreationTester *instance, GType type)
{
    GObject* obj = g_object_new (type, NULL);
    g_object_take_ref(obj);
    instance->obj = obj;
    
    return obj;
}