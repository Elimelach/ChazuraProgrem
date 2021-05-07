namespace ChazuraProgram.Models
{
    public interface ISponsorPoints
    {
        string DescriptionElse { get; set; }
        string DescriptionName { get; set; }
        SponserType GetSponserType { get; set; }
    }
}