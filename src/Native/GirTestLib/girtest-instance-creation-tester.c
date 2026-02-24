#include "girtest-instance-creation-tester.h"

/**
 * GirTestInstanceCreationTester:
 *
 * Contains functions to verify instance creation in bindings.
 */

struct _GirTestInstanceCreationTester
{
    GObject parent_instance;
    GObject* obj;
};

G_DEFINE_TYPE(GirTestInstanceCreationTester, girtest_instance_creation_tester, G_TYPE_OBJECT)

static void
girtest_instance_creation_tester_dispose (GObject *gobject)
{
    GirTestInstanceCreationTester *self = GIRTEST_INSTANCE_CREATION_TESTER (gobject);
    g_clear_object (&self->obj);

    G_OBJECT_CLASS (girtest_instance_creation_tester_parent_class)->dispose (gobject);
}

static void
girtest_instance_creation_tester_class_init(GirTestInstanceCreationTesterClass *class)
{
    GObjectClass *object_class = G_OBJECT_CLASS (class);

    object_class->dispose = girtest_instance_creation_tester_dispose;
}

static void
girtest_instance_creation_tester_init(GirTestInstanceCreationTester *value)
{
}

/**
 * girtest_instance_creation_tester_new:
 *
 * Returns: A new instance
 */
GirTestInstanceCreationTester* 
girtest_instance_creation_tester_new()
{
    return g_object_new (GIRTEST_TYPE_INSTANCE_CREATION_TESTER, NULL);
}

/**
 * girtest_instance_creation_tester_get_ref_count:
 * @instance: The instance
 *
 * Returns: The ref_count of the instance
 */
guint
girtest_instance_creation_tester_get_ref_count(GirTestInstanceCreationTester *instance)
{
    return ((GObject *)instance)->ref_count;
}

/**
 * girtest_instance_creation_tester_set_obj_transfer_none:
 * @instance: Instance
 * @obj: (transfer none): Object to store
 */
void 
girtest_instance_creation_tester_set_obj_transfer_none(GirTestInstanceCreationTester *instance, GObject *obj)
{
    g_object_ref(obj);
    instance->obj = obj;
}

/**
 * girtest_instance_creation_tester_set_obj_transfer_full:
 * @instance: Instance
 * @obj: (transfer full): Object to store
 */
void 
girtest_instance_creation_tester_set_obj_transfer_full(GirTestInstanceCreationTester *instance, GObject *obj)
{
    instance->obj = obj;
}

/**
 * girtest_instance_creation_tester_create_transfer_full:
 * @type: The Type to create an instance of.
 *
 * Returns: (transfer full): Instance of the given type.
 */
GObject*
girtest_instance_creation_tester_create_transfer_full(GType type)
{
    return g_object_new (type, NULL);
}

/**
 * girtest_instance_creation_tester_create_transfer_none:
 * @instance: Instance
 * @type: The Type to create an instance of.
 *
 * Returns: (transfer none): Instance of the given type.
 */
GObject* girtest_instance_creation_tester_create_transfer_none(GirTestInstanceCreationTester *instance, GType type)
{
    GObject* obj = g_object_new (type, NULL);
    instance->obj = obj;
    
    return obj;
}

/**
 * girtest_instance_creation_tester_interface_create_transfer_full:
 * @type: The Type to create an instance of.
 *
 * Returns: (transfer full): Instance of the given type.
 */
GirTestExecutor* girtest_instance_creation_tester_interface_create_transfer_full()
{
	return GIRTEST_EXECUTOR(girtest_executor_private_impl_new());
}

/**
 * girtest_instance_creation_tester_interface_create_transfer_none:
 * @instance: Instance
 *
 * Returns: (transfer none): Instance of the given type.
 */
GirTestExecutor* girtest_instance_creation_tester_interface_create_transfer_none(GirTestInstanceCreationTester *instance)
{
 	GirTestExecutor* obj = GIRTEST_EXECUTOR(girtest_executor_private_impl_new());
    instance->obj = G_OBJECT(obj);

	return GIRTEST_EXECUTOR(obj);
}