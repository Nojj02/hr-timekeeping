using System;
using FluentAssertions;
using Xunit;

namespace Hr.Timekeeping.UnitTests
{
    public class UnitTest1
    {
        [Fact]
        public void IsAbsent_NoActionTakenYet()
        {
            var dailyAttendance = new DailyAttendance();

            dailyAttendance.IsAbsent.Should().BeTrue();
        }

        [Fact]
        public void IsAbsent_HasTimeInEntry()
        {
            var dailyAttendance = new DailyAttendance();
            dailyAttendance.RegisterTimeIn(new DateTime(2020, 01, 01));

            dailyAttendance.IsAbsent.Should().BeTrue();
        }

        [Fact]
        public void IsNotAbsent_HasBothTimeInAndTimeOutEntry()
        {
            var dailyAttendance = new DailyAttendance();
            dailyAttendance.RegisterTimeIn(new DateTime(2020, 01, 01));
            dailyAttendance.RegisterTimeOut(new DateTime(2020, 01, 01));

            dailyAttendance.IsAbsent.Should().BeFalse();
        }
    }

    public class DailyAttendance
    {
        private DateTime? _timeIn;
        private DateTime? _timeOut;

        public DailyAttendance()
        {
        }

        public void RegisterTimeIn(DateTime dateTime)
        {
            _timeIn = dateTime;
        }

        public void RegisterTimeOut(DateTime dateTime)
        {
            _timeOut = dateTime;
        }

        public bool IsAbsent => !(_timeIn.HasValue && _timeOut.HasValue);
    }
}
