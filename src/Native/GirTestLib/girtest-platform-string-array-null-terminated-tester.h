#pragma once

#include <glib-object.h>

G_BEGIN_DECLS

#define GIRTEST_TYPE_PLATFORM_STRING_ARRAY_NULL_TERMINATED_TESTER girtest_platform_string_array_null_terminated_tester_get_type()

G_DECLARE_FINAL_TYPE(GirTestPlatformStringArrayNullTerminatedTester, girtest_platform_string_array_null_terminated_tester,
                     GIRTEST, PLATFORM_STRING_ARRAY_NULL_TERMINATED_TESTER, GObject)

gchar* girtest_platform_string_array_null_terminated_tester_get_last_element_transfer_none(gchar** data);
gchar* girtest_platform_string_array_null_terminated_tester_get_last_element_transfer_full(gchar** data);
gchar* girtest_platform_string_array_null_terminated_tester_get_last_element_transfer_none_nullable(gchar** data);
gchar* girtest_platform_string_array_null_terminated_tester_get_last_element_transfer_full_nullable(gchar** data);
gchar** girtest_platform_string_array_null_terminated_tester_return_transfer_full(gchar** data);
gchar** girtest_platform_string_array_null_terminated_tester_return_transfer_none(gchar** data);
gchar** girtest_platform_string_array_null_terminated_tester_return_transfer_container(gchar* first, gchar* second);
gchar** girtest_platform_string_array_null_terminated_tester_return_transfer_full_nullable(gchar** data);
gchar** girtest_platform_string_array_null_terminated_tester_return_transfer_none_nullable(gchar** data);
gchar** girtest_platform_string_array_null_terminated_tester_return_transfer_container_nullable(gchar* first, gchar* second);
gchar** girtest_platform_string_array_null_terminated_tester_optional_size_out_transfer_full(gchar** data, int *size);
gchar** girtest_platform_string_array_null_terminated_tester_size_out_transfer_full(gchar** data, int *size);
gchar** girtest_platform_string_array_null_terminated_tester_optional_gsize_parameter_out_transfer_full(gchar** data, gsize *size);
gchar** girtest_platform_string_array_null_terminated_tester_gsize_parameter_out_transfer_full(gchar** data, gsize *size);
void girtest_platform_string_array_null_terminated_tester_parameter_out_transfer_full(gchar** data, gchar*** output);
void girtest_platform_string_array_null_terminated_tester_parameter_out_transfer_none(gchar** data, gchar*** output);
void girtest_platform_string_array_null_terminated_tester_parameter_out_transfer_container(gchar* first, gchar* second, gchar*** output);
void girtest_platform_string_array_null_terminated_tester_parameter_out_transfer_full_nullable(gchar** data, gchar*** output);
void girtest_platform_string_array_null_terminated_tester_parameter_out_transfer_none_nullable(gchar** data, gchar*** output);
void girtest_platform_string_array_null_terminated_tester_parameter_out_transfer_container_nullable(gchar* first, gchar* second, gchar*** output);

G_END_DECLS
