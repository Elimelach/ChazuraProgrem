namespace ChazuraProgram.Models
{
    public class GridDTO
    {
        public string Filter { get; set; } = "all";
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 4;
        public string SortField { get; set; }
        public string SortDirection { get; set; } = "asc";
    }
}
