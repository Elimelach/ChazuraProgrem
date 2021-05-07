using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChazuraProgram.Models
{
    public static class Editer
    {
        public static List<CustomLimud> UpdateCustom(List<CustomLimud> customList,CustomLimud custom,
            CustomVM customVM)
        {
            if (custom.LimudString != customVM.Description)
                foreach (var limud in customList) limud.LimudString = customVM.Description;
            
            if (custom.EmailNotify != customVM.EmailNotify)
                foreach (var limud in customList) limud.EmailNotify = customVM.EmailNotify;

            if (custom.Date.Date != customVM.Date.Date) 
            {
                TimeSpan dif =  customVM.Date.Date- custom.Date.Date;
                foreach (var limud in customList) limud.Date += dif;
            }
            return customList;
        }
        public static List<CustomLimud> UpdateTypeCustom(List<CustomLimud> customList,CustomTypeVM typeVM) 
        {
            TimeSpan dif = typeVM.Date - typeVM.OldDate;
            foreach (var limud in customList)
            {
                limud.Type = typeVM.Title;
                limud.EmailNotify = typeVM.EmailNotify;
                limud.Date += dif;
            }
            return customList;
        }
    }
}
