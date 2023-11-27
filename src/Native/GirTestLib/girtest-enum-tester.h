#pragma once

#include <glib-object.h>

G_BEGIN_DECLS

/**
 * EnumTesterSimpleEnum:
 * @A: 1
 * @B: 2
 * @C: 3
 *
 * Enum to test bindings
 */
typedef enum {
    SIMPLE_ENUM_A = 1,
    SIMPLE_ENUM_B = 2,
    SIMPLE_ENUM_C = 3
} GirTestEnumTesterSimpleEnum;

#define GIRTEST_TYPE_ENUM_TESTER girtest_enum_tester_get_type()

G_DECLARE_FINAL_TYPE(GirTestEnumTester, girtest_enum_tester, GIRTEST, ENUM_TESTER, GObject)

G_END_DECLS
