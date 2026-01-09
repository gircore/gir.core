#pragma once

#include <glib-object.h>

G_BEGIN_DECLS

#define GIRTEST_TYPE_INSTANCE_CREATION_TESTER girtest_instance_creation_tester_get_type()

G_DECLARE_FINAL_TYPE(GirTestInstanceCreationTester, girtest_instance_creation_tester, GIRTEST, INSTANCE_CREATION_TESTER, GObject)

GirTestInstanceCreationTester* girtest_instance_creation_tester_new();
guint girtest_instance_creation_tester_get_ref_count(GirTestInstanceCreationTester *instance);
void girtest_instance_creation_tester_set_obj_transfer_none(GirTestInstanceCreationTester *instance, GObject *obj);
void girtest_instance_creation_tester_set_obj_transfer_full(GirTestInstanceCreationTester *instance, GObject *obj);
G_END_DECLS

