#include "girtest-typed-record-tester.h"

G_DEFINE_BOXED_TYPE (GirTestTypedRecordTester, girtest_typed_record_tester, girtest_typed_record_tester_ref, girtest_typed_record_tester_unref)

/**
 * girtest_typed_record_tester_new: (constructor)
 *
 * Returns: (transfer full): a new `GirTestTypedRecordTester`
 **/
GirTestTypedRecordTester *
girtest_typed_record_tester_new ()
{
    GirTestTypedRecordTester *result;
    result = g_new0 (GirTestTypedRecordTester, 1);
    result->ref_count = 1;
    result->custom_string = g_strdup("Hello");
    return result;
}

/**
 * girtest_typed_record_tester_try_new:
 * @returnNull: TRUE to return null, FALSE to create a new instance.
 *
 * Returns: (transfer full) (nullable): a new `GirTestTypedRecordTester` or NULL
 **/
GirTestTypedRecordTester * 
girtest_typed_record_tester_try_new (gboolean returnNull)
{
	if(returnNull)
		return NULL;

	return girtest_typed_record_tester_new();
}

/**
 * girtest_typed_record_tester_ref:
 * @self: a `GirTestRecordTester`
 *
 * Increments the reference count on `data`.
 *
 * Returns: (transfer full): the data.
 **/
GirTestTypedRecordTester *
girtest_typed_record_tester_ref (GirTestTypedRecordTester *self)
{
    g_return_val_if_fail (self != NULL, NULL);
    self->ref_count += 1;
    return self;
}

/**
 * girtest_typed_record_tester_try_ref:
 * @self: a `GirTestRecordTester`
 * @returnNull: TRUE to return NULL, otherwise FALSE
 *
 * Increments the reference count on `data`.
 *
 * Returns: (transfer full) (nullable): the data or NULL
 **/
GirTestTypedRecordTester * 
girtest_typed_record_tester_try_ref (GirTestTypedRecordTester *self, gboolean returnNull)
{
	if(returnNull)
		return NULL;

	return girtest_typed_record_tester_ref(self);
}

/**
 * girtest_typed_record_tester_mirror:
 * @data: a `GirTestRecordTester`
 *
 * Mirrors the given data as the return value. Ownership is not transferred.
 *
 * Returns: (transfer none): the mirrored data.
 **/
GirTestTypedRecordTester * 
girtest_typed_record_tester_mirror(GirTestTypedRecordTester *data)
{
    return data;
}

/**
 * girtest_typed_record_tester_nullable_mirror:
 * @data: a `GirTestRecordTester`
 * @mirror: true to mirror data, false to return NULL
 *
 * Mirrors the given data as the return value if @mirror is true. Ownership is not transferred.
 *
 * Returns: (transfer none) (nullable): the mirrored data or NULL.
 **/
GirTestTypedRecordTester * 
girtest_typed_record_tester_nullable_mirror(GirTestTypedRecordTester *data, gboolean mirror)
{
    if(!mirror)
        return NULL;

    return data;
}

/**
 * girtrest_typed_record_tester_unref:
 * @self: (transfer full): a `GirTestTypedRecordTester`
 *
 * Decrements the reference count on `data` and frees the
 * data if the reference count is 0.
 **/
void
girtest_typed_record_tester_unref (GirTestTypedRecordTester *self)
{
    g_return_if_fail (self != NULL);

    self->ref_count -= 1;
    if (self->ref_count > 0)
        return;

    g_free(self->custom_string);
    g_free (self);
}

/**
 * girtest_typed_record_tester_get_ref_count:
 * @self: a `GirTestTypedRecordTester`
 *
 * Returns: The current ref count of the record.
 **/
int
girtest_typed_record_tester_get_ref_count(GirTestTypedRecordTester *self)
{
    g_return_val_if_fail (self != NULL, -1);
    return self->ref_count;
}

/**
 * girtest_typed_record_tester_try_get_ref_count:
 * @dummy: not used
 * @self: (nullable): a `GirTestTypedRecordTester`
 *
 * Returns: The current ref count of the record or -1 if @self is NULL
 **/
int girtest_typed_record_tester_try_get_ref_count(int dummy, GirTestTypedRecordTester *self)
{
    if(self == NULL)
        return -1;

    return self->ref_count;
}

/**
 * girtest_typed_record_tester_take_and_unref:
 * @self: (transfer full): a `GirTestTypedRecordTester`
 *
 * Takes ownership and decrements the reference count on `data` and frees the
 * data if the reference count is 0.
 **/
void
girtest_typed_record_tester_take_and_unref(GirTestTypedRecordTester *self)
{
    girtest_typed_record_tester_unref(self);
}

/**
 * girtest_typed_record_tester_take_and_unref_func:
 * @dummy: Just an unused dummy value
 * @data: (transfer full): a `GirTestTypedRecordTester`
 *
 * Takes ownership and decrements the reference count on `data` and frees the
 * data if the reference count is 0.
 **/
void
girtest_typed_record_tester_take_and_unref_func(int dummy, GirTestTypedRecordTester *data)
{
    girtest_typed_record_tester_take_and_unref(data);
}

/**
 * girtest_typed_record_tester_take_and_unref_func_nullable:
 * @dummy: Just an unused dummy value
 * @data: (transfer full) (nullable): a `GirTestTypedRecordTester`
 *
 * Takes ownership and decrements the reference count on `data` and frees the
 * data if the reference count is 0.
 **/
void
girtest_typed_record_tester_take_and_unref_func_nullable(int dummy, GirTestTypedRecordTester *data)
{
    if(data == NULL)
        return;

    girtest_typed_record_tester_take_and_unref(data);
}

/**
 * girtest_typed_record_tester_get_ref_count_sum:
 * @data: (array length=size): an array of `GirTestTypedRecordTester` pointers
 * @size: The length of @data
 *
 * Returns: The count of all refs of the @data.
 **/
int girtest_typed_record_tester_get_ref_count_sum(GirTestTypedRecordTester * const *data, gsize size)
{
    int sum = 0;

    for (int i = 0; i < size; i++)
    {
        sum = sum + girtest_typed_record_tester_get_ref_count(data[i]);
    }

    return sum;
}

/**
 * girtest_typed_record_tester_get_ref_count_sum_nullable:
 * @data: (nullable) (array length=size): an array of `GirTestTypedRecordTester` pointers
 * @size: The length of @data
 *
 * Returns: The count of all refs of the @data. -1 if NULL is supplied as @data.
 **/
int girtest_typed_record_tester_get_ref_count_sum_nullable(GirTestTypedRecordTester * const *data, gsize size)
{
    if(data == NULL)
        return -1;

    return girtest_typed_record_tester_get_ref_count_sum(data, size);
}

/**
 * girtest_typed_record_tester_run_callback_return_no_ownership_transfer:
 * @callback: (scope call): a callback
 *
 * Calls the callback and returns the newly created instance.
 * 
 * Returns: (transfer none): a GirTestTypedRecordTester
 **/
GirTestTypedRecordTester *
girtest_typed_record_tester_run_callback_return_no_ownership_transfer(GirTestCreateTypedRecordTesterNoOwnershipTransfer callback)
{ 
    return callback();
}

/**
 * girtest_typed_record_tester_run_callback_return_no_ownership_transfer_nullable:
 * @callback: (scope call): a callback
 *
 * Calls the callback and returns the newly created instance or NULL
 * 
 * Returns: (transfer none) (nullable): a GirTestTypedRecordTester
 **/
GirTestTypedRecordTester * girtest_typed_record_tester_run_callback_return_no_ownership_transfer_nullable(GirTestCreateTypedRecordTesterNoOwnershipTransferNullable callback)
{
	return callback();
}

/**
 * girtest_typed_record_tester_run_callback_return_full_ownership_transfer:
 * @callback: (scope call): a callback
 *
 * Calls the callback and returns the newly created instance.
 * 
 * Returns: (transfer full): a GirTestTypedRecordTester
 **/
GirTestTypedRecordTester * girtest_typed_record_tester_run_callback_return_full_ownership_transfer(GirTestCreateTypedRecordTesterFullOwnershipTransfer callback)
{
    return callback();
}

/**
 * girtest_typed_record_tester_run_callback_return_full_ownership_transfer_nullable:
 * @callback: (scope call): a callback
 *
 * Calls the callback and returns the newly created instance.
 * 
 * Returns: (transfer full) (nullable): a GirTestTypedRecordTester or NULL
 **/
GirTestTypedRecordTester * girtest_typed_record_tester_run_callback_return_full_ownership_transfer_nullable(GirTestCreateTypedRecordTesterFullOwnershipTransferNullable callback)
{
	return callback();
}

/**
 * girtest_typed_record_tester_run_callback_parameter_full_ownership_transfer:
 * @callback: (scope call): a callback
 *
 * Calls the callback and supplies a new TypedRecordTester.
 **/
void
girtest_typed_record_tester_run_callback_parameter_full_ownership_transfer(GirTestGetTypedRecordTesterFullOwnershipTransfer callback)
{
    callback(girtest_typed_record_tester_new());
}

/**
 * girtest_typed_record_tester_run_callback_parameter_full_ownership_transfer_nullable:
 * @useNull: TRUE to pass null to the callback, otherwise FALSE.
 * @callback: (scope call): a callback
 *
 * Calls the callback and supplies a new TypedRecordTester if @useNull is FALSE.
 **/
void girtest_typed_record_tester_run_callback_parameter_full_ownership_transfer_nullable(gboolean useNull, GirTestGetTypedRecordTesterFullOwnershipTransferNullable callback)
{
    if(useNull)
        callback(NULL);
    else
        callback(girtest_typed_record_tester_new());
}

/**
 * girtest_typed_record_tester_run_callback_parameter_no_ownership_transfer:
 * @callback: (scope call): a callback
 * @data: (transfer none): A GirTestTypedRecordTester
 *
 * Calls the callback and supplies the given TypedRecordTester.
 **/
void
girtest_typed_record_tester_run_callback_parameter_no_ownership_transfer(GirTestGetTypedRecordTesterNoOwnershipTransfer callback, GirTestTypedRecordTester *data)
{
    callback(data);
}

/**
 * girtest_typed_record_tester_run_callback_parameter_no_ownership_transfer_nullable:
 * @callback: (scope call): a callback
 * @data: (transfer none) (nullable): A GirTestTypedRecordTester
 *
 * Calls the callback and supplies the given TypedRecordTester.
 **/
void
girtest_typed_record_tester_run_callback_parameter_no_ownership_transfer_nullable(GirTestGetTypedRecordTesterNoOwnershipTransferNullable callback, GirTestTypedRecordTester *data)
{
    callback(data);
}

/**
 * girtest_typed_record_tester_run_callback_create_nullable_full_ownership_transfer_out:
 * @callback: (scope call): a callback
 *
 * Calls the callback and returns the output parameter of the callback
 * 
 * Returns: (transfer full) (nullable): a GirTestTypedRecordTester
 **/
GirTestTypedRecordTester *  girtest_typed_record_tester_run_callback_create_nullable_full_ownership_transfer_out(GirTestCreateNullableTypedRecordTesterFullOwnershipTransferInCallback callback)
{
    GirTestTypedRecordTester **ptr = &((GirTestTypedRecordTester *){0});
    
    callback(ptr);

    return *ptr;
}

/**
 * girtest_typed_record_tester_equals:
 * @self: An instance
 * @other: Another instance
 *
 * Compare the data of the given instances.
 * 
 * Returns: if the given instances contain the same data.
 **/
gboolean girtest_typed_record_tester_equals(GirTestTypedRecordTester *self, GirTestTypedRecordTester *other)
{
    return self->ref_count == other->ref_count 
        && self->custom_enum == other->custom_enum 
        && self->custom_bitfield == other->custom_bitfield
        && g_strcmp0(self->custom_string, other->custom_string) == 0
        && self->custom_int_private == other->custom_int_private;
}