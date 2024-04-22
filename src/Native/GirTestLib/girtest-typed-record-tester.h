#pragma once

#include <glib-object.h>

G_BEGIN_DECLS

/**
 * GirTestTypedRecordTesterEnum:
 * @A: 1
 * @B: 2
 *
 * Enum to test bindings.
 */
typedef enum {
    TYPED_RECORD_TESTER_ENUM_A = 0,
    TYPED_RECORD_TESTER_ENUM_B = 1
} GirTestTypedRecordTesterEnum;

/**
 * GirTestTypedRecordTesterBitfield:
 * @ZERO: No flags set.
 * @ONE: Set first flag.
 *
 * Flags to test bindings.
 */
typedef enum {
    TYPED_RECORD_TESTER_ZERO = 0,
    TYPED_RECORD_TESTER_ONE  = (1 << 0)
} GirTestTypedRecordTesterBitfield;

/**
 * GirTestTypedRecordTester:
 *
 * Just a record.
 */
struct _GirTestTypedRecordTester
{
    int ref_count;
    GirTestTypedRecordTesterEnum custom_enum;
    GirTestTypedRecordTesterBitfield custom_bitfield;
    gchar* custom_string;
    /* < private > */
    int custom_int_private;
};

typedef struct _GirTestTypedRecordTester GirTestTypedRecordTester;
#define GIRTEST_TYPE_TYPED_RECORD_TESTER (girtest_typed_record_tester_get_type())

GType girtest_typed_record_tester_get_type (void) G_GNUC_CONST;

/**
 * GirTestCreateTypedRecordTesterNoOwnershipTransfer:
 *
 * Returns: (transfer none): a new OpaqueRecordTester.
 */
typedef GirTestTypedRecordTester* (*GirTestCreateTypedRecordTesterNoOwnershipTransfer) ();

/**
 * GirTestCreateTypedRecordTesterNoOwnershipTransferNullable:
 *
 * Returns: (transfer none) (nullable): a new OpaqueRecordTester or NULL.
 */
typedef GirTestTypedRecordTester* (*GirTestCreateTypedRecordTesterNoOwnershipTransferNullable) ();

/**
 * GirTestCreateTypedRecordTesterFullOwnershipTransfer:
 *
 * Returns: (transfer full): a new TypedRecordTester.
 */
typedef GirTestTypedRecordTester* (*GirTestCreateTypedRecordTesterFullOwnershipTransfer) ();

/**
 * GirTestCreateTypedRecordTesterFullOwnershipTransferNullable:
 *
 * Returns: (transfer full) (nullable): a new TypedRecordTester or NULL.
 */
typedef GirTestTypedRecordTester* (*GirTestCreateTypedRecordTesterFullOwnershipTransferNullable) ();

/**
 * GirTestGetTypedRecordTesterFullOwnershipTransfer:
 * @data: (transfer full): An TypedRecordTester
 */
typedef void (*GirTestGetTypedRecordTesterFullOwnershipTransfer) (GirTestTypedRecordTester *data);

/**
 * GirTestGetTypedRecordTesterFullOwnershipTransferNullable:
 * @data: (transfer full) (nullable): An TypedRecordTester
 */
typedef void (*GirTestGetTypedRecordTesterFullOwnershipTransferNullable) (GirTestTypedRecordTester *data);

/**
 * GirTestGetTypedRecordTesterNoOwnershipTransfer:
 * @data: (transfer none): An TypedRecordTester
 */
typedef void (*GirTestGetTypedRecordTesterNoOwnershipTransfer) (GirTestTypedRecordTester *data);

/**
 * GirTestGetTypedRecordTesterNoOwnershipTransferNullable:
 * @data: (transfer none) (nullable): An TypedRecordTester
 */
typedef void (*GirTestGetTypedRecordTesterNoOwnershipTransferNullable) (GirTestTypedRecordTester *data);

/**
 * GirTestCreateNullableTypedRecordTesterFullOwnershipTransferInCallback:
 * @data: (transfer full) (nullable) (out): A TypedRecordTester
 */
typedef void (*GirTestCreateNullableTypedRecordTesterFullOwnershipTransferInCallback) (GirTestTypedRecordTester **data);

GirTestTypedRecordTester * girtest_typed_record_tester_new ();
GirTestTypedRecordTester * girtest_typed_record_tester_try_new (gboolean returnNull);
GirTestTypedRecordTester * girtest_typed_record_tester_ref (GirTestTypedRecordTester *self);
GirTestTypedRecordTester * girtest_typed_record_tester_try_ref (GirTestTypedRecordTester *self, gboolean returnNull);
GirTestTypedRecordTester * girtest_typed_record_tester_mirror(GirTestTypedRecordTester *data);
GirTestTypedRecordTester * girtest_typed_record_tester_nullable_mirror(GirTestTypedRecordTester *data, gboolean mirror);
void girtest_typed_record_tester_unref(GirTestTypedRecordTester *self);
int girtest_typed_record_tester_get_ref_count(GirTestTypedRecordTester *self);
int girtest_typed_record_tester_try_get_ref_count(int dummy, GirTestTypedRecordTester *self);
void girtest_typed_record_tester_take_and_unref(GirTestTypedRecordTester *self);
void girtest_typed_record_tester_take_and_unref_func(int dummy, GirTestTypedRecordTester *data);
void girtest_typed_record_tester_take_and_unref_func_nullable(int dummy, GirTestTypedRecordTester *data);
int girtest_typed_record_tester_get_ref_count_sum(GirTestTypedRecordTester * const *data, gsize size);
int girtest_typed_record_tester_get_ref_count_sum_nullable(GirTestTypedRecordTester * const *data, gsize size);
GirTestTypedRecordTester * girtest_typed_record_tester_run_callback_return_no_ownership_transfer(GirTestCreateTypedRecordTesterNoOwnershipTransfer callback);
GirTestTypedRecordTester * girtest_typed_record_tester_run_callback_return_no_ownership_transfer_nullable(GirTestCreateTypedRecordTesterNoOwnershipTransferNullable callback);
GirTestTypedRecordTester * girtest_typed_record_tester_run_callback_return_full_ownership_transfer(GirTestCreateTypedRecordTesterFullOwnershipTransfer callback);
GirTestTypedRecordTester * girtest_typed_record_tester_run_callback_return_full_ownership_transfer_nullable(GirTestCreateTypedRecordTesterFullOwnershipTransferNullable callback);
void girtest_typed_record_tester_run_callback_parameter_full_ownership_transfer(GirTestGetTypedRecordTesterFullOwnershipTransfer callback);
void girtest_typed_record_tester_run_callback_parameter_full_ownership_transfer_nullable(gboolean useNull, GirTestGetTypedRecordTesterFullOwnershipTransferNullable callback);
void girtest_typed_record_tester_run_callback_parameter_no_ownership_transfer(GirTestGetTypedRecordTesterNoOwnershipTransfer callback, GirTestTypedRecordTester *data);
void girtest_typed_record_tester_run_callback_parameter_no_ownership_transfer_nullable(GirTestGetTypedRecordTesterNoOwnershipTransferNullable callback, GirTestTypedRecordTester *data);
GirTestTypedRecordTester *  girtest_typed_record_tester_run_callback_create_nullable_full_ownership_transfer_out(GirTestCreateNullableTypedRecordTesterFullOwnershipTransferInCallback callback);
gboolean girtest_typed_record_tester_equals(GirTestTypedRecordTester *self, GirTestTypedRecordTester *other);
G_END_DECLS
