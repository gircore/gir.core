#pragma once

#include <glib-object.h>

G_BEGIN_DECLS

typedef guint64 (*GirTestULongCallback) (guint64 val);

typedef struct _GirTestULongRecordTester GirTestULongRecordTester;

struct _GirTestULongRecordTester
{
    guint64 l;
};

gsize girtest_ulong_record_tester_get_sizeof_l(GirTestULongRecordTester* record);
guint64 girtest_ulong_record_tester_get_max_ulong_value();
gboolean girtest_ulong_record_tester_is_max_ulong_value(guint64 value);
guint64 girtest_ulong_record_tester_run_callback(guint64 value, GirTestULongCallback callback);
G_END_DECLS