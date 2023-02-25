#pragma once

#include <glib-object.h>

G_BEGIN_DECLS

#define GIRTEST_TYPE_PROPERTY_TESTER girtest_property_tester_get_type()

G_DECLARE_FINAL_TYPE(GirTestPropertyTester, girtest_property_tester,
                     GIRTEST, PROPERTY_TESTER, GObject)

GirTestPropertyTester*
girtest_property_tester_new (void);

G_END_DECLS

