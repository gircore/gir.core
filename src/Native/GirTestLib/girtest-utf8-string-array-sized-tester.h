#pragma once

#include <glib-object.h>

G_BEGIN_DECLS

#define GIRTEST_TYPE_UTF8_STRING_ARRAY_SIZED_TESTER girtest_utf8_string_array_sized_tester_get_type()

G_DECLARE_FINAL_TYPE(GirTestUtf8StringArraySizedTester, girtest_utf8_string_array_sized_tester,
                     GIRTEST, UTF8_STRING_ARRAY_SIZED_TESTER, GObject)

/* This matches gst_init */
void girtest_utf8_string_array_sized_tester_parameter_inout_with_length_parameter_transfer_full(int* argc, char*** argv);
char* girtest_utf8_string_array_sized_tester_parameter_in_with_length_parameter_transfer_none(int elements, char** array);
char* girtest_utf8_string_array_sized_tester_parameter_in_with_length_parameter_transfer_full(int elements, char** array);
void girtest_utf8_string_array_sized_tester_parameter_out_with_length_parameter_transfer_none(gboolean returnNull, int* elements, const char*** array);
void girtest_utf8_string_array_sized_tester_parameter_out_with_length_parameter_transfer_full(gboolean returnNull, int* elements, char*** array);
G_END_DECLS
