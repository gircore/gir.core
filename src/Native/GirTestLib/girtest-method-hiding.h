#pragma once

#include <glib-object.h>

G_BEGIN_DECLS

#define GIRTEST_TYPE_METHOD_HIDING_BASE girtest_method_hiding_base_get_type()
G_DECLARE_DERIVABLE_TYPE(GirTestMethodHidingBase, girtest_method_hiding_base, GIRTEST, METHOD_HIDING_BASE, GObject)

struct _GirTestMethodHidingBaseClass
{
  GObjectClass parent_class;
};

gchar *
girtest_method_hiding_base_to_string(GirTestMethodHidingBase *instance);

gchar *
girtest_method_hiding_base_custom_string(GirTestMethodHidingBase *instance);

#define GIRTEST_TYPE_METHOD_HIDING_SUBCLASS girtest_method_hiding_subclass_get_type()
G_DECLARE_FINAL_TYPE(GirTestMethodHidingSubclass, girtest_method_hiding_subclass, GIRTEST, METHOD_HIDING_SUBCLASS, GirTestMethodHidingBase)

struct _GirTestMethodHidingSubclassClass
{
  GirTestMethodHidingBaseClass parent_class;
};

GirTestMethodHidingSubclass*
girtest_method_hiding_subclass_new(void);

gchar *
girtest_method_hiding_subclass_custom_string(GirTestMethodHidingSubclass *instance);

G_END_DECLS
