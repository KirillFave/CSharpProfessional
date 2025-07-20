namespace ApplicationHelper;

public static class DateTimeHelper
{
    public static int GetYearDayNumber(this DateTime dateTime)
    {
        return (dateTime - dateTime.GetYearFirstDay()).Days + 1;
    }

    public static DateTime GetYearFirstDay(this DateTime dateTime)
    {
        return new DateTime(dateTime.Year, 1, 1);
    }
}
