#pragma once

#include <glib-object.h>

G_BEGIN_DECLS

#define GIRTEST_TYPE_ERROR_TESTER girtest_error_tester_get_type()

#define GIRTEST_ERROR_TESTER_ERROR (girtest_error_tester_error_quark())

typedef enum
{
    GIRTEST_ERROR_TESTER_ERROR_FAILURE,
} GirTestErrorTesterError;

G_DECLARE_FINAL_TYPE(GirTestErrorTester, girtest_error_tester,
                     GIRTEST, ERROR_TESTER, GObject)

GirTestErrorTester*
girtest_error_tester_new_can_fail (gboolean fail, GError **error);

gboolean
girtest_error_tester_method_can_fail (GirTestErrorTester *tester, gboolean fail, GError **error);

gboolean
girtest_error_tester_function_can_fail (gboolean fail, GError **error);

G_END_DECLS
