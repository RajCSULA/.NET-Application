using Nager.Date;
using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    public class Program
    {
        public static void Main(string[] args)
        {

            Console.Write("Enter StartDate : ");
            var startdate = DateTime.Parse(Console.ReadLine());
            // Console.Write("Enter enddate : ");
            // var enddate = DateTime.Parse(Console.ReadLine());
            Console.Write("Enter noOfProcessingDates : ");
            var noOfProcessingDates = Convert.ToInt32(Console.ReadLine());
            var r = new Calendar();
            // var totalDays = r.CalculateBusinessDays(startdate, enddate);
            //var totalDayswhileLoop = r.GetWorkingDays(startdate, enddate);

            // var date = r.ConverDateTimeToPT(startdate);
            var getEndDate = r.GetEndDate(startdate, noOfProcessingDates);
            // var getEndDate2 = r.GetEndDate2(startdate, noOfProcessingDates);
            // Console.WriteLine("The number of Business days are " + totalDays);
            Console.WriteLine("EndDate is " + getEndDate);
            Console.ReadLine();
        }

    }
    public class Calendar
    {
        public int CalculateBusinessDays(DateTime startDate, DateTime enDate)
        {
            var totalDays = 0;
            for (var date = startDate; date <= enDate; date = date.AddDays(1))
            {
                if (date.DayOfWeek != DayOfWeek.Saturday
                    && date.DayOfWeek != DayOfWeek.Sunday
                    && !IsAHoliday(date))
                    totalDays++;
            }

            return totalDays;
        }

        public DateTime GetEndDate(DateTime startDate, int noOfProcessingDays)
        {
            int count = 0;
            var cutoffTime = DateTime.Parse("16:00:00");
            //var timezone = TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time");
            //var dateTime = TimeZoneInfo.ConvertTime(startDate, timezone);

            bool isNextDay = startDate.Hour >= cutoffTime.Hour && startDate.Minute >= cutoffTime.Minute && startDate.Second >= cutoffTime.Second;

            if (isNextDay)
            {
                startDate = startDate.AddDays(1);
            }

            //int count = 0;
            for (var i = 1; i <= noOfProcessingDays; i++)
            {
                var date = startDate.AddDays(i);
                if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday || hday(date))
                {
                    count++;
                }
            }
            var endDate = startDate.AddDays(noOfProcessingDays + count);

            while (endDate.DayOfWeek == DayOfWeek.Saturday || endDate.DayOfWeek == DayOfWeek.Sunday || hday(endDate))
            {
                endDate = endDate.AddDays(1);
            }
            return endDate;
        }

        public DateTime GetEndDateV2(DateTime startDate, int noOfProcessingDays)
        {
            int count = 0;
            for (var i = 1; i <= noOfProcessingDays; i++)
            {
                var date = startDate.AddDays(i);
                if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday || IsAHoliday(date))
                {
                    count++;
                }
            }
            var endDate = startDate.AddDays(noOfProcessingDays + count);

            while (endDate.DayOfWeek == DayOfWeek.Saturday || endDate.DayOfWeek == DayOfWeek.Sunday || IsAHoliday(endDate))
            {
                endDate = endDate.AddDays(1);
            }
            return endDate;
        }


        public DateTime ConvertDateTimeToPt(DateTime date)
        {
            var timezone = TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time");
            var dateTime = TimeZoneInfo.ConvertTime(DateTime.Now, timezone);

            return dateTime;
        }

        public bool IsAHoliday(DateTime date)
        {
            var bankHolidays = GetHolidays(date.Year);
            return (bankHolidays.ContainsValue(date.ToString("yyyy/MM/dd")));
        }

        public static bool hday(DateTime date)
        {

            if (DateSystem.IsPublicHoliday(date, CountryCode.US))
            {
                return true;
            }
            return false;
        }
        public static Dictionary<string, string> GetHolidays(int year)
        {
            Dictionary<string, string> holidays = new Dictionary<string, string>
            {
                {"NEW_YEARS", GetNewYearsDay(year)},
                {"MLK_DAY", GetMLKDay(year)},
                {"PRESIDENTS_DAY", GetPresidentsDay(year)},
                {"MEMORIAL_DAY", GetMemorialDay(year)},
                {"INDEPENDENCE_DAY", GetIndependenceDay(year)},
                {"LABOR_DAY", getLaborDay(year)},
                {"COLUMBUS_DAY", GetColumbusDay(year)},
                {"VETERANS_DAY", GetVetsDay(year)},
                {"THANKSGIVING", GetThanksGivingDay(year)},
                {"CHRISTMAS_DAY", GetChristmasDay(year)}
            };

            return holidays;
        }

        private static string GetNewYearsDay(int year)
        {
            DateTime nyDate = new DateTime(year, 1, 1);
            if (nyDate.DayOfWeek == DayOfWeek.Sunday)
                return nyDate.AddDays(1).ToString("yyyy/MM/dd");
            else
                return nyDate.ToString("yyyy/MM/dd");
        }

        private static string GetMLKDay(int year)
        {
            DateTime mlkDate = new DateTime(year, 1, 15); // Earliest Date that MLK day can be.
            while (mlkDate.DayOfWeek != DayOfWeek.Monday)
            {
                mlkDate = mlkDate.AddDays(1);
            }
            return mlkDate.ToString("yyyy/MM/dd");
        }

        private static string GetPresidentsDay(int year)
        {
            DateTime potusDate = new DateTime(year, 2, 19); // Latest Date that President's day can be.

            while (potusDate.DayOfWeek != DayOfWeek.Monday)
            {
                potusDate = potusDate.AddDays(1);
            }
            return potusDate.ToString("yyyy/MM/dd");
        }

        private static string GetMemorialDay(int year)
        {
            DateTime memDate = new DateTime(year, 5, 28); // Latest Date that Memorial day can be.
            while (memDate.DayOfWeek != DayOfWeek.Monday)
            {
                memDate = memDate.AddDays(-1);
            }
            return memDate.ToString("yyyy/MM/dd");
        }

        private static string GetIndependenceDay(int year)
        {
            DateTime indDate = new DateTime(year, 7, 4);
            if (indDate.DayOfWeek == DayOfWeek.Sunday)
                return indDate.AddDays(1).ToString("yyyy/MM/dd");
            else
                return indDate.ToString("yyyy/MM/dd");
        }

        private static string getLaborDay(int year)
        {
            DateTime laborDate = new DateTime(year, 9, 3); // Latest Date that labor day can be.
            while (laborDate.DayOfWeek != DayOfWeek.Monday)
            {
                laborDate = laborDate.AddDays(1);
            }
            return laborDate.ToString("yyyy/MM/dd");
        }

        private static string GetColumbusDay(int year)
        {
            DateTime colDate = new DateTime(year, 10, 8); // Earliest Date that Columbus day can be.
            while (colDate.DayOfWeek != DayOfWeek.Monday)
            {
                colDate = colDate.AddDays(1);
            }
            return colDate.ToString("yyyy/MM/dd");
        }

        private static string GetVetsDay(int year)
        {
            DateTime vetsDate = new DateTime(year, 11, 11);
            if (vetsDate.DayOfWeek == DayOfWeek.Sunday)
                return vetsDate.AddDays(1).ToString("yyyy/MM/dd");
            else
                return vetsDate.ToString("yyyy/MM/dd");
        }

        private static string GetThanksGivingDay(int year)
        {
            DateTime colDate = new DateTime(year, 11, 22); // Earliest Date that Thanksgiving day can be.
            while (colDate.DayOfWeek != DayOfWeek.Thursday)
            {
                colDate = colDate.AddDays(1);
            }
            return colDate.ToString("yyyy/MM/dd");
        }


        private static string GetChristmasDay(int year)
        {
            DateTime xmasDate = new DateTime(year, 12, 25);
            if (xmasDate.DayOfWeek == DayOfWeek.Sunday)
                return xmasDate.AddDays(1).ToString("yyyy/MM/dd");
            else
                return xmasDate.ToString("yyyy/MM/dd");
        }


        public int GetWorkingDays(DateTime startDate, DateTime enDate)
        {
            var totalDays = 0;
            while (startDate <= enDate)
            {
                if (startDate.DayOfWeek != DayOfWeek.Saturday
                    && startDate.DayOfWeek != DayOfWeek.Sunday
                    && !IsAHoliday(startDate))
                    totalDays++;
                startDate = startDate.AddDays(1);

            }
            return totalDays;
        }

        public DateTime GetEndDate2(DateTime startDate, int noOfProcessingDays)
        {
            int count = 0;
            int i = 1;
            while (i <= noOfProcessingDays)
            {
                var date = startDate.AddDays(i);
                if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday || IsAHoliday(date))
                {
                    count++;
                }
                i++;
            }
            var endDate = startDate.AddDays(noOfProcessingDays + count);

            while (endDate.DayOfWeek == DayOfWeek.Saturday || endDate.DayOfWeek == DayOfWeek.Sunday || IsAHoliday(endDate))
            {
                endDate = endDate.AddDays(1);
            }
            return endDate;
        }

        /// <summary>
        /// Determines if this date is a federal holiday.
        /// </summary>
        /// <param name="date">This date</param>
        /// <returns>True if this date is a federal holiday</returns>
        public static bool IsFederalHoliday(DateTime date)
        {
            // to ease typing
            var nthWeekDay = (int)(Math.Ceiling((double)date.Day / 7.0d));
            var dayOfWeek = date.DayOfWeek;
            var isThursday = dayOfWeek == DayOfWeek.Thursday;
            var isFriday = dayOfWeek == DayOfWeek.Friday;
            var isMonday = dayOfWeek == DayOfWeek.Monday;
            var isWeekend = dayOfWeek == DayOfWeek.Saturday || dayOfWeek == DayOfWeek.Sunday;

            // New Years Day (Jan 1, or preceding Friday/following Monday if weekend)
            if ((date.Month == 12 && date.Day == 31 && isFriday) ||
                (date.Month == 1 && date.Day == 1 && !isWeekend) ||
                (date.Month == 1 && date.Day == 2 && isMonday)) return true;

            // MLK day (3rd monday in January)
            if (date.Month == 1 && isMonday && nthWeekDay == 3) return true;

            // President’s Day (3rd Monday in February)
            if (date.Month == 2 && isMonday && nthWeekDay == 3) return true;

            // Memorial Day (Last Monday in May)
            if (date.Month == 5 && isMonday && date.AddDays(7).Month == 6) return true;

            // Independence Day (July 4, or preceding Friday/following Monday if weekend)
            if ((date.Month == 7 && date.Day == 3 && isFriday) ||
                (date.Month == 7 && date.Day == 4 && !isWeekend) ||
                (date.Month == 7 && date.Day == 5 && isMonday)) return true;

            // Labor Day (1st Monday in September)
            if (date.Month == 9 && isMonday && nthWeekDay == 1) return true;

            // Columbus Day (2nd Monday in October)
            if (date.Month == 10 && isMonday && nthWeekDay == 2) return true;

            // Veteran’s Day (November 11, or preceding Friday/following Monday if weekend))
            if ((date.Month == 11 && date.Day == 10 && isFriday) ||
                (date.Month == 11 && date.Day == 11 && !isWeekend) ||
                (date.Month == 11 && date.Day == 12 && isMonday)) return true;

            // Thanksgiving Day (4th Thursday in November)
            if (date.Month == 11 && isThursday && nthWeekDay == 4) return true;

            // Christmas Day (December 25, or preceding Friday/following Monday if weekend))
            if ((date.Month == 12 && date.Day == 24 && isFriday) ||
                (date.Month == 12 && date.Day == 25 && !isWeekend) ||
                (date.Month == 12 && date.Day == 26 && isMonday)) return true;

            return false;
        }

    }


    //uncomment below to convert an image to base64string 

    //public class Program
    // {
    //    public static void Main(string[] args)
    //    {
    //        string ImagePath1 = "C:\\MRDCChecks\\checks\\2009235_1.TIFF";
    //        string ImagePath2 = "C:\\MRDCChecks\\checks\\Image.GIF";
    //        byte[] imageFileData = null;

    //         using (FileStream fsSource = new FileStream(ImagePath1, FileMode.Open, FileAccess.Read))
    //        {
    //            // Read the source file into a byte array.
    //            imageFileData = new byte[fsSource.Length];
    //            fsSource.Read(imageFileData, 0, imageFileData.Length);
    //            fsSource.Close();
    //        }

    //        var image = Convert.ToBase64String(imageFileData);

    //         var utility= new ImageUtility();

    //        var result = utility.ToGif(Convert.FromBase64String(image));

    //        using (FileStream sourceStream = new FileStream(ImagePath2, FileMode.Append, FileAccess.Write, FileShare.None,
    //            4096, true))
    //        {
    //            sourceStream.Seek(0, SeekOrigin.End);
    //             sourceStream.WriteAsync(result, 0, result.Length);
    //        }

    //     }
    // }
}
