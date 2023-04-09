#include "girtest-executor.h"

G_DEFINE_INTERFACE (GirTestExecutor, girtest_executor, G_TYPE_OBJECT)

static void
girtest_executor_default_init (GirTestExecutorInterface *iface)
{
}

void
girtest_executor_exec (GirTestExecutor *self)
{
    GirTestExecutorInterface *iface;

    g_return_if_fail (GIRTEST_IS_EXECUTOR (self));

    iface = GIRTEST_EXECUTOR_GET_IFACE (self);
    g_return_if_fail (iface->exec != NULL);
    iface->exec (self);
}
