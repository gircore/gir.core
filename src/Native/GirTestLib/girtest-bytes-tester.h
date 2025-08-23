#pragma once

#include <glib-object.h>

G_BEGIN_DECLS

#define GIRTEST_TYPE_BYTES_TESTER girtest_bytes_tester_get_type()

G_DECLARE_FINAL_TYPE(GirTestBytesTester, girtest_bytes_tester,
                     GIRTEST, BYTES_TESTER, GObject)

GBytes* girtest_bytes_tester_return_bytes(guint8 byte1, guint8 byte2, guint8 byte3);

G_END_DECLS
