#pragma once

#include <glib-object.h>

G_BEGIN_DECLS

#define GIRTEST_TYPE_SIGNALER girtest_signaler_get_type()

G_DECLARE_INTERFACE (GirTestSignaler, girtest_signaler, GIRTEST, SIGNALER, GObject)

struct _GirTestSignalerInterface
{
    GTypeInterface parent_iface;

    void (*emit) (GirTestSignaler *self);
};

void girtest_signaler_emit (GirTestSignaler *self);
void girtest_signaler_my_signal (GirTestSignaler *self);
G_END_DECLS
