#pragma once

#include <glib-object.h>
#include "girtest-opaque-typed-record-tester.h"

G_BEGIN_DECLS

#define GIRTEST_TYPE_LIST_TESTER girtest_list_tester_get_type()

G_DECLARE_FINAL_TYPE(GirTestListTester, girtest_list_tester, GIRTEST, LIST_TESTER, GObject)

GList* girtest_list_tester_get_strings_transfer_full();
GList* girtest_list_tester_get_strings_transfer_none();
GList* girtest_list_tester_get_strings_transfer_full_empty();

GSList* girtest_list_tester_get_records_transfer_full();
GSList* girtest_list_tester_get_records_transfer_container();
GSList* girtest_list_tester_get_records_transfer_none();

int girtest_list_tester_get_static_record_ref_count(int position);

G_END_DECLS
