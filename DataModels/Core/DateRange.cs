using Repository.Core.Interfaces;

namespace Repository.Core;

public class DateRange : IRange<DateTime>
{
    public DateTime Start { get; private set; }
    public DateTime End { get; private set; }
    public DateRange(DateTime start, DateTime end)
    {
        Start = start;
        End = end;

    }

    public bool Includes(DateTime value)
    {
        return Start <= value && value <= End;
    }

    public bool Includes(IRange<DateTime> range)
    {
        return Start <= range.Start && range.End <= End; ;
    }
}
