#pragma once

#include <glib-object.h>

G_BEGIN_DECLS

/**
 * GirTestIntAlias:
 * 
 * A int value with a special name.
 */
typedef int GirTestIntAlias;

/**
 * GirTestPointerAlias:
 * 
 * A pointer value with a special name.
 */
typedef gpointer GirTestPointerAlias;

/**
 * GIRTEST_INT_ALIAS_ZERO: (value 0) (type GirTestIntAlias)
 *
 * Constant to define an undefined int alias.
 */
#define GIRTEST_INT_ALIAS_ZERO ((GirTestIntAlias) 0)

#define GIRTEST_TYPE_ALIAS_TESTER girtest_alias_tester_get_type()

G_DECLARE_FINAL_TYPE(GirTestAliasTester, girtest_alias_tester, GIRTEST, ALIAS_TESTER, GObject)

gint
girtest_alias_tester_to_int(GirTestIntAlias alias);

GirTestIntAlias
girtest_alias_tester_to_int_alias(int i);

void
girtest_alias_tester_alias_out(GirTestIntAlias* alias);

void
girtest_alias_tester_alias_in_out(GirTestIntAlias* alias);

gpointer
girtest_alias_tester_to_pointer(GirTestPointerAlias alias);

GirTestPointerAlias
girtest_alias_tester_to_pointer_alias(gpointer pointer);

GirTestIntAlias*
girtest_alias_tester_get_array_transfer_full (guint *n_elements);

G_END_DECLS
