#include "girtest-opaque-typed-record-tester.h"

/**
 * GirTestOpaqueTypedRecordTester:
 *
 * Just an opaque record.
 */
struct _GirTestOpaqueTypedRecordTester
{
    int ref_count;
};

G_DEFINE_BOXED_TYPE (GirTestOpaqueTypedRecordTester, girtest_opaque_typed_record_tester, girtest_opaque_typed_record_tester_ref, girtest_opaque_typed_record_tester_unref)

/**
 * girtest_opaque_typed_record_tester_new: (constructor)
 *
 * Returns: (transfer full): a new `GirTestOpaqueTypedRecordTester`
 **/
GirTestOpaqueTypedRecordTester *
girtest_opaque_typed_record_tester_new ()
{
    GirTestOpaqueTypedRecordTester *result;
    result = g_new0 (GirTestOpaqueTypedRecordTester, 1);
    result->ref_count = 1;
    return result;
}

/**
 * girtest_opaque_typed_record_tester_try_new:
 * @returnNull: TRUE to return null, FALSE to create a new instance.
 *
 * Returns: (transfer full) (nullable): a new `GirTestOpaqueTypedRecordTester` or NULL
 **/
GirTestOpaqueTypedRecordTester * 
girtest_opaque_typed_record_tester_try_new (gboolean returnNull)
{
	if(returnNull)
		return NULL;

	return girtest_opaque_typed_record_tester_new();
}

/**
 * girtest_opaque_typed_record_tester_ref:
 * @self: a `GirTestRecordTester`
 *
 * Increments the reference count on `data`.
 *
 * Returns: (transfer full): the data.
 **/
GirTestOpaqueTypedRecordTester *
girtest_opaque_typed_record_tester_ref (GirTestOpaqueTypedRecordTester *self)
{
    g_return_val_if_fail (self != NULL, NULL);
    self->ref_count += 1;
    return self;
}

/**
 * girtest_opaque_typed_record_tester_try_ref:
 * @self: a `GirTestRecordTester`
 * @returnNull: TRUE to return NULL, otherwise FALSE
 *
 * Increments the reference count on `data`.
 *
 * Returns: (transfer full) (nullable): the data or NULL
 **/
GirTestOpaqueTypedRecordTester * 
girtest_opaque_typed_record_tester_try_ref (GirTestOpaqueTypedRecordTester *self, gboolean returnNull)
{
	if(returnNull)
		return NULL;

	return girtest_opaque_typed_record_tester_ref(self);
}

/**
 * girtest_opaque_typed_record_tester_mirror:
 * @data: a `GirTestRecordTester`
 *
 * Mirrors the given data as the return value. Ownership is not transferred.
 *
 * Returns: (transfer none): the mirrored data.
 **/
GirTestOpaqueTypedRecordTester * 
girtest_opaque_typed_record_tester_mirror(GirTestOpaqueTypedRecordTester *data)
{
    return data;
}

/**
 * girtest_opaque_typed_record_tester_nullable_mirror:
 * @data: a `GirTestRecordTester`
 * @mirror: true to mirror data, false to return NULL
 *
 * Mirrors the given data as the return value if @mirror is true. Ownership is not transferred.
 *
 * Returns: (transfer none) (nullable): the mirrored data or NULL.
 **/
GirTestOpaqueTypedRecordTester * 
girtest_opaque_typed_record_tester_nullable_mirror(GirTestOpaqueTypedRecordTester *data, gboolean mirror)
{
    if(!mirror)
        return NULL;

    return data;
}

/**
 * girtrest_opaque_typed_record_tester_unref:
 * @self: (transfer full): a `GirTestOpaqueTypedRecordTester`
 *
 * Decrements the reference count on `data` and frees the
 * data if the reference count is 0.
 **/
void
girtest_opaque_typed_record_tester_unref (GirTestOpaqueTypedRecordTester *self)
{
    g_return_if_fail (self != NULL);

    self->ref_count -= 1;
    if (self->ref_count > 0)
        return;

    g_free (self);
}

/**
 * girtest_opaque_typed_record_tester_get_ref_count:
 * @self: a `GirTestOpaqueTypedRecordTester`
 *
 * Returns: The current ref count of the opaque record.
 **/
int
girtest_opaque_typed_record_tester_get_ref_count(GirTestOpaqueTypedRecordTester *self)
{
    g_return_val_if_fail (self != NULL, -1);
    return self->ref_count;
}

/**
 * girtest_opaque_typed_record_tester_try_get_ref_count:
 * @dummy: not used
 * @self: (nullable): a `GirTestOpaqueTypedRecordTester`
 *
 * Returns: The current ref count of the opaque record or -1 if @self is NULL
 **/
int girtest_opaque_typed_record_tester_try_get_ref_count(int dummy, GirTestOpaqueTypedRecordTester *self)
{
    if(self == NULL)
        return -1;

    return self->ref_count;
}

/**
 * girtest_opaque_typed_record_tester_take_and_unref:
 * @self: (transfer full): a `GirTestOpaqueTypedRecordTester`
 *
 * Takes ownership and decrements the reference count on `data` and frees the
 * data if the reference count is 0.
 **/
void
girtest_opaque_typed_record_tester_take_and_unref(GirTestOpaqueTypedRecordTester *self)
{
    girtest_opaque_typed_record_tester_unref(self);
}

/**
 * girtest_opaque_typed_record_tester_take_and_unref_func:
 * @dummy: Just an unused dummy value
 * @data: (transfer full): a `GirTestOpaqueTypedRecordTester`
 *
 * Takes ownership and decrements the reference count on `data` and frees the
 * data if the reference count is 0.
 **/
void
girtest_opaque_typed_record_tester_take_and_unref_func(int dummy, GirTestOpaqueTypedRecordTester *data)
{
    girtest_opaque_typed_record_tester_take_and_unref(data);
}

/**
 * girtest_opaque_typed_record_tester_take_and_unref_func_nullable:
 * @dummy: Just an unused dummy value
 * @data: (transfer full) (nullable): a `GirTestOpaqueTypedRecordTester`
 *
 * Takes ownership and decrements the reference count on `data` and frees the
 * data if the reference count is 0.
 **/
void
girtest_opaque_typed_record_tester_take_and_unref_func_nullable(int dummy, GirTestOpaqueTypedRecordTester *data)
{
    if(data == NULL)
        return;

    girtest_opaque_typed_record_tester_take_and_unref(data);
}

/**
 * girtest_opaque_typed_record_tester_get_ref_count_sum:
 * @data: (array length=size): an array of `GirTestOpaqueTypedRecordTester` pointers
 * @size: The length of @data
 *
 * Returns: The count of all refs of the @data.
 **/
int girtest_opaque_typed_record_tester_get_ref_count_sum(GirTestOpaqueTypedRecordTester * const *data, gsize size)
{
    int sum = 0;

    for (int i = 0; i < size; i++)
    {
        sum = sum + girtest_opaque_typed_record_tester_get_ref_count(data[i]);
    }

    return sum;
}

/**
 * girtest_opaque_typed_record_tester_get_ref_count_sum_nullable:
 * @data: (nullable) (array length=size): an array of `GirTestOpaqueTypedRecordTester` pointers
 * @size: The length of @data
 *
 * Returns: The count of all refs of the @data. -1 if NULL is supplied as @data.
 **/
int girtest_opaque_typed_record_tester_get_ref_count_sum_nullable(GirTestOpaqueTypedRecordTester * const *data, gsize size)
{
    if(data == NULL)
        return -1;

    return girtest_opaque_typed_record_tester_get_ref_count_sum(data, size);
}

/**
 * girtest_opaque_typed_record_tester_run_callback_return_no_ownership_transfer:
 * @callback: (scope call): a callback
 *
 * Calls the callback and returns the newly created instance.
 * 
 * Returns: (transfer none): a GirTestOpaqueTypedRecordTester
 **/
GirTestOpaqueTypedRecordTester *
girtest_opaque_typed_record_tester_run_callback_return_no_ownership_transfer(GirTestCreateOpaqueTypedRecordTesterNoOwnershipTransfer callback)
{ 
    return callback();
}

/**
 * girtest_opaque_typed_record_tester_run_callback_return_no_ownership_transfer_nullable:
 * @callback: (scope call): a callback
 *
 * Calls the callback and returns the newly created instance or NULL
 * 
 * Returns: (transfer none) (nullable): a GirTestOpaqueTypedRecordTester
 **/
GirTestOpaqueTypedRecordTester * girtest_opaque_typed_record_tester_run_callback_return_no_ownership_transfer_nullable(GirTestCreateOpaqueTypedRecordTesterNoOwnershipTransferNullable callback)
{
	return callback();
}

/**
 * girtest_opaque_typed_record_tester_run_callback_return_full_ownership_transfer:
 * @callback: (scope call): a callback
 *
 * Calls the callback and returns the newly created instance.
 * 
 * Returns: (transfer full): a GirTestOpaqueTypedRecordTester
 **/
GirTestOpaqueTypedRecordTester * girtest_opaque_typed_record_tester_run_callback_return_full_ownership_transfer(GirTestCreateOpaqueTypedRecordTesterFullOwnershipTransfer callback)
{
    return callback();
}

/**
 * girtest_opaque_typed_record_tester_run_callback_return_full_ownership_transfer_nullable:
 * @callback: (scope call): a callback
 *
 * Calls the callback and returns the newly created instance.
 * 
 * Returns: (transfer full) (nullable): a GirTestOpaqueTypedRecordTester or NULL
 **/
GirTestOpaqueTypedRecordTester * girtest_opaque_typed_record_tester_run_callback_return_full_ownership_transfer_nullable(GirTestCreateOpaqueTypedRecordTesterFullOwnershipTransferNullable callback)
{
	return callback();
}

/**
 * girtest_opaque_typed_record_tester_run_callback_parameter_full_ownership_transfer:
 * @callback: (scope call): a callback
 *
 * Calls the callback and supplies a new OpaqueTypedRecordTester.
 **/
void
girtest_opaque_typed_record_tester_run_callback_parameter_full_ownership_transfer(GirTestGetOpaqueTypedRecordTesterFullOwnershipTransfer callback)
{
    callback(girtest_opaque_typed_record_tester_new());
}

/**
 * girtest_opaque_typed_record_tester_run_callback_parameter_full_ownership_transfer_nullable:
 * @useNull: TRUE to pass null to the callback, otherwise FALSE.
 * @callback: (scope call): a callback
 *
 * Calls the callback and supplies a new OpaqueTypedRecordTester if @useNull is FALSE.
 **/
void girtest_opaque_typed_record_tester_run_callback_parameter_full_ownership_transfer_nullable(gboolean useNull, GirTestGetOpaqueTypedRecordTesterFullOwnershipTransferNullable callback)
{
    if(useNull)
        callback(NULL);
    else
        callback(girtest_opaque_typed_record_tester_new());
}

/**
 * girtest_opaque_typed_record_tester_run_callback_parameter_no_ownership_transfer:
 * @callback: (scope call): a callback
 * @data: (transfer none): A GirTestOpaqueTypedRecordTester
 *
 * Calls the callback and supplies the given OpaqueTypedRecordTester.
 **/
void
girtest_opaque_typed_record_tester_run_callback_parameter_no_ownership_transfer(GirTestGetOpaqueTypedRecordTesterNoOwnershipTransfer callback, GirTestOpaqueTypedRecordTester *data)
{
    callback(data);
}

/**
 * girtest_opaque_typed_record_tester_run_callback_parameter_no_ownership_transfer_nullable:
 * @callback: (scope call): a callback
 * @data: (transfer none) (nullable): A GirTestOpaqueTypedRecordTester
 *
 * Calls the callback and supplies the given OpaqueTypedRecordTester.
 **/
void
girtest_opaque_typed_record_tester_run_callback_parameter_no_ownership_transfer_nullable(GirTestGetOpaqueTypedRecordTesterNoOwnershipTransferNullable callback, GirTestOpaqueTypedRecordTester *data)
{
    callback(data);
}

/**
 * girtest_opaque_typed_record_tester_equals:
 * @self: instance
 * @other: other instance
 *
 * Compares two instances if they contain the same data.
 * 
 * Returns: If both instances have the same data.
 **/
gboolean
girtest_opaque_typed_record_tester_equals(GirTestOpaqueTypedRecordTester *self, GirTestOpaqueTypedRecordTester *other)
{
    return self->ref_count == other->ref_count;
}