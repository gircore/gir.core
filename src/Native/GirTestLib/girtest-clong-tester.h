#pragma once

#include <glib-object.h>

G_BEGIN_DECLS

typedef glong (*GirTestCLongCallback) (glong val);

typedef struct _GirTestCLongTester GirTestCLongTester;

struct _GirTestCLongTester
{
    long l;
};

gsize girtest_clong_tester_get_sizeof_l(GirTestCLongTester* record);
glong girtest_clong_tester_get_max_long_value();
glong girtest_clong_tester_get_min_long_value();
gboolean girtest_clong_tester_is_max_long_value(glong value);
gboolean girtest_clong_tester_is_min_long_value(glong value);
glong girtest_clong_tester_run_callback(glong value, GirTestCLongCallback callback);
G_END_DECLS