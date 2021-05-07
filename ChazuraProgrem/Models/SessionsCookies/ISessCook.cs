namespace ChazuraProgram.Models
{
    public interface ISessCook
    {
        void ClearUsersCookSess();
        //string GetNameFromCookie();
        User GetUserObgFromSession();
        //void SetUserNameInCookie(string name);
        void SetUserObgInSession(User user);
        void SetRedirectFromDetails(int count);
        int GetRedirectFromDetails();
        void ClearRedirectFromDetails();
        void ClearRoutesDetail();
        void SetRoutesDetail( RouteDictionary route);
        RouteDictionary GetRoutesDetail();
        void SetSponsor(SponsorInfo info);
        SponsorInfo GetSponsor();
        void ClearSponsor();
        void ClearSession();
        void SetAdminLoIntoUserAcct(User user);
        User GetAdminLoIntoUserAcc();
        void ClearAdminLoIntoUserAcc();
        void ClearRouteGrid();
        RouteDictionary GetRouteGrid();
        void SetRouteGrid(RouteDictionary routes);
    }
}