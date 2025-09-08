#pragma once

#include <glib-object.h>

G_BEGIN_DECLS

typedef gint64 (*GirTestLongCallback) (gint64 val);

typedef struct _GirTestLongRecordTester GirTestLongRecordTester;

struct _GirTestLongRecordTester
{
    gint64 l;
};

gsize girtest_long_record_tester_get_sizeof_l(GirTestLongRecordTester* record);
gint64 girtest_long_record_tester_get_max_long_value();
gint64 girtest_long_record_tester_get_min_long_value();
gboolean girtest_long_record_tester_is_max_long_value(gint64 value);
gboolean girtest_long_record_tester_is_min_long_value(gint64 value);
gint64 girtest_long_record_tester_run_callback(gint64 value, GirTestLongCallback callback);
G_END_DECLS