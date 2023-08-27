#include "girtest-instantiatable-fundamental.h"

#include <gobject/gvaluecollector.h>

typedef struct _GirTestInstantiatableFundamentalClass GirTestInstantiatableFundamentalClass;
typedef struct _GirTestInstantiatableFundamentalTypeInfo GirTestInstantiatableFundamentalTypeInfo;

/**
 * GirTestInstantiatableFundamental:
 *
 * A fundamental type designed for unit tests.
 */

struct _GirTestInstantiatableFundamental
{
    GTypeInstance parent_instance;
    gatomicrefcount ref_count;
    GType value_type;
};

struct _GirTestInstantiatableFundamentalClass
{
  void     (* finalize)      (GirTestInstantiatableFundamental *inst);
  gboolean (* is_static)     (GirTestInstantiatableFundamental *inst);
};

struct _GirTestInstantiatableFundamentalTypeInfo
{
  gsize instance_size;
  void     (* instance_init) (GirTestInstantiatableFundamental *inst);
  void     (* finalize)      (GirTestInstantiatableFundamental *inst);
  gboolean (* is_static)     (GirTestInstantiatableFundamental *inst);
};

#define GIRTEST_INSTANTIATABLE_FUNDAMENTAL_GET_CLASS(obj)   (G_TYPE_INSTANCE_GET_CLASS ((obj), GIRTEST_TYPE_INSTANTIATABLE_FUNDAMENTAL, GirTestInstantiatableFundamentalClass))

static void
girtest_instantiatable_fundamental_init (GirTestInstantiatableFundamental *self)
{
  g_atomic_ref_count_init (&self->ref_count);
}

static void
value_instantiatable_fundamental_init(GValue *value)
{
  value->data[0].v_pointer = NULL;
}

static void
girtest_instantiatable_fundamental_finalize (GirTestInstantiatableFundamental *self)
{
  g_type_free_instance ((GTypeInstance *) self);
}

static void
girtest_instantiatable_fundamental_class_init(GirTestInstantiatableFundamentalClass *class)
{
  class->finalize = girtest_instantiatable_fundamental_finalize;
}

/**
 * girtest_instantiatable_fundamental_new:
 *
 * Creates a new `GirTestInstantiatableFundamental`.
 *
 * Returns: (transfer full): The newly created `GirTestInstantiatableFundamental`.
 */
GirTestInstantiatableFundamental*
girtest_instantiatable_fundamental_new()
{
  GirTestInstantiatableFundamental *self;
  self = (GirTestInstantiatableFundamental *)g_type_create_instance(GIRTEST_TYPE_INSTANTIATABLE_FUNDAMENTAL);
  ((GTypeInstance *)self)->g_class->g_type = GIRTEST_TYPE_INSTANTIATABLE_FUNDAMENTAL;
  return self;
}

/**
 * girtest_instantiatable_fundamental_unref:
 * @self: (transfer full): a `GirTestInstantiatableFundamental`
 *
 * Releases a reference on the given `GirTestInstantiatableFundamental`.
 *
 * If the reference was the last, the resources associated to the `self` are
 * freed.
 */
void
girtest_instantiatable_fundamental_unref (GirTestInstantiatableFundamental *self)
{
  g_return_if_fail (GIRTEST_IS_INSTANTIATABLE_FUNDAMENTAL (self));

  if (g_atomic_ref_count_dec (&self->ref_count)) {
    girtest_instantiatable_fundamental_finalize(self);
  }
}

/**
 * girtest_instantiatable_fundamental_ref:
 * @self: a `GirTestInstantiatableFundamental`
 *
 * Acquires a reference on the given `GirTestInstantiatableFundamental`.
 *
 * Returns: (transfer full): the `GirTestInstantiatableFundamental` with an additional reference
 */
GirTestInstantiatableFundamental *
girtest_instantiatable_fundamental_ref (GirTestInstantiatableFundamental *self)
{
  g_return_val_if_fail (GIRTEST_IS_INSTANTIATABLE_FUNDAMENTAL (self), NULL);

  g_atomic_ref_count_inc (&self->ref_count);

  return self;
}

static void
value_instantiatable_fundamental_copy (const GValue *src,
                                    GValue       *dst)
{
  if (src->data[0].v_pointer != NULL && GIRTEST_IS_INSTANTIATABLE_FUNDAMENTAL(src->data[0].v_pointer)) {
    g_atomic_ref_count_inc (src->data[0].v_pointer);
    dst->data[0].v_pointer = src->data[0].v_pointer;
  }
  else
    dst->data[0].v_pointer = NULL;
}

static gpointer
value_instantiatable_fundamental_peek_pointer (const GValue *value)
{
  return value->data[0].v_pointer;
}

static void
value_instantiatable_fundamental_free_value (GValue *value)
{
  if (value->data[0].v_pointer != NULL)
    g_clear_pointer (&value->data[0].v_pointer, girtest_instantiatable_fundamental_unref);
}

static char *
value_instantiatable_fundamental_collect_value (GValue      *value,
                                guint        n_collect_values,
                                GTypeCValue *collect_values,
                                guint        collect_flags)
{
  GirTestInstantiatableFundamental *inst = collect_values[0].v_pointer;

  if (inst == NULL)
    {
      value->data[0].v_pointer = NULL;
      return NULL;
    }

  if (inst->parent_instance.g_class == NULL)
    return g_strconcat ("invalid unclassed GirTestInstantiatableFundamental pointer for "
                        "value type '",
                        G_VALUE_TYPE_NAME (value),
                        "'",
                        NULL);

  value->data[0].v_pointer = girtest_instantiatable_fundamental_ref (inst);

  return NULL;
}

static char *
value_instantiatable_fundamental_lcopy_value (const GValue *value,
                              guint         n_collect_values,
                              GTypeCValue  *collect_values,
                              guint         collect_flags)
{
  GirTestInstantiatableFundamental **inst = collect_values[0].v_pointer;

  if (inst == NULL)
    return g_strconcat ("value location for '",
                        G_VALUE_TYPE_NAME (value),
                        "' passed as NULL",
                        NULL);

  if (value->data[0].v_pointer == NULL)
    *inst = NULL;
  else if (collect_flags & G_VALUE_NOCOPY_CONTENTS)
    *inst = value->data[0].v_pointer;
  else
    *inst = girtest_instantiatable_fundamental_ref (value->data[0].v_pointer);

  return NULL;
}

GType
girtest_instantiatable_fundamental_get_type (void)
{
  static gsize type_id;

  if (g_once_init_enter (&type_id))
    {
      static const GTypeFundamentalInfo finfo = {
        (G_TYPE_FLAG_CLASSED |
         G_TYPE_FLAG_INSTANTIATABLE |
         G_TYPE_FLAG_DERIVABLE |
         G_TYPE_FLAG_DEEP_DERIVABLE),
      };

      static const GTypeValueTable value_table = {
        value_instantiatable_fundamental_init,
        value_instantiatable_fundamental_free_value,
        value_instantiatable_fundamental_copy,
        value_instantiatable_fundamental_peek_pointer,
        "p",
        value_instantiatable_fundamental_collect_value,
        "p",
        value_instantiatable_fundamental_lcopy_value,
      };

      const GTypeInfo info = {
        /* Class */
        sizeof (GirTestInstantiatableFundamentalClass),
        (GBaseInitFunc) NULL,
        (GBaseFinalizeFunc) NULL,
        (GClassInitFunc) girtest_instantiatable_fundamental_class_init,
        (GClassFinalizeFunc) NULL,
        NULL,

        /* Instance */
        sizeof (GirTestInstantiatableFundamental),
        0,
        (GInstanceInitFunc) girtest_instantiatable_fundamental_init,

        /* GValue */
        &value_table,
      };

      GType type =
        g_type_register_fundamental (g_type_fundamental_next (),
                                     g_intern_static_string ("GirTestInstantiatableFundamental"),
                                     &info, &finfo,
                                     // G_TYPE_FLAG_NONE only available in gobject>=2.74
                                     0 /* G_TYPE_FLAG_NONE */);

      g_once_init_leave (&type_id, type);
    }
  return type_id;
}
