#pragma once

#include <glib-object.h>

G_BEGIN_DECLS

typedef struct _GirTestOpaqueTypedRecordTester GirTestOpaqueTypedRecordTester;
#define GIRTEST_TYPE_OPAQUE_TYPED_RECORD_TESTER (girtest_opaque_typed_record_tester_get_type())

GType girtest_opaque_typed_record_tester_get_type (void) G_GNUC_CONST;

/**
 * GirTestCreateOpaqueTypedRecordTesterNoOwnershipTransfer:
 *
 * Returns: (transfer none): a new OpaqueRecordTester.
 */
typedef GirTestOpaqueTypedRecordTester* (*GirTestCreateOpaqueTypedRecordTesterNoOwnershipTransfer) ();

/**
 * GirTestCreateOpaqueTypedRecordTesterNoOwnershipTransferNullable:
 *
 * Returns: (transfer none) (nullable): a new OpaqueRecordTester or NULL.
 */
typedef GirTestOpaqueTypedRecordTester* (*GirTestCreateOpaqueTypedRecordTesterNoOwnershipTransferNullable) ();

/**
 * GirTestCreateOpaqueTypedRecordTesterFullOwnershipTransfer:
 *
 * Returns: (transfer full): a new OpaqueTypedRecordTester.
 */
typedef GirTestOpaqueTypedRecordTester* (*GirTestCreateOpaqueTypedRecordTesterFullOwnershipTransfer) ();

/**
 * GirTestCreateOpaqueTypedRecordTesterFullOwnershipTransferNullable:
 *
 * Returns: (transfer full) (nullable): a new OpaqueTypedRecordTester or NULL.
 */
typedef GirTestOpaqueTypedRecordTester* (*GirTestCreateOpaqueTypedRecordTesterFullOwnershipTransferNullable) ();

/**
 * GirTestGetOpaqueTypedRecordTesterFullOwnershipTransfer:
 * @data: (transfer full): An OpaqueTypedRecordTester
 */
typedef void (*GirTestGetOpaqueTypedRecordTesterFullOwnershipTransfer) (GirTestOpaqueTypedRecordTester *data);

/**
 * GirTestGetOpaqueTypedRecordTesterFullOwnershipTransferNullable:
 * @data: (transfer full) (nullable): An OpaqueTypedRecordTester
 */
typedef void (*GirTestGetOpaqueTypedRecordTesterFullOwnershipTransferNullable) (GirTestOpaqueTypedRecordTester *data);

/**
 * GirTestGetOpaqueTypedRecordTesterNoOwnershipTransfer:
 * @data: (transfer none): An OpaqueTypedRecordTester
 */
typedef void (*GirTestGetOpaqueTypedRecordTesterNoOwnershipTransfer) (GirTestOpaqueTypedRecordTester *data);

/**
 * GirTestGetOpaqueTypedRecordTesterNoOwnershipTransferNullable:
 * @data: (transfer none) (nullable): An OpaqueTypedRecordTester
 */
typedef void (*GirTestGetOpaqueTypedRecordTesterNoOwnershipTransferNullable) (GirTestOpaqueTypedRecordTester *data);

GirTestOpaqueTypedRecordTester * girtest_opaque_typed_record_tester_new ();
GirTestOpaqueTypedRecordTester * girtest_opaque_typed_record_tester_try_new (gboolean returnNull);
GirTestOpaqueTypedRecordTester * girtest_opaque_typed_record_tester_ref (GirTestOpaqueTypedRecordTester *self);
GirTestOpaqueTypedRecordTester * girtest_opaque_typed_record_tester_try_ref (GirTestOpaqueTypedRecordTester *self, gboolean returnNull);
GirTestOpaqueTypedRecordTester * girtest_opaque_typed_record_tester_mirror(GirTestOpaqueTypedRecordTester *data);
GirTestOpaqueTypedRecordTester * girtest_opaque_typed_record_tester_nullable_mirror(GirTestOpaqueTypedRecordTester *data, gboolean mirror);
void girtest_opaque_typed_record_tester_unref(GirTestOpaqueTypedRecordTester *self);
int girtest_opaque_typed_record_tester_get_ref_count(GirTestOpaqueTypedRecordTester *self);
int girtest_opaque_typed_record_tester_try_get_ref_count(int dummy, GirTestOpaqueTypedRecordTester *self);
void girtest_opaque_typed_record_tester_take_and_unref(GirTestOpaqueTypedRecordTester *self);
void girtest_opaque_typed_record_tester_take_and_unref_func(int dummy, GirTestOpaqueTypedRecordTester *data);
void girtest_opaque_typed_record_tester_take_and_unref_func_nullable(int dummy, GirTestOpaqueTypedRecordTester *data);
int girtest_opaque_typed_record_tester_get_ref_count_sum(GirTestOpaqueTypedRecordTester * const *data, gsize size);
int girtest_opaque_typed_record_tester_get_ref_count_sum_nullable(GirTestOpaqueTypedRecordTester * const *data, gsize size);
void girtest_opaque_typed_record_tester_out_parameter_no_ownership_transfer(GirTestOpaqueTypedRecordTester **data);
void girtest_opaque_typed_record_tester_out_parameter_no_ownership_transfer_nullable(GirTestOpaqueTypedRecordTester **data);
void girtest_opaque_typed_record_tester_out_parameter_full_ownership_transfer(GirTestOpaqueTypedRecordTester **data);
void girtest_opaque_typed_record_tester_out_parameter_full_ownership_transfer_nullable(GirTestOpaqueTypedRecordTester **data);
GirTestOpaqueTypedRecordTester * girtest_opaque_typed_record_tester_run_callback_return_no_ownership_transfer(GirTestCreateOpaqueTypedRecordTesterNoOwnershipTransfer callback);
GirTestOpaqueTypedRecordTester * girtest_opaque_typed_record_tester_run_callback_return_no_ownership_transfer_nullable(GirTestCreateOpaqueTypedRecordTesterNoOwnershipTransferNullable callback);
GirTestOpaqueTypedRecordTester * girtest_opaque_typed_record_tester_run_callback_return_full_ownership_transfer(GirTestCreateOpaqueTypedRecordTesterFullOwnershipTransfer callback);
GirTestOpaqueTypedRecordTester * girtest_opaque_typed_record_tester_run_callback_return_full_ownership_transfer_nullable(GirTestCreateOpaqueTypedRecordTesterFullOwnershipTransferNullable callback);
void girtest_opaque_typed_record_tester_run_callback_parameter_full_ownership_transfer(GirTestGetOpaqueTypedRecordTesterFullOwnershipTransfer callback);
void girtest_opaque_typed_record_tester_run_callback_parameter_full_ownership_transfer_nullable(gboolean useNull, GirTestGetOpaqueTypedRecordTesterFullOwnershipTransferNullable callback);
void girtest_opaque_typed_record_tester_run_callback_parameter_no_ownership_transfer(GirTestGetOpaqueTypedRecordTesterNoOwnershipTransfer callback, GirTestOpaqueTypedRecordTester *data);
void girtest_opaque_typed_record_tester_run_callback_parameter_no_ownership_transfer_nullable(GirTestGetOpaqueTypedRecordTesterNoOwnershipTransferNullable callback, GirTestOpaqueTypedRecordTester *data);
gboolean girtest_opaque_typed_record_tester_equals(GirTestOpaqueTypedRecordTester *self, GirTestOpaqueTypedRecordTester *other);
G_END_DECLS
