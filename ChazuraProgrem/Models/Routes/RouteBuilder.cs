namespace ChazuraProgram.Models
{
    public class RouteBuilder
    {

        protected ISessCook Session { get; set; }
        protected RouteDictionary Routes { get; set; }
        public RouteBuilder(ISessCook session)
        {
            Session = session;
            Routes = Session.GetRoutesDetail()?? new RouteDictionary();
        }
        public RouteBuilder (ISessCook session,DetailPram detailPram)
        {
            Session = session;
            Routes = new RouteDictionary
            {
                Date = detailPram.Date,
                Id = detailPram.Id,
                PageId = detailPram.PageId,
                Description = detailPram.Description,
                ChazuraType = detailPram.ChazurahType.ToString(),
                ChazuraTimes= detailPram.ChazuraTimes.ToString()
                
            };
            SaveRouteSegments();

        }
        public void SaveRouteSegments() =>
            Session.SetRoutesDetail(Routes);
        public RouteDictionary CurrentRoute => Routes;

    }
}
