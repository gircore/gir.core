#include "girtest-error-tester.h"

/**
 * GirTestErrorTester:
 *
 * Contains functions to test GError-based error handling.
 */

G_DEFINE_QUARK (girtest-error-tester-error-quark, girtest_error_tester_error)

struct _GirTestErrorTester
{
    GObject parent_instance;
};

G_DEFINE_TYPE(GirTestErrorTester, girtest_error_tester, G_TYPE_OBJECT)

static void
girtest_error_tester_init(GirTestErrorTester *value)
{
}

static void
girtest_error_tester_class_init(GirTestErrorTesterClass *class)
{
}

/**
 * girtest_error_tester_new_can_fail:
 * @fail: Specifies whether the constructor should fail.
 * @error: Return location for a potential error.
 *
 * Creates a new `GirTestErrorTester`, or produces an error depending on the boolean parameter.
 *
 * Returns: (transfer full) (nullable): The newly created `GirTestErrorTester`, on success.
 */
GirTestErrorTester*
girtest_error_tester_new_can_fail (gboolean fail, GError **error)
{
    g_return_val_if_fail (error == NULL || *error == NULL, NULL);

    if (fail)
    {
        g_set_error(error, GIRTEST_ERROR_TESTER_ERROR,
                    GIRTEST_ERROR_TESTER_ERROR_FAILURE, "Constructor failed");
        return NULL;
    }

    return g_object_new (GIRTEST_TYPE_ERROR_TESTER, NULL);
}

/**
 * girtest_error_tester_method_can_fail:
 * @tester: an `ErrorTester`
 * @fail: Specifies whether the method should fail.
 * @error: Return location for a potential error.
 *
 * Returns an error depending on the boolean parameter.
 *
 * Returns: A bool indicating success or failure.
 */
gboolean
girtest_error_tester_method_can_fail (GirTestErrorTester *tester, gboolean fail, GError **error)
{
    g_return_val_if_fail (error == NULL || *error == NULL, FALSE);

    if (fail)
    {
        g_set_error(error, GIRTEST_ERROR_TESTER_ERROR,
                    GIRTEST_ERROR_TESTER_ERROR_FAILURE, "Method failed");
        return FALSE;
    }

    return TRUE;
}

/**
 * girtest_error_tester_function_can_fail:
 * @fail: Specifies whether the function should fail.
 * @error: Return location for a potential error.
 *
 * Returns an error depending on the boolean parameter.
 *
 * Returns: A bool indicating success or failure.
 */
gboolean
girtest_error_tester_function_can_fail (gboolean fail, GError **error)
{
    g_return_val_if_fail (error == NULL || *error == NULL, FALSE);

    if (fail)
    {
        g_set_error(error, GIRTEST_ERROR_TESTER_ERROR,
                    GIRTEST_ERROR_TESTER_ERROR_FAILURE, "Function failed");
        return FALSE;
    }

    return TRUE;
}
