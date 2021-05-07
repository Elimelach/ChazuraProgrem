namespace ChazuraProgram.Models
{
    public interface IChazuraUnitOfWork
    {
        Repository<DafimShas> DafimShas { get; }
        Repository<DatesChart> DatesChart { get; }
        Repository<MeshctaShas> Meshcta { get; }
        Repository<ShasChazuraData> ShasChazuraData { get; }
        Repository<User> Users { get; }
        Repository<LimudChart> Limud { get; }
        Repository<Completed> Completed { get; }
        Repository<CustomLimud> Custom { get; }
        Repository<Shas1Sided> Shas1Sided { get; }
        Repository<ShasChazuraAumidData> ShasChazuraAumidData { get; }
        ChazuraContext DBContext { get; }
        Repository<Payment> Payment { get; }
        Repository<SponsorData> Sponsor { get; }
        Repository<DefaultSponsor> DefaultSponsor { get; }

        public void Save();

    }
}