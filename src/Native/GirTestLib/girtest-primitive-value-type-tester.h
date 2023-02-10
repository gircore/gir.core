#pragma once

#include <glib-object.h>

G_BEGIN_DECLS

#define GIRTEST_TYPE_PRIMITIVE_VALUE_TYPE_TESTER girtest_primitive_value_type_tester_get_type()

G_DECLARE_FINAL_TYPE(GirTestPrimitiveValueTypeTester, girtest_primitive_value_type_tester,
                     GIRTEST, PRIMITIVE_VALUE_TYPE_TESTER, GObject)

int
girtest_primitive_value_type_tester_int_in(int val);

void girtest_primitive_value_type_tester_int_in_out(int *val);

void girtest_primitive_value_type_tester_int_in_out_optional(int *val);

void girtest_primitive_value_type_tester_int_out(int *result);

void girtest_primitive_value_type_tester_int_out_optional(int *result);

G_END_DECLS

