#ifndef GIR_TEST_PRIMITIVEVALUETYPE_H
#define GIR_TEST_PRIMITIVEVALUETYPE_H

#include <glib-object.h>

G_BEGIN_DECLS

#define GIRTEST_TYPE_PRIMITIVEVALUETYPE girtest_primitive_value_type_get_type()
G_DECLARE_FINAL_TYPE(GirTestPrimitiveValueType, girtest_primitive_value_type,
                     GIRTEST, PRIMITIVEVALUETYPE, GObject)

int girtest_primitive_value_type_int_in(int val);

G_END_DECLS

#endif
