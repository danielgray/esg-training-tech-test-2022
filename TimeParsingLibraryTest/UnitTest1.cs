using TimeParsingLibrary;

namespace TimeParsingLibraryTest;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void AddOneDay()
    {
        string addOneDay = Class1.Parse("now()+1d");

        Assert.AreEqual(addOneDay, "2022-01-09T09:00:00Z");
    }

    [TestMethod]
    public void SubtractOneDay()
    {
        string subtractOneDay = Class1.Parse("now()-1d");

        Assert.AreEqual(subtractOneDay, "2022-01-07T09:00:00Z");
    }

    [TestMethod]
    public void SnappedToDay()
    {
        string snappedToDay = Class1.Parse("now()@d");

        Assert.AreEqual(snappedToDay, "2022-01-08T00:00:00Z");
    }

    [TestMethod]
    public void SubtractOneYearSnappedToMonth()
    {
        string subtractOneYearSnappedToMonth = Class1.Parse("now()-1y@mon");

        Assert.AreEqual(subtractOneYearSnappedToMonth, "2021-01-01T00:00:00Z");
    }

    [TestMethod]
    public void Add10DaysAdd12Hours()
    {
        string add10DaysAdd12Hours = Class1.Parse("now()+10d+12h");

        Assert.AreEqual(add10DaysAdd12Hours, "2022-01-18T21:00:00Z");
    }

    // ADD

    [TestMethod]
    public void AddYears()
    {
        string addYear = Class1.Parse("now()+100y");

        var addYearTestDate = new DateTime(2022, 1, 8, 9, 0, 0, DateTimeKind.Utc);

        var addYearTestResult = addYearTestDate.AddYears(100);

        Assert.AreEqual(addYear, addYearTestResult.ToString("yyyy-MM-ddTHH:mm:ssZ"));
    }

    [TestMethod]
    public void AddMonths()
    {
        string addMonth = Class1.Parse("now()+100mon");

        var addMonthTestDate = new DateTime(2022, 1, 8, 9, 0, 0, DateTimeKind.Utc);

        var addMonthTestResult = addMonthTestDate.AddMonths(100);

        Assert.AreEqual(addMonth, addMonthTestResult.ToString("yyyy-MM-ddTHH:mm:ssZ"));
    }

    [TestMethod]
    public void AddDays()
    {
        string addDays = Class1.Parse("now()+12345d");

        var addDaysTestDate = new DateTime(2022, 1, 8, 9, 0, 0, DateTimeKind.Utc);

        var addDaysTestResult = addDaysTestDate.AddDays(12345);

        Assert.AreEqual(addDays, addDaysTestResult.ToString("yyyy-MM-ddTHH:mm:ssZ"));
    }

    [TestMethod]
    public void AddHours()
    {
        string addHours = Class1.Parse("now()+12345h");

        var addHoursTestDate = new DateTime(2022, 1, 8, 9, 0, 0, DateTimeKind.Utc);

        var addHoursTestResult = addHoursTestDate.AddHours(12345);

        Assert.AreEqual(addHours, addHoursTestResult.ToString("yyyy-MM-ddTHH:mm:ssZ"));
    }

    [TestMethod]
    public void AddMinutes()
    {
        string addMinutes = Class1.Parse("now()+12345m");

        var addMinutesTestDate = new DateTime(2022, 1, 8, 9, 0, 0, DateTimeKind.Utc);

        var addMinutesTestResult = addMinutesTestDate.AddMinutes(12345);

        Assert.AreEqual(addMinutes, addMinutesTestResult.ToString("yyyy-MM-ddTHH:mm:ssZ"));
    }

    [TestMethod]
    public void AddSeconds()
    {
        string addSeconds = Class1.Parse("now()+123456789s");

        var addSecondsTestDate = new DateTime(2022, 1, 8, 9, 0, 0, DateTimeKind.Utc);

        var addSecondsTestResult = addSecondsTestDate.AddSeconds(123456789);

        Assert.AreEqual(addSeconds, addSecondsTestResult.ToString("yyyy-MM-ddTHH:mm:ssZ"));
    }

    // SUBTRACT

    [TestMethod]
    public void SubtractYears()
    {
        string subtractYear = Class1.Parse("now()-100y");

        var subtractYearTestDate = new DateTime(2022, 1, 8, 9, 0, 0, DateTimeKind.Utc);
        
        var subtractYearTestResult = subtractYearTestDate.AddYears(-100);

        Assert.AreEqual(subtractYear, subtractYearTestResult.ToString("yyyy-MM-ddTHH:mm:ssZ"));
    }

    [TestMethod]
    public void SubtractMonths()
    {
        string subtractMonth = Class1.Parse("now()-100mon");

        var subtractMonthTestDate = new DateTime(2022, 1, 8, 9, 0, 0, DateTimeKind.Utc);

        var subtractMonthTestResult = subtractMonthTestDate.AddMonths(-100);

        Assert.AreEqual(subtractMonth, subtractMonthTestResult.ToString("yyyy-MM-ddTHH:mm:ssZ"));
    }

    [TestMethod]
    public void SubtractDays()
    {
        string subtractDays = Class1.Parse("now()-12345d");

        var subtractDaysTestDate = new DateTime(2022, 1, 8, 9, 0, 0, DateTimeKind.Utc);

        var subtractDaysTestResult = subtractDaysTestDate.AddDays(-12345);

        Assert.AreEqual(subtractDays, subtractDaysTestResult.ToString("yyyy-MM-ddTHH:mm:ssZ"));
    }

    [TestMethod]
    public void SubtractHours()
    {
        string subtractHours = Class1.Parse("now()-12345h");

        var subtractHoursTestDate = new DateTime(2022, 1, 8, 9, 0, 0, DateTimeKind.Utc);

        var subtractHoursTestResult = subtractHoursTestDate.AddHours(-12345);

        Assert.AreEqual(subtractHours, subtractHoursTestResult.ToString("yyyy-MM-ddTHH:mm:ssZ"));
    }

    [TestMethod]
    public void SubtractMinutes()
    {
        string subtractMinutes = Class1.Parse("now()-12345m");

        var subtractMinutesTestDate = new DateTime(2022, 1, 8, 9, 0, 0, DateTimeKind.Utc);

        var subtractMinutesTestResult = subtractMinutesTestDate.AddMinutes(-12345);

        Assert.AreEqual(subtractMinutes, subtractMinutesTestResult.ToString("yyyy-MM-ddTHH:mm:ssZ"));
    }

    [TestMethod]
    public void SubtractSeconds()
    {
        string subtractSeconds = Class1.Parse("now()-123456789s");

        var subtractSecondsTestDate = new DateTime(2022, 1, 8, 9, 0, 0, DateTimeKind.Utc);

        var subtractSecondsTestResult = subtractSecondsTestDate.AddSeconds(-123456789);

        Assert.AreEqual(subtractSeconds, subtractSecondsTestResult.ToString("yyyy-MM-ddTHH:mm:ssZ"));
    }
}