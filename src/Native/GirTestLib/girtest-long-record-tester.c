#include "girtest-long-record-tester.h"

/**
 * GirTestLongRecordTester:
 *
 * Test records with long fields
 */

gsize girtest_long_record_tester_get_sizeof_l(GirTestLongRecordTester* record)
{
    return sizeof(record->l);
}

gsize girtest_long_record_tester_get_sizeof_ul(GirTestLongRecordTester* record)
{
    return sizeof(record->ul);
}