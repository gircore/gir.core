#pragma once

#include <glib-object.h>

G_BEGIN_DECLS

/**
 * GirTestTypedRecordCopyAnnotationTester: 
 *  (copy-func girtest_typed_record_copy_annotation_tester_ref) 
 *  (free-func girtest_typed_record_copy_annotation_tester_unref)
 *
 * Just an record.
 */
struct _GirTestTypedRecordCopyAnnotationTester
{
    int ref_count;
};

typedef struct _GirTestTypedRecordCopyAnnotationTester GirTestTypedRecordCopyAnnotationTester;
#define GIRTEST_TYPE_TYPED_RECORD_COPY_ANNOTATION_TESTER (girtest_typed_record_copy_annotation_tester_get_type())

GType girtest_typed_record_copy_annotation_tester_get_type (void) G_GNUC_CONST;

/**
 * GirTestCreateTypedRecordCopyAnnotationTesterNoOwnershipTransfer:
 *
 * Returns: (transfer none): a new RecordTester.
 */
typedef GirTestTypedRecordCopyAnnotationTester* (*GirTestCreateTypedRecordCopyAnnotationTesterNoOwnershipTransfer) ();

/**
 * GirTestCreateTypedRecordCopyAnnotationTesterNoOwnershipTransferNullable:
 *
 * Returns: (transfer none) (nullable): a new RecordTester or NULL.
 */
typedef GirTestTypedRecordCopyAnnotationTester* (*GirTestCreateTypedRecordCopyAnnotationTesterNoOwnershipTransferNullable) ();

/**
 * GirTestGetTypedRecordCopyAnnotationTesterNoOwnershipTransfer:
 * @data: (transfer none): An TypedRecordTester
 */
typedef void (*GirTestGetTypedRecordCopyAnnotationTesterNoOwnershipTransfer) (GirTestTypedRecordCopyAnnotationTester *data);

/**
 * GirTestGetTypedRecordCopyAnnotationTesterNoOwnershipTransferNullable:
 * @data: (transfer none) (nullable): An TypedRecordTester
 */
typedef void (*GirTestGetTypedRecordCopyAnnotationTesterNoOwnershipTransferNullable) (GirTestTypedRecordCopyAnnotationTester *data);

GirTestTypedRecordCopyAnnotationTester * girtest_typed_record_copy_annotation_tester_new ();
GirTestTypedRecordCopyAnnotationTester * girtest_typed_record_copy_annotation_tester_ref (GirTestTypedRecordCopyAnnotationTester *self);
void girtest_typed_record_copy_annotation_tester_unref (GirTestTypedRecordCopyAnnotationTester *self);
GirTestTypedRecordCopyAnnotationTester * girtest_typed_record_copy_annotation_tester_mirror(GirTestTypedRecordCopyAnnotationTester *data);
GirTestTypedRecordCopyAnnotationTester * girtest_typed_record_copy_annotation_tester_nullable_mirror(GirTestTypedRecordCopyAnnotationTester *data, gboolean mirror);
int girtest_typed_record_copy_annotation_tester_get_ref_count(GirTestTypedRecordCopyAnnotationTester *self);
void girtest_typed_record_copy_annotation_tester_take_and_unref(GirTestTypedRecordCopyAnnotationTester *self);
void girtest_typed_record_copy_annotation_tester_take_and_unref_func(int dummy, GirTestTypedRecordCopyAnnotationTester *data);
void girtest_typed_record_copy_annotation_tester_take_and_unref_func_nullable(int dummy, GirTestTypedRecordCopyAnnotationTester *data);
GirTestTypedRecordCopyAnnotationTester * girtest_typed_record_copy_annotation_tester_run_callback_return_no_ownership_transfer(GirTestCreateTypedRecordCopyAnnotationTesterNoOwnershipTransfer callback);
GirTestTypedRecordCopyAnnotationTester * girtest_typed_record_copy_annotation_tester_run_callback_return_no_ownership_transfer_nullable(GirTestCreateTypedRecordCopyAnnotationTesterNoOwnershipTransferNullable callback);
void girtest_typed_record_copy_annotation_tester_run_callback_parameter_no_ownership_transfer(GirTestGetTypedRecordCopyAnnotationTesterNoOwnershipTransfer callback, GirTestTypedRecordCopyAnnotationTester *data);
void girtest_typed_record_copy_annotation_tester_run_callback_parameter_no_ownership_transfer_nullable(GirTestGetTypedRecordCopyAnnotationTesterNoOwnershipTransferNullable callback, GirTestTypedRecordCopyAnnotationTester *data);
G_END_DECLS
