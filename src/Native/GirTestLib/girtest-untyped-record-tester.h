#pragma once

#include <glib-object.h>

G_BEGIN_DECLS

typedef struct _GirTestUntypedRecordTester GirTestUntypedRecordTester;
typedef struct _GirTestUntypedRecordContainerTester GirTestUntypedRecordContainerTester;

struct _GirTestUntypedRecordTester
{
    int a;
};

struct _GirTestUntypedRecordContainerTester
{
    GirTestUntypedRecordTester* data;
    GirTestUntypedRecordContainerTester *next;
};

/**
 * GirTestUntypedRecordCallbackOutParameterCallerAllocates:
 * @record:(out caller-allocates): the pointer to the integer value
 **/
typedef void (*GirTestUntypedRecordCallbackOutParameterCallerAllocates) (GirTestUntypedRecordTester *record);

/**
 * GirTestUntypedRecordCallbackOutParameterCalleeAllocates:
 * @record:(out callee-allocates): the pointer to the integer value
 **/
typedef void (*GirTestUntypedRecordCallbackOutParameterCalleeAllocates) (GirTestUntypedRecordTester **record);

GirTestUntypedRecordTester* girtest_untyped_record_tester_new_with_a(int a);
GirTestUntypedRecordTester * girtest_untyped_record_tester_try_new(gboolean returnNull);
GirTestUntypedRecordTester* girtest_untyped_record_tester_mirror(GirTestUntypedRecordTester *data);
GirTestUntypedRecordTester * girtest_untyped_record_tester_nullable_mirror(GirTestUntypedRecordTester *data, gboolean mirror);
int girtest_untyped_record_tester_get_a(GirTestUntypedRecordTester* record);
int girtest_untyped_record_tester_get_a_nullable(int fallback, GirTestUntypedRecordTester* record);
void girtest_untyped_record_tester_out_parameter_caller_allocates(int v, GirTestUntypedRecordTester *record);
GirTestUntypedRecordContainerTester* girtest_untyped_record_tester_returns_transfer_container();
GirTestUntypedRecordTester* girtest_untyped_record_tester_get_nth_container_data(GirTestUntypedRecordContainerTester* container, guint n);
GirTestUntypedRecordTester* girtest_untyped_record_tester_callback_out_parameter_caller_allocates(GirTestUntypedRecordCallbackOutParameterCallerAllocates callback);
GirTestUntypedRecordTester* girtest_untyped_record_tester_callback_out_parameter_callee_allocates(GirTestUntypedRecordCallbackOutParameterCalleeAllocates callback);
int girtest_untyped_record_tester_get_a_from_last_element(GirTestUntypedRecordTester* array, int length);
int girtest_untyped_record_tester_get_a_from_last_element_pointer(GirTestUntypedRecordTester** array, int length);
gboolean girtest_untyped_record_tester_equals(GirTestUntypedRecordTester *self, GirTestUntypedRecordTester *other);
G_END_DECLS