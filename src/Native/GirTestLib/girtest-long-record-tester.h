#pragma once

#include <glib-object.h>

G_BEGIN_DECLS

typedef struct _GirTestLongRecordTester GirTestLongRecordTester;

struct _GirTestLongRecordTester
{
    long l;
    unsigned long ul;
};

gsize girtest_long_record_tester_get_sizeof_l(GirTestLongRecordTester* record);
gsize girtest_long_record_tester_get_sizeof_ul(GirTestLongRecordTester* record);
G_END_DECLS