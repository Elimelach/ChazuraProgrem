using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ChazuraProgram.Models
{
    public class SeedShasAumudim
    {
        private IChazuraUnitOfWork Data { get; set; }
        private ChazuraContext Context { get; set; }
        public SeedShasAumudim(IChazuraUnitOfWork data ,ChazuraContext ctx)
        {
            Data = data;
            Context = ctx;
        }
        List<DafimShas> dafimShas;
        private void GetDafim()
        {
            dafimShas = (List<DafimShas>)Data.DafimShas.List(new QueryOptions<DafimShas>
            {
                OrderBy = d => d.DafID
            });
        }

        private List<Shas1Sided> shas1Sided;
        private void LoadAmid()
        {
            shas1Sided = new List<Shas1Sided>();
            foreach (var d in dafimShas)
            {
                Shas1Sided shas1 = new Shas1Sided
                {
                    AumidHebraw = "."+d.DafHebraw  ,
                    AumidID = d.DafID + "-1",
                    AumidNumber = 1,
                    DafNumber = d.DafNumber,
                    MeshactaID = d.MeshactaID
                };
                shas1Sided.Add(shas1);
                if (d.TwoSided)
                {
                    shas1 = new Shas1Sided
                    {
                        AumidHebraw =":"+ d.DafHebraw  ,
                        AumidID = d.DafID + "-2",
                        AumidNumber = 2,
                        DafNumber = d.DafNumber,
                        MeshactaID = d.MeshactaID
                    };
                    shas1Sided.Add(shas1);
                }
            }
        }
        public void SeedDatabase()
        {
            GetDafim();
            LoadAmid();
            foreach (var u in shas1Sided)
            {
                Data.Shas1Sided.Insert(u);
            }
            Data.Save();
        }
        public void DeleteDatabase()
        {
            Context.Database.ExecuteSqlRaw("delete Shas1Sided");

        }
    }
}
