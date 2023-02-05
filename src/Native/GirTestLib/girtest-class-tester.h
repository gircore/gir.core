#pragma once

#include <glib-object.h>

G_BEGIN_DECLS

#define GIRTEST_TYPE_CLASS_TESTER girtest_class_tester_get_type()

G_DECLARE_FINAL_TYPE(GirTestClassTester, girtest_class_tester, GIRTEST, CLASS_TESTER, GObject)

void
girtest_class_tester_transfer_ownership_full_and_unref(GObject *object);

G_END_DECLS

