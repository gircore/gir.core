#pragma once

#include <glib-object.h>

G_BEGIN_DECLS

typedef gulong (*GirTestULongCallback) (gulong val);

typedef struct _GirTestULongTester GirTestULongTester;

struct _GirTestULongTester
{
    unsigned long ul;
};

gsize girtest_ulong_tester_get_sizeof_ul(GirTestULongTester* record);
gulong girtest_ulong_tester_get_max_unsigned_long_value();
gboolean girtest_ulong_tester_is_max_unsigned_long_value(gulong value);
gulong girtest_ulong_tester_run_callback(gulong value, GirTestULongCallback callback);
G_END_DECLS