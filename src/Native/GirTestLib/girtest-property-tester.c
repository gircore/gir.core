#include "girtest-property-tester.h"
#include "girtest-typed-record-tester.h"
#include "data/girtest-executor-impl.h"
#include "data/girtest-executor-private-impl.h"

/**
 * GirTestPropertyTester:
 *
 * Contains properties to test bindings.
 */

typedef enum
{
    PROP_STRING_VALUE = 1,
    PROP_PROPERTY_TESTER = 2,
    PROP_TYPED_RECORD_VALUE = 3,
    PROP_INT_VALUE = 4,
    PROP_BOOLEAN_VALUE = 5,
    PROP_OBJECT_VALUE = 6,
    PROP_EXECUTOR_VALUE = 7,
    PROP_EXECUTOR_ANONYMOUS_VALUE = 8,
    PROP_UINT64_VALUE = 9,
    PROP_INT64_VALUE = 10,
    PROP_LONG_VALUE = 11,
    PROP_ULONG_VALUE = 12,
    N_PROPERTIES
} PropertyTesterProperty;

struct _GirTestPropertyTester
{
    GObject parent_instance;

    gchar* string_value;
    gchar* property_tester;
    GirTestTypedRecordTester* record;
    GObject* object_value;
    GirTestExecutor* executor_value;
    GirTestExecutor* executor_anonymous_value;
    gint int_value;
    gboolean boolean_value;
    guint64 uint64_value;
    gint64 int64_value;
    glong long_value;
    gulong ulong_value;
};

G_DEFINE_TYPE(GirTestPropertyTester, girtest_property_tester, G_TYPE_OBJECT)

static GParamSpec *properties[N_PROPERTIES];


static void
girtest_property_tester_get_property (GObject    *object,
                                    guint       prop_id,
                                    GValue     *value,
                                    GParamSpec *pspec)
{
    GirTestPropertyTester *self = GIRTEST_PROPERTY_TESTER (object);

    switch (prop_id)
    {
    case PROP_STRING_VALUE:
        g_value_set_string (value, self->string_value);
        break;
    case PROP_PROPERTY_TESTER:
        g_value_set_string (value, self->property_tester);
        break;
    case PROP_TYPED_RECORD_VALUE:
        g_value_set_boxed (value, self->record);
        break;
    case PROP_INT_VALUE:
        g_value_set_int (value, self->int_value);
        break;
    case PROP_BOOLEAN_VALUE:
        g_value_set_boolean (value, self->boolean_value);
        break;
    case PROP_OBJECT_VALUE:
        g_value_set_object (value, self->object_value);
        break;
    case PROP_EXECUTOR_VALUE:
        g_value_set_object (value, self->executor_value);
        break;
    case PROP_EXECUTOR_ANONYMOUS_VALUE:
        g_value_set_object (value, self->executor_anonymous_value);
        break;
    case PROP_UINT64_VALUE:
        g_value_set_uint64 (value, self->uint64_value);
        break;
    case PROP_INT64_VALUE:
        g_value_set_int64 (value, self->int64_value);
        break;
    case PROP_LONG_VALUE:
        g_value_set_long (value, self->long_value);
        break;
    case PROP_ULONG_VALUE:
        g_value_set_ulong (value, self->ulong_value);
        break;
    default:
        G_OBJECT_WARN_INVALID_PROPERTY_ID (object, prop_id, pspec);
    }
}

static void
girtest_property_tester_set_property (GObject      *object,
                                    guint         prop_id,
                                    const GValue *value,
                                    GParamSpec   *pspec)
{
    GirTestPropertyTester *self = GIRTEST_PROPERTY_TESTER (object);

    switch (prop_id)
    {
    case PROP_STRING_VALUE:
        self->string_value = g_value_dup_string (value);
        break;
    case PROP_PROPERTY_TESTER:
        self->property_tester = g_value_dup_string (value);
        break;
    case PROP_TYPED_RECORD_VALUE:
        self->record = g_value_get_boxed (value);
        break;
    case PROP_INT_VALUE:
        self->int_value = g_value_get_int (value);
        break;
    case PROP_BOOLEAN_VALUE:
        self->boolean_value = g_value_get_boolean (value);
        break;
    case PROP_OBJECT_VALUE:
        self->object_value = g_value_get_object (value);
        break;
    case PROP_UINT64_VALUE:
        self->uint64_value = g_value_get_uint64 (value);
        break;
    case PROP_INT64_VALUE:
        self->int64_value = g_value_get_int64 (value);
        break;
    case PROP_LONG_VALUE:
        self->long_value = g_value_get_long (value);
        break;
    case PROP_ULONG_VALUE:
        self->ulong_value = g_value_get_ulong (value);
        break;
    default:
        G_OBJECT_WARN_INVALID_PROPERTY_ID (object, prop_id, pspec);
    }
}

static void
girtest_property_tester_init(GirTestPropertyTester *value)
{
    value->executor_value = GIRTEST_EXECUTOR(girtest_executor_impl_new());
    value->executor_anonymous_value = GIRTEST_EXECUTOR(girtest_executor_private_impl_new());
}

static void
girtest_property_tester_class_init(GirTestPropertyTesterClass *class)
{
    GObjectClass *object_class = G_OBJECT_CLASS (class);

    object_class->set_property = girtest_property_tester_set_property;
    object_class->get_property = girtest_property_tester_get_property;

    properties[PROP_STRING_VALUE] =
      g_param_spec_string ("string-value",
                           "String Value",
                           "A string value",
                           NULL  /* default value */,
                           G_PARAM_READWRITE);

    properties[PROP_PROPERTY_TESTER] =
      g_param_spec_string ("property-tester",
                           "Property Tester",
                           "A string value named like it's class",
                           NULL  /* default value */,
                           G_PARAM_READWRITE);

   /**
   * GirPropertyTester:record-value:
   * Contains a typed record tester
   */
    properties[PROP_TYPED_RECORD_VALUE] =
      g_param_spec_boxed ("record-value", NULL, NULL, GIRTEST_TYPE_TYPED_RECORD_TESTER, G_PARAM_READWRITE);

    properties[PROP_OBJECT_VALUE] =
      g_param_spec_object ("object-value",
                           "Object Value",
                           "An object value",
                           G_TYPE_OBJECT,
                           G_PARAM_READWRITE);

    properties[PROP_EXECUTOR_VALUE] =
        g_param_spec_object ("executor-value",
                             "Executor Value",
                             "An executor value",
                             GIRTEST_TYPE_EXECUTOR,
                             G_PARAM_READABLE);

    properties[PROP_EXECUTOR_ANONYMOUS_VALUE] =
        g_param_spec_object ("executor-anonymous-value",
                            "Executor Anonymous Value",
                            "An executor value",
                            GIRTEST_TYPE_EXECUTOR,
                            G_PARAM_READABLE);

    properties[PROP_INT_VALUE] =
          g_param_spec_int ("int-value",
                               "Integer Value",
                               "An int value",
                               G_MININT,
                               G_MAXINT,
                               0  /* default value */,
                               G_PARAM_READWRITE);

    properties[PROP_BOOLEAN_VALUE] =
              g_param_spec_boolean ("boolean-value",
                                   "Boolean Value",
                                   "A boolean value",
                                   FALSE  /* default value */,
                                   G_PARAM_READWRITE);

    properties[PROP_UINT64_VALUE] =
          g_param_spec_uint64 ("uint64-value",
                               "UInt64 Value",
                               "A uint64 value",
                               0,
                               G_MAXUINT64,
                               0  /* default value */,
                               G_PARAM_READWRITE);

    properties[PROP_INT64_VALUE] =
            g_param_spec_int64 ("int64-value",
                                "Int64 Value",
                                "An int64 value",
                                G_MININT64,
                                G_MAXINT64,
                                0  /* default value */,
                                G_PARAM_READWRITE);

    properties[PROP_LONG_VALUE] =
          g_param_spec_long ("long-value",
                             "Long Value",
                             "A glong value",
                             G_MINLONG,
                             G_MAXLONG,
                             0  /* default value */,
                             G_PARAM_READWRITE);

    properties[PROP_ULONG_VALUE] =
          g_param_spec_ulong ("ulong-value",
                              "ULong Value",
                              "A gulong value",
                              0,
                              G_MAXULONG,
                              0  /* default value */,
                              G_PARAM_READWRITE);

    g_object_class_install_properties (object_class, N_PROPERTIES, properties);
}

/**
 * girtest_property_tester_new:
 *
 * Creates a new `GirTestPropertyTester`.
 *
 * Returns: The newly created `GirTestPropertyTester`.
 */
GirTestPropertyTester*
girtest_property_tester_new (void)
{
    return g_object_new (GIRTEST_TYPE_PROPERTY_TESTER, NULL);
}
