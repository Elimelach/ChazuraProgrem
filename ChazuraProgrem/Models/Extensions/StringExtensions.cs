using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChazuraProgram.Models
{
    public static class StringExtensions
    {
        public static string GetDashDateString(this string date)
        {
            return date.Replace('/', '-');
        }
        public static string GetDashString(this string statment)
        {
            return statment.Trim().Replace(' ', '-');
        }
        public static string DashToSpace(this string statment)
        {
            return statment.Replace('-', ' ').Trim();
        }
        public static bool EqualsNoCase(this string s, string tocompare) =>
           s?.ToLower() == tocompare?.ToLower();
    }
}
