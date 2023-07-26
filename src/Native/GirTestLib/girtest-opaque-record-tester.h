#pragma once

#include <glib-object.h>

G_BEGIN_DECLS

typedef struct _GirTestOpaqueRecordTester GirTestOpaqueRecordTester;
#define GIRTEST_TYPE_OPAQUE_RECORD_TESTER (girtest_opaque_record_tester_get_type())

GType girtest_opaque_record_tester_get_type (void) G_GNUC_CONST;

GirTestOpaqueRecordTester * girtest_opaque_record_tester_new ();
GirTestOpaqueRecordTester * girtest_opaque_record_tester_ref (GirTestOpaqueRecordTester *self);
void girtest_opaque_record_tester_unref(GirTestOpaqueRecordTester *self);

G_END_DECLS
