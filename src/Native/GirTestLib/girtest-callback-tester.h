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
typedef GObject* (*GirTestObjectReturnCallback) ();
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
girtest_callback_tester_run_callback_with_object_return(GirTestObjectReturnCallback callback);

gboolean
girtest_callback_tester_run_callback_with_executor_interface_return(GirTestExecutorReturnCallback callback);

int
girtest_callback_tester_run_callback_with_out_pointed_primitive_value_type(GirTestOutPointedPrimitiveValueTypeCallback callback);

void
girtest_callback_tester_run_callback_with_pointed_primitive_value_type_alias(GirTestPointedPrimitiveValueTypeAliasCallback callback, GType* type);

G_END_DECLS
