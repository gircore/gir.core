#pragma once

#include <glib-object.h>

G_BEGIN_DECLS

#define GIRTEST_TYPE_SIGNAL_TESTER girtest_signal_tester_get_type()

G_DECLARE_FINAL_TYPE(GirTestSignalTester, girtest_signal_tester,
                     GIRTEST, SIGNAL_TESTER, GObject)

GirTestSignalTester*
girtest_signal_tester_new (void);

void
girtest_signal_tester_emit_my_signal_fubar (GirTestSignalTester *tester);

void
girtest_signal_tester_emit_gbytes_signal (GirTestSignalTester *tester);

G_END_DECLS

