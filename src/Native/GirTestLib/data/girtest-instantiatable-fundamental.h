#pragma once

#include <glib-object.h>

G_BEGIN_DECLS

GType
girtest_instantiatable_fundamental_get_type (void);

#define GIRTEST_TYPE_INSTANTIATABLE_FUNDAMENTAL girtest_instantiatable_fundamental_get_type()
#define GIRTEST_IS_INSTANTIATABLE_FUNDAMENTAL(obj)  (G_TYPE_CHECK_INSTANCE_TYPE ((obj), GIRTEST_TYPE_INSTANTIATABLE_FUNDAMENTAL))
#define GIRTEST_INSTANTIATABLE_FUNDAMENTAL(obj)     (G_TYPE_CHECK_INSTANCE_CAST ((obj), GIRTEST_TYPE_INSTANTIATABLE_FUNDAMENTAL, GirTestInstantiatableFundamental))

typedef struct _GirTestInstantiatableFundamental GirTestInstantiatableFundamental;

void
girtest_instantiatable_fundamental_unref (GirTestInstantiatableFundamental *self);

GirTestInstantiatableFundamental*
girtest_instantiatable_fundamental_new();

G_END_DECLS

