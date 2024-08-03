#include "girtest-typed-record-copy-annotation-tester.h"

G_DEFINE_BOXED_TYPE (GirTestTypedRecordCopyAnnotationTester, girtest_typed_record_copy_annotation_tester, girtest_typed_record_copy_annotation_tester_ref, girtest_typed_record_copy_annotation_tester_unref)

/**
 * girtest_typed_record_copy_annotation_tester_new: (constructor)
 *
 * Returns: (transfer full): a new `GirTestTypedRecordCopyAnnotationTester`
 **/
GirTestTypedRecordCopyAnnotationTester *
girtest_typed_record_copy_annotation_tester_new ()
{
    GirTestTypedRecordCopyAnnotationTester *result;
    result = g_new0 (GirTestTypedRecordCopyAnnotationTester, 1);
    result->ref_count = 1;
    return result;
}

/**
 * girtest_typed_record_copy_annotation_tester_ref:
 * @self: a `GirTestRecordTester`
 *
 * Increments the reference count on `data`.
 *
 * Returns: (transfer full): the data.
 **/
GirTestTypedRecordCopyAnnotationTester *
girtest_typed_record_copy_annotation_tester_ref (GirTestTypedRecordCopyAnnotationTester *self)
{
    g_return_val_if_fail (self != NULL, NULL);
    self->ref_count += 1;
    return self;
}

/**
 * girtest_typed_record_copy_annotation_tester_mirror:
 * @data: a `GirTestRecordTester`
 *
 * Mirrors the given data as the return value. Ownership is not transferred.
 *
 * Returns: (transfer none): the mirrored data.
 **/
GirTestTypedRecordCopyAnnotationTester * 
girtest_typed_record_copy_annotation_tester_mirror(GirTestTypedRecordCopyAnnotationTester *data)
{
    return data;
}

/**
 * girtest_typed_record_copy_annotation_tester_nullable_mirror:
 * @data: a `GirTestRecordTester`
 * @mirror: true to mirror data, false to return NULL
 *
 * Mirrors the given data as the return value if @mirror is true. Ownership is not transferred.
 *
 * Returns: (transfer none) (nullable): the mirrored data or NULL.
 **/
GirTestTypedRecordCopyAnnotationTester * 
girtest_typed_record_copy_annotation_tester_nullable_mirror(GirTestTypedRecordCopyAnnotationTester *data, gboolean mirror)
{
    if(!mirror)
        return NULL;

    return data;
}

/**
 * girtest_typed_record_copy_annotation_tester_unref:
 * @self: (transfer full): a `GirTestTypedRecordCopyAnnotationTester`
 *
 * Decrements the reference count on `data` and frees the
 * data if the reference count is 0.
 **/
void
girtest_typed_record_copy_annotation_tester_unref (GirTestTypedRecordCopyAnnotationTester *self)
{
    g_return_if_fail (self != NULL);

    self->ref_count -= 1;
    if (self->ref_count > 0)
        return;

    g_free (self);
}

/**
 * girtest_typed_record_copy_annotation_tester_get_ref_count:
 * @self: a `GirTestTypedRecordCopyAnnotationTester`
 *
 * Returns: The current ref count of the record.
 **/
int
girtest_typed_record_copy_annotation_tester_get_ref_count(GirTestTypedRecordCopyAnnotationTester *self)
{
    g_return_val_if_fail (self != NULL, -1);
    return self->ref_count;
}

/**
 * girtest_typed_record_copy_annotation_tester_take_and_unref:
 * @self: (transfer full): a `GirTestTypedRecordCopyAnnotationTester`
 *
 * Takes ownership and decrements the reference count on `data` and frees the
 * data if the reference count is 0.
 **/
void
girtest_typed_record_copy_annotation_tester_take_and_unref(GirTestTypedRecordCopyAnnotationTester *self)
{
    girtest_typed_record_copy_annotation_tester_unref(self);
}

/**
 * girtest_typed_record_copy_annotation_tester_take_and_unref_func:
 * @dummy: Just an unused dummy value
 * @data: (transfer full): a `GirTestTypedRecordCopyAnnotationTester`
 *
 * Takes ownership and decrements the reference count on `data` and frees the
 * data if the reference count is 0.
 **/
void
girtest_typed_record_copy_annotation_tester_take_and_unref_func(int dummy, GirTestTypedRecordCopyAnnotationTester *data)
{
    girtest_typed_record_copy_annotation_tester_take_and_unref(data);
}

/**
 * girtest_typed_record_copy_annotation_tester_take_and_unref_func_nullable:
 * @dummy: Just an unused dummy value
 * @data: (transfer full) (nullable): a `GirTestTypedRecordCopyAnnotationTester`
 *
 * Takes ownership and decrements the reference count on `data` and frees the
 * data if the reference count is 0.
 **/
void
girtest_typed_record_copy_annotation_tester_take_and_unref_func_nullable(int dummy, GirTestTypedRecordCopyAnnotationTester *data)
{
    if(data == NULL)
        return;

    girtest_typed_record_copy_annotation_tester_take_and_unref(data);
}

/**
 * girtest_typed_record_copy_annotation_tester_run_callback_return_no_ownership_transfer:
 * @callback: (scope call): a callback
 *
 * Calls the callback and returns the newly created instance.
 * 
 * Returns: (transfer none): a GirTestTypedRecordCopyAnnotationTester
 **/
GirTestTypedRecordCopyAnnotationTester *
girtest_typed_record_copy_annotation_tester_run_callback_return_no_ownership_transfer(GirTestCreateTypedRecordCopyAnnotationTesterNoOwnershipTransfer callback)
{ 
    return callback();
}

/**
 * girtest_typed_record_copy_annotation_tester_run_callback_return_no_ownership_transfer_nullable:
 * @callback: (scope call): a callback
 *
 * Calls the callback and returns the newly created instance or NULL
 * 
 * Returns: (transfer none) (nullable): a GirTestTypedRecordCopyAnnotationTester
 **/
GirTestTypedRecordCopyAnnotationTester * girtest_typed_record_copy_annotation_tester_run_callback_return_no_ownership_transfer_nullable(GirTestCreateTypedRecordCopyAnnotationTesterNoOwnershipTransferNullable callback)
{
	return callback();
}

/**
 * girtest_typed_record_copy_annotation_tester_run_callback_parameter_no_ownership_transfer:
 * @callback: (scope call): a callback
 * @data: (transfer none): A GirTestTypedRecordCopyAnnotationTester
 *
 * Calls the callback and supplies the given TypedRecordTester.
 **/
void
girtest_typed_record_copy_annotation_tester_run_callback_parameter_no_ownership_transfer(GirTestGetTypedRecordCopyAnnotationTesterNoOwnershipTransfer callback, GirTestTypedRecordCopyAnnotationTester *data)
{
    callback(data);
}

/**
 * girtest_typed_record_copy_annotation_tester_run_callback_parameter_no_ownership_transfer_nullable:
 * @callback: (scope call): a callback
 * @data: (transfer none) (nullable): A GirTestTypedRecordCopyAnnotationTester
 *
 * Calls the callback and supplies the given TypedRecordTester.
 **/
void
girtest_typed_record_copy_annotation_tester_run_callback_parameter_no_ownership_transfer_nullable(GirTestGetTypedRecordCopyAnnotationTesterNoOwnershipTransferNullable callback, GirTestTypedRecordCopyAnnotationTester *data)
{
    callback(data);
}