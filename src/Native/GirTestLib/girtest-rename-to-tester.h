#pragma once

#include <glib-object.h>

G_BEGIN_DECLS

#define GIRTEST_TYPE_RENAME_TO_TESTER girtest_rename_to_tester_get_type()

G_DECLARE_FINAL_TYPE(GirTestRenameToTester, girtest_rename_to_tester, GIRTEST, RENAME_TO_TESTER, GObject)

gint
girtest_rename_to_tester_get(gint i);

gint
girtest_rename_to_tester_get2(gint i1, gint i2);

G_END_DECLS

