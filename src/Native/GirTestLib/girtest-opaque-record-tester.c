#include "girtest-opaque-record-tester.h"

/**
 * GirTestOpaqueRecordTester:
 *
 * Just an opaque record.
 */
struct _GirTestOpaqueRecordTester
{
    int ref_count;
};

G_DEFINE_BOXED_TYPE (GirTestOpaqueRecordTester, girtest_opaque_record_tester, girtest_opaque_record_tester_ref, girtest_opaque_record_tester_unref)

/**
 * girtest_opaque_record_tester_new: (constructor)
 *
 * Returns: (transfer full): a new `GirTestOpaqueRecordTester`
 **/
GirTestOpaqueRecordTester *
girtest_opaque_record_tester_new ()
{
    GirTestOpaqueRecordTester *result;
    result = g_new0 (GirTestOpaqueRecordTester, 1);
    result->ref_count = 1;
    return result;
}

/**
 * girtest_opaque_record_tester_ref:
 * @self: a `GirTestRecordTester`
 *
 * Increments the reference count on `data`.
 *
 * Returns: (transfer full): the data.
 **/
GirTestOpaqueRecordTester *
girtest_opaque_record_tester_ref (GirTestOpaqueRecordTester *self)
{
    g_return_val_if_fail (self != NULL, NULL);
    self->ref_count += 1;
    return self;
}

/**
 * girtrest_opaque_record_tester_unref:
 * @self: (transfer full): a `GirTestOpaqueRecordTester`
 *
 * Decrements the reference count on `data` and frees the
 * data if the reference count is 0.
 **/
void
girtest_opaque_record_tester_unref (GirTestOpaqueRecordTester *self)
{
    g_return_if_fail (self != NULL);

    self->ref_count -= 1;
    if (self->ref_count > 0)
        return;

    g_free (self);
}
