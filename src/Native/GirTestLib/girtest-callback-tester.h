#pragma once

#include <glib-object.h>
#include "data/girtest-executor.h"

G_BEGIN_DECLS

#define GIRTEST_TYPE_CALLBACK_TESTER girtest_callback_tester_get_type()

G_DECLARE_FINAL_TYPE(GirTestCallbackTester, girtest_callback_tester,
                     GIRTEST, CALLBACK_TESTER, GObject)

typedef int (*GirTestIntCallback) (int val);
typedef void (*GirTestCallbackWithCallback) (GirTestIntCallback callback);
typedef void (*GirTestIntPointerCallback) (int *val);
typedef GType (*GirTestTypeReturnCallback) ();
typedef GirTestExecutor* (*GirTestExecutorReturnCallback) ();

/**
 * GirTestOutPointerCallback:
 * @result: (out): the pointer to the result
 **/
typedef void (*GirTestOutPointerCallback) (gpointer *result);

/**
 * GirTestOutPointedPrimitiveValueTypeCallback:
 * @result: (out): the pointer to the integer value
 **/
typedef void (*GirTestOutPointedPrimitiveValueTypeCallback) (int *result);

/**
 * GirTestOutPointedPrimitiveValueTypeAliasCallback:
 * @type: the pointer to a GType
 **/
typedef void (*GirTestPointedPrimitiveValueTypeAliasCallback) (GType *type);

/**
 * GirTestThrowingCallback:
 * @error: return location for an error
 *
 * Can return an error
 */
typedef void (*GirTestCallbackError) (GError **error);

/**
 * GirTestCallbackErrorNotLastParam:
 * @error: (out): return location for an error
 * @dummyValue: Some integer to ensure the error not being in the last place
 *
 * Can return an error
 */
typedef void (* GirTestCallbackErrorNotLastParam) (GError **error, int dummyValue);

typedef void (*GirTestCallbackWithErrorCallback)  (GirTestCallbackError callback);
typedef void (*GirTestCallbackWithErrorCallbackNotLastParam)  (GirTestCallbackErrorNotLastParam callback);

/**
 * GirTestConstStringCallback:
 *
 * Returns: a string that is owned by the GirTest and must not be freed.
 */
typedef const gchar* (*GirTestConstStringCallback) ();

/**
 * GirTestNullableConstStringCallback:
 *
 * Returns: (Nullable): a string that is owned by the GirTest and must not be freed or NULL.
 */
typedef const gchar* (*GirTestNullableConstStringCallback) ();

/**
 * GirTestNullableClassParameterCallback:
 * @data: (nullable): An instance of GirTestCallbackTester or NULL.
 */
typedef const void (*GirTestNullableClassParameterCallback) (GirTestCallbackTester* data);

/**
 * GirTestTransferFullObjectReturnCallback:
 *
 * Returns: (transfer full): An object
 */
typedef GObject* (*GirTestTransferFullObjectReturnCallback) ();

/**
 * GirTestTransferNoneObjectReturnCallback:
 *
 * Returns: (transfer none): An object
 */
typedef GObject* (*GirTestTransferNoneObjectReturnCallback) ();

/**
 * GirTestTransferFullObjectParameterCallback:
 * @obj: (transfer full) (nullable): An object
 * 
 * Transfers obj to the callback.
 */
typedef void (*GirTestTransferFullObjectParameterCallback) (GObject* obj);

/**
 * GirTestTransferNoneObjectParameterCallback:
 * @obj: (transfer none) (nullable): An object
 * 
 * Does not transfer obj to the callback, the caller retains ownership.
 */
typedef void (*GirTestTransferNoneObjectParameterCallback) (GObject* obj);

/**
 * GirTestOutTransferFullObjectParameterCallback:
 * @obj: (out) (transfer full) (nullable): An object
 * 
 * Transfers obj to the callback.
 */
typedef void (*GirTestOutTransferFullObjectParameterCallback) (GObject** obj);

/**
 * GirTestOutTransferNoneObjectParameterCallback:
 * @obj: (out) (transfer none) (nullable): An object
 * 
 * Does not transfer obj to the callback, the caller retains ownership.
 */
typedef void (*GirTestOutTransferNoneObjectParameterCallback) (GObject** obj);

typedef enum {
    SIMPLE_ENUM_A = 1,
    SIMPLE_ENUM_B = 2,
    SIMPLE_ENUM_C = 3,
    SIMPLE_ENUM_MAX = 2147483647,
    SIMPLE_ENUM_MIN = -2147483648
} GirTestCallbackTesterSimpleEnum;

/**
 * GirTestEnumOutCallback:
 * @e: (out): return location for an enum
 */
typedef void (*GirTestEnumOutCallback) (GirTestCallbackTesterSimpleEnum *e);

/**
 * GirTestEnumRefCallback:
 * @e: (inout): input and return location for an enum
 */
typedef void (*GirTestEnumRefCallback) (GirTestCallbackTesterSimpleEnum *e);

GirTestCallbackTester*
girtest_callback_tester_new (void);

void
girtest_callback_tester_set_notified_callback(GirTestCallbackTester *tester,
                                              GirTestIntCallback callback,
                                              gpointer data,
                                              GDestroyNotify notify);

int
girtest_callback_tester_run_notified_callback(GirTestCallbackTester *tester,
                                              int value,
                                              gboolean done);

void
girtest_callback_tester_run_callback_with_mirror_input_callback(GirTestCallbackWithCallback callback);

void
girtest_callback_tester_run_callback_with_raise_error_callback(GirTestCallbackWithErrorCallback callback);

void
girtest_callback_tester_run_callback_with_raise_error_callback_not_last_param(GirTestCallbackWithErrorCallbackNotLastParam callback);

int
girtest_callback_tester_run_callback_return_int_pointer_value(GirTestIntPointerCallback callback);

gpointer
girtest_callback_tester_run_callback_out_pointer(GirTestOutPointerCallback callback);

void
girtest_callback_tester_run_callback_error(GirTestCallbackError callback, GError **error);

void
girtest_callback_tester_run_callback_error_not_last_param(GirTestCallbackErrorNotLastParam callback, GError **error);

GType
girtest_callback_tester_run_callback_with_type_return(GirTestTypeReturnCallback callback);

GObject*
girtest_callback_tester_run_callback_with_object_return(GirTestTransferFullObjectReturnCallback callback);

gboolean
girtest_callback_tester_run_callback_with_executor_interface_return(GirTestExecutorReturnCallback callback);

int
girtest_callback_tester_run_callback_with_out_pointed_primitive_value_type(GirTestOutPointedPrimitiveValueTypeCallback callback);

void
girtest_callback_tester_run_callback_with_pointed_primitive_value_type_alias(GirTestPointedPrimitiveValueTypeAliasCallback callback, GType* type);

const gchar*
girtest_callback_tester_run_callback_with_constant_string_return(GirTestConstStringCallback callback);

const gchar*
girtest_callback_tester_run_callback_with_nullable_constant_string_return(GirTestNullableConstStringCallback callback);

void
girtest_callback_tester_run_callback_with_nullable_class_parameter(GirTestNullableClassParameterCallback callback, GirTestCallbackTester* data); 

guint
girtest_callback_tester_run_callback_with_object_return_transfer_full(GirTestTransferFullObjectReturnCallback callback);

guint
girtest_callback_tester_run_callback_with_object_return_transfer_none(GirTestTransferNoneObjectReturnCallback callback);

GirTestCallbackTesterSimpleEnum
girtest_callback_tester_run_callback_enum_out(GirTestEnumOutCallback callback);

GirTestCallbackTesterSimpleEnum
girtest_callback_tester_run_callback_enum_ref(GirTestEnumRefCallback callback);

GObject*
girtest_callback_tester_roundtrip_object_get_instance(GirTestCallbackTester* instance); 

void
girtest_callback_tester_roundtrip_object_run_callback_return_transfer_full(GirTestCallbackTester* instance, GirTestTransferFullObjectReturnCallback callback); 

void
girtest_callback_tester_roundtrip_object_run_callback_parameter_transfer_full(GirTestCallbackTester* instance, GirTestTransferFullObjectParameterCallback callback); 

void
girtest_callback_tester_roundtrip_object_run_callback_return_transfer_none(GirTestCallbackTester* instance, GirTestTransferNoneObjectReturnCallback callback); 

void
girtest_callback_tester_roundtrip_object_run_callback_parameter_transfer_none(GirTestCallbackTester* instance, GirTestTransferNoneObjectParameterCallback callback); 

void
girtest_callback_tester_roundtrip_object_run_callback_out_transfer_full(GirTestCallbackTester* instance, GirTestOutTransferFullObjectParameterCallback callback); 

void
girtest_callback_tester_roundtrip_object_run_callback_out_transfer_none(GirTestCallbackTester* instance, GirTestOutTransferNoneObjectParameterCallback callback);

G_END_DECLS
