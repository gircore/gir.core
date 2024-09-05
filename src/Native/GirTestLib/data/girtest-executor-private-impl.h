#pragma once

#include <glib-object.h>
#include "girtest-executor.h"

G_BEGIN_DECLS

G_DECLARE_FINAL_TYPE(GirTestExecutorPrivateImpl, girtest_executor_private_impl, GIRTEST, EXECUTOR_PRIVATE_IMPL, GObject)

GirTestExecutorPrivateImpl*
girtest_executor_private_impl_new ();

G_END_DECLS
