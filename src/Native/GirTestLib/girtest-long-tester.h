#pragma once

#include <glib-object.h>

G_BEGIN_DECLS

typedef glong (*GirTestLongCallback) (glong val);

typedef struct _GirTestLongTester GirTestLongTester;

struct _GirTestLongTester
{
    long l;
};

gsize girtest_long_tester_get_sizeof_l(GirTestLongTester* record);
glong girtest_long_tester_get_max_long_value();
glong girtest_long_tester_get_min_long_value();
gboolean girtest_long_tester_is_max_long_value(glong value);
gboolean girtest_long_tester_is_min_long_value(glong value);
glong girtest_long_tester_run_callback(glong value, GirTestLongCallback callback);
G_END_DECLS