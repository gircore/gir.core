#pragma once

#include <glib-object.h>

G_BEGIN_DECLS

typedef struct _GirTestLongTester GirTestLongTester;

struct _GirTestLongTester
{
    long l;
    unsigned long ul;
};

gsize girtest_long_tester_get_sizeof_l(GirTestLongTester* record);
gsize girtest_long_tester_get_sizeof_ul(GirTestLongTester* record);
G_END_DECLS