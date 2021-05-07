using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;




namespace ChazuraProgram.Models
{
    public class HebrewDateLimit : ValidationAttribute, IClientModelValidator
    {
        readonly DateTime MaxDate = new DateTime(2239, 09, 29);
        readonly DateTime MinDate = new DateTime(1583, 01, 01);


        protected override ValidationResult IsValid(object value, ValidationContext ctx)
        {
            if (value is DateTime dateToCheck)
            {
                
                if (dateToCheck <= MaxDate && dateToCheck >= MinDate)
                {
                    return ValidationResult.Success;
                }
            }
            return new ValidationResult(GetMsg());
        }

        public void AddValidation(ClientModelValidationContext ctx)
        {
            if (!ctx.Attributes.ContainsKey("data-val"))
                ctx.Attributes.Add("data-val", "true");
            ctx.Attributes.Add("data-val-mindate-years",
                MinDate.ToString());
           
            ctx.Attributes.Add("data-val-mindate",
                GetMsg());
           
        }

        private string GetMsg() => base.ErrorMessage ??
               $" Date must be at between {MinDate.ToShortDateString()} and {MaxDate.ToShortDateString()}.";
    }
}
