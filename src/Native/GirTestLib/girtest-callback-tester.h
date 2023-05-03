#pragma once

#include <glib-object.h>

G_BEGIN_DECLS

#define GIRTEST_TYPE_CALLBACK_TESTER girtest_callback_tester_get_type()

G_DECLARE_FINAL_TYPE(GirTestCallbackTester, girtest_callback_tester,
                     GIRTEST, CALLBACK_TESTER, GObject)

typedef int (*GirTestIntCallback)  (int val);

GirTestCallbackTester*
girtest_callback_tester_new (void);

void
girtest_callback_tester_set_notified_callback(GirTestCallbackTester *tester,
                                              GirTestIntCallback callback,
                                              gpointer data,
                                              GDestroyNotify notify);

int
girtest_callback_tester_run_notified_callback(GirTestCallbackTester *tester,
                                              int value,
                                              gboolean done);

G_END_DECLS

