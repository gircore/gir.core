#pragma once

#include <glib-object.h>

G_BEGIN_DECLS

#define GIRTEST_TYPE_RECORD_TESTER (girtest_record_tester_get_type())

/**
 * GirTestRecordTester:
 *
 * Just a record.
 */
typedef struct
{
    int ref_count;
} GirTestRecordTester;

GType girtest_record_tester_get_type (void) G_GNUC_CONST;

GirTestRecordTester * girtest_record_tester_new ();
GirTestRecordTester * girtest_record_tester_ref (GirTestRecordTester *self);
void girtest_record_tester_unref(GirTestRecordTester *self);

G_END_DECLS
