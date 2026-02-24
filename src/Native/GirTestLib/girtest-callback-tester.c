#include "girtest-callback-tester.h"

/**
 * GirTestCallbackTester:
 *
 * Contains functions for testing bindings with callbacks.
 */

struct _GirTestCallbackTester
{
    GObject parent_instance;

    GirTestIntCallback notified_callback;
    gpointer notified_data;
    GDestroyNotify notified_destroy;
};

G_DEFINE_TYPE(GirTestCallbackTester, girtest_callback_tester, G_TYPE_OBJECT)

static void
girtest_callback_tester_init(GirTestCallbackTester *tester)
{
    tester->notified_callback = NULL;
    tester->notified_data = NULL;
    tester->notified_destroy = NULL;
}

static void
girtest_callback_tester_class_init(GirTestCallbackTesterClass *class)
{
}

/**
 * girtest_callback_tester_new:
 *
 * Creates a new `GirTestCallbackTester`.
 *
 * Returns: The newly created `GirTestCallbackTester`.
 */
GirTestCallbackTester*
girtest_callback_tester_new (void)
{
    return g_object_new (GIRTEST_TYPE_CALLBACK_TESTER, NULL);
}

/**
 * girtest_callback_tester_set_notified_callback:
 * @callback: (scope notified): a function to set as the notified callback.
 * @data: data to pass to @notify.
 * @notify: (nullable): function to call when the callback is removed, or %NULL.
 *
 * Assigns a callback with 'notified' scope, which can later be called with
 * girtest_callback_tester_run_notified_callback().
 */
void
girtest_callback_tester_set_notified_callback(GirTestCallbackTester *tester,
                                              GirTestIntCallback callback,
                                              gpointer data,
                                              GDestroyNotify notify)
{
    tester->notified_callback = callback;
    tester->notified_data = data;
    tester->notified_destroy = notify;
}

/**
 * girtest_callback_tester_run_notified_callback:
 * @value: value to pass to the callback.
 * @done: If true, runs the GDestroyNotify callback.
 *
 * Runs the notified callback with the provided value, or returns -1 if the
 * callback was not set or has been destroyed.
 */
int
girtest_callback_tester_run_notified_callback(GirTestCallbackTester *tester,
                                              int value,
                                              gboolean done)
{
    if (!tester->notified_callback)
        return -1;

    int result = tester->notified_callback(value);

    if (done)
    {
        if (tester->notified_destroy)
            tester->notified_destroy(tester->notified_data);

        tester->notified_callback = NULL;
        tester->notified_data = NULL;
        tester->notified_destroy = NULL;
    }

    return result;
}

int
mirror_input(int input)
{
    return input;
}

/**
 * girtest_callback_tester_run_callback_with_mirror_input_callback:
 * @callback: (scope call): a function that is called and provides another function
 *
 * Calls the callback and offers a functions which mirrors data.
 **/
void
girtest_callback_tester_run_callback_with_mirror_input_callback(GirTestCallbackWithCallback callback)
{
    callback(mirror_input);
}

/**
 * girtest_callback_tester_run_callback_return_int_pointer_value:
 * @callback: (scope call): a function that is called and allows to define an integer.
 *
 * Calls the callback and returns the data provided in the pointer parameter. The given value is initialized with 42.
 **/
int
girtest_callback_tester_run_callback_return_int_pointer_value(GirTestIntPointerCallback callback)
{
    int i = 42;
    callback(&i);

    return i;
}

/**
 * girtest_callback_tester_run_callback_out_pointer:
 * @callback: (scope call): a function that is called and allows to return a pointer.
 *
 * Calls the callback and returns the output pointer. 
 **/
gpointer
girtest_callback_tester_run_callback_out_pointer(GirTestOutPointerCallback callback)
{
    gpointer p = 0;

    callback(&p);

    return p;
}

/**
 * girtest_callback_tester_run_callback_error:
 * @callback: (scope call): a function that is called and allows to throw an error
 * @error: Return location for an error
 *
 * Calls the callback and can return an error.
 **/
void
girtest_callback_tester_run_callback_error(GirTestCallbackError callback, GError **error)
{
    callback(error);
}

void raise_error(GError **error)
{
    g_set_error_literal(error, 1, 1, "CustomError");
}

void raise_error_not_last_param(GError **error, int dummyValue)
{
    g_set_error_literal(error, 1, 1, "CustomError");
}

/**
 * girtest_callback_tester_run_callback_with_raise_error_callback:
 * @callback: (scope call): a function that is called and provides another function
 *
 * Calls the callback and offers a functions which raises an error.
 **/
void
girtest_callback_tester_run_callback_with_raise_error_callback(GirTestCallbackWithErrorCallback callback)
{
    callback(raise_error);
}

/**
 * girtest_callback_tester_run_callback_error_not_last_param:
 * @callback: (scope call): a function that is called and allows to throw an error
 * @error: Return location for an error
 *
 * Calls the callback and can return an error.
 **/
void
girtest_callback_tester_run_callback_error_not_last_param(GirTestCallbackErrorNotLastParam callback, GError **error)
{
    callback(error, 42);
}

/**
 * girtest_callback_tester_run_callback_with_raise_error_callback_not_last_param:
 * @callback: (scope call): a function that is called and provides another function
 *
 * Calls the callback and offers a functions which raises an error.
 **/
void
girtest_callback_tester_run_callback_with_raise_error_callback_not_last_param(GirTestCallbackWithErrorCallbackNotLastParam callback)
{
    callback(raise_error_not_last_param);
}

/**
 * girtest_callback_tester_run_callback_with_type_return:
 * @callback: (scope call): a function that is called and returns a type
 *
 * Calls the callback and returns it's result
 **/
GType
girtest_callback_tester_run_callback_with_type_return(GirTestTypeReturnCallback callback)
{
    return callback();
}

/**
 * girtest_callback_tester_run_callback_with_object_return:
 * @callback: (scope call): a function that is called and returns a new object
 *
 * Calls the callback and returns the new object
 * 
 * Returns: (transfer full): The new object
 **/
GObject*
girtest_callback_tester_run_callback_with_object_return(GirTestObjectReturnCallback callback)
{
    return callback();
}

/**
 * girtest_callback_tester_run_callback_with_executor_interface_return:
 * @callback: (scope call): a function that is called and returns an executor
 *
 * Calls the callback and returns if the given executor is valid
 * 
 * Returns: If the executor is valid
 **/
gboolean
girtest_callback_tester_run_callback_with_executor_interface_return(GirTestExecutorReturnCallback callback)
{
    GirTestExecutor* executor = callback();
    return executor != NULL;
}

/**
 * girtest_callback_tester_run_callback_with_out_pointed_primitive_value_type:
 * @callback: (scope call): a function that is called and outputs an integer
 *
 * Calls the callback and returns the value of the callbacks out parameter.
 * 
 * Returns: The out value of the callback
 **/
int
girtest_callback_tester_run_callback_with_out_pointed_primitive_value_type(GirTestOutPointedPrimitiveValueTypeCallback callback)
{
    int result;
    callback(&result);

    return result;
}

/**
 * girtest_callback_tester_run_callback_with_pointed_primitive_value_type_alias:
 * @callback: (scope call): a function that is called and receives a GType
 * @type: A GType that is passed into the callback
 *
 * Calls the callback and uses the given GType as argument.
 **/
void
girtest_callback_tester_run_callback_with_pointed_primitive_value_type_alias(GirTestPointedPrimitiveValueTypeAliasCallback callback, GType* type)
{
    callback(type);
}

/**
 * girtest_callback_tester_run_callback_with_constant_string_return:
 * @callback: (scope call): a function that is called and returns a constant string
 *
 * Calls the callback and returns it's return value.
 *
 * Returns: the result of the callback.
 **/
const gchar*
girtest_callback_tester_run_callback_with_constant_string_return(GirTestConstStringCallback callback)
{
    return callback();
}

/**
 * girtest_callback_tester_run_callback_with_nullable_constant_string_return:
 * @callback: (scope call): a function that is called and returns a constant string or NULL
 *
 * Calls the callback and returns it's return value.
 *
 * Returns: (nullable): the result of the callback.
 **/
const gchar*
girtest_callback_tester_run_callback_with_nullable_constant_string_return(GirTestNullableConstStringCallback callback)
{
    return callback();
}

/**
 * girtest_callback_tester_run_callback_with_nullable_class_parameter:
 * @callback: (scope call): a function that is called and passed on the @data parameter.
 * @data: (nullable): The instance to pass on.
 *
 * Calls the callback and passes on @data.
 **/
void
girtest_callback_tester_run_callback_with_nullable_class_parameter(GirTestNullableClassParameterCallback callback, GirTestCallbackTester* data)
{
    callback(data);
}

/**
 * girtest_callback_tester_run_callback_with_object_return_transfer_full:
 * @callback: (scope call): a function that is called and returns a new object
 *
 * Calls the callback and returns the ref count of the new object
 * 
 * Returns: the ref count of the fully transfered instance.
 **/
guint
girtest_callback_tester_run_callback_with_object_return_transfer_full(GirTestTransferFullObjectReturnCallback callback)
{
    GObject* obj = callback();
    guint count = obj->ref_count;
    g_object_unref(obj);
    return count;
}

/**
 * girtest_callback_tester_run_callback_with_object_return_transfer_none:
 * @callback: (scope call): a function that is called and returns a new object
 *
 * Calls the callback and returns the ref count of the new object
 * 
 * Returns: the ref count of transfered instance.
 **/
guint
girtest_callback_tester_run_callback_with_object_return_transfer_none(GirTestTransferNoneObjectReturnCallback callback)
{
    GObject* obj = callback();
    return obj->ref_count;
}

/**
 * girtest_callback_tester_run_callback_enum_out:
 * @callback: (scope call): a function that is called and has an enum out parameter.
 *
 * Calls the callback. 
 *
 * Returns: The enum value assigned to the out parameter by the callback `GirTestEnumOutCallback`.
 **/
GirTestCallbackTesterSimpleEnum
girtest_callback_tester_run_callback_enum_out(GirTestEnumOutCallback callback)
{
    GirTestCallbackTesterSimpleEnum e = 0;

    callback(&e);

    return e;
}

/**
 * girtest_callback_tester_run_callback_enum_ref:
 * @callback: (scope call): a function that is called and has an enum ref parameter.
 *
 * Calls the callback. 
 *
 * Returns: The enum value assigned to the ref parameter by the callback `GirTestEnumRefCallback`.
 **/
GirTestCallbackTesterSimpleEnum
girtest_callback_tester_run_callback_enum_ref(GirTestEnumRefCallback callback)
{
    GirTestCallbackTesterSimpleEnum e = SIMPLE_ENUM_A;

    callback(&e);

    return e;
}