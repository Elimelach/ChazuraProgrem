using Microsoft.AspNetCore.Http;

namespace ChazuraProgram.Models
{
    public class GridBuilder
    {

        protected RouteDictionary Routes { get; set; }
        protected ISessCook Session { get; set; }

        public GridBuilder(ISessCook sess) {
            Session = sess;
            Routes = Session.GetRouteGrid() ?? new RouteDictionary();
        }

        public GridBuilder(ISessCook sess, GridDTO values, string defaultSortField) {
            Session = sess;

            Routes = new RouteDictionary
            {
                PageNumber = values.PageNumber,
                PageSize = values.PageSize,
                SortField = values.SortField ?? defaultSortField,
                SortDirection = values.SortDirection,
                Filter=values.Filter
                
            }; // clear previous route segment values

            SaveRouteSegments();
        }

        public void SaveRouteSegments() =>
            Session.SetRouteGrid(Routes);

        public int GetTotalPages(int count) {
            int size = Routes.PageSize;
            return (count + size - 1) / size;
        }
            
        public RouteDictionary CurrentRoute => Routes;
        
       
    }
}
