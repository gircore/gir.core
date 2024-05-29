#include "girtest-long-tester.h"

/**
 * GirTestLongTester:
 *
 * Test records with long fields
 */

gsize girtest_long_tester_get_sizeof_l(GirTestLongTester* record)
{
    return sizeof(record->l);
}

gsize girtest_long_tester_get_sizeof_ul(GirTestLongTester* record)
{
    return sizeof(record->ul);
}