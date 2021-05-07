using System;
using System.Collections.Generic;

namespace ChazuraProgram.Models
{
    public static class AlaphBaisConverter
    {
        private static readonly Dictionary<int, string> DafimVsNumbers = new Dictionary<int, string>
        {
            {1,"א"}, {2,"ב"},{3,"ג"},{4,"ד"},{5,"ה"},{6,"ו"},{7,"ז"},{8,"ח"},{9,"ט"},
            {10,"י"},{20,"כ"},{30,"ל"},{40,"מ"},{50,"נ"},{60,"ס"},{70,"ע"},
            {80,"פ"},{90,"צ"},{100,"ק"},{200,"ר"},{300,"ש"},{400,"ת"},{500,"תק"},{600,"תר"},{700,"תש"},
            {800,"תת"},{900,"תתק"},{1000,"א'"},{2000,"ב'"},{3000,"ג'"},{4000,"ד'"},{5000,"ה'"},{6000,"ו'"},
            {7000,"ז'"},{8000,"ח'"},{9000,"ט'"}
        };

        /// <summary>
        /// Converts an integer to אב
        /// </summary>
        /// <param name="n">integer between 1-9999</param>
        /// <returns>אב string </returns>
        public static string ConvertToAlaphBaith(int n)
        {
            if (n < 1 || n > 9999)
            {
                throw new ArgumentOutOfRangeException("n", "This method can only accept numbers in the range 1-9999.");
            }
            string daf = "";
            Stack<char> dafNumStack = new Stack<char>(3);
            foreach (var item in n.ToString()) dafNumStack.Push(item);

            int iTimes = 1;
            foreach (var item in dafNumStack)
            {
                if (item == '0')
                {
                    iTimes *= 10;
                    continue;
                }
                daf = DafimVsNumbers[Convert.ToInt32(item.ToString()) * iTimes] + daf;
                iTimes *= 10;
            }
            //if (daf.Length > 1)
            //{
            //    if ((daf[0] == 'י' && (daf[1] == 'ה' || daf[1] == 'ו')) || (daf.Length == 3
            //        && daf[1] == 'י' && (daf[2] == 'ה' || daf[2] == 'ו')))
            //    {
            //        daf = daf.Replace('י', 'ט').Replace('ו', 'ז');
            //        daf = daf.Replace('ה', 'ו');
            //    }
            //}
            daf = ChangeABArangment(daf);
            return daf;
        }
        private static string ChangeABArangment(string daf)
        {
            string yid = "י";
            string hah = "ה";
            string y_h = yid + hah;
            string dafname = daf.Replace(y_h, "טו");
            dafname = dafname.Replace("יו", "טז");
            if(dafname.Length==2) dafname = dafname.Replace("שד", "דש");
            dafname = dafname.Replace("שמד", "דשמ");
            dafname = dafname.Replace("רצח", "חצר");
            return dafname;
        }
    }

}
