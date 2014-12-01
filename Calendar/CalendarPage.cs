using System;
using System.Collections.Generic;
using System.Linq;

namespace Calendar
{
    class CalendarPage
    {
        // публичные поля очень плохо использовать - лучше сделать свойства
        public const int DistributionByDayOfWeekMatrixWidth = 8;
        public DateTime Date { get; set; }
        public int[][] DistributionByDaysOfTheWeek { get; set; }
    
        // Tuple здесь использовать совершенно неуместно, с учетом еще того,
        // что он используется в другом классе, а публичный кортеж - вообще плохо
        // т.е. кортеж можно исползовать только в рамках одного класса и только приватный
        public Tuple<int, int> DayLocation;

        public CalendarPage(DateTime date)
        {
            Date = date;

            // DistributeByDaysOfTheWeek - очень непонятное название, надо как-то переделать
            var distribution = DistributeByDaysOfTheWeek(Date);
            DistributionByDaysOfTheWeek = InitWeekNumbers(distribution, Date);
            // тут тоже как то плохо DayLocation и т.п. - лучше для каждый день в таблице сделать как тип - типа
            // воскресный, будни, даже лучше сделать абстракцию - ячейка таблицы, чтоб отрисовщик вообще не знал,
            // что он страницу календаря отрисовывает, а просто некую таблицу, или заголовок и т.п.
            // можно сделать например, чтоб данные хранились как html документ - заголовки, таблицы и т.п.
            // а отрисовщик будет просто отрисовывать этот html документ - тут тоже можно сделать
            // абстрактный отрисовщик (или интерфейс) и конкретную реализацию (так можно будет легко сменить потом способ
            // отрисовки - например в виде построения не изображения, а прямо html файла)
            DayLocation = GetDayLocation(Date.Day);
        }

        // эти методы надо сделать приватными и не статическими, а при тестировании проверять не 
        // возвращаемые значения, а то, что получилось в соответствующих свойствах экземпляра класса
        public static List<int[]> DistributeByDaysOfTheWeek(DateTime date)
        {
            var daysInMonth = DateTime.DaysInMonth(date.Year, date.Month);
            var dayOfWeek = date.FirstDayOfMonth().DayOfWeek();
            var result = new List<int[]>();
            for (var dayOfMonth = 1; dayOfMonth <= daysInMonth; dayOfMonth++, dayOfWeek = dayOfWeek % 7 + 1)
            {
                if (result.Count == 0 || dayOfWeek == 1)
                    result.Add(new int[DistributionByDayOfWeekMatrixWidth]);
                result.Last()[dayOfWeek] = dayOfMonth;
            }
            return result;
        }

        public static int[][] InitWeekNumbers(List<int[]> arrayOfWeeks, DateTime date)
        {
            var weekOfYear = GetWeekOfYear(date.FirstDayOfMonth().DayOfYear);
            foreach (var week in arrayOfWeeks)
                week[0] = weekOfYear++;
            if (date.Month == 12 && new DateTime(date.Year, 12, 31).DayOfWeek != DayOfWeek.Sunday)
                arrayOfWeeks[arrayOfWeeks.Count - 1][0] = 1;
            return arrayOfWeeks.ToArray();
        }

        private Tuple<int, int> GetDayLocation(int day)
        {
            for (var i = 0; i < DistributionByDaysOfTheWeek.Length; i++)
                for (var j = 1; j < DistributionByDaysOfTheWeek[i].Length; j++)
                    if (DistributionByDaysOfTheWeek[i][j] == day)
                        return Tuple.Create(i, j);
            throw new Exception("Date not found in DistributionByDaysOfTheWeek Matrix");
        }

        public static int GetWeekOfYear(int dayOfYear)
        {
            return (dayOfYear - 1) / 7 + 1;
        }
    }
}