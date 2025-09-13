using System;
using AwesomeAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GLib.Tests;

[TestClass, TestCategory("UnitTest")]
public class SListTest : Test
{
    [TestMethod]
    public void CanEnumerateSList()
    {
        var valueArr = new IntPtr[] { 1234, -1234, 0 };

        // Create zeroed SList handle to pass to "Append"
        var slist = new SList(new Internal.SListOwnedHandle(IntPtr.Zero));
        foreach (var value in valueArr)
        {
            // First append creates a new SList
            slist = SList.Append(slist, value);
        }

        // Length should be 3 after creating a new SList by using "Append" three times
        SList.Length(slist).Should().Be((uint) valueArr.Length);

        var index = 0;
        foreach (SListElement element in slist)
        {
            // Validate pointer values
            element.Data.Should().Be(valueArr[index]);
            index++;
        }
    }

    [TestMethod]
    public void CanEnumerateSListString()
    {
        // Test enumerable for GLib.SList with strings stored as a pointer
        var valueArr = new Internal.NullableUtf8StringHandle[]
        {
            Internal.NullableUtf8StringOwnedHandle.Create("Hello World"),
            Internal.NullableUtf8StringOwnedHandle.Create(""),
            Internal.NullableUtf8StringOwnedHandle.Create(null)
        };

        var slist = new SList(new Internal.SListOwnedHandle(IntPtr.Zero));
        foreach (var value in valueArr)
        {
            slist = SList.Append(slist, value.DangerousGetHandle());
        }

        SList.Length(slist).Should().Be((uint) valueArr.Length);

        int index = 0;
        foreach (var value in slist.AsStringsUTF8())
        {
            value.Should().Be(valueArr[index].ConvertToString());
            index++;
        }
    }
}
