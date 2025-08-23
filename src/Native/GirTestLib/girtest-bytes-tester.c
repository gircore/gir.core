#include "girtest-bytes-tester.h"

/**
 * GirTestBytesTester:
 *
 * Contains functions for testing bindings with GBytes.
 */

struct _GirTestBytesTester
{
    GObject parent_instance;
};

G_DEFINE_TYPE(GirTestBytesTester, girtest_bytes_tester, G_TYPE_OBJECT)

static void
girtest_bytes_tester_init(GirTestBytesTester *value)
{
}

static void
girtest_bytes_tester_class_init(GirTestBytesTesterClass *class)
{
}

/**
 * girtest_bytes_tester_return_bytes:
 * @byte1: first byte
 * @byte2: second byte
 * @byte3: third byte
 *
 * Takes the given bytes and creates a GBytes instance from them.
 */
GBytes*
girtest_bytes_tester_return_bytes(guint8 byte1, guint8 byte2, guint8 byte3)
{
    guint8 data[] = { byte1, byte2, byte3 };
    return g_bytes_new(data, sizeof(data));
}