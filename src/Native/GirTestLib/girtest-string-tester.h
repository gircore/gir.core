#pragma once

#include <glib-object.h>

G_BEGIN_DECLS

typedef gchar *  (*GirTestReturnStringFunc)();

#define GIRTEST_TYPE_STRING_TESTER girtest_string_tester_get_type()

G_DECLARE_FINAL_TYPE(GirTestStringTester, girtest_string_tester,
                     GIRTEST, STRING_TESTER, GObject)

int
girtest_string_tester_utf8_in(const gchar *s);

void
girtest_string_tester_utf8_in_transfer_full(gchar *s);

int
girtest_string_tester_utf8_in_nullable(const gchar *s);

void
girtest_string_tester_utf8_in_nullable_transfer_full(gchar *s);

int
girtest_string_tester_filename_in(const gchar *s);

void
girtest_string_tester_filename_in_transfer_full(gchar *s);

int
girtest_string_tester_filename_in_nullable(const gchar *s);

void
girtest_string_tester_filename_in_nullable_transfer_full(gchar *s);

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

gchar *
girtest_string_tester_utf8_return_unexpected_null();

gchar *
girtest_string_tester_filename_return_unexpected_null();

void
girtest_string_tester_utf8_out_transfer_none(const gchar *s, const gchar **result);

void
girtest_string_tester_utf8_out_optional_transfer_none(const gchar *s, const gchar **result);

void
girtest_string_tester_utf8_out_nullable_transfer_none(const gchar *s, const gchar **result);

void
girtest_string_tester_utf8_out_nullable_optional_transfer_none(const gchar *s, const gchar **result);

void
girtest_string_tester_utf8_out_transfer_full(const gchar *s, gchar **result);

void
girtest_string_tester_utf8_out_optional_transfer_full(const gchar *s, gchar **result);

void
girtest_string_tester_utf8_out_nullable_transfer_full(const gchar *s, gchar **result);

void
girtest_string_tester_utf8_out_nullable_optional_transfer_full(const gchar *s, gchar **result);

void
girtest_string_tester_filename_out_transfer_none(const gchar *s, const gchar **result);

void
girtest_string_tester_filename_out_optional_transfer_none(const gchar *s, const gchar **result);

void
girtest_string_tester_filename_out_nullable_transfer_none(const gchar *s, const gchar **result);

void
girtest_string_tester_filename_out_nullable_optional_transfer_none(const gchar *s, const gchar **result);

void
girtest_string_tester_filename_out_transfer_full(const gchar *s, gchar **result);

void
girtest_string_tester_filename_out_optional_transfer_full(const gchar *s, gchar **result);

void
girtest_string_tester_filename_out_nullable_transfer_full(const gchar *s, gchar **result);

void
girtest_string_tester_filename_out_nullable_optional_transfer_full(const gchar *s, gchar **result);

G_END_DECLS

