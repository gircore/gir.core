#include "girtest-bitfield-tester.h"

/**
 * GirTestBitfieldTester:
 *
 * Contains functions for testing bitfields.
 */

struct _GirTestBitfieldTester
{
    GObject parent_instance;
};

G_DEFINE_TYPE(GirTestBitfieldTester, girtest_bitfield_tester, G_TYPE_OBJECT)

static void
girtest_bitfield_tester_init(GirTestBitfieldTester *value)
{
}

static void
girtest_bitfield_tester_class_init(GirTestBitfieldTesterClass *class)
{
}

/**
 * girtest_bitfield_tester_reset_flags:
 * @flags: some flags
 *
 * Resets all flags on the given input
 */
void
girtest_bitfield_tester_reset_flags(GirTestBitfieldTesterSimpleFlags *flags)
{
    *flags = SIMPLE_FAGS_ZERO;
}