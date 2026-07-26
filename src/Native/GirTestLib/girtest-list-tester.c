#include "girtest-list-tester.h"

/**
 * GirTestListTester:
 *
 * Contains functions for testing bindings with GLib.List and GLib.SList
 * container types.
 */

struct _GirTestListTester
{
    GObject parent_instance;
};

G_DEFINE_TYPE(GirTestListTester, girtest_list_tester, G_TYPE_OBJECT)

static void
girtest_list_tester_init(GirTestListTester *value)
{
}

static void
girtest_list_tester_class_init(GirTestListTesterClass *class)
{
}

/* Static elements which are owned by this library. They are never freed
   so that unowned elements can be handed out safely. */
static GirTestOpaqueTypedRecordTester *static_records[2] = { NULL, NULL };
static GSList *static_record_list = NULL;
static GList *static_string_list = NULL;

static void
ensure_static_data()
{
    if (static_records[0] != NULL)
        return;

    static_records[0] = girtest_opaque_typed_record_tester_new();
    static_records[1] = girtest_opaque_typed_record_tester_new();

    static_record_list = g_slist_append(static_record_list, static_records[0]);
    static_record_list = g_slist_append(static_record_list, static_records[1]);

    static_string_list = g_list_append(static_string_list, "FOO");
    static_string_list = g_list_append(static_string_list, "BAR");
}

/**
 * girtest_list_tester_get_strings_transfer_full:
 *
 * Obtains a list of strings. The caller owns the list and the strings.
 *
 * Returns: (transfer full) (element-type utf8): A list of strings.
 **/
GList *
girtest_list_tester_get_strings_transfer_full()
{
    GList *list = NULL;

    list = g_list_append(list, g_strdup("FOO"));
    list = g_list_append(list, g_strdup("BAR"));

    return list;
}

/**
 * girtest_list_tester_get_strings_transfer_none:
 *
 * Obtains a list of strings which is owned by this library.
 *
 * Returns: (transfer none) (element-type utf8): A list of strings.
 **/
GList *
girtest_list_tester_get_strings_transfer_none()
{
    ensure_static_data();

    return static_string_list;
}

/**
 * girtest_list_tester_get_strings_transfer_full_empty:
 *
 * Obtains an empty list. An empty list is represented by a NULL pointer.
 *
 * Returns: (transfer full) (element-type utf8): An empty list.
 **/
GList *
girtest_list_tester_get_strings_transfer_full_empty()
{
    return NULL;
}

/**
 * girtest_list_tester_get_records_transfer_full:
 *
 * Obtains a list of newly created records. The caller owns the list and
 * the records, so the ref count of each record stays at one.
 *
 * Returns: (transfer full) (element-type GirTestOpaqueTypedRecordTester): A list of records.
 **/
GSList *
girtest_list_tester_get_records_transfer_full()
{
    GSList *list = NULL;

    list = g_slist_append(list, girtest_opaque_typed_record_tester_new());
    list = g_slist_append(list, girtest_opaque_typed_record_tester_new());

    return list;
}

/**
 * girtest_list_tester_get_records_transfer_container:
 *
 * Obtains a newly allocated list of records which are owned by this library.
 * The caller owns the list but must reference the records to keep them.
 *
 * Returns: (transfer container) (element-type GirTestOpaqueTypedRecordTester): A list of records.
 **/
GSList *
girtest_list_tester_get_records_transfer_container()
{
    GSList *list = NULL;

    ensure_static_data();

    list = g_slist_append(list, static_records[0]);
    list = g_slist_append(list, static_records[1]);

    return list;
}

/**
 * girtest_list_tester_get_records_transfer_none:
 *
 * Obtains a list of records which is owned by this library.
 *
 * Returns: (transfer none) (element-type GirTestOpaqueTypedRecordTester): A list of records.
 **/
GSList *
girtest_list_tester_get_records_transfer_none()
{
    ensure_static_data();

    return static_record_list;
}

/**
 * girtest_list_tester_get_static_record_ref_count:
 * @position: the position of the record
 *
 * Returns: The current ref count of the static record at @position.
 **/
int
girtest_list_tester_get_static_record_ref_count(int position)
{
    ensure_static_data();

    return girtest_opaque_typed_record_tester_get_ref_count(static_records[position]);
}
