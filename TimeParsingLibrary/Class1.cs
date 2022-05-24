using System.Text.RegularExpressions;

namespace TimeParsingLibrary;

public static class Class1
{
    const int day = 86400;
    const int hour = 3600;
    const int minute = 60;

    private static Dictionary<int, int> daysInMonth = new Dictionary<int, int>()
    {
        {1, 31 },
        {2, 28 },
        {3, 31 },
        {4, 30 },
        {5, 31 },
        {6, 30 },
        {7, 31 },
        {8, 31 },
        {9, 30 },
        {10, 31 },
        {11, 30 },
        {12, 31 },
    };

    public static string Parse(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return "Invalid";
        }

        string valueToLower = value.ToLower();

        if (!valueToLower.StartsWith("now()"))
        {
            return "Invalid";
        }

        var y = 2022;
        var mon = 1;
        var d = 8;
        var h = 9;
        var m = 0;
        var s = 0;

        if (valueToLower == "now()")
        {
            return $"{y}-{mon:00}-{d:00}T{h:00}:{m:00}:{s:00}Z";
        }

        string valueToParse = valueToLower.Replace("now()", "");

        var tokens = Regex.Matches(valueToParse, @"\+[0-9]+[a-z]+|-[0-9]+[a-z]+|@[a-z]+");

        foreach (var token in tokens)
        {
            string tokenValue = token.ToString();

            var expression = tokenValue.Substring(0, 1);

            switch (expression)
            {
                case "+":
                    Add(tokenValue, ref y, ref mon, ref d, ref h, ref m, ref s);
                    break;
                case "-":
                    Subtract(tokenValue, ref y, ref mon, ref d, ref h, ref m, ref s);
                    break;
                case "@":
                    Snap(tokenValue, ref y, ref mon, ref d, ref h, ref m, ref s);
                    break;
            }
        }

        return $"{y}-{mon:00}-{d:00}T{h:00}:{m:00}:{s:00}Z";
    }

    private static void AddSeconds(decimal totalSeconds, ref int y, ref int mon, ref int d, ref int h, ref int m, ref int s)
    {
        var daysOut = Convert.ToInt32(Math.Floor(totalSeconds / day));
        var hoursOut = Math.Floor((totalSeconds - daysOut * day) / hour);
        var minutesOut = Math.Floor((totalSeconds - daysOut * day - hoursOut * hour) / minute);
        var secondsOut = totalSeconds - daysOut * day - hoursOut * hour - minutesOut * minute;

        if (s + secondsOut >= 60)
        {
            minutesOut++;
            s = (s + Convert.ToInt32(secondsOut)) % 60;
        }
        else
        {
            s += Convert.ToInt32(secondsOut);
        }

        if (m + minutesOut >= 60)
        {
            hoursOut++;
            m = (m + Convert.ToInt32(minutesOut)) % 60;
        }
        else
        {
            m += Convert.ToInt32(minutesOut);
        }

        if (h + hoursOut >= 24)
        {
            daysOut++;
            h = (h + Convert.ToInt32(hoursOut)) % 24;
        }
        else
        {
            h += Convert.ToInt32(hoursOut);
        }

        AddDays(ref daysOut, ref y, ref mon, ref d);
    }

    private static void AddDays(ref int daysOut, ref int y, ref int mon, ref int d)
    {
        while (daysOut > 0)
        {
            int daysInMonth = GetDaysInMonth(mon, y);

            if (d + daysOut >= daysInMonth)
            {
                var daysToRemove = daysInMonth - d;

                daysOut -= daysToRemove;

                // remove one day because we later make it the first of the month
                daysOut = daysOut - 1;

                if (mon == 12)
                {
                    // we have reached the end of the year
                    y++;
                    mon = 1;
                    d = 1;
                }
                else
                {
                    mon++;
                    d = 1;
                }
            }
            else
            {
                d += daysOut;
                daysOut = 0;
            }
        }
    }

    private static void SubtractSeconds(decimal totalSeconds, ref int y, ref int mon, ref int d, ref int h, ref int m, ref int s)
    {
        var daysOut = Convert.ToInt32(Math.Floor(totalSeconds / day));
        var hoursOut = Math.Floor((totalSeconds - daysOut * day) / hour);
        var minutesOut = Math.Floor((totalSeconds - daysOut * day - hoursOut * hour) / minute);
        var secondsOut = totalSeconds - daysOut * day - hoursOut * hour - minutesOut * minute;

        if (s - secondsOut < 0)
        {
            minutesOut++;
            s = (60 + s) - Convert.ToInt32(secondsOut);
        }
        else
        {
            s -= Convert.ToInt32(secondsOut);
        }

        if (m - minutesOut < 0)
        {
            hoursOut++;
            m = (60 + m) - Convert.ToInt32(minutesOut);
        }
        else
        {
            m -= Convert.ToInt32(minutesOut);
        }

        if (h - hoursOut < 0)
        {
            daysOut++;
            h = (24 + h) - Convert.ToInt32(hoursOut);
        }
        else
        {
            h -= Convert.ToInt32(hoursOut);
        }

        SubtractDays(ref daysOut, ref y, ref mon, ref d);
    }

    private static void SubtractDays(ref int daysOut, ref int y, ref int mon, ref int d)
    {
        while (daysOut > 0)
        {
            int daysInMonth = GetDaysInMonth(mon, y);

            if (d - daysOut < 1)
            {
                var daysToRemove = daysInMonth;

                if (d != 0)
                {
                    daysToRemove = d;
                }

                daysOut -= daysToRemove;

                if (mon == 1)
                {
                    // reached the start of the year
                    y--;
                    mon = 12;
                    d = GetDaysInMonth(mon, y);
                }
                else
                {
                    mon--;
                    d = GetDaysInMonth(mon, y);
                }
            }
            else
            {
                d -= daysOut;
                daysOut = 0;
            }
        }
    }

    private static int GetDaysInMonth(int mon, int y)
    {
        int daysInMonth = Class1.daysInMonth[mon];

        if (RequiresLeapYearAdjustment(mon, y))
        {
            daysInMonth++;
        }

        return daysInMonth;
    }

    private static bool RequiresLeapYearAdjustment(int mon, int y)
    {
        if (mon == 2 && y % 4 == 0)
        {
            // this is a leap year
            return true;
        }
        return false;
    }

    private static int SetDayOfMonth(int y, int mon, int d)
    {
        int daysInMonth = GetDaysInMonth(mon, y);
        if (d > daysInMonth)
        {
            d = daysInMonth;
        }
        return d;
    }

    private static void Add(string token, ref int y, ref int mon, ref int d, ref int h, ref int m, ref int s)
    {
        var unit = Regex.Match(token, @"\D+", RegexOptions.RightToLeft).Value;
        var value = int.Parse(Regex.Match(token, @"\d+").Value);

        switch (unit)
        {
            case "s":
                AddSeconds(value, ref y, ref mon, ref d, ref h, ref m, ref s);
                break;
            case "m":
                AddSeconds(value * minute, ref y, ref mon, ref d, ref h, ref m, ref s);
                break;
            case "h":
                AddSeconds(value * hour, ref y, ref mon, ref d, ref h, ref m, ref s);
                break;
            case "d":
                AddDays(ref value, ref y, ref mon, ref d);
                break;
            case "mon":

                var yearsToAdd = value / 12;
                y += yearsToAdd;

                var monthsToAdd = value % 12;
                if (mon + monthsToAdd > 12)
                {
                    y++;
                    mon = Math.Abs(mon - monthsToAdd);
                }
                else
                {
                    mon += monthsToAdd;
                }

                d = SetDayOfMonth(y, mon, d);
                break;
            case "y":
                y += value;
                d = SetDayOfMonth(y, mon, d);
                break;
        }
    }

    private static void Subtract(string token, ref int y, ref int mon, ref int d, ref int h, ref int m, ref int s)
    {
        var unit = Regex.Match(token, @"\D+", RegexOptions.RightToLeft).Value;
        var value = int.Parse(Regex.Match(token, @"\d+").Value);

        switch (unit)
        {
            case "s":
                SubtractSeconds(value, ref y, ref mon, ref d, ref h, ref m, ref s);
                break;
            case "m":
                SubtractSeconds(value * minute, ref y, ref mon, ref d, ref h, ref m, ref s);
                break;
            case "h":
                SubtractSeconds(value * hour, ref y, ref mon, ref d, ref h, ref m, ref s);
                break;
            case "d":
                SubtractDays(ref value, ref y, ref mon, ref d);
                break;
            case "mon":

                var yearsToSubtract = value / 12;
                y -= yearsToSubtract;

                var monthsToSubtract = value % 12;
                if (monthsToSubtract >= mon)
                {
                    y--;
                    mon = 12 - Math.Abs(mon - monthsToSubtract);
                    // if we move back a year
                    //mon++;
                }
                else
                {
                    mon -= monthsToSubtract;
                }

                d = SetDayOfMonth(y, mon, d);
                break;
            case "y":
                y -= value;
                d = SetDayOfMonth(y, mon, d);
                break;
        }
    }

    private static void Snap(string token, ref int y, ref int mon, ref int d, ref int h, ref int m, ref int s)
    {
        var unit = token.Substring(token.LastIndexOf('@') + 1);

        switch (unit)
        {
            case "s":
                break;
            case "m":
                s = 0;
                break;
            case "h":
                m = s = 0;
                break;
            case "d":
                h = m = s = 0;
                break;
            case "mon":
                d = 1;
                h = m = s = 0;
                break;
            case "y":
                mon = d = 1;
                h = m = s = 0;
                break;
        }
    }
}
