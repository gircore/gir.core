#include "girtest-executor-impl.h"

struct _GirTestExecutorImpl
{
    GObject parent_instance;
};

static void girtest_executor_impl_interface_init (GirTestExecutorInterface *iface);

G_DEFINE_TYPE_WITH_CODE (GirTestExecutorImpl, girtest_executor_impl, G_TYPE_OBJECT,
                         G_IMPLEMENT_INTERFACE (GIRTEST_TYPE_EXECUTOR,
                                                girtest_executor_impl_interface_init))

static void
girtest_executor_impl_exec (GirTestExecutor *self)
{
    g_print ("Called: exec\n");
}

static void
girtest_executor_impl_interface_init (GirTestExecutorInterface *iface)
{
    iface->exec = girtest_executor_impl_exec;
}

static void
girtest_executor_impl_init(GirTestExecutorImpl *value)
{
}

static void
girtest_executor_impl_class_init(GirTestExecutorImplClass *class)
{
}

GirTestExecutorImpl*
girtest_executor_impl_new ()
{
    return g_object_new (GIRTEST_TYPE_EXECUTOR_IMPL, NULL);
}