namespace ChazuraProgram.Models
{
    public class DetailPram
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public string PageId { get; set; }
        public string Description { get; set; }
        public ChazurahType ChazurahType { get; set; }
        public ChazuraTimes ChazuraTimes { get; set; }
    }
}
