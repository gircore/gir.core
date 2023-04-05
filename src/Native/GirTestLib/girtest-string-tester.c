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
 * girtest_string_tester_utf8_in_transfer_full:
 * @s: (in) (transfer full): Pointer to the start of a UTF-8 encoded string
 *
 * Test for a nullable UTF-8 string parameter which is freed immediately.
 */
void
girtest_string_tester_utf8_in_transfer_full(gchar *s)
{
    g_free(s);
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
 * girtest_string_tester_utf8_in_nullable_transfer_full:
 * @s: (in) (nullable) (transfer full): Pointer to the start of a UTF-8 encoded string, or NULL.
 *
 * Test for a nullable UTF-8 string parameter which is freed immediately if not NULL.
 */
void
girtest_string_tester_utf8_in_nullable_transfer_full(gchar *s)
{
    if(s)
        g_free(s);
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
 * girtest_string_tester_filename_in_transfer_full:
 * @s: (in) (type filename) (transfer full): A string in the filename encoding.
 *
 * Test for a filename string parameter which is freed immediately.
 */
void
girtest_string_tester_filename_in_transfer_full(gchar *s)
{
    GError *error = NULL;
    gchar *utf8 = g_filename_to_utf8(s, -1, NULL, NULL, &error);
    g_assert_no_error(error);
    g_free(utf8);
    g_free(s);
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
 * girtest_string_tester_filename_in_nullable_transfer_full:
 * @s: (in) (nullable) (type filename) (transfer full): A string in the filename encoding, or NULL.
 *
 * Test for a filename string parameter which is freed immediately if not NULL
 */
void
girtest_string_tester_filename_in_nullable_transfer_full(gchar *s)
{    
    if(s)
    {
        GError *error = NULL;
        gchar *utf8 = g_filename_to_utf8(s, -1, NULL, NULL, &error);
        g_assert_no_error(error);
        g_free(utf8);
        g_free(s);
    }
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

/**
 * girtest_string_tester_utf8_return_unexpected_null:
 *
 * Returns null but is missing the annotation.
 *
 * Returns: NULL
 */
gchar *
girtest_string_tester_utf8_return_unexpected_null()
{
    return NULL;
}

/**
 * girtest_string_tester_filename_return_unexpected_null:
 *
 * Returns null but is missing the annotation.
 *
 * Returns: (type filename): NULL
 */
gchar *
girtest_string_tester_filename_return_unexpected_null()
{
    return NULL;
}

/**
 * girtest_string_tester_utf8_out_transfer_none:
 * @s: (in): Pointer to the start of a UTF-8 encoded string.
 * @result: (out) (transfer none): A reference to the input string, with no ownership.
 *
 * Test for a UTF-8 string output value with no ownership transfer.
 */
void
girtest_string_tester_utf8_out_transfer_none(const gchar *s, const gchar **result)
{
    g_assert_nonnull(s);
    g_assert_nonnull(result);
    *result = s;
}

/**
 * girtest_string_tester_utf8_out_optional_transfer_none:
 * @s: (in): Pointer to the start of a UTF-8 encoded string.
 * @result: (out) (optional) (transfer none): A reference to the input string, with no ownership.
 *
 * Test for an optional UTF-8 string output value with no ownership transfer.
 */
void
girtest_string_tester_utf8_out_optional_transfer_none(const gchar *s, const gchar **result)
{
    g_assert_nonnull(s);
    if (result)
        *result = s;
}

/**
 * girtest_string_tester_utf8_out_nullable_transfer_none:
 * @s: (in) (nullable): Pointer to the start of a UTF-8 encoded string, or NULL.
 * @result: (out) (nullable) (transfer none): A reference to the input string, with no ownership.
 *
 * Test for a nullable UTF-8 string output value with no ownership transfer.
 */
void
girtest_string_tester_utf8_out_nullable_transfer_none(const gchar *s, const gchar **result)
{
    g_assert_nonnull(result);
    *result = s;
}

/**
 * girtest_string_tester_utf8_out_nullable_optional_transfer_none:
 * @s: (in) (nullable): Pointer to the start of a UTF-8 encoded string, or NULL.
 * @result: (out) (optional) (nullable) (transfer none): A reference to the input string, with no ownership.
 *
 * Test for an optional and nullable UTF-8 string output value with no ownership transfer.
 */
void
girtest_string_tester_utf8_out_nullable_optional_transfer_none(const gchar *s, const gchar **result)
{
    if (result)
        *result = s;
}

/**
 * girtest_string_tester_utf8_out_transfer_full:
 * @s: (in): Pointer to the start of a UTF-8 encoded string.
 * @result: (out) (transfer full): A copy of the input string.
 *
 * Test for a UTF-8 string output value with full ownership transfer.
 */
void
girtest_string_tester_utf8_out_transfer_full(const gchar *s, gchar **result)
{
    g_assert_nonnull(s);
    g_assert_nonnull(result);
    *result = g_strdup(s);
}

/**
 * girtest_string_tester_utf8_out_optional_transfer_full:
 * @s: (in): Pointer to the start of a UTF-8 encoded string.
 * @result: (out) (optional) (transfer full): A copy of the input string.
 *
 * Test for an optional UTF-8 string output value with full ownership transfer.
 */
void
girtest_string_tester_utf8_out_optional_transfer_full(const gchar *s, gchar **result)
{
    g_assert_nonnull(s);
    if (result)
        *result = g_strdup(s);
}

/**
 * girtest_string_tester_utf8_out_nullable_transfer_full:
 * @s: (in) (nullable): Pointer to the start of a UTF-8 encoded string, or NULL.
 * @result: (out) (nullable) (transfer full): A copy of the input string.
 *
 * Test for a nullable UTF-8 string output value with full ownership transfer.
 */
void
girtest_string_tester_utf8_out_nullable_transfer_full(const gchar *s, gchar **result)
{
    g_assert_nonnull(result);
    *result = s ? g_strdup(s) : NULL;
}

/**
 * girtest_string_tester_utf8_out_nullable_optional_transfer_full:
 * @s: (in) (nullable): Pointer to the start of a UTF-8 encoded string, or NULL.
 * @result: (out) (optional) (nullable) (transfer full): A copy of the input string.
 *
 * Test for a optional and nullable UTF-8 string output value with full ownership transfer.
 */
void
girtest_string_tester_utf8_out_nullable_optional_transfer_full(const gchar *s, gchar **result)
{
    if (result)
        *result = s ? g_strdup(s) : NULL;
}

/**
 * girtest_string_tester_filename_out_transfer_none:
 * @s: (in) (type filename): Pointer to the start of a filename encoded string.
 * @result: (out) (type filename) (transfer none): A reference to the input string, with no ownership.
 *
 * Test for a filename string output value with no ownership transfer.
 */
void
girtest_string_tester_filename_out_transfer_none(const gchar *s, const gchar **result)
{
    g_assert_nonnull(s);
    g_assert_nonnull(result);
    *result = s;
}

/**
 * girtest_string_tester_filename_out_optional_transfer_none:
 * @s: (in) (type filename): Pointer to the start of a filename encoded string.
 * @result: (out) (type filename) (optional) (transfer none): A reference to the input string, with no ownership.
 *
 * Test for an optional filename string output value with no ownership transfer.
 */
void
girtest_string_tester_filename_out_optional_transfer_none(const gchar *s, const gchar **result)
{
    g_assert_nonnull(s);
    if (result)
        *result = s;
}

/**
 * girtest_string_tester_filename_out_nullable_transfer_none:
 * @s: (in) (type filename) (nullable): Pointer to the start of a filename encoded string, or NULL.
 * @result: (out) (type filename) (nullable) (transfer none): A reference to the input string, with no ownership.
 *
 * Test for a nullable filename string output value with no ownership transfer.
 */
void
girtest_string_tester_filename_out_nullable_transfer_none(const gchar *s, const gchar **result)
{
    g_assert_nonnull(result);
    *result = s;
}

/**
 * girtest_string_tester_filename_out_nullable_optional_transfer_none:
 * @s: (in) (type filename) (nullable): Pointer to the start of a filename encoded string, or NULL.
 * @result: (out) (type filename) (optional) (nullable) (transfer none): A reference to the input string, with no ownership.
 *
 * Test for an optional and nullable filename string output value with no ownership transfer.
 */
void
girtest_string_tester_filename_out_nullable_optional_transfer_none(const gchar *s, const gchar **result)
{
    if (result)
        *result = s;
}

/**
 * girtest_string_tester_filename_out_transfer_full:
 * @s: (in) (type filename): Pointer to the start of a filename encoded string.
 * @result: (out) (type filename) (transfer full): A copy of the input string.
 *
 * Test for a filename string output value with full ownership transfer.
 */
void
girtest_string_tester_filename_out_transfer_full(const gchar *s, gchar **result)
{
    g_assert_nonnull(s);
    g_assert_nonnull(result);
    *result = g_strdup(s);
}

/**
 * girtest_string_tester_filename_out_optional_transfer_full:
 * @s: (in) (type filename): Pointer to the start of a filename encoded string.
 * @result: (out) (type filename) (optional) (transfer full): A copy of the input string.
 *
 * Test for an optional filename string output value with full ownership transfer.
 */
void
girtest_string_tester_filename_out_optional_transfer_full(const gchar *s, gchar **result)
{
    g_assert_nonnull(s);
    if (result)
        *result = g_strdup(s);
}

/**
 * girtest_string_tester_filename_out_nullable_transfer_full:
 * @s: (in) (type filename) (nullable): Pointer to the start of a filename encoded string, or NULL.
 * @result: (out) (type filename) (nullable) (transfer full): A copy of the input string.
 *
 * Test for a nullable filename string output value with full ownership transfer.
 */
void
girtest_string_tester_filename_out_nullable_transfer_full(const gchar *s, gchar **result)
{
    g_assert_nonnull(result);
    *result = s ? g_strdup(s) : NULL;
}

/**
 * girtest_string_tester_filename_out_nullable_optional_transfer_full:
 * @s: (in) (type filename) (nullable): Pointer to the start of a filename encoded string, or NULL.
 * @result: (out) (type filename) (optional) (nullable) (transfer full): A copy of the input string.
 *
 * Test for a optional and nullable filename string output value with full ownership transfer.
 */
void
girtest_string_tester_filename_out_nullable_optional_transfer_full(const gchar *s, gchar **result)
{
    if (result)
        *result = s ? g_strdup(s) : NULL;
}