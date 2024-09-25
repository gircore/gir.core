#pragma once

#include <glib-object.h>

G_BEGIN_DECLS

#define GIRTEST_TYPE_INTEGER_ARRAY_TESTER girtest_integer_array_tester_get_type()

G_DECLARE_FINAL_TYPE(GirTestIntegerArrayTester, girtest_integer_array_tester,
                     GIRTEST, INTEGER_ARRAY_TESTER, GObject)

/**
 * GirTestIntegerArrayTesterCallback:
 * @buf: (array length=count): bytes to be written.
 * @count: number of bytes in @buf.
 *
 * A callback which transfers data.
 * 
 */
typedef void (*GirTestIntegerArrayTesterCallback) (const gint *buf, gsize count);

/**
 * GirTestIntegerArrayTesterCallbackNoLength:
 * @buf: (array): bytes to be written.
 *
 * A callback which transfers data.
 * 
 */
typedef void (*GirTestIntegerArrayTesterCallbackNoLength) (const gint *buf);

const gint* girtest_integer_array_tester_data_return();
gssize girtest_integer_array_tester_data_out_caller_allocates(void *buffer, gsize count);
gsize girtest_integer_array_tester_get_data_count();
void girtest_integer_array_tester_invoke_callback(GirTestIntegerArrayTesterCallback callback);
void girtest_integer_array_tester_invoke_callback_no_length(GirTestIntegerArrayTesterCallbackNoLength callback);
gint girtest_integer_array_tester_get_data_from_const_pointer(gconstpointer data, gsize size);
gint girtest_integer_array_tester_get_data_with_gint64_size(gconstpointer data, gint64 size);
void girtest_integer_array_tester_clear_data(void *data, gsize count);
G_END_DECLS

