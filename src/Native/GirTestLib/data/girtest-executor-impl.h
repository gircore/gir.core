#pragma once

#include <glib-object.h>
#include "girtest-executor.h"

G_BEGIN_DECLS

#define GIRTEST_TYPE_EXECUTOR_IMPL girtest_executor_impl_get_type()

G_DECLARE_FINAL_TYPE(GirTestExecutorImpl, girtest_executor_impl, GIRTEST, EXECUTOR_IMPL, GObject)

GirTestExecutorImpl*
girtest_executor_impl_new ();

G_END_DECLS
