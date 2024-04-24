#pragma once

#include <glib-object.h>

G_BEGIN_DECLS

typedef struct _GirTestOpaqueUntypedRecordTester GirTestOpaqueUntypedRecordTester;

/**
 * GirTestCreateOpaqueUntypedRecordTesterNoOwnershipTransfer:
 *
 * Returns: (transfer none): a new OpaqueRecordTester.
 */
typedef GirTestOpaqueUntypedRecordTester* (*GirTestCreateOpaqueUntypedRecordTesterNoOwnershipTransfer) ();

/**
 * GirTestCreateOpaqueUntypedRecordTesterNoOwnershipTransferNullable:
 *
 * Returns: (transfer none) (nullable): a new OpaqueRecordTester or NULL.
 */
typedef GirTestOpaqueUntypedRecordTester* (*GirTestCreateOpaqueUntypedRecordTesterNoOwnershipTransferNullable) ();

/**
 * GirTestCreateOpaqueUntypedRecordTesterFullOwnershipTransfer:
 *
 * Returns: (transfer full): a new OpaqueUntypedRecordTester.
 */
typedef GirTestOpaqueUntypedRecordTester* (*GirTestCreateOpaqueUntypedRecordTesterFullOwnershipTransfer) ();

/**
 * GirTestCreateOpaqueUntypedRecordTesterFullOwnershipTransferNullable:
 *
 * Returns: (transfer full) (nullable): a new OpaqueUntypedRecordTester or NULL.
 */
typedef GirTestOpaqueUntypedRecordTester* (*GirTestCreateOpaqueUntypedRecordTesterFullOwnershipTransferNullable) ();

/**
 * GirTestGetOpaqueUntypedRecordTesterFullOwnershipTransfer:
 * @data: (transfer full): An OpaqueUntypedRecordTester
 */
typedef void (*GirTestGetOpaqueUntypedRecordTesterFullOwnershipTransfer) (GirTestOpaqueUntypedRecordTester *data);

/**
 * GirTestGetOpaqueUntypedRecordTesterFullOwnershipTransferNullable:
 * @data: (transfer full) (nullable): An OpaqueUntypedRecordTester
 */
typedef void (*GirTestGetOpaqueUntypedRecordTesterFullOwnershipTransferNullable) (GirTestOpaqueUntypedRecordTester *data);

/**
 * GirTestGetOpaqueUntypedRecordTesterNoOwnershipTransfer:
 * @data: (transfer none): An OpaqueUntypedRecordTester
 */
typedef void (*GirTestGetOpaqueUntypedRecordTesterNoOwnershipTransfer) (GirTestOpaqueUntypedRecordTester *data);

/**
 * GirTestGetOpaqueUntypedRecordTesterNoOwnershipTransferNullable:
 * @data: (transfer none) (nullable): An OpaqueUntypedRecordTester
 */
typedef void (*GirTestGetOpaqueUntypedRecordTesterNoOwnershipTransferNullable) (GirTestOpaqueUntypedRecordTester *data);

GirTestOpaqueUntypedRecordTester * girtest_opaque_untyped_record_tester_new ();
GirTestOpaqueUntypedRecordTester * girtest_opaque_untyped_record_tester_try_new (gboolean returnNull);
GirTestOpaqueUntypedRecordTester * girtest_opaque_untyped_record_tester_ref (GirTestOpaqueUntypedRecordTester *self);
GirTestOpaqueUntypedRecordTester * girtest_opaque_untyped_record_tester_try_ref (GirTestOpaqueUntypedRecordTester *self, gboolean returnNull);
GirTestOpaqueUntypedRecordTester * girtest_opaque_untyped_record_tester_mirror(GirTestOpaqueUntypedRecordTester *data);
GirTestOpaqueUntypedRecordTester * girtest_opaque_untyped_record_tester_nullable_mirror(GirTestOpaqueUntypedRecordTester *data, gboolean mirror);
void girtest_opaque_untyped_record_tester_unref(GirTestOpaqueUntypedRecordTester *self);
int girtest_opaque_untyped_record_tester_get_ref_count(GirTestOpaqueUntypedRecordTester *self);
int girtest_opaque_untyped_record_tester_try_get_ref_count(int dummy, GirTestOpaqueUntypedRecordTester *self);
void girtest_opaque_untyped_record_tester_take_and_unref(GirTestOpaqueUntypedRecordTester *self);
void girtest_opaque_untyped_record_tester_take_and_unref_func(int dummy, GirTestOpaqueUntypedRecordTester *data);
void girtest_opaque_untyped_record_tester_take_and_unref_func_nullable(int dummy, GirTestOpaqueUntypedRecordTester *data);
int girtest_opaque_untyped_record_tester_get_ref_count_sum(GirTestOpaqueUntypedRecordTester * const *data, gsize size);
int girtest_opaque_untyped_record_tester_get_ref_count_sum_nullable(GirTestOpaqueUntypedRecordTester * const *data, gsize size);
GirTestOpaqueUntypedRecordTester * girtest_opaque_untyped_record_tester_run_callback_return_no_ownership_transfer(GirTestCreateOpaqueUntypedRecordTesterNoOwnershipTransfer callback);
GirTestOpaqueUntypedRecordTester * girtest_opaque_untyped_record_tester_run_callback_return_no_ownership_transfer_nullable(GirTestCreateOpaqueUntypedRecordTesterNoOwnershipTransferNullable callback);
GirTestOpaqueUntypedRecordTester * girtest_opaque_untyped_record_tester_run_callback_return_full_ownership_transfer(GirTestCreateOpaqueUntypedRecordTesterFullOwnershipTransfer callback);
GirTestOpaqueUntypedRecordTester * girtest_opaque_untyped_record_tester_run_callback_return_full_ownership_transfer_nullable(GirTestCreateOpaqueUntypedRecordTesterFullOwnershipTransferNullable callback);
void girtest_opaque_untyped_record_tester_run_callback_parameter_full_ownership_transfer(GirTestGetOpaqueUntypedRecordTesterFullOwnershipTransfer callback);
void girtest_opaque_untyped_record_tester_run_callback_parameter_full_ownership_transfer_nullable(gboolean useNull, GirTestGetOpaqueUntypedRecordTesterFullOwnershipTransferNullable callback);
void girtest_opaque_untyped_record_tester_run_callback_parameter_no_ownership_transfer(GirTestGetOpaqueUntypedRecordTesterNoOwnershipTransfer callback, GirTestOpaqueUntypedRecordTester *data);
void girtest_opaque_untyped_record_tester_run_callback_parameter_no_ownership_transfer_nullable(GirTestGetOpaqueUntypedRecordTesterNoOwnershipTransferNullable callback, GirTestOpaqueUntypedRecordTester *data);
gboolean girtest_opaque_untyped_record_tester_equals(GirTestOpaqueUntypedRecordTester *self, GirTestOpaqueUntypedRecordTester *other);
G_END_DECLS
