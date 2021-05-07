using System;
using Xunit;
using ChazuraProgram.Models;


namespace XUnitChazuraTesting
{
    public class DateTimeExt_Test
    {
        [Fact]
        public void ToDate_testingValidInput()
        {
            DateTime date = DateTime.Now;
            string dateString = date.GetDashDate();

            DateTime convertedToDate = dateString.ToDate();

            Assert.IsType<DateTime>(convertedToDate);
        }
        [Fact]
        public void ToDate_testingNotValidInput()
        {

            string dateString = "uy";

            DateTime convertedToDate = dateString.ToDate();

            Assert.IsType<DateTime>(convertedToDate);
        }
    }
}
