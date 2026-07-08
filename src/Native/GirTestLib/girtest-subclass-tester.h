#pragma once

#include <glib-object.h>

G_BEGIN_DECLS

#define GIRTEST_TYPE_SUBCLASS_TESTER girtest_subclass_tester_get_type()

G_DECLARE_FINAL_TYPE(GirTestSubClassTester, girtest_subclass_tester, GIRTEST, SUBCLASS_TESTER, GObject)

void
girtest_subclass_tester_increase_counter(GirTestSubClassTester* self);

int
girtest_subclass_tester_get_counter(GirTestSubClassTester* self);

G_END_DECLS

