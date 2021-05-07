using System.Collections.Generic;

namespace ChazuraProgram.Models
{
    public class SeedDafim
    {
        Repository<MeshctaShas> RepositoryMesc { get; set; }
        Repository<DafimShas> RepositoryDaf { get; set; }
        public SeedDafim(ChazuraContext ctx)
        {
            RepositoryDaf = new Repository<DafimShas>(ctx);
            RepositoryMesc = new Repository<MeshctaShas>(ctx);
        }
        List<MeshctaShas> ShasMeshactas = new List<MeshctaShas>();
        public void GetAndInsertDafim()
        {
            ShasMeshactas = (List<MeshctaShas>)RepositoryMesc.List(new QueryOptions<MeshctaShas>());
            foreach (var meschta in ShasMeshactas)
            {
                for (int i = meschta.StartsAtDaf; i <= meschta.TotolDafim; i++)
                {
                    string p = i.ToString().PadLeft(3, '0');
                    DafimShas shas = new DafimShas { DafHebraw =AlaphBaisConverter.ConvertToAlaphBaith(i),
                       
                        DafID = meschta.MeshactaID + p,
                        
                        MeshactaID=meschta.MeshactaID,
                        DafNumber=i
                    };
                    RepositoryDaf.Insert(shas);
                }
               
            }
            RepositoryDaf.Save();
        }
    }
}
