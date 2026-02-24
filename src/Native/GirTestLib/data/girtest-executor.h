#pragma once

#include <glib-object.h>

G_BEGIN_DECLS

#define GIRTEST_TYPE_EXECUTOR girtest_executor_get_type()

G_DECLARE_INTERFACE (GirTestExecutor, girtest_executor, GIRTEST, EXECUTOR, GObject)

struct _GirTestExecutorInterface
{
    GTypeInterface parent_iface;

    void (*exec) (GirTestExecutor *self);
};

void girtest_executor_exec (GirTestExecutor *self);
guint girtest_executor_get_ref_count(GirTestExecutor *self);

G_END_DECLS
