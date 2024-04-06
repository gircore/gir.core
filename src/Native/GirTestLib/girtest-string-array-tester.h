#pragma once

#include <glib-object.h>

G_BEGIN_DECLS

#define GIRTEST_TYPE_STRING_ARRAY_TESTER girtest_string_array_tester_get_type()

G_DECLARE_FINAL_TYPE(GirTestStringArrayTester, girtest_string_array_tester,
                     GIRTEST, STRING_ARRAY_TESTER, GObject)

const char** girtest_string_array_tester_utf8_return_transfer_none();
const char** girtest_string_array_tester_filename_return_transfer_none();
const char** girtest_string_array_tester_utf8_return_transfer_none_nullable(gboolean return_null);
const char** girtest_string_array_tester_filename_return_transfer_none_nullable(gboolean return_null);
gchar* girtest_string_array_tester_utf8_return_element_parameter_null_terminated_transfer_none(const gchar** data, int position);
gchar* girtest_string_array_tester_filename_return_element_parameter_null_terminated_transfer_none(const gchar** data, int position);
gchar* girtest_string_array_tester_utf8_return_element_parameter_null_terminated_transfer_none_nullable(const gchar** data, int position);
gchar* girtest_string_array_tester_filename_return_element_parameter_null_terminated_transfer_none_nullable(const gchar** data, int position);
G_END_DECLS
