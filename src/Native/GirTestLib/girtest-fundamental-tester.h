#pragma once

#include <glib-object.h>

#include "data/girtest-instantiatable-fundamental.h"

G_BEGIN_DECLS

#define GIRTEST_TYPE_FUNDAMENTAL_TESTER girtest_fundamental_tester_get_type()

typedef struct _GirTestFundamentalTester GirTestFundamentalTester;

G_DECLARE_FINAL_TYPE(GirTestFundamentalTester, girtest_fundamental_tester,
                     GIRTEST, FUNDAMENTAL_TESTER, GObject)

GirTestInstantiatableFundamental*
girtest_fundamental_tester_create_fundamental (gboolean return_null);

G_END_DECLS

