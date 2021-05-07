using ChazuraProgram.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChazuraProgram.Models
{
    public class RouteDictionary : Dictionary<string, string>
    {

        public int Id
        {
            get => Get(nameof(DetailPram.Id)).ToInt();
            set => this[nameof(DetailPram.Id)] = value.ToString();
        }

        public string Date
        {
            get => Get(nameof(DetailPram.Date));
            set => this[nameof(DetailPram.Date)] = value.GetDashDateString();
        }

        public string PageId
        {
            get => Get(nameof(DetailPram.PageId));
            set => this[nameof(DetailPram.PageId)] = value;
        }

        public string Description
        {
            get => Get(nameof(DetailPram.Description));
            set => this[nameof(DetailPram.Description)] = value;
        }
        public string ChazuraType
        {
            get => Get(nameof(DetailPram.ChazurahType));
            set => this[nameof(DetailPram.ChazurahType)] = value;
        }
        public string ChazuraTimes
        {
            get => Get(nameof(DetailPram.ChazuraTimes));
            set => this[nameof(DetailPram.ChazuraTimes)] = value;
        }

        public int PageNumber
        {
            get => Get(nameof(GridDTO.PageNumber)).ToInt();
            set => this[nameof(GridDTO.PageNumber)] = value.ToString();
        }

       

        public int PageSize
        {
            get => Get(nameof(GridDTO.PageSize)).ToInt();
            set => this[nameof(GridDTO.PageSize)] = value.ToString();
        }

       

        public string SortField
        {
            get => Get(nameof(GridDTO.SortField));
            set => this[nameof(GridDTO.SortField)] = value;
        }

        public string SortDirection
        {
            get => Get(nameof(GridDTO.SortDirection));
            set => this[nameof(GridDTO.SortDirection)] = value;
        }
        public string Filter
        {
            get => Get(nameof(GridDTO.Filter));
            set => this[nameof(GridDTO.Filter)] = value;
        }
        public string StartDate
        {
            get => Get(nameof(AdminSponsorGridDTO.StartDate));
            set => this[nameof(AdminSponsorGridDTO.StartDate)] = value;
        }
        public string FilterTime
        {
            get => Get(nameof(AdminSponsorGridDTO.FilterTime));
            set => this[nameof(AdminSponsorGridDTO.FilterTime)] = value;
        }

        public void SetSortAndDirection(string fieldName, RouteDictionary current)
        {
            this[nameof(GridDTO.SortField)] = fieldName;

            if (current.SortField.EqualsNoCase(fieldName) &&
                current.SortDirection == "asc")
                this[nameof(GridDTO.SortDirection)] = "desc";
            else
                this[nameof(GridDTO.SortDirection)] = "asc";
        }
        internal void SetFilter(string filterField)
        {
            this[nameof(GridDTO.Filter)] = filterField;
        }
        internal void SetStartFilter(string startFilter)
        {
            this[nameof(AdminSponsorGridDTO.FilterTime)] = startFilter;
        }
        private string Get(string key) => Keys.Contains(key) ? this[key] : null;

        // return a new dictionary that contains the same values as this dictionary.
        // needed so that pages can change the route values when calculating paging, sorting,
        // and filtering links, without changing the values of the current route
        public RouteDictionary Clone()
        {
            var clone = new RouteDictionary();
            foreach (var key in Keys)
            {
                clone.Add(key, this[key]);
            }
            return clone;
        }
        

    }
}
