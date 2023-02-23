#include "girtest-string-tester.h"

/**
 * GirTestStringTester:
 *
 * Contains functions for testing bindings with string parameters.
 */

struct _GirTestStringTester
{
    GObject parent_instance;
};

G_DEFINE_TYPE(GirTestStringTester, girtest_string_tester, G_TYPE_OBJECT)

static void
girtest_string_tester_init(GirTestStringTester *value)
{
}

static void
girtest_string_tester_class_init(GirTestStringTesterClass *class)
{
}

/**
 * girtest_string_tester_utf8_in:
 * @s: (in): Pointer to the start of a UTF-8 encoded string.
 *
 * Test for a UTF-8 string parameter.
 *
 * Returns: The number of characters in the string.
 */
int
girtest_string_tester_utf8_in(const gchar *s)
{
    return g_utf8_strlen(s, -1);
}

/**
 * girtest_string_tester_utf8_in_nullable:
 * @s: (in) (nullable): Pointer to the start of a UTF-8 encoded string, or NULL.
 *
 * Test for a nullable UTF-8 string parameter.
 *
 * Returns: The number of characters in the string, or -1 if NULL.
 */
int
girtest_string_tester_utf8_in_nullable(const gchar *s)
{
    return s ? girtest_string_tester_utf8_in(s) : -1;
}

/**
 * girtest_string_tester_filename_in:
 * @s: (in) (type filename): A string in the filename encoding.
 *
 * Test for a filename string parameter.
 *
 * Returns: The number of characters in the string.
 */
int
girtest_string_tester_filename_in(const gchar *s)
{
    GError *error = NULL;
    gchar *utf8 = g_filename_to_utf8(s, -1, NULL, NULL, &error);
    g_assert_no_error(error);

    int len = g_utf8_strlen(utf8, -1);
    g_free(utf8);

    return len;
}

/**
 * girtest_string_tester_filename_in_nullable:
 * @s: (in) (type filename) (nullable): A string in the filename encoding, or NULL.
 *
 * Test for a nullable filename string parameter.
 *
 * Returns: The number of characters in the string, or NULL.
 */
int
girtest_string_tester_filename_in_nullable(const gchar *s)
{
    return s ? girtest_string_tester_filename_in(s) : -1;
}

/**
 * girtest_string_tester_utf8_return_transfer_full:
 * @s: (in): Pointer to the start of a UTF-8 encoded string.
 *
 * Test for a UTF-8 string return value with ownership transfer.
 *
 * Returns: (transfer full): A copy of the input string.
 */
gchar *
girtest_string_tester_utf8_return_transfer_full(const gchar *s)
{
    g_assert_nonnull(s);
    return g_strdup(s);
}

/**
 * girtest_string_tester_utf8_return_nullable_transfer_full:
 * @s: (in) (nullable): Pointer to the start of a UTF-8 encoded string, or NULL.
 *
 * Test for a nullable UTF-8 string return value with ownership transfer.
 *
 * Returns: (transfer full) (nullable): A copy of the input string.
 */
gchar *
girtest_string_tester_utf8_return_nullable_transfer_full(const gchar *s)
{
    return s ? girtest_string_tester_utf8_return_transfer_full(s) : NULL;
}

/**
 * girtest_string_tester_utf8_return_transfer_none:
 * @s: (in): Pointer to the start of a UTF-8 encoded string.
 *
 * Test for a UTF-8 string return value with no ownership transfer.
 *
 * Returns: (transfer none): A reference to the input string, with no ownership.
 */
const gchar *
girtest_string_tester_utf8_return_transfer_none(const gchar *s)
{
    g_assert_nonnull(s);
    return s;
}

/**
 * girtest_string_tester_utf8_return_nullable_transfer_none:
 * @s: (in) (nullable): Pointer to the start of a UTF-8 encoded string, or NULL.
 *
 * Test for a nullable UTF-8 string return value with no ownership transfer.
 *
 * Returns: (transfer none) (nullable): A reference to the input string, with no ownership.
 */
const gchar *
girtest_string_tester_utf8_return_nullable_transfer_none(const gchar *s)
{
    return s;
}

/**
 * girtest_string_tester_filename_return_transfer_full:
 * @s: (in) (type filename): Pointer to the start of a string in the filename encoding.
 *
 * Test for a filename string return value with ownership transfer.
 *
 * Returns: (type filename) (transfer full): A copy of the input string.
 */
gchar *
girtest_string_tester_filename_return_transfer_full(const gchar *s)
{
    g_assert_nonnull(s);
    return g_strdup(s);
}

/**
 * girtest_string_tester_filename_return_nullable_transfer_full:
 * @s: (in) (type filename) (nullable): Pointer to the start of a string in the filename encoding, or NULL.
 *
 * Test for a nullable filename string return value with ownership transfer.
 *
 * Returns: (type filename) (transfer full) (nullable): A copy of the input string.
 */
gchar *
girtest_string_tester_filename_return_nullable_transfer_full(const gchar *s)
{
    return s ? girtest_string_tester_filename_return_transfer_full(s) : NULL;
}

/**
 * girtest_string_tester_filename_return_transfer_none:
 * @s: (in) (type filename): Pointer to the start of a string in the filename encoding.
 *
 * Test for a filename string return value with no ownership transfer.
 *
 * Returns: (type filename) (transfer none): A reference to the input string, with no ownership.
 */
const gchar *
girtest_string_tester_filename_return_transfer_none(const gchar *s)
{
    g_assert_nonnull(s);
    return s;
}

/**
 * girtest_string_tester_filename_return_nullable_transfer_none:
 * @s: (in) (type filename) (nullable): Pointer to the start of a string in the filename encoding, or NULL.
 *
 * Test for a nullable filename string return value with no ownership transfer.
 *
 * Returns: (transfer none) (type filename) (nullable): A reference to the input string, with no ownership.
 */
const gchar *
girtest_string_tester_filename_return_nullable_transfer_none(const gchar *s)
{
    return s;
}

/**
 * girtest_string_tester_callback_return_string_transfer_full:
 * @callback: (scope notified): a `GirTestReturnStringFunc`
 * @user_data: user data for callback
 * @destroy: destroy notifiy for user data
 *
 * Test for a UTF-8 string returned by a managed callback.
 *
 * Returns: The number of characters in the value returned by the callback.
 */
int
girtest_string_tester_callback_return_string_transfer_full (GirTestReturnStringFunc callback, gpointer user_data, GDestroyNotify destroy)
{
    gchar *retval = (*callback)();
    g_assert_nonnull(retval);

    int len = g_utf8_strlen(retval, -1);
    g_free(retval);
    destroy(user_data);

    return len;
}