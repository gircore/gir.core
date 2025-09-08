#pragma once

#include <glib-object.h>

G_BEGIN_DECLS

typedef gulong (*GirTestCULongCallback) (gulong val);

typedef struct _GirTestCULongRecordTester GirTestCULongRecordTester;

struct _GirTestCULongRecordTester
{
    unsigned long ul;
};

gsize girtest_cu_long_record_tester_get_sizeof_ul(GirTestCULongRecordTester* record);
gulong girtest_cu_long_record_tester_get_max_unsigned_long_value();
gboolean girtest_cu_long_record_tester_is_max_unsigned_long_value(gulong value);
gulong girtest_cu_long_record_tester_run_callback(gulong value, GirTestCULongCallback callback);
G_END_DECLS