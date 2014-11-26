using System;
using System.Linq;

namespace Calendar
{
    class ZodiacalSign
    {
        public static readonly Tuple<int, int, string>[] DateRangeToZodiacalSignMap =
        {
            Tuple.Create(321, 420, "Овен"),
            Tuple.Create(421, 521, "Телец"),
            Tuple.Create(522, 621, "Близнецы"),
            Tuple.Create(622, 722, "Рак"),
            Tuple.Create(723, 823, "Лев"),
            Tuple.Create(824, 923, "Дева"),
            Tuple.Create(924, 1023, "Весы"),
            Tuple.Create(1024, 1122, "Скорпион"),
            Tuple.Create(1123, 1221, "Стрелец"),
            Tuple.Create(1222, 1231, "Козерог"),
            Tuple.Create(101, 120, "Козерог"),
            Tuple.Create(121, 219, "Водолей"),
            Tuple.Create(220, 320, "Рыбы")
        };

        public static string GetZodiacalSign(DateTime date)
        {
            var mmddDateFormat = int.Parse(date.Month + date.Day.ToString().PadLeft(2, '0'));
            return DateRangeToZodiacalSignMap
                .First(entry => entry.Item1 <= mmddDateFormat && mmddDateFormat <= entry.Item2)
                .Item3;
        }
    }
}
