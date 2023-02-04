#pragma once

#include <glib-object.h>

G_BEGIN_DECLS

#define GIRTEST_TYPE_RETURNING_SIGNAL_TESTER girtest_returning_signal_tester_get_type()

G_DECLARE_FINAL_TYPE(GirTestReturningSignalTester, girtest_returning_signal_tester,
                     GIRTEST, RETURNING_SIGNAL_TESTER, GObject)

GirTestReturningSignalTester*
girtest_returning_signal_tester_new (void);

gboolean
girtest_returning_signal_tester_emit_return_bool (GirTestReturningSignalTester *tester);

G_END_DECLS

