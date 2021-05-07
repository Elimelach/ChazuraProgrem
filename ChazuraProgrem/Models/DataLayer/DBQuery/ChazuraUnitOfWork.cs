namespace ChazuraProgram.Models
{
    public class ChazuraUnitOfWork : IChazuraUnitOfWork
    {
        private ChazuraContext Context { get; set; }
        public ChazuraUnitOfWork(ChazuraContext ctx) => Context = ctx;
        private Repository<DafimShas> dafimshas;

        public Repository<DafimShas> DafimShas
        {
            get
            {
                if (dafimshas == null) dafimshas = new Repository<DafimShas>(Context);
                return dafimshas;
            }
        }
        private Repository<MeshctaShas> meshchte;

        public Repository<MeshctaShas> Meshcta
        {
            get
            {
                if (meshchte == null) meshchte = new Repository<MeshctaShas>(Context);
                return meshchte;
            }
        }
        private Repository<User> users;

        public Repository<User> Users
        {
            get
            {
                if (users == null) users = new Repository<User>(Context);
                return users;
            }
        }
        private Repository<DatesChart> datesChart;

        public Repository<DatesChart> DatesChart
        {
            get
            {
                if (datesChart == null) datesChart = new Repository<DatesChart>(Context);
                return datesChart;
            }
        }
        private Repository<ShasChazuraData> shasChazuraData;

        public Repository<ShasChazuraData> ShasChazuraData
        {
            get
            {
                if (shasChazuraData == null) shasChazuraData = new Repository<ShasChazuraData>(Context);
                return shasChazuraData;
            }
        }
        private Repository<LimudChart> limud;
        public Repository<LimudChart> Limud
        {
            get
            {
                if (limud == null) limud = new Repository<LimudChart>(Context);
                return limud;
            }
        }
        private Repository<Completed> completed;
        public Repository<Completed> Completed 
        {
            get
            {
                if (completed == null) completed = new Repository<Completed>(Context);
                return completed;
            }
        }
        private Repository<Shas1Sided> shas1Sided;
        public Repository<Shas1Sided> Shas1Sided
        {
            get
            {
                if (shas1Sided == null) shas1Sided = new Repository<Shas1Sided>(Context);
                return shas1Sided;
            }
        }
        private Repository<ShasChazuraAumidData> shasAumidChazuraData;

        public Repository<ShasChazuraAumidData> ShasChazuraAumidData
        {
            get
            {
                if (shasAumidChazuraData == null) shasAumidChazuraData = new Repository<ShasChazuraAumidData>(Context);
                return shasAumidChazuraData; 
            }
        }
        private Repository<CustomLimud> customLimud;

        public Repository<CustomLimud> Custom
        {
            get
            {
                if (customLimud == null) customLimud = new Repository<CustomLimud>(Context);
                return customLimud;
            }
        }
        private Repository<Payment> payment;
        public Repository<Payment> Payment
        {
            get
            {
                if (payment == null) payment = new Repository<Payment>(Context);
                return payment;
            }
        }
        private Repository<SponsorData> sponsorData;
        public Repository<SponsorData> Sponsor
        {
            get
            {
                if (sponsorData == null) sponsorData = new Repository<SponsorData>(Context);
                return sponsorData;
            }
        }
        private Repository<DefaultSponsor> defaultSponsor;
        public Repository<DefaultSponsor> DefaultSponsor
        {
            get
            {
                if (defaultSponsor == null) defaultSponsor = new Repository<DefaultSponsor>(Context);
                return defaultSponsor;
            }
        }
        public ChazuraContext DBContext => Context;

        public void Save()
        {
            Context.SaveChanges();
        }
    }
}
