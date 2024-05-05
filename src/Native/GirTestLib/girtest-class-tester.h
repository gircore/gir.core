#pragma once

#include <glib-object.h>
#include "data/girtest-executor.h"

G_BEGIN_DECLS

#define GIRTEST_TYPE_CLASS_TESTER girtest_class_tester_get_type()

G_DECLARE_FINAL_TYPE(GirTestClassTester, girtest_class_tester, GIRTEST, CLASS_TESTER, GObject)

GirTestClassTester*
girtest_class_tester_new (void);

void
girtest_class_tester_transfer_ownership_full_and_unref(GObject *object);

GObject*
girtest_class_tester_create_hidden_instance (void);

void
girtest_class_tester_take_executor(GirTestClassTester* self, GirTestExecutor* executor);

void
girtest_class_tester_free_executor(GirTestClassTester* self);

G_END_DECLS

