#include "girtest-executor-private-impl.h"

#define GIRTEST_TYPE_EXECUTOR_PRIVATE_IMPL girtest_executor_private_impl_get_type()

struct _GirTestExecutorPrivateImpl
{
    GObject parent_instance;
};

static void girtest_executor_private_impl_interface_init (GirTestExecutorInterface *iface);

G_DEFINE_TYPE_WITH_CODE (GirTestExecutorPrivateImpl, girtest_executor_private_impl, G_TYPE_OBJECT,
                         G_IMPLEMENT_INTERFACE (GIRTEST_TYPE_EXECUTOR,
                                                girtest_executor_private_impl_interface_init))

static void
girtest_executor_private_impl_exec (GirTestExecutor *self)
{
    g_print ("Called: exec\n");
}

static void
girtest_executor_private_impl_interface_init (GirTestExecutorInterface *iface)
{
    iface->exec = girtest_executor_private_impl_exec;
}

static void
girtest_executor_private_impl_init(GirTestExecutorPrivateImpl *value)
{
}

static void
girtest_executor_private_impl_class_init(GirTestExecutorPrivateImplClass *class)
{
}

GirTestExecutorPrivateImpl*
girtest_executor_private_impl_new ()
{
    return g_object_new (GIRTEST_TYPE_EXECUTOR_PRIVATE_IMPL, NULL);
}