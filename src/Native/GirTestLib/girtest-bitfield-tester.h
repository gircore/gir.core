#pragma once

#include <glib-object.h>

G_BEGIN_DECLS

/**
 * DemoFlags:
 * @ZERO: No flags set.
 * @ONE: Set first flag.
 * @TWO: Set second flag.
 *
 * Flags to test bindings
 */
typedef enum {
    SIMPLE_FAGS_ZERO = 0,
    SIMPLE_FAGS_ONE  = (1 << 0),
    SIMPLE_FAGS_TWO  = (1 << 1)
} GirTestBitfieldTesterSimpleFlags;

#define GIRTEST_TYPE_BITFIELD_TESTER girtest_bitfield_tester_get_type()

G_DECLARE_FINAL_TYPE(GirTestBitfieldTester, girtest_bitfield_tester, GIRTEST, BITFIELD_TESTER, GObject)

void
girtest_bitfield_tester_reset_flags(GirTestBitfieldTesterSimpleFlags *flags);

G_END_DECLS
