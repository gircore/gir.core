#include "girtest-record-tester.h"

G_DEFINE_BOXED_TYPE (GirTestRecordTester, girtest_record_tester, girtest_record_tester_ref, girtest_record_tester_unref)

/**
 * girtest_record_tester_new: (constructor)
 *
 * Returns: (transfer full): a new `GirTestRecordTester`
 **/
GirTestRecordTester *
girtest_record_tester_new ()
{
    GirTestRecordTester *result;
    result = g_new0 (GirTestRecordTester, 1);
    result->ref_count = 1;
    return result;
}

/**
 * girtest_record_tester_ref:
 * @self: a `GirTestRecordTester`
 *
 * Increments the reference count on `data`.
 *
 * Returns: (transfer full): the data.
 **/
GirTestRecordTester *
girtest_record_tester_ref (GirTestRecordTester *self)
{
    g_return_val_if_fail (self != NULL, NULL);
    self->ref_count += 1;
    return self;
}

/**
 * girtrest_record_tester_unref:
 * @data: (transfer full): a `GirTestRecordTester`
 *
 * Decrements the reference count on `data` and frees the
 * data if the reference count is 0.
 **/
void
girtest_record_tester_unref (GirTestRecordTester *self)
{
    g_return_if_fail (self != NULL);

    self->ref_count -= 1;
    if (self->ref_count > 0)
        return;

    g_free (self);
}
