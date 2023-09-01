#pragma once

#include <glib-object.h>

G_BEGIN_DECLS

#define GIRTEST_TYPE_BYTE_ARRAY_TESTER girtest_byte_array_tester_get_type()

G_DECLARE_FINAL_TYPE(GirTestByteArrayTester, girtest_byte_array_tester,
                     GIRTEST, BYTE_ARRAY_TESTER, GObject)

/**
 * GirTestByteArrayTesterCallback:
 * @buf: (array length=count) (element-type guint8): bytes to be written.
 * @count: number of bytes in @buf.
 *
 * A callback which transfers data.
 * 
 */
typedef void (*GirTestByteArrayTesterCallback) (const guchar *buf, gsize count);

/**
 * GirTestByteArrayTesterCallbackNoLength:
 * @buf: (array) (element-type guint8): bytes to be written.
 *
 * A callback which transfers data.
 * 
 */
typedef void (*GirTestByteArrayTesterCallbackNoLength) (const guchar *buf);

GirTestByteArrayTester* girtest_byte_array_tester_new_from_data(guint8 *buffer, gsize len);
const guchar* girtest_byte_array_tester_data_return();
gssize girtest_byte_array_tester_data_out_caller_allocates(void *buffer, gsize count);
gsize girtest_byte_array_tester_get_data_size();
void girtest_byte_array_tester_invoke_callback(GirTestByteArrayTesterCallback callback);
void girtest_byte_array_tester_invoke_callback_no_length(GirTestByteArrayTesterCallbackNoLength callback);
guint8 girtest_byte_array_tester_get_data_from_const_pointer(gconstpointer data, gsize size);
void girtest_byte_array_tester_clear_data(void *data, gsize count);
void girtest_byte_array_tester_remove_last_array_element(guint8 *buffer, gsize *len);
G_END_DECLS
