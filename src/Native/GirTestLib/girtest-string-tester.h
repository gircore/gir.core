#pragma once

#include <glib-object.h>

G_BEGIN_DECLS

typedef gchar *  (*GirTestReturnStringFunc)();

#define GIRTEST_TYPE_STRING_TESTER girtest_string_tester_get_type()

G_DECLARE_FINAL_TYPE(GirTestStringTester, girtest_string_tester,
                     GIRTEST, STRING_TESTER, GObject)

int
girtest_string_tester_utf8_in(const gchar *s);

int
girtest_string_tester_utf8_in_nullable(const gchar *s);

int
girtest_string_tester_filename_in(const gchar *s);

int
girtest_string_tester_filename_in_nullable(const gchar *s);

gchar *
girtest_string_tester_utf8_return_transfer_full(const gchar *s);

gchar *
girtest_string_tester_utf8_return_nullable_transfer_full(const gchar *s);

const gchar *
girtest_string_tester_utf8_return_transfer_none(const gchar *s);

const gchar *
girtest_string_tester_utf8_return_nullable_transfer_none(const gchar *s);

gchar *
girtest_string_tester_filename_return_transfer_full(const gchar *s);

gchar *
girtest_string_tester_filename_return_nullable_transfer_full(const gchar *s);

const gchar *
girtest_string_tester_filename_return_transfer_none(const gchar *s);

const gchar *
girtest_string_tester_filename_return_nullable_transfer_none(const gchar *s);

int
girtest_string_tester_callback_return_string_transfer_full(GirTestReturnStringFunc callback, gpointer user_data, GDestroyNotify destroy);

G_END_DECLS

