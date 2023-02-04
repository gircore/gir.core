#ifndef GIR_TEST_CLASSTYPE_H
#define GIR_TEST_CLASSTYPE_H

#include <glib-object.h>

G_BEGIN_DECLS

#define GIRTEST_TYPE_CLASSTYPE girtest_class_type_get_type()
G_DECLARE_FINAL_TYPE(GirTestClassType, girtest_class_type,
                     GIRTEST, CLASSTYPE, GObject)

void girtest_class_type_transfer_ownership_full_and_unref(GObject *object);

G_END_DECLS

#endif
