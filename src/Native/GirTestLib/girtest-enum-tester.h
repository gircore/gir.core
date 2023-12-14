#pragma once

#include <glib-object.h>

G_BEGIN_DECLS

/**
 * GirTestEnumTesterSimpleEnum:
 * @A: 1
 * @B: 2
 * @C: 3
 * @MAX: Max int value
 * @MIN: Min int value
 *
 * Enum to test bindings.
 */
typedef enum {
    SIMPLE_ENUM_A = 1,
    SIMPLE_ENUM_B = 2,
    SIMPLE_ENUM_C = 3,
    SIMPLE_ENUM_MAX = 2147483647,
    SIMPLE_ENUM_MIN = -2147483648
} GirTestEnumTesterSimpleEnum;

#define GIRTEST_TYPE_ENUM_TESTER girtest_enum_tester_get_type()

G_DECLARE_FINAL_TYPE(GirTestEnumTester, girtest_enum_tester, GIRTEST, ENUM_TESTER, GObject)

G_END_DECLS
