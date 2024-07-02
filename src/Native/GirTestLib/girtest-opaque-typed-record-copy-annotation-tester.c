#include "girtest-opaque-typed-record-copy-annotation-tester.h"

/**
 * GirTestOpaqueTypedRecordCopyAnnotationTester: 
 *  (copy-func girtest_opaque_typed_record_copy_annotation_tester_ref) 
 *  (free-func girtest_opaque_typed_record_copy_annotation_tester_unref)
 *
 * Just an opaque record.
 */
struct _GirTestOpaqueTypedRecordCopyAnnotationTester
{
    int ref_count;
};

G_DEFINE_BOXED_TYPE (GirTestOpaqueTypedRecordCopyAnnotationTester, girtest_opaque_typed_record_copy_annotation_tester, girtest_opaque_typed_record_copy_annotation_tester_ref, girtest_opaque_typed_record_copy_annotation_tester_unref)

/**
 * girtest_opaque_typed_record_copy_annotation_tester_new: (constructor)
 *
 * Returns: (transfer full): a new `GirTestOpaqueTypedRecordCopyAnnotationTester`
 **/
GirTestOpaqueTypedRecordCopyAnnotationTester *
girtest_opaque_typed_record_copy_annotation_tester_new ()
{
    GirTestOpaqueTypedRecordCopyAnnotationTester *result;
    result = g_new0 (GirTestOpaqueTypedRecordCopyAnnotationTester, 1);
    result->ref_count = 1;
    return result;
}

/**
 * girtest_opaque_typed_record_copy_annotation_tester_ref:
 * @self: a `GirTestRecordTester`
 *
 * Increments the reference count on `data`.
 *
 * Returns: (transfer full): the data.
 **/
GirTestOpaqueTypedRecordCopyAnnotationTester *
girtest_opaque_typed_record_copy_annotation_tester_ref (GirTestOpaqueTypedRecordCopyAnnotationTester *self)
{
    g_return_val_if_fail (self != NULL, NULL);
    self->ref_count += 1;
    return self;
}

/**
 * girtest_opaque_typed_record_copy_annotation_tester_mirror:
 * @data: a `GirTestRecordTester`
 *
 * Mirrors the given data as the return value. Ownership is not transferred.
 *
 * Returns: (transfer none): the mirrored data.
 **/
GirTestOpaqueTypedRecordCopyAnnotationTester * 
girtest_opaque_typed_record_copy_annotation_tester_mirror(GirTestOpaqueTypedRecordCopyAnnotationTester *data)
{
    return data;
}

/**
 * girtest_opaque_typed_record_copy_annotation_tester_nullable_mirror:
 * @data: a `GirTestRecordTester`
 * @mirror: true to mirror data, false to return NULL
 *
 * Mirrors the given data as the return value if @mirror is true. Ownership is not transferred.
 *
 * Returns: (transfer none) (nullable): the mirrored data or NULL.
 **/
GirTestOpaqueTypedRecordCopyAnnotationTester * 
girtest_opaque_typed_record_copy_annotation_tester_nullable_mirror(GirTestOpaqueTypedRecordCopyAnnotationTester *data, gboolean mirror)
{
    if(!mirror)
        return NULL;

    return data;
}

/**
 * girtrest_opaque_typed_record_copy_annotation_tester_unref:
 * @self: (transfer full): a `GirTestOpaqueTypedRecordCopyAnnotationTester`
 *
 * Decrements the reference count on `data` and frees the
 * data if the reference count is 0.
 **/
void
girtest_opaque_typed_record_copy_annotation_tester_unref (GirTestOpaqueTypedRecordCopyAnnotationTester *self)
{
    g_return_if_fail (self != NULL);

    self->ref_count -= 1;
    if (self->ref_count > 0)
        return;

    g_free (self);
}

/**
 * girtest_opaque_typed_record_copy_annotation_tester_get_ref_count:
 * @self: a `GirTestOpaqueTypedRecordCopyAnnotationTester`
 *
 * Returns: The current ref count of the opaque record.
 **/
int
girtest_opaque_typed_record_copy_annotation_tester_get_ref_count(GirTestOpaqueTypedRecordCopyAnnotationTester *self)
{
    g_return_val_if_fail (self != NULL, -1);
    return self->ref_count;
}

/**
 * girtest_opaque_typed_record_copy_annotation_tester_take_and_unref:
 * @self: (transfer full): a `GirTestOpaqueTypedRecordCopyAnnotationTester`
 *
 * Takes ownership and decrements the reference count on `data` and frees the
 * data if the reference count is 0.
 **/
void
girtest_opaque_typed_record_copy_annotation_tester_take_and_unref(GirTestOpaqueTypedRecordCopyAnnotationTester *self)
{
    girtest_opaque_typed_record_copy_annotation_tester_unref(self);
}

/**
 * girtest_opaque_typed_record_copy_annotation_tester_take_and_unref_func:
 * @dummy: Just an unused dummy value
 * @data: (transfer full): a `GirTestOpaqueTypedRecordCopyAnnotationTester`
 *
 * Takes ownership and decrements the reference count on `data` and frees the
 * data if the reference count is 0.
 **/
void
girtest_opaque_typed_record_copy_annotation_tester_take_and_unref_func(int dummy, GirTestOpaqueTypedRecordCopyAnnotationTester *data)
{
    girtest_opaque_typed_record_copy_annotation_tester_take_and_unref(data);
}

/**
 * girtest_opaque_typed_record_copy_annotation_tester_take_and_unref_func_nullable:
 * @dummy: Just an unused dummy value
 * @data: (transfer full) (nullable): a `GirTestOpaqueTypedRecordCopyAnnotationTester`
 *
 * Takes ownership and decrements the reference count on `data` and frees the
 * data if the reference count is 0.
 **/
void
girtest_opaque_typed_record_copy_annotation_tester_take_and_unref_func_nullable(int dummy, GirTestOpaqueTypedRecordCopyAnnotationTester *data)
{
    if(data == NULL)
        return;

    girtest_opaque_typed_record_copy_annotation_tester_take_and_unref(data);
}

/**
 * girtest_opaque_typed_record_copy_annotation_tester_run_callback_return_no_ownership_transfer:
 * @callback: (scope call): a callback
 *
 * Calls the callback and returns the newly created instance.
 * 
 * Returns: (transfer none): a GirTestOpaqueTypedRecordCopyAnnotationTester
 **/
GirTestOpaqueTypedRecordCopyAnnotationTester *
girtest_opaque_typed_record_copy_annotation_tester_run_callback_return_no_ownership_transfer(GirTestCreateOpaqueTypedRecordCopyAnnotationTesterNoOwnershipTransfer callback)
{ 
    return callback();
}

/**
 * girtest_opaque_typed_record_copy_annotation_tester_run_callback_return_no_ownership_transfer_nullable:
 * @callback: (scope call): a callback
 *
 * Calls the callback and returns the newly created instance or NULL
 * 
 * Returns: (transfer none) (nullable): a GirTestOpaqueTypedRecordCopyAnnotationTester
 **/
GirTestOpaqueTypedRecordCopyAnnotationTester * girtest_opaque_typed_record_copy_annotation_tester_run_callback_return_no_ownership_transfer_nullable(GirTestCreateOpaqueTypedRecordCopyAnnotationTesterNoOwnershipTransferNullable callback)
{
	return callback();
}

/**
 * girtest_opaque_typed_record_copy_annotation_tester_run_callback_parameter_no_ownership_transfer:
 * @callback: (scope call): a callback
 * @data: (transfer none): A GirTestOpaqueTypedRecordCopyAnnotationTester
 *
 * Calls the callback and supplies the given OpaqueTypedRecordTester.
 **/
void
girtest_opaque_typed_record_copy_annotation_tester_run_callback_parameter_no_ownership_transfer(GirTestGetOpaqueTypedRecordCopyAnnotationTesterNoOwnershipTransfer callback, GirTestOpaqueTypedRecordCopyAnnotationTester *data)
{
    callback(data);
}

/**
 * girtest_opaque_typed_record_copy_annotation_tester_run_callback_parameter_no_ownership_transfer_nullable:
 * @callback: (scope call): a callback
 * @data: (transfer none) (nullable): A GirTestOpaqueTypedRecordCopyAnnotationTester
 *
 * Calls the callback and supplies the given OpaqueTypedRecordTester.
 **/
void
girtest_opaque_typed_record_copy_annotation_tester_run_callback_parameter_no_ownership_transfer_nullable(GirTestGetOpaqueTypedRecordCopyAnnotationTesterNoOwnershipTransferNullable callback, GirTestOpaqueTypedRecordCopyAnnotationTester *data)
{
    callback(data);
}