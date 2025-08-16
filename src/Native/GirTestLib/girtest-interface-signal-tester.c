#include "girtest-interface-signal-tester.h"

struct _GirTestInterfaceSignalTester
{
    GObject parent_instance;
};

static void girtest_interface_signal_tester_interface_init (GirTestSignalerInterface *iface);

G_DEFINE_TYPE_WITH_CODE (GirTestInterfaceSignalTester, girtest_interface_signal_tester, G_TYPE_OBJECT,
                         G_IMPLEMENT_INTERFACE (GIRTEST_TYPE_SIGNALER,
                                                girtest_interface_signal_tester_interface_init))

static void
girtest_interface_signal_tester_emit (GirTestSignaler *self)
{
    girtest_signaler_my_signal(GIRTEST_SIGNALER (self));
}

static void
girtest_interface_signal_tester_interface_init (GirTestSignalerInterface *iface)
{
    iface->emit = girtest_interface_signal_tester_emit;
}

static void
girtest_interface_signal_tester_init(GirTestInterfaceSignalTester *value)
{
}

static void
girtest_interface_signal_tester_class_init(GirTestInterfaceSignalTesterClass *class)
{
}

GirTestInterfaceSignalTester*
girtest_interface_signal_tester_new ()
{
    return g_object_new (GIRTEST_TYPE_INTERFACE_SIGNAL_TESTER, NULL);
}