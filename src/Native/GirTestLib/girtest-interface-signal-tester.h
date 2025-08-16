#pragma once

#include <glib-object.h>
#include "data/girtest-signaler.h"

G_BEGIN_DECLS

#define GIRTEST_TYPE_INTERFACE_SIGNAL_TESTER girtest_interface_signal_tester_get_type()

G_DECLARE_FINAL_TYPE(GirTestInterfaceSignalTester, girtest_interface_signal_tester, GIRTEST, INTERFACE_SIGNAL_TESTER, GObject)

GirTestInterfaceSignalTester*
girtest_interface_signal_tester_new ();

G_END_DECLS
